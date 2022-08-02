using Muafiyet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    [Authorize]

    public class IcerikResmiController : Controller
    {
        // GET: IcerikResmi
        MuafContext ctx = new MuafContext();
        public ActionResult Index(int ID)
        {
            DersIcerikleri di = ctx.DersIcerikleris.FirstOrDefault(x => x.AlinanDersID == ID);

           // ViewBag.ResimYolu = resimYolu;
            return View(di);
        }
        public ActionResult IcerikOnizleme(int ID)
        {
            DersIcerikleri di = ctx.DersIcerikleris.FirstOrDefault(x => x.AlinanDersID == ID);       
            return View(di);
        }
    }
}