﻿using ConsommiTounsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsommiTounsi.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }
        public new ActionResult Profile()
        {
            
            UserRegisterModel user = (UserRegisterModel) Session["User"];
            System.Diagnostics.Debug.WriteLine(user.role);
            System.Diagnostics.Debug.WriteLine(user.companyName);



            return View();
        }
    }
}