using ConsommiTounsi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
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
            return View();
        }

        public ActionResult CustomerDetails( long id)
        {
            System.Diagnostics.Debug.WriteLine("heloooooooo !!!!");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/customer/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync("search/" + id).Result;
            Customer customer= response.Content.ReadAsAsync<Customer>().Result;
            System.Diagnostics.Debug.WriteLine(customer.lastName);
            return View(customer);
           
        }
        public ActionResult UpdateCustomer( int id)
        {

                System.Diagnostics.Debug.WriteLine("heloooooooo !!!!");
                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/customer/");
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                response = client1.GetAsync("search/" + id).Result;
                Customer customer = response.Content.ReadAsAsync<Customer>().Result;
                System.Diagnostics.Debug.WriteLine(customer.lastName);
            customer.birthdateString = customer.birthdateFormatted.ToString();
            return View(customer);
            
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCustomer(Customer  model, HttpPostedFileBase file )
        {


            if (ModelState.IsValid)
            {
                if(file != null)
                { 
                    var newFileName = file.FileName + model.userName + Path.GetExtension(file.FileName);
                    model.urlImage = newFileName;
                    var path = Path.Combine(Server.MapPath("~/UserImages/"), newFileName);
                    file.SaveAs(path);
                }
              
              
                model.birthdateFormatted = Convert.ToDateTime(model.birthdateString);
                var customerJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                System.Diagnostics.Debug.WriteLine(customerJson.ToString());
                
               
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

                var content = new StringContent(customerJson.ToString(), Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                HttpResponseMessage response = client.PutAsync("customer/update/"+model.userId, content).Result;

                System.Diagnostics.Debug.WriteLine(response);
                return RedirectToAction("/customermanagement");

            }
           
            return View(model);
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
        public ActionResult Delete(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/customer/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("delete/"+id).Result;
                return RedirectToAction("CustomerManagement");
            }
            catch
            {
                return View();
            }
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
        public async Task<ActionResult> AddCustomer(Customer model , HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {

                var newFileName = file.FileName+model.userName + Path.GetExtension(file.FileName);
                model.urlImage = newFileName;
                //model.birthdateFormatted = Convert.ToDateTime(model.birthdateString);
                var customerJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                System.Diagnostics.Debug.WriteLine(customerJson.ToString());
                var path = Path.Combine(Server.MapPath("~/UserImages/"), newFileName);
                file.SaveAs(path);
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
