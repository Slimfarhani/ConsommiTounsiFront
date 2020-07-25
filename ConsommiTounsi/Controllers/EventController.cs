using ConsommiTounsi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsi.Controllers
{
    public class EventController : Controller
    {
        // /Events/List
        public new ActionResult List()
        {
            return View();
        }

        public new ActionResult EventDetails(long id)

        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            response = client.GetAsync("events/search/" + id).Result;
            Event Event = response.Content.ReadAsAsync<Event>().Result;
            response = client.GetAsync("events/Donations/"+id).Result;
            string donation = response.Content.ReadAsAsync<string>().Result;
            if (donation != null)
            {
                ViewBag.donation = donation;

            }

            else
            {
                ViewBag.donation = "0";

            }
            return View(Event);
   
        }
        public new ActionResult AddEvent()
        {
            return View();
        }

        // POST: /Event/Add
        [HttpPost]
        [AllowAnonymous]
    
        public async Task<ActionResult> AddEvent(Event model, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {
                model.StartDatedateFormatted= Convert.ToDateTime(model.StartDateString);
                model.EndDatedateFormatted = Convert.ToDateTime(model.EndDateString);
                model.UrlImage = file.FileName;
                Supplier supplier = new Supplier();
                UserRegisterModel currentUser = (UserRegisterModel)Session["User"];
                System.Diagnostics.Debug.WriteLine("current user id "+currentUser.userId);

                supplier.userId = currentUser.userId.ToString();
                System.Diagnostics.Debug.WriteLine("supplier  id " + supplier.userId);
                model.Supplier = supplier;
                var customerJson = await Task.Run(() => JsonConvert.SerializeObject(model));

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
                var content = new StringContent(customerJson.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                System.Diagnostics.Debug.WriteLine(file.FileName);
                var path = Path.Combine(Server.MapPath("~/EventImages/"), file.FileName);
                Image image = Image.FromStream(file.InputStream, true, true);
                image.Save(path, ImageFormat.Png);

                response = client.PostAsync("events/add", content).Result;
                return List();

            }
            return View(model);

            // If we got this far, something failed, redisplay form

        }


        public new ActionResult DisplaySupplierEvents(long id)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/events/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
  
            HttpResponseMessage response = client.GetAsync("BySupplier/"+id).Result;
            IEnumerable<Event> events = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;

            return View(events);

        }






        /*
        public new ActionResult Profile()
        {
            return View();
        }
        */


    }
}