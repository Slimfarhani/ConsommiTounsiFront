using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using ConsommiTounsi.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Data;
using Domain.Entities;
using Newtonsoft.Json;

namespace ConsommiTounsi.Controllers
{
    public class StockController : Controller
    {
        // GET: Product
        MyContext context;
        public ActionResult Index(string searchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            if (!String.IsNullOrEmpty(searchString))
            {
                System.Diagnostics.Debug.Write("searchsearch");
                response = client.GetAsync("stockByProductName/"+searchString).Result;
            }
            else
            {
                System.Diagnostics.Debug.Write("simplesimple");
                response = client.GetAsync("stock").Result;
            }

            IEnumerable<Stock> stocks = response.Content.ReadAsAsync<IEnumerable<Stock>>().Result;
            ViewBag.stocks = stocks;
            return View();
        }
        public ActionResult IndexDetail(long productid,long supplierid )
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync("stockByProductAndSupplier/" + productid + "/" + supplierid).Result;
            Stock stock = response.Content.ReadAsAsync<Stock>().Result;
            System.Diagnostics.Debug.WriteLine(stock.product.productName);
            return View(stock);
        }
        [HttpPost]
        [AllowAnonymous]
        public int AddToCartFromIndex(int productid,int supplierid,float price,string name,string urlimage,int total)
        {
            System.Diagnostics.Debug.WriteLine("productid : " + productid);
            System.Diagnostics.Debug.WriteLine("supplierid : " + supplierid);
            System.Diagnostics.Debug.WriteLine("total : " + price);
            System.Diagnostics.Debug.WriteLine("number : " + total);
            var UserLoggedIn = Session["User"] as UserRegisterModel;
            if (UserLoggedIn != null)
            {
                context = new MyContext();
                OrderItem item = new OrderItem();
                item.ProductId = productid;
                item.SupplierId = supplierid;
                item.Quantity = total;
                item.Total = price;
                item.UserID = UserLoggedIn.userId;
                item.Name = name;
                item.ImageUrl = urlimage;
                context.OrderItems.Add(item);
                context.SaveChanges();
                UpdateCartNotification();
                return (int)Session["ItemNumber"];
            }
            return (int)Session["ItemNumber"];


        }
        public void UpdateCartNotification()
        {
            context = new MyContext();
            var UserLoggedIn = Session["User"] as UserRegisterModel;
            IEnumerable<OrderItem> items;
            items = context.OrderItems.OrderByDescending(o => o.OrderItemId).Where(o => o.UserID == UserLoggedIn.userId);
            items = items.Take(3);
            Session["ItemsInCartNotification"] = items;
            Session["ItemNumber"] = context.OrderItems.Count(m => m.UserID == UserLoggedIn.userId);
            try
            {

                Session["ItemsInCartTotal"] = context.OrderItems.Where(o => o.UserID == UserLoggedIn.userId).Select(o => o.Total*o.Quantity).Sum();
            }
            catch(Exception e)
            {
                Session["ItemsInCartTotal"] = (float)0;
            }
        }
        public void DeleteAllItemsInCart()
        {
            context = new MyContext();
            foreach (var entity in context.OrderItems)
                context.OrderItems.Remove(entity);
            context.SaveChanges();
            UpdateCartNotification();
        }
        public int RemoveItemFromCart(long itemid)
        {
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);
            OrderItem item = context.OrderItems.First(o=>o.OrderItemId==itemid);
            context.OrderItems.Remove(item);
            context.SaveChanges();
            UpdateCartNotification();
            return (int)Session["ItemNumber"];
        }
        public float MinusItemNumberInCart(long itemid,int number)
        {
            System.Diagnostics.Debug.WriteLine("number : " + number);
            if (number > 1)
            {
                number--;
            }
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);

            var item= context.OrderItems.SingleOrDefault(o => o.OrderItemId == itemid);
            item.Quantity = number;
            context.SaveChanges();
            UpdateCartNotification();
            return item.Quantity*item.Total;
            
        }
        public float PlusItemNumberInCart(long itemid, int number)
        {
            System.Diagnostics.Debug.WriteLine("number : " + number);
            
                number++;
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);

            var item = context.OrderItems.SingleOrDefault(o => o.OrderItemId == itemid);
            item.Quantity = number;
            context.SaveChanges();
            UpdateCartNotification();
            return item.Quantity * item.Total;

        }
        
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

    }
}