using Muafiyet.App_Classes;
using Muafiyet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    [Authorize]

    public class KullanicilarController : Controller
    {
        // GET: Kullanicilar
        MuafContext ctx = new MuafContext();

        public ActionResult Index(int? filtreID)
        {
            List<Kullanicilar> k = null;
            
            if (filtreID == -1)//Üye
            {
                if (UyeInfo.KullaniciEMail == "adminMuaf")
                {
                    k = ctx.Kullanicilars.Where(x => x.KullaniciUnvanID != 11).ToList();
                }
                else
                {
                    k = ctx.Kullanicilars.Where(x => x.KullaniciUnvanID != 11 && x.KullaniciUniID == UyeInfo.KullaniciUniID && x.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.KullaniciBolumID == UyeInfo.KullaniciBolumID).ToList();
                }                
               
            }
            else if (filtreID == -2)//Öğrenci
            {
                if (UyeInfo.KullaniciEMail == "adminMuaf")
                {
                    k = ctx.Kullanicilars.Where(x => x.KullaniciUnvanID == 11).ToList();
                }
                else
                {
                    k = ctx.Kullanicilars.Where(x => x.KullaniciUnvanID == 11 && x.KullaniciUniID == UyeInfo.KullaniciUniID && x.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.KullaniciBolumID == UyeInfo.KullaniciBolumID).ToList();
                }
                    
            }
            else if (filtreID == 0)//Tümü
            {
                if (UyeInfo.KullaniciEMail == "adminMuaf")
                {
                    k = ctx.Kullanicilars.ToList();
                }
                else
                {
                    k = ctx.Kullanicilars.Where(x => x.KullaniciUniID == UyeInfo.KullaniciUniID && x.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.KullaniciBolumID == UyeInfo.KullaniciBolumID).ToList();
                }
                    
            }          
            else//İlk açılış üyeleri getirir.
            {
                if (UyeInfo.KullaniciEMail == "adminMuaf")
                {
                    k = ctx.Kullanicilars.Where(x => x.KullaniciUnvanID != 11).ToList();
                }
                else
                {
                    k = ctx.Kullanicilars.Where(x => x.KullaniciUnvanID != 11 && x.KullaniciUniID == UyeInfo.KullaniciUniID && x.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.KullaniciBolumID == UyeInfo.KullaniciBolumID).ToList();
                }
                   
            }

            List<Kullanicilar> bolumYetkilileri = ctx.Kullanicilars.Where(x => x.KullaniciUniID == UyeInfo.KullaniciUniID && x.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.KullaniciBolumID == UyeInfo.KullaniciBolumID && x.KullaniciRolID < 4).ToList();
            
            ViewBag.ikinciUye = "Tanımlanmamış";
            ViewBag.birinciUye = "Tanımlanmamış";
            ViewBag.baskanUye = "Tanımlanmamış";

            foreach (Kullanicilar item in bolumYetkilileri)
            {
                if (item.KullaniciRolID==3)
                {
                    ViewBag.ikinciUye = item.KullaniciAdi.ToString() + " " + item.KullaniciSoyadi.ToString();
                }
                else if (item.KullaniciRolID == 2)
                {
                    ViewBag.birinciUye = item.KullaniciAdi.ToString() + " " + item.KullaniciSoyadi.ToString(); 
                }
                else if (item.KullaniciRolID == 1)
                {
                    ViewBag.baskanUye = item.KullaniciAdi.ToString() + " " + item.KullaniciSoyadi.ToString();
                }
            }       

            ViewBag.Kullanicilar = k;
                  
            return View();
        }

        //[HttpPost]
        //public ActionResult Ekle(string KullaniciOkulNo, int KullaniciUnvanID, string KullaniciAdi, string KullaniciSoyadi, string KullaniciTlf, string KullaniciEMail, string KullaniciSifre, string KullaniciAdres)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Kullanicilar k = new Kullanicilar();
        //        k.KullaniciOkulNo = KullaniciOkulNo;
        //        k.KullaniciUnvanID = KullaniciUnvanID;
        //        k.KullaniciAdi = KullaniciAdi;
        //        k.KullaniciSoyadi = KullaniciSoyadi;
        //        k.KullaniciTlf = KullaniciTlf;
        //        k.KullaniciEMail = KullaniciEMail;
        //        k.KullaniciSifre = KullaniciSifre;
        //        k.KullaniciAdres = KullaniciAdres;
        //        //Dinamik olmalı
        //        k.KullaniciUniID = 26;
        //        k.KullaniciFakulteID = 6;
        //        k.KullaniciBolumID = 4;

        //        ctx.Kullanicilars.Add(k);
        //        ctx.SaveChanges();
        //        return RedirectToAction("Ekle", "Basvurular");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        public ActionResult Ekle(Kullanicilar k)
        {
            k.KullaniciUniID = UyeInfo.KullaniciUniID;
            k.KullaniciFakulteID = UyeInfo.KullaniciFakulteID;
            k.KullaniciBolumID = UyeInfo.KullaniciBolumID;
            k.AktifMi = 1;
            if (k.KullaniciUnvanID == 11) k.KullaniciRolID = 6;
            else k.KullaniciRolID = 6;

            ctx.Kullanicilars.Add(k);
            ctx.SaveChanges();

            TempData["Durum"] = "Ekle";
            TempData["Text"] = k.KullaniciAdi.ToString()+" "+ k.KullaniciSoyadi.ToString();

            return RedirectToAction("Index", "Kullanicilar");
        }

        public String BasvurudanEkle(Kullanicilar k)
        {
            k.KullaniciUniID = UyeInfo.KullaniciUniID;
            k.KullaniciFakulteID = UyeInfo.KullaniciFakulteID;
            k.KullaniciBolumID = UyeInfo.KullaniciBolumID;
            k.AktifMi = 1;
            if (k.KullaniciUnvanID == 11) k.KullaniciRolID = 6;
            else k.KullaniciRolID = 6;

            ctx.Kullanicilars.Add(k);
            ctx.SaveChanges();

            TempData["Durum"] = "Ekle";
            TempData["Text"] = k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();

            return "basarili";
        }

        [HttpPost]
        public ActionResult Guncelle(Kullanicilar k)
        {
            Kullanicilar kk = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciID == k.KullaniciID);

            kk.KullaniciOkulNo = k.KullaniciOkulNo;          
            kk.KullaniciAdi = k.KullaniciAdi;
            kk.KullaniciSoyadi = k.KullaniciSoyadi;
            kk.KullaniciTlf = k.KullaniciTlf;
            kk.KullaniciEMail = k.KullaniciEMail;
            kk.KullaniciSifre = k.KullaniciSifre;
            kk.KullaniciAdres = k.KullaniciAdres;
            kk.KullaniciUnvanID = k.KullaniciUnvanID;
            
            ctx.SaveChanges();

            TempData["Durum"] = "Güncelle";
            TempData["Text"] = k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();

            return RedirectToAction("Index", "Kullanicilar");

        }

        [HttpPost]
        public string Sil(int ID)
        {
            Kullanicilar k= ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciID == ID);            
            ctx.Kullanicilars.Remove(k);


          List<MuafiyetBasvurulari> mf = ctx.MuafiyetBasvurularis.Where(x => x.OgrenciID == ID).ToList();

            string sonuc = "";

            if (mf.Count==0)
            {
                try
                {
                    ctx.SaveChanges();
                    //return JavaScript(String.Format("Toast({0})", "basarili"));
                    TempData["Durum"] = "Sil";
                    TempData["Text"] = k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();
                    sonuc= "başarılı";
                }
                catch (Exception)
                {
                    //return JavaScript(String.Format("Toast({0})", "hata"));
                    TempData["Durum"] = "Hata";
                    TempData["Text"] = "İşleminiz gerçekleştirilemedi!";
                    sonuc= "hata";
                }
            }
            else
            {
                TempData["Durum"] = "silinemez";
                TempData["Text"] = "Bu öğrenciye ait başvuru bulunnuyor !";
                sonuc = "silinemez";
            }

            return sonuc; 
        }

        [HttpPost]
        public ActionResult RolGuncelle(int KullaniciID, int KullaniciRolID)
        {
            Kullanicilar k = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciID == KullaniciID);

            if (KullaniciRolID == 1)
            {
                Kullanicilar komisyonBaskani = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciUniID == k.KullaniciUniID && x.KullaniciFakulteID == k.KullaniciFakulteID && x.KullaniciBolumID == k.KullaniciBolumID && x.KullaniciRolID == 1);

                if (komisyonBaskani==null)//Daha önce hiç komisyon başkanı atanmadı ise kaydet
                {
                    k.KullaniciRolID = 1;
                    ctx.SaveChanges();

                    TempData["Durum"] = "Rol Güncelle";
                    TempData["Text"] = k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();      
                }
                else //Önceki komisyon başkanını öğr. üye yap ve uyarı mesajı ver
                {
                    komisyonBaskani.KullaniciRolID = 5;
                    k.KullaniciRolID = 1;
                    ctx.SaveChanges();

                    TempData["Durum"] = "Rol Güncelle-Uyarı";
                    TempData["TextHataUyari"]= k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();
                    TempData["Text"] = komisyonBaskani.KullaniciAdi.ToString() + " " + komisyonBaskani.KullaniciSoyadi.ToString();
                }
            }
            else if (KullaniciRolID == 2)
            {
                Kullanicilar komisyonbirinciUye = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciUniID == k.KullaniciUniID && x.KullaniciFakulteID == k.KullaniciFakulteID && x.KullaniciBolumID == k.KullaniciBolumID && x.KullaniciRolID == 2);

                if (komisyonbirinciUye==null)
                {
                    k.KullaniciRolID = 2;
                    ctx.SaveChanges();

                    TempData["Durum"] = "Rol Güncelle";
                    TempData["Text"] = k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();
                }
                else
                {
                    komisyonbirinciUye.KullaniciRolID = 5;             
                    k.KullaniciRolID = 2;
                    ctx.SaveChanges();

                    TempData["Durum"] = "Rol Güncelle-Uyarı";
                    TempData["TextHataUyari"] = k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();
                    TempData["Text"] = komisyonbirinciUye.KullaniciAdi.ToString() + " " + komisyonbirinciUye.KullaniciSoyadi.ToString();
                }
            }
            else if (KullaniciRolID == 3)
            {
                Kullanicilar komisyonikinciUye = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciUniID == k.KullaniciUniID && x.KullaniciFakulteID == k.KullaniciFakulteID && x.KullaniciBolumID == k.KullaniciBolumID && x.KullaniciRolID == 3);

                if (komisyonikinciUye==null)
                {
                    k.KullaniciRolID = 3;
                    ctx.SaveChanges();

                    TempData["Durum"] = "Rol Güncelle";
                    TempData["Text"] = k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();
                }
                else
                {
                    komisyonikinciUye.KullaniciRolID = 5;
                    k.KullaniciRolID = 3;
                    ctx.SaveChanges();

                    TempData["Durum"] = "Rol Güncelle-Uyarı";
                    TempData["TextHataUyari"] = k.KullaniciAdi.ToString() + " " + k.KullaniciSoyadi.ToString();
                    TempData["Text"] = komisyonikinciUye.KullaniciAdi.ToString() + " " + komisyonikinciUye.KullaniciSoyadi.ToString();
                }
            }
            else if (KullaniciRolID == 6)
            {
                TempData["Durum"] = "Hata";                
                TempData["Text"] = "Öğrenci, komisyon üyesi olarak atanamaz !";
            }

            return RedirectToAction("Index");

        }
    }
}