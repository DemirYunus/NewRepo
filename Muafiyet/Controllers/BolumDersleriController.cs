using Muafiyet.App_Classes;
using Muafiyet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    public class BolumDersleriController : Controller
    {
        // GET: BolumDersleri
        MuafContext ctx = new MuafContext();
        //public ActionResult Index()
        //{
        //    List<BolumDersleri> bl = ctx.BolumDersleris.Where(x => x.Ders_UniID == UyeInfo.KullaniciUniID && x.Ders_FakulteID == UyeInfo.KullaniciFakulteID && x.Ders_BolumID == UyeInfo.KullaniciBolumID).ToList();
        //    return View(bl);
        //}

        //public ActionResult Ekle(BolumDersleri blm)
        //{
        //    blm.Ders_UniID = UyeInfo.KullaniciUniID;
        //    blm.Ders_FakulteID = UyeInfo.KullaniciFakulteID;
        //    blm.Ders_BolumID = UyeInfo.KullaniciBolumID;

        //    ctx.BolumDersleris.Add(blm);
        //    ctx.SaveChanges();


        //    TempData["Durum"] = "Ekle";
        //    TempData["Text"] = blm.DersAdi.ToString();

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult Guncelle(BolumDersleri blm)
        //{
        //    BolumDersleri bl = ctx.BolumDersleris.FirstOrDefault(x => x.DersID == blm.DersID);

        //    bl.DersKodu = blm.DersKodu;
        //    bl.DersAdi = blm.DersAdi;
        //    bl.Ders_SinifID = blm.Ders_SinifID;
        //    bl.Kredi = blm.Kredi;
        //    bl.AKTS = blm.AKTS;

        //    ctx.SaveChanges();

        //    TempData["Durum"] = "Güncelle";
        //    TempData["Text"] = bl.DersAdi.ToString();

        //    return RedirectToAction("Index");
        //}

        //public string Sil(int DersID)
        //{
        //    BolumDersleri bl = ctx.BolumDersleris.FirstOrDefault(x => x.DersID == DersID);
        //    ctx.BolumDersleris.Remove(bl);
       

        //    try
        //    {
        //        ctx.SaveChanges();
        //        TempData["Durum"] = "Sil";
        //        TempData["Text"] = bl.DersAdi.ToString();
        //        return "başarılı";
        //    }
        //    catch (Exception)
        //    {
        //        TempData["Durum"] = "Hata";
        //        TempData["Text"] = "İşleminiz gerçekleştirilemedi!";
        //        return "hata";
        //    }            
        //}
    }
}