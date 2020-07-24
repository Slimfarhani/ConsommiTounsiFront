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
    public class ProfileController : Controller
    {
        // GET: Profile
        MyContext context;
        public ActionResult Index()
        {
            return View();
        }
        public new ActionResult Profile()
        {
            UserRegisterModel user = (UserRegisterModel)Session["User"];
            return View(user);
        }
        public ActionResult ProfileUser()
        {
            return PartialView();
        }
        public ActionResult ProfileCart()
        {
            context = new MyContext();
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            IEnumerable<OrderItem> items = context.OrderItems.OrderByDescending(o => o.OrderItemId).Where(o => o.UserID == UserLoggedIn.userId);
            
            return PartialView(items);
        }
    }
}