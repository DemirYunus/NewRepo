using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    public class HataController : Controller
    {
        // GET: Hata
        public ActionResult UygunOlmayanFormat()
        {
            return View();
        }
    }
}