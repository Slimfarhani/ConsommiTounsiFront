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
        public string RemoveItemFromCart(long itemid)
        {
            return "";
        }
    }
}