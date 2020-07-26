using ConsommiTounsi.Models;
using Data;
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
    public class BlogController : Controller
    {
        // GET: Customer
        MyContext context;
        public ActionResult Index(String search)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("post").Result;
            if (!String.IsNullOrEmpty(search))
            {
                System.Diagnostics.Debug.Write("search");
                response = client.GetAsync("postByCustomerName/" + search).Result;
            }
            else
            {
                System.Diagnostics.Debug.Write("simple");
                response = client.GetAsync("post").Result;
            }
          
            IEnumerable <Post> posts= response.Content.ReadAsAsync<IEnumerable<Post>>().Result;

            ViewBag.posts = posts;
            return View(posts);
        }
        /*  [HttpPost]
          [AllowAnonymous]
          public async Task<ActionResult> CreatePost (CustomerPost model, HttpPostedFileBase file)
          {
              if (model.post == null)
              {

                  model.post = new Post();
              }


               if (ModelState.IsValid)
              {

                  var postJson = await Task.Run(() => JsonConvert.SerializeObject(model.post));
                  HttpClient client = new HttpClient();
                  client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
                  var content = new StringContent(postJson.ToString(), Encoding.UTF8, "application/json");
                  HttpResponseMessage response= client.GetAsync("post").Result;


                  return Redirect("/Blog/Index");

              }
              return View(model);
          }*/

        [AllowAnonymous]
        public ActionResult AddPost()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPost(Post model)
        {

            if (ModelState.IsValid)
            {
                var postJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                System.Diagnostics.Debug.WriteLine(postJson.ToString());

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

                var content = new StringContent(postJson.ToString(), Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                HttpResponseMessage response = client.PostAsync("/post/{userid}", content).Result;

                System.Diagnostics.Debug.WriteLine(response);
                return RedirectToAction("/Index");

            }
            return View(model);
        }
    }
}