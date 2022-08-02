using Muafiyet.App_Classes;
using Muafiyet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Uye
        MuafContext ctx = new MuafContext();
        public ActionResult Index()
        {
            Ayarlar a = ctx.Ayarlars.FirstOrDefault(x => x.AyarUniID == UyeInfo.KullaniciUniID && x.AyarFakulteID == UyeInfo.KullaniciFakulteID && x.AyarBolumID == UyeInfo.KullaniciBolumID);

            ViewBag.NotTipID = a.NotTipiID;
            ViewBag.Kredi1 = a.Kredi1;
            ViewBag.Kredi2 = a.Kredi2;
            ViewBag.Kredi3 = a.Kredi3;
            ViewBag.Kredi4 = a.Kredi4;
            ViewBag.AKTS1 = a.AKTS1;
            ViewBag.AKTS2 = a.AKTS2;
            ViewBag.AKTS3 = a.AKTS3;
            ViewBag.AKTS4 = a.AKTS14;
            ViewBag.IntibakTipi = a.AKTS_Kredi;

            return View();
        }

        public ActionResult Kaydet(Ayarlar a)
        {
            Ayarlar aa = ctx.Ayarlars.FirstOrDefault(x => x.AyarUniID == UyeInfo.KullaniciUniID && x.AyarFakulteID == UyeInfo.KullaniciFakulteID && x.AyarBolumID == UyeInfo.KullaniciBolumID);

            aa.NotTipiID = a.NotTipiID;

            ctx.SaveChanges();
            TempData["Durum"] = "NotTipi";
            TempData["Text"] = "";

            return RedirectToAction("Index");            
        }

        public ActionResult IntibakKaydet(Ayarlar a)
        {
            Ayarlar aa = ctx.Ayarlars.FirstOrDefault(x => x.AyarUniID == UyeInfo.KullaniciUniID && x.AyarFakulteID == UyeInfo.KullaniciFakulteID && x.AyarBolumID == UyeInfo.KullaniciBolumID);

            aa.AKTS_Kredi = a.AKTS_Kredi;
            aa.Kredi1 = a.Kredi1;
            aa.Kredi2 = a.Kredi2;
            aa.Kredi3 = a.Kredi3;
            aa.Kredi4 = a.Kredi4;
            aa.AKTS1 = a.AKTS1;
            aa.AKTS2 = a.AKTS2;
            aa.AKTS3 = a.AKTS3;
            aa.AKTS14 = a.AKTS14;

            ctx.SaveChanges();
            TempData["Durum"] = "Intibak";
            if(a.AKTS_Kredi==1) TempData["Text"] = "KREDİ'ye göre yapılacak";
            else TempData["Text"] = "AKTS'ye göre yapılacak";

            return RedirectToAction("Index");
        }
    }
}