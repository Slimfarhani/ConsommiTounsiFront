using ConsommiTounsi.Models;
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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("modelKPI").Result;
            ModelKPI ModelKPI = response.Content.ReadAsAsync<ModelKPI>().Result;

            return View(ModelKPI);
        }
        public ActionResult CustomerManagement()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/customer/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("list").Result;
            IEnumerable<Customer> Customers = response.Content.ReadAsAsync<IEnumerable<Customer>>().Result;

            return View(Customers);

        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult AddCustomer()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCustomer(Customer model)
        {
            
            if (ModelState.IsValid)
            {
                var customerJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                System.Diagnostics.Debug.WriteLine(customerJson.ToString());

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

                var content = new StringContent(customerJson.ToString(), Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                HttpResponseMessage response = client.PostAsync("customer/add", content).Result;
                
                System.Diagnostics.Debug.WriteLine(response);
                return RedirectToAction("/customermanagement");
           
            }
            return View(model);
        }
    }
}
