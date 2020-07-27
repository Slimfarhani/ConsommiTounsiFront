using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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
        public ActionResult ManageSupplierAds()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("ad").Result;
            IEnumerable<Ad> Ads = response.Content.ReadAsAsync<IEnumerable<Ad>>().Result;

            return View(Ads);

        }

        public ActionResult ManageAds()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("ad").Result;
            IEnumerable<Ad> Ads = response.Content.ReadAsAsync<IEnumerable<Ad>>().Result;

            return View(Ads);

        }


        public ActionResult SupplierAds()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var UserLoggedIn = Session["User"] as UserRegisterModel;
            long supplierid = 0;
            if (UserLoggedIn != null)
            {
                supplierid = UserLoggedIn.userId;
            }
            HttpResponseMessage response = client.GetAsync("supplierAds/" + supplierid).Result;
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
        public async Task<ActionResult> Create(Ad model, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine(file.FileName);
                model.imageUrl = file.FileName;
                System.Diagnostics.Debug.WriteLine(file.FileName);
                var path1 = Path.Combine(Server.MapPath("~/AdImages/"), file.FileName);
                var path2 = Path.Combine(Server.MapPath("~/ResizedAdImages/"), file.FileName);
                Image image = Image.FromStream(file.InputStream, true, true);
                var img1 = ResizeImage(image, 720, 960);
                var img2 = ResizeImage(image, 1200, 1600);
                img1.Save(path1, ImageFormat.Png);
                img2.Save(path2, ImageFormat.Png);
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
                HttpResponseMessage response = client.PostAsync("ad/" + supplierid, content).Result;

                System.Diagnostics.Debug.WriteLine(response);
                return RedirectToAction("ManageSupplierAds");

            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Edit(long id)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("ad/" + id).Result;
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
                HttpPostedFileBase file = Request.Files[0];
                model.imageUrl = file.FileName;
                System.Diagnostics.Debug.WriteLine(file.FileName);
                var path1 = Path.Combine(Server.MapPath("~/AdImages/"), file.FileName);
                var path2 = Path.Combine(Server.MapPath("~/ResizedAdImages/"), file.FileName);
                Image image = Image.FromStream(file.InputStream, true, true);
                var img1 = ResizeImage(image, 720, 960);
                var img2 = ResizeImage(image, 1200, 1600);
                img1.Save(path1, ImageFormat.Png);
                img2.Save(path2, ImageFormat.Png);

                var AdJson = await Task.Run(() => JsonConvert.SerializeObject(model));
                System.Diagnostics.Debug.WriteLine(AdJson.ToString());

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

                var content = new StringContent(AdJson.ToString(), Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
                HttpResponseMessage response = client.PutAsync("ad/" + model.adId, content).Result;

                System.Diagnostics.Debug.WriteLine(response);
                return RedirectToAction("ManageSupplierAds");

            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> Details(long adId, string title ,string type, float cost , string imageUrl, int actualViewNumber, DateTime endDateFormatted)
        {
            var UserLoggedIn = Session["User"] as UserRegisterModel;
            long userId = 0;
            if (UserLoggedIn != null)
            {
                userId = UserLoggedIn.userId;
            }
            AdViews adViews = new AdViews();
            adViews.adId = adId;
            adViews.userId = userId;
            var AdJson = await Task.Run(() => JsonConvert.SerializeObject(adViews));
            System.Diagnostics.Debug.WriteLine(AdJson.ToString());

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");

            var content = new StringContent(AdJson.ToString(), Encoding.UTF8, "application/json");
            System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync());
            HttpResponseMessage response = client.PostAsync("adViews", content).Result;

            System.Diagnostics.Debug.WriteLine(response);
            Ad ad = new Ad();
            ad.adId = adId;
            ad.cost = cost;
            ad.title = title;
            ad.type = type;
            ad.imageUrl = imageUrl;
            ad.actualViewNumber = actualViewNumber;
            ad.endDateFormatted = endDateFormatted;
            return View(ad);
        }


        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        [AllowAnonymous]
        public ActionResult Delete(long id)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("ad/" + id).Result;
            Ad ad = response.Content.ReadAsAsync<Ad>().Result;
            return View(ad);
        }

        [HttpPost, ActionName("Delete")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            HttpResponseMessage response = client.DeleteAsync("ad/" + id).Result;

            System.Diagnostics.Debug.WriteLine(response);
            return RedirectToAction("ManageAds");
        }

        [AllowAnonymous]
        public ActionResult Validate(long id)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("ad/" + id).Result;
            Ad ad = response.Content.ReadAsAsync<Ad>().Result;
            return View(ad);
        }

        [HttpPost, ActionName("Validate")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ValidationConfirmed(long id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            HttpResponseMessage response = client.DeleteAsync("ad/validate/" + id).Result;

            System.Diagnostics.Debug.WriteLine(response);
            return RedirectToAction("ManageAds");
        }

    }
}
