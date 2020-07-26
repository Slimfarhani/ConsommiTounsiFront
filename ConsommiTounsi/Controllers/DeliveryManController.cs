using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConsommiTounsi.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.SessionState;
using System.Security.Policy;
using System.Text;
using Microsoft.Owin.Security.Provider;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Data;
using Domain.Entities;





namespace ConsommiTounsi.Controllers
{
    public class DeliveryManController : Controller
    {
        // GET: DeliveryMan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterDeliveryMan()
        {
            return View();

        }


        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterDeliveryMan(DeliveryManModel model)
        {
            System.Diagnostics.Debug.WriteLine("Debut RegisterDeliveryMan");


            if (ModelState.IsValid)
            {
                var DeliveryManJson = await Task.Run(() => JsonConvert.SerializeObject(model));

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
                var content = new StringContent(DeliveryManJson.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response;
 
                response = client.PostAsync("DeliveryMan/add", content).Result;

            }


            return View(model);

            // If we got this far, something failed, redisplay form

        }

        public ActionResult IndexDetail(long Delivery_ManId)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync("DeliveryMan/getById/" + Delivery_ManId).Result;
            DeliveryManModel deliveryManModel = response.Content.ReadAsAsync<DeliveryManModel>().Result;
            return View(deliveryManModel);
        }

        public ActionResult ListDeliveryMan(string searchString)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            System.Diagnostics.Debug.Write("searchsearch " + searchString);

            {
                System.Diagnostics.Debug.Write("simplesimple");
                response = client.GetAsync("DeliveryMan/getAll").Result;
            }

            IEnumerable<DeliveryManModel> DeliveryManModels = response.Content.ReadAsAsync<IEnumerable<DeliveryManModel>>().Result;
            ViewBag.DeliveryManModels = DeliveryManModels;
            return View();
        }

    }
}