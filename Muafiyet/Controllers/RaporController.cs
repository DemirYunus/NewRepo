using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    public class RaporController : Controller
    {
        // GET: Rapor
        public ActionResult KmsnRaporu(int MuafBasvID)
        {
            return View();
        }
    }
}