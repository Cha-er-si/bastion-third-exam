using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bastion_Exam_Retying.Models;

namespace Bastion_Exam_Retying.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            Product price = new Product
            {
                price = 1500
            };
            return View(price);
        }
    }
}