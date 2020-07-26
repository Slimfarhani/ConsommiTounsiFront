using ConsommiTounsi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("post").Result;
            IEnumerable<Post> posts = response.Content.ReadAsAsync<IEnumerable<Post>>().Result;

            return View(posts);
        }
        public ActionResult PostCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PostCreate(Post model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.imageUrl = file.FileName;
                    var path1 = Path.Combine(Server.MapPath("~/PostImages/"), file.FileName);
                    Image image = Image.FromStream(file.InputStream, true, true);
                    var img1 = ResizeImage(image, 820, 481);
                    img1.Save(path1, ImageFormat.Png);
                }
                if ((UserRegisterModel)Session["User"] != null)
                {

                    model.User = (UserRegisterModel)Session["User"];
                }
                var postJson = await Task.Run(() => JsonConvert.SerializeObject(model));

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
                var content = new StringContent(postJson.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response;

                response = client.PostAsync("post/" + model.User.userId, content).Result;

                return Redirect("/Blog/Index");

            }
            return View(model);
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
    }
}