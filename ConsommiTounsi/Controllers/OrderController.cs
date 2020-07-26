using ConsommiTounsi.Models;
using Data;
using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsi.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        MyContext context;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexCustomer()
        {

            HttpClient client = new HttpClient();
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            System.Diagnostics.Debug.Write("simplesimple");
            response = client.GetAsync("orderbycustomer/" + UserLoggedIn.userId).Result;

            IEnumerable<Order> orders = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
            return PartialView(orders);
        }
        public async Task<ActionResult> Create()
        {
            System.Diagnostics.Debug.WriteLine("here");
            var UserLoggedIn = (UserRegisterModel)Session["User"];
            context = new MyContext();
            IEnumerable<OrderItem> items = context.OrderItems.OrderByDescending(o => o.OrderItemId).Where(o => o.UserID == UserLoggedIn.userId);
            Order order = new Order();
            List<Order_Detail> details = new List<Order_Detail>();
            foreach(OrderItem item in items)
            {
                Product product = new Product();
                product.productId = item.ProductId;
                Supplier supplier = new Supplier();
                supplier.userId = item.SupplierId;
                Order_Detail detail = new Order_Detail();
                detail.product = product;
                detail.supplier = supplier;
                detail.price = item.Price;
                detail.quantity = item.Quantity;
                details.Add(detail);
            }
            order.isPaid = false;
            order.details = details;
            Customer customer = new Customer();
            customer.userId = UserLoggedIn.userId;
            order.customer = customer;
            var orderJson = await Task.Run(() => JsonConvert.SerializeObject(order));

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            var content = new StringContent(orderJson.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            response = client.PostAsync("order", content).Result;
            foreach (var entity in context.OrderItems.Where(o => o.UserID == UserLoggedIn.userId))
                context.OrderItems.Remove(entity);
            context.SaveChanges();
            UpdateCartNotification();
            return View();
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

    }
}