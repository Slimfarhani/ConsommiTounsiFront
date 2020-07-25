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
using System.Web.UI.WebControls;

namespace ConsommiTounsi.Controllers
{
    public class AdminController : Controller
    {
        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UpdateAdmin()
        {
            try
            {
                AdminUpdateModel admin = new AdminUpdateModel();
                UserRegisterModel session = (UserRegisterModel)Session["user"];

                admin.OldPasswordVerif = session.password;
                System.Diagnostics.Debug.WriteLine(admin.OldPasswordVerif);

                admin.userId = session.userId;
                admin.userName = session.userName;
                admin.role = session.role;
                admin.OldUserName = admin.userName;
                return View(admin);
            }
            catch(System.NullReferenceException e)
            {
                return View("Index");
            }

            
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAdmin(AdminUpdateModel model)
        {


            if (ModelState.IsValid)
            {

                if (verifLogin(model.userName) || model.userName == model.OldUserName)
                {

                    var adminJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
                    var content = new StringContent(adminJson.ToString(), Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                    HttpResponseMessage response = client.PutAsync("admin/update/" + model.userId, content).Result;
                    System.Diagnostics.Debug.WriteLine(response);
                    return RedirectToAction("/index");
                }
                else
                {
                    ViewData["errorLogin"] = "Login already exists";
                    return View(model);
                }

            }

            return View(model);
        }
        public ActionResult CustomerDetails( long id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/customer/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync("search/" + id).Result;
            Customer customer= response.Content.ReadAsAsync<Customer>().Result;
            return View(customer);
           
        }
        public ActionResult SupplierDetails(long id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync("supplier/search/" + id).Result;
            Supplier supplier = response.Content.ReadAsAsync<Supplier>().Result;
            
            response = client.GetAsync("stockBySupplier/26").Result;
            

            IEnumerable<Stock> stocks = response.Content.ReadAsAsync<IEnumerable<Stock>>().Result;
            System.Diagnostics.Debug.WriteLine("supplier : " + stocks.FirstOrDefault().supplier.userId);
            ViewBag.stocks = stocks;
            return View(supplier);

        }
        String updateUsername;
        public ActionResult UpdateCustomer( int id)
        {

                HttpClient client1 = new HttpClient();
                client1.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/customer/");
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response;
                response = client1.GetAsync("search/" + id).Result;
                Customer customer = response.Content.ReadAsAsync<Customer>().Result;
            customer.birthdateString = customer.birthdateFormatted.ToString();
            customer.OldUserName = customer.userName;
            return View(customer);
            
        }
        public ActionResult UpdateSupplier(int id)
        {

            HttpClient client1 = new HttpClient();
            client1.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/supplier/");
            client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client1.GetAsync("search/" + id).Result;
            Supplier supplier = response.Content.ReadAsAsync<Supplier>().Result;
            supplier.OldUserName = supplier.userName;
            return View(supplier);


        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateCustomer(Customer  model, HttpPostedFileBase file )
        {

            
            if (ModelState.IsValid)
            {
                if (verifLogin(model.userName) || model.userName == model.OldUserName)
                {


                    if (file != null)
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
                    HttpResponseMessage response = client.PutAsync("customer/update/" + model.userId, content).Result;

                    System.Diagnostics.Debug.WriteLine(response);
                    return RedirectToAction("/customermanagement");
                }
                else
                {
                    ViewData["errorLogin"] = "Login already exists";
                    return View(model);
                }

            }
           
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateSupplier(Supplier model, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {
                if (verifLogin(model.userName) ||  model.userName == model.OldUserName)
                {
                    if (file != null)
                    {
                        var newFileName = file.FileName + model.userName + Path.GetExtension(file.FileName);
                        model.urlImage = newFileName;
                        var path = Path.Combine(Server.MapPath("~/UserImages/"), newFileName);
                        file.SaveAs(path);
                    }


                    var supplierJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                    System.Diagnostics.Debug.WriteLine(supplierJson.ToString());


                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

                    var content = new StringContent(supplierJson.ToString(), Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                    HttpResponseMessage response = client.PutAsync("supplier/update/" + model.userId, content).Result;

                    System.Diagnostics.Debug.WriteLine(response);
                    return RedirectToAction("/suppliermanagement");
                }
                else
                {
                    ViewData["errorLogin"] = "Login already exists";
                    return View(model);
                }

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
        public ActionResult SupplierManagement()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/supplier/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("list").Result;
            IEnumerable<Supplier> suppliers = response.Content.ReadAsAsync<IEnumerable<Supplier>>().Result;

            return View(suppliers);

        }
        public ActionResult DeleteCustomer(int id)
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
        public ActionResult DeleteSupplier(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/supplier/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync("delete/" + id).Result;
                return RedirectToAction("SupplierManagement");
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
                if (verifLogin(model.userName))
                {

                    var newFileName = file.FileName + model.userName + Path.GetExtension(file.FileName);
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
                else
                {
                    ViewData["errorLogin"] = "Login already exists";
                    return View(model);
                }
           
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult AddSupplier()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSupplier(Supplier model, HttpPostedFileBase file)
        {
         

            if (ModelState.IsValid)
            {
                if (verifLogin(model.userName))
                {
                    var newFileName = file.FileName + model.userName + Path.GetExtension(file.FileName);
                    model.urlImage = newFileName;
                    //model.birthdateFormatted = Convert.ToDateTime(model.birthdateString);
                    var supplierJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                    System.Diagnostics.Debug.WriteLine(supplierJson.ToString());
                    var path = Path.Combine(Server.MapPath("~/UserImages/"), newFileName);
                    file.SaveAs(path);
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

                    var content = new StringContent(supplierJson.ToString(), Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                    HttpResponseMessage response = client.PostAsync("supplier/add", content).Result;

                    System.Diagnostics.Debug.WriteLine(response);
                    return RedirectToAction("/suppliermanagement");
                }
                else
                {
                    ViewData["errorLogin"] = "Login already exists";
                    return View(model);
                }


            }
       
            return View(model);
        }

        public Boolean verifLogin(String login )
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/verifyuser/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync(login).Result;
            Boolean valid = response.Content.ReadAsAsync<Boolean>().Result;

            return valid;

        }
        public ActionResult PageNotFound()
        {
            return View();
        }

    }

}
