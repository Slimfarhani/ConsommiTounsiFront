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
using System.Threading.Tasks;
using System.Text;

namespace ConsommiTounsi.Controllers
{
    public class StockController : Controller
    {
        MyContext context;
        // GET: Product
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
        public ActionResult IndexByCategory(string category)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            
                response = client.GetAsync("stock").Result;
            

            IEnumerable<Stock> stocks = response.Content.ReadAsAsync<IEnumerable<Stock>>().Result;
            stocks = stocks.Where(s => s.product.category.ToString() == category);
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
        public ActionResult StockCartIndex()
        {
            context = new MyContext();
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            IEnumerable<StockItem> items = context.StockItems.OrderByDescending(o => o.StockItemId).Where(o => o.UserID == UserLoggedIn.userId);
            return View(items);
        }
        public int AddToStockCartFromIndex(int productid, float price, string name, string urlimage, int total)
        {
            System.Diagnostics.Debug.WriteLine("productid : " + productid);
            System.Diagnostics.Debug.WriteLine("total : " + price);
            System.Diagnostics.Debug.WriteLine("number : " + total);
            var UserLoggedIn = Session["User"] as UserRegisterModel;
            if (UserLoggedIn != null)
            {
                context = new MyContext();
                StockItem item = new StockItem();
                item.ProductId = productid;
                item.Quantity = total;
                item.Price = price;
                item.UserID = UserLoggedIn.userId;
                item.Name = name;
                item.ImageUrl = urlimage;
                context.StockItems.Add(item);
                context.SaveChanges();
                UpdateStockCartNotification();
                return (int)Session["StockItemNumber"];
            }
            return (int)Session["StockItemNumber"];


        }
        public void UpdateStockCartNotification()
        {
            context = new MyContext();
            var UserLoggedIn = Session["User"] as UserRegisterModel;
            IEnumerable<StockItem> items;
            items = context.StockItems.OrderByDescending(o => o.StockItemId).Where(o => o.UserID == UserLoggedIn.userId);
            items = items.Take(3);
            Session["StockItemsInStockCartNotification"] = items;
            Session["StockItemNumber"] = context.StockItems.Count(m => m.UserID == UserLoggedIn.userId);
            try
            {

                Session["StockItemsInStockCartTotal"] = context.StockItems.Where(o => o.UserID == UserLoggedIn.userId).Select(o => o.Price * o.Quantity).Sum();
            }
            catch (Exception e)
            {
                Session["StockItemsInStockCartTotal"] = (float)0;
            }
        }
        public void DeleteAllStockItemsInStockCart()
        {
            context = new MyContext();
            var UserLoggedIn = Session["User"] as UserRegisterModel;
            foreach (var entity in context.StockItems.Where(o => o.UserID == UserLoggedIn.userId))
                context.StockItems.Remove(entity);
            context.SaveChanges();
            UpdateStockCartNotification();
        }
        public int RemoveStockItemFromStockCart(long itemid)
        {
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);
            StockItem item = context.StockItems.First(o => o.StockItemId == itemid);
            context.StockItems.Remove(item);
            context.SaveChanges();
            UpdateStockCartNotification();
            return (int)Session["StockItemNumber"];
        }
        public float MinusStockItemNumberInStockCart(long itemid, int number)
        {
            System.Diagnostics.Debug.WriteLine("number : " + number);
            if (number > 1)
            {
                number--;
            }
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);

            var item = context.StockItems.SingleOrDefault(o => o.StockItemId == itemid);
            item.Quantity = number;
            context.SaveChanges();
            UpdateStockCartNotification();
            return item.Quantity * item.Price;

        }
        public float PlusStockItemNumberInStockCart(long itemid, int number)
        {
            System.Diagnostics.Debug.WriteLine("number : " + number);

            number++;
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);

            var item = context.StockItems.SingleOrDefault(o => o.StockItemId == itemid);
            item.Quantity = number;
            context.SaveChanges();
            UpdateStockCartNotification();
            return item.Quantity * item.Price;

        }
        public float UpdateStockInStockCart(long itemid,float price)
        {
            
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);

            var item = context.StockItems.SingleOrDefault(o => o.StockItemId == itemid);
            item.Price = price;
            context.SaveChanges();
            UpdateStockCartNotification();
            return item.Quantity * item.Price;
        }
        public async Task<ActionResult> UpdateStock()
        {
            System.Diagnostics.Debug.WriteLine("here");
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            context = new MyContext();
            IEnumerable<StockItem> items = context.StockItems.OrderByDescending(o => o.StockItemId).Where(o => o.UserID == UserLoggedIn.userId);
            List<Stock> stocks = new List<Stock>();
            foreach (StockItem item in items)
            {
                Product product = new Product();
                product.productId = item.ProductId;
                Supplier supplier = new Supplier();
                supplier.userId = item.UserID;
                Stock stock = new Stock();
                stock.price = item.Price;
                stock.quantity = item.Quantity;
                stock.supplier = supplier;
                stock.product = product;
                stocks.Add(stock);
            }
            
            var stocksJson = await Task.Run(() => JsonConvert.SerializeObject(stocks));

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            var content = new StringContent(stocksJson.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            response = client.PostAsync("updatestockfromcart", content).Result;
            foreach (var entity in context.StockItems.Where(o => o.UserID == UserLoggedIn.userId))
                context.StockItems.Remove(entity);
            context.SaveChanges();
            UpdateStockCartNotification();
            return View();
        }
        public ActionResult ProfileStock()
        {
            HttpClient client = new HttpClient();
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            System.Diagnostics.Debug.Write("simplesimple");
            response = client.GetAsync("stockbysupplier/"+UserLoggedIn.userId).Result;

            IEnumerable<Stock> stocks = response.Content.ReadAsAsync<IEnumerable<Stock>>().Result;
            return PartialView(stocks);
        }
        public void RemoveStock(long productid)
        {
            HttpClient client = new HttpClient();
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.DeleteAsync("deletestockbysupplier/" + UserLoggedIn.userId + "/" + productid).Result;

        }
        public void UpdateAllStock()
        {
            HttpClient client = new HttpClient();
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync("stockbysupplier/" + UserLoggedIn.userId).Result;

            context = new MyContext();
            IEnumerable<Stock> stocks = response.Content.ReadAsAsync<IEnumerable<Stock>>().Result;
            if (UserLoggedIn != null)
            {
                foreach(Stock stock in stocks)
                {
                    StockItem item = new StockItem();
                    item.ProductId = stock.product.productId;
                    item.Quantity = (int)stock.quantity;
                    item.Price = (float)stock.price;
                    item.UserID = UserLoggedIn.userId;
                    item.Name = stock.product.productName;
                    item.ImageUrl = stock.product.imageUrl;
                    context.StockItems.Add(item);
                    
                }
                context.SaveChanges();
                UpdateStockCartNotification();

            }
        }


    }
}