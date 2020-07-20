using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using ConsommiTounsi.Models;

namespace ConsommiTounsi.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("stock").Result;
            IEnumerable<Stock> stocks = response.Content.ReadAsAsync<IEnumerable<Stock>>().Result;
            System.Diagnostics.Debug.WriteLine("price "+stocks.ToList().FirstOrDefault().price);
            ViewBag.stocks = stocks;
            return View();
        }
    }
}