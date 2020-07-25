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
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsi.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(ProductFormModel model, HttpPostedFileBase file)
        {
            if ((model.stock.price != null && model.stock.quantity == null) || (model.stock.price == null && model.stock.quantity != null))
            {

                return View(model);
            }
            
            System.Diagnostics.Debug.WriteLine("mesure : " + model.product.mesure);
            
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    model.product.imageUrl = file.FileName;
                    var path1 = Path.Combine(Server.MapPath("~/ProductImages/"), file.FileName);
                    var path2 = Path.Combine(Server.MapPath("~/ResizedProductImages/"), file.FileName);
                    var path3 = Path.Combine(Server.MapPath("~/ResizedProductImagesForCartNotif/"), file.FileName);
                    Image image = Image.FromStream(file.InputStream, true, true);
                    var img1 = ResizeImage(image, 720, 960);
                    var img2 = ResizeImage(image, 1200, 1600);
                    var img3 = ResizeImage(image, 320, 320);
                    img1.Save(path1, ImageFormat.Png);
                    img2.Save(path2, ImageFormat.Png);
                    img3.Save(path3, ImageFormat.Png);
                }
                
                var productJson = await Task.Run(() => JsonConvert.SerializeObject(model.product));

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
                var content = new StringContent(productJson.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                
                response = client.PostAsync("product", content).Result;
                Product product = response.Content.ReadAsAsync<Product>().Result;
                var UserLoggedIn= (UserRegisterModel)Session["User"];
                if (model.stock.price != null && model.stock.quantity != null)
                {
                    model.stock.product = product;
                    model.stock.supplier = new Supplier();
                    model.stock.supplier.userId = UserLoggedIn.userId;
                    var stockJson = await Task.Run(() => JsonConvert.SerializeObject(model.stock));
                    HttpClient client1 = new HttpClient();
                    client1.BaseAddress = new Uri("http://localhost:8080/springboot-crud-rest/api/v1/");
                    var content1 = new StringContent(stockJson.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage response1;
                    response1 = client.PostAsync("stockfornewproduct", content1).Result;
                }
                return Redirect("/Stock/Index");

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