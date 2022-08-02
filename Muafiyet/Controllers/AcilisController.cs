using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Muafiyet.Content
{
    public class AcilisController : Controller
    {
        // GET: Acilis
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Panel()
        {
            return View();
        }
    }
}