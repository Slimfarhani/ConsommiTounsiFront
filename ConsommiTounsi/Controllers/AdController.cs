using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConsommiTounsi.Models;
using Newtonsoft.Json;

namespace ConsommiTounsi.Controllers
{
    public class AdController : Controller
    {
        public ActionResult Index()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("ad").Result;
            IEnumerable<Ad> Ads = response.Content.ReadAsAsync<IEnumerable<Ad>>().Result;

            return View(Ads);

        }
        [AllowAnonymous]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ad model)
        {

            if (ModelState.IsValid)
            {
                var UserLoggedIn = Session["User"] as UserRegisterModel;
                long supplierid = 0;
                if (UserLoggedIn != null)
                {
                    supplierid = UserLoggedIn.userId;
                }
                model.isValid = false;
                model.actualViewNumber = 0;
                //model.startDateFormatted = Convert.ToDateTime(model.startDateString);
                //model.endDateFormatted = Convert.ToDateTime(model.endDateString);
                var AdJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                System.Diagnostics.Debug.WriteLine(AdJson.ToString());

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

                var content = new StringContent(AdJson.ToString(), Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                HttpResponseMessage response = client.PostAsync("ad/"+ supplierid, content).Result;

                System.Diagnostics.Debug.WriteLine(response);
                return RedirectToAction("Index");

            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Edit(long id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("ad/"+ id).Result;
            Ad ad = response.Content.ReadAsAsync<Ad>().Result;

            return View(ad);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Ad model)
        {

            if (ModelState.IsValid)
            {
                var AdJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                System.Diagnostics.Debug.WriteLine(AdJson.ToString());

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

                var content = new StringContent(AdJson.ToString(), Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                HttpResponseMessage response = client.PutAsync("ad/" + model.adId, content).Result;

                System.Diagnostics.Debug.WriteLine(response);
                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}
