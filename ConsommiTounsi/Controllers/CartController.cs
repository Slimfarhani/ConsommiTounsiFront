using ConsommiTounsi.Models;
using Data;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsi.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        MyContext context;
        public ActionResult Index()
        {
            context = new MyContext();
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            IEnumerable<OrderItem> items = context.OrderItems.OrderByDescending(o => o.OrderItemId).Where(o => o.UserID == UserLoggedIn.userId);
            return View(items);
        }
        [HttpPost]
        [AllowAnonymous]
        public int AddToCartFromIndex(int productid, int supplierid, float price, string name, string urlimage, int total)
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
                item.Price = price;
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

                Session["ItemsInCartTotal"] = context.OrderItems.Where(o => o.UserID == UserLoggedIn.userId).Select(o => o.Price * o.Quantity).Sum();
            }
            catch (Exception e)
            {
                Session["ItemsInCartTotal"] = (float)0;
            }
        }
        public void DeleteAllItemsInCart()
        {
            context = new MyContext();
            var UserLoggedIn = Session["User"] as UserRegisterModel;
            foreach (var entity in context.OrderItems.Where(o => o.UserID == UserLoggedIn.userId))
                context.OrderItems.Remove(entity);
            context.SaveChanges();
            UpdateCartNotification();
        }
        public int RemoveItemFromCart(long itemid)
        {
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);
            OrderItem item = context.OrderItems.First(o => o.OrderItemId == itemid);
            context.OrderItems.Remove(item);
            context.SaveChanges();
            UpdateCartNotification();
            return (int)Session["ItemNumber"];
        }
        public float MinusItemNumberInCart(long itemid, int number)
        {
            System.Diagnostics.Debug.WriteLine("number : " + number);
            if (number > 1)
            {
                number--;
            }
            context = new MyContext();
            System.Diagnostics.Debug.WriteLine("id : " + itemid);

            var item = context.OrderItems.SingleOrDefault(o => o.OrderItemId == itemid);
            item.Quantity = number;
            context.SaveChanges();
            UpdateCartNotification();
            return item.Quantity * item.Price;

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
            return item.Quantity * item.Price;

        }
        public void SelectZone(string zone)
        {
            Session["zone"] = zone;
        }
    }
}