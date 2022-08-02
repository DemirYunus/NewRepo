using Muafiyet.App_Classes;
using Muafiyet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    [Authorize]

    public class DersSecimiController : Controller
    {
        // GET: DersSecimi
        MuafContext ctx = new MuafContext();
        public ActionResult Index(int ID)
        {            
            MuafiyetBasvurulari mf = ctx.MuafiyetBasvurularis.FirstOrDefault(x => x.MuafBasvuruID == ID);           

            ViewBag.NotTipi = mf.NotTipiID;
           
            //Dinamik Olmalı
            List<DersIcerikleri> blD = ctx.DersIcerikleris.Where(x => x.AlinanUniID == UyeInfo.KullaniciUniID && x.AlinanFakulteID == UyeInfo.KullaniciFakulteID && x.AlinanBolumID == UyeInfo.KullaniciBolumID).ToList();
            MuafiyetBasvurulari bi = ctx.MuafiyetBasvurularis.FirstOrDefault(x => x.MuafBasvuruID == ID);
            List<DersIcerikleri> dic = ctx.DersIcerikleris.Where(x => x.AlinanUniID == bi.MuafIsUniID && x.AlinanFakulteID == bi.MuaIstFaktID && x.AlinanBolumID == bi.MuafIsBolumID).ToList();

           
            ViewBag.Universiteler = Data.Universite;        
            ViewBag.BolumDersleri = blD;
            ViewBag.DersIcerikleri = dic;
            ViewBag.MuafBasvID = ID;

            if (mf.Denklik == 1) TempData["Denklik"] = "1";
            else TempData["Denklik"] = "0";

            string faklte = "";
            foreach (Fakulteler ff in Data.Fakulte)
            {
                if (ff.FakulteID == bi.MuaIstFaktID)
                {
                    faklte = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ff.Fakulte.ToLower());
                    break;
                }
            }
            string bolm = "";
            foreach (Bolumler bb in Data.Bolum)
            {
                if (bb.BolumID == bi.MuafIsBolumID)
                {
                    bolm = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bb.Bolum.ToLower());
                    break;
                }
            }           

            ViewBag.BasvuruAdSoyad = bi.Kullanicilar.KullaniciAdi + " " + bi.Kullanicilar.KullaniciSoyadi;
            ViewBag.Okul = @System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bi.Universiteler.Universite.ToLower()) + " " + faklte + " " + bolm;
            ViewBag.BasvuranBilgi = @System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bi.Universiteler.Universite.ToLower()) + " " + faklte + " " + bolm;//bi.Kullanicilar.KullaniciAdi + " " + bi.Kullanicilar.KullaniciSoyadi + " " +
            ViewBag.BasvuranBilgi2 = bi.Kullanicilar.KullaniciAdi + " " + bi.Kullanicilar.KullaniciSoyadi;

            ViewBag.BasvurulanUniID = bi.MuafIsUniID;
            ViewBag.BasvurulanUni= bi.Universiteler.Universite.ToString().Split(new[] { " ÜNİVERSİTESİ" }, StringSplitOptions.None)[0];
            ViewBag.MevcutUni =  bi.Kullanicilar.Universiteler.Universite.ToString().Split(new[] { " ÜNİVERSİTESİ" }, StringSplitOptions.None)[0];
            ViewBag.BasvurulanFakulteID = bi.MuaIstFaktID;
            ViewBag.BasvurulanBolumID = bi.MuafIsBolumID;

            List<MuafIstDersler> di = ctx.MuafIstDerslers.Where(x => x.MuafBasvID == ID).OrderBy(x=>x.CokluGrupID).ToList();            

            return View(di);
        }

        public ActionResult Ekle(MuafIstDersler mf, string cokluTercih)
        {
            int coklugrup = 0;
            Random rnd = new Random();
            if (cokluTercih=="on")
            {
                coklugrup = rnd.Next(1, 1000000000); 
            }

            MuafIstDersler m = ctx.MuafIstDerslers.FirstOrDefault(x => x.MuafIstDersID == mf.MuafIstDersID && x.AlinanDersID == mf.AlinanDersID && x.MuafBasvID == mf.MuafBasvID);
            ViewBag.DurumKayitt = 0;

            if (m == null)
            {
                //MuafIstDersler m = new MuafIstDersler();
                List<IcerikKararlari> i = ctx.IcerikKararlaris.Where(x => x.AlinanDersID == mf.AlinanDersID && x.MuafIstDersID == mf.MuafIstDersID).ToList();

                int nihaiKarar = 0;

                if (i.Count() > 0)
                {
                    if (i[0].KararUye1 == 0 || i[0].KararUye2 == 0 || i[0].KararUye3 == 0)
                    {
                        nihaiKarar = 0; //Değerlendirilmemiş    
                    }
                    else
                    {
                        if (i[0].KararUye1 + i[0].KararUye2 + i[0].KararUye3 > 1)
                        {
                            nihaiKarar = 1; //Uygun
                        }
                        else
                        {
                            nihaiKarar = -1;// Uygun Değil
                        }
                    }
                }
                else
                {
                    nihaiKarar = 0; //Değerlendirilmemiş 
                }

                mf.DanismanUygunluk = 0;
                mf.IcerikUygunluk = nihaiKarar;
                mf.KrediUygunluk = 0;
                mf.NotUygunluk = 0;
                mf.CokluGrupID = coklugrup;

                if (mf.AlinanDersNotu == null) mf.AlinanDersNotu = "YO";

                ctx.MuafIstDerslers.Add(mf);
                ctx.SaveChanges();

                if (i.Count() == 0)
                {
                    IcerikKararlari ik = new IcerikKararlari();
                    ik.MuafIstDersID = mf.MuafIstDersID;
                    ik.AlinanDersID = mf.AlinanDersID;
                    ik.KararUye1 = 0;
                    ik.KararUye2 = 0;
                    ik.KararUye3 = 0;
                    ik.KararTarihi = DateTime.Now;
                    ik.KararUniID = UyeInfo.KullaniciUniID;
                    ik.KararFakulteID = UyeInfo.KullaniciFakulteID;
                    ik.KararBolumID = UyeInfo.KullaniciBolumID;
                    ik.CokluGrupID = coklugrup;

                    ctx.IcerikKararlaris.Add(ik);
                    ctx.SaveChanges();
                }


                TempData["Durum"] = "Ekle";
                TempData["Text"] = "Tamam";

                ViewBag.MuafBasvID = mf.MuafBasvID;
                return RedirectToAction("Index", new { ID = mf.MuafBasvID });
            }
            else
            {
                ViewBag.MuafBasvID = mf.MuafBasvID;
                TempData["Kayitlimi"] = "Kayitli";
                TempData["Durum"] = "Hata";
                TempData["Text"] = "Bu ders daha önce eklenmiş!";
                return RedirectToAction("Index", new { ID = mf.MuafBasvID });
            }
        }

        [HttpPost]
        public string Sil(int ID)
        {
            MuafIstDersler m = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == ID);
            if (m.CokluGrupID > 0)
            {
                List<MuafIstDersler> mx = ctx.MuafIstDerslers.Where(x => x.CokluGrupID == m.CokluGrupID).ToList();
                foreach (MuafIstDersler item in mx)
                {
                    MuafIstDersler mm = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == item.ID);
                    ctx.MuafIstDerslers.Remove(mm);

                    IcerikKararlari ic = ctx.IcerikKararlaris.FirstOrDefault(x => x.AlinanDersID == mm.AlinanDersID && x.MuafIstDersID == mm.MuafIstDersID && x.CokluGrupID == mm.CokluGrupID);

                    if (ic.KararUye1 == 0 || ic.KararUye2 == 0 || ic.KararUye3 == 0)
                    {
                        ctx.IcerikKararlaris.Remove(ic);
                    }
                }
            }
            else
            {
                ctx.MuafIstDerslers.Remove(m);

                IcerikKararlari ic = ctx.IcerikKararlaris.FirstOrDefault(x => x.AlinanDersID == m.AlinanDersID && x.MuafIstDersID == m.MuafIstDersID);

                if (ic.KararUye1 == 0 || ic.KararUye2 == 0 || ic.KararUye3 == 0)
                {
                    ctx.IcerikKararlaris.Remove(ic);
                }
            }        
           

            try
            {
                ctx.SaveChanges();
                //return JavaScript(String.Format("Toast({0})", "basarili"));
                TempData["Durum"] = "Sil";
                TempData["Text"] = "Tamam";
                return "başarılı";
            }
            catch (Exception)
            {
                //return JavaScript(String.Format("Toast({0})", "hata"));
                TempData["Durum"] = "Hata";
                TempData["Text"] = "İşleminiz gerçekleştirilemedi!";
                return "hata";
            }

        }

        [HttpPost]
        public string CokluSil(int ID)
        {
            MuafIstDersler m = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == ID);
            IcerikKararlari i = ctx.IcerikKararlaris.FirstOrDefault(x => x.MuafIstDersID == m.MuafIstDersID && x.AlinanDersID == m.AlinanDersID);           
            ctx.MuafIstDerslers.Remove(m);
            ctx.IcerikKararlaris.Remove(i);
            

            ViewBag.MuafIstDersID = m.MuafIstDersID;

            List<MuafIstDersler> mx = ctx.MuafIstDerslers.Where(x => x.CokluGrupID == m.CokluGrupID).ToList();
            foreach (MuafIstDersler item in mx)
            {
                if (item.ID != ID)
                {
                    MuafIstDersler md = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == item.ID);
                    md.KrediUygunluk = 0;
                    md.NotUygunluk = 0;
                    md.DanismanUygunluk = 0;
                    md.IcerikUygunluk = 0;
                    ctx.SaveChanges();
                    //İçerik kararları tabloda sıfırlanıyor
                    IcerikKararlari ic = ctx.IcerikKararlaris.FirstOrDefault(x => x.MuafIstDersID == item.MuafIstDersID && x.AlinanDersID == item.AlinanDersID && x.CokluGrupID==item.CokluGrupID);
                    ic.KararUye1 = 0;
                    ic.KararUye2 = 0;
                    ic.KararUye3 = 0;
                    ctx.SaveChanges();
                }
            }
            if (mx.Count==2)//Henüz silmediği için 2
            {
                //MuafIstDersler my = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == m.CokluGrupID);
                //my.CokluGrupID = 0;
                //ctx.SaveChanges();
            }

            try
            {
                ctx.SaveChanges();
                //return JavaScript(String.Format("Toast({0})", "basarili"));
                TempData["Durum"] = "Sil";
                TempData["Text"] = "Tamam";
                return "başarılı";
            }
            catch (Exception)
            {
                //return JavaScript(String.Format("Toast({0})", "hata"));
                TempData["Durum"] = "Hata";
                TempData["Text"] = "İşleminiz gerçekleştirilemedi!";
                return "hata";
            }

        }

        public ActionResult KararKaydet(int ID, string DanismanUygunlukk, string NotUygunluk, string KrediUygunluk)
        {
            MuafIstDersler mf = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == ID);
            if (mf.CokluGrupID>0)
            {
                List<MuafIstDersler> mx = ctx.MuafIstDerslers.Where(x => x.CokluGrupID == mf.CokluGrupID).ToList();
                foreach (MuafIstDersler item in mx)
                {
                    MuafIstDersler mm = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == item.ID);
                    if (DanismanUygunlukk == "on") mm.DanismanUygunluk = 1;
                    else mm.DanismanUygunluk = -1;
                    if (KrediUygunluk == "on") mm.KrediUygunluk = 1;
                    else mm.KrediUygunluk = -1;
                    if (NotUygunluk == "on") mm.NotUygunluk = 1;
                    else mm.NotUygunluk = -1;

                    ctx.SaveChanges();
                }
            }
            else
            {
                if (DanismanUygunlukk == "on") mf.DanismanUygunluk = 1;
                else mf.DanismanUygunluk = -1;
                if (KrediUygunluk == "on") mf.KrediUygunluk = 1;
                else mf.KrediUygunluk = -1;
                if (NotUygunluk == "on") mf.NotUygunluk = 1;
                else mf.NotUygunluk = -1;

                ctx.SaveChanges();
            }        

            #region notification güncelleniyor

            List<MuafIstDersler> mff = ctx.MuafIstDerslers.Where(x => x.DanismanUygunluk == 0 || x.IcerikUygunluk == 0 || x.NotUygunluk == 0 || x.KrediUygunluk == 0).ToList();
            Notification.BekleyenBasvuruBildiriSayisi = mff.Count;
            Notification.BekleyenToplamSayisi = Notification.BekleyenIcerikBildiriSayisi +Notification.BekleyenBasvuruBildiriSayisi;

            #endregion

            return RedirectToAction("Index", new { ID = mf.MuafBasvID });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult IcerikEkle(DersIcerikleri di, HttpPostedFileBase file, int MuafBasvuruID)
        {
            string yol = "";

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg" || Path.GetExtension(file.FileName).ToLower() == ".png" || Path.GetExtension(file.FileName).ToLower() == ".gif" || Path.GetExtension(file.FileName).ToLower() == ".jpeg" || Path.GetExtension(file.FileName).ToLower() == ".pdf")
                    {
                        //yol = Path.Combine(Server.MapPath("~/images"), file.FileName);
                        //file.SaveAs(yol);
                        //di.IcerikResimYolu = "/images/" + file.FileName.ToString();

                        string yeniDosyaAdi = Guid.NewGuid().ToString() +
    Path.GetExtension(file.FileName);
                        yol = Path.Combine(Server.MapPath("~/images"), yeniDosyaAdi);
                        file.SaveAs(yol);
                        di.IcerikResimYolu = yeniDosyaAdi;

                        TempData["Durum"] = "İçerikEkle";
                        TempData["Text"] = di.AlinanDersAdi.ToString();
                    }
                    else
                    {
                        TempData["Durum"] = "Hata-Ekle";
                        TempData["Text"] = di.AlinanDersAdi.ToString();
                    }
                }
            }
            else
            {
                TempData["Durum"] = "İçerikEkle";
                TempData["Text"] = di.AlinanDersAdi.ToString();
            }


            di.AlinanDersAdi = di.AlinanDersAdi.TrimStart();
            di.AlinanDersKodu = di.AlinanDersKodu.TrimStart();

            ctx.DersIcerikleris.Add(di);
            ctx.SaveChanges();

            return RedirectToAction("Index", new { ID = MuafBasvuruID });
            //return RedirectToAction("Index","DersSecimi", new { ID = MuafBasvuruID });// mf.MuafBasvID
        }

        public ActionResult CokluSecim(int ID)
        {
            ViewBag.AnaID = ID;
            MuafIstDersler mf = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == ID);

            List<MuafIstDersler> m = null;
            if (mf.CokluGrupID>0)
            {
                 m = ctx.MuafIstDerslers.Where(x => x.CokluGrupID == mf.CokluGrupID).ToList();
            }            
            ViewBag.MevcutDers = mf.DersIcerikleri.AlinanDersAdi;
            ViewBag.BasvurulanUni = mf.DersIcerikleri1.Universiteler.Universite;

            ViewBag.DersAdi = mf.DersIcerikleri1.AlinanDersAdi;
            ViewBag.Kredisi = mf.DersIcerikleri1.AlinanKredisi;
            ViewBag.Notu = mf.AlinanDersNotu;

            List<Universiteler> uni = ctx.Universitelers.ToList();
            ViewBag.Universiteler = uni;

            List<DersIcerikleri> blD = ctx.DersIcerikleris.Where(x => x.AlinanUniID == UyeInfo.KullaniciUniID && x.AlinanFakulteID == UyeInfo.KullaniciFakulteID && x.AlinanBolumID == UyeInfo.KullaniciBolumID).ToList();
            ViewBag.BolumDersleri = blD;
           
            List<DersIcerikleri> dic = ctx.DersIcerikleris.Where(x => x.AlinanUniID == mf.MuafiyetBasvurulari.MuafIsUniID && x.AlinanFakulteID == mf.MuafiyetBasvurulari.MuaIstFaktID && x.AlinanBolumID == mf.MuafiyetBasvurulari.MuafIsBolumID).ToList();
            ViewBag.DersIcerikleri = dic;

            ViewBag.NotTipi = mf.MuafiyetBasvurulari.NotTipiID;
            ViewBag.MuafBasvID = mf.MuafiyetBasvurulari.MuafBasvuruID;
            ViewBag.CokluGrupID = mf.CokluGrupID;
            ViewBag.MuafIstDersID = mf.MuafIstDersID;

            return View(m);
        }

        [HttpPost]
        public ActionResult CokluEkle(MuafIstDersler mf)
        {
            MuafIstDersler m = ctx.MuafIstDerslers.FirstOrDefault(x => x.MuafIstDersID == mf.MuafIstDersID && x.AlinanDersID == mf.AlinanDersID && x.MuafBasvID == mf.MuafBasvID);
            ViewBag.DurumKayitt = 0;
           
            if (m == null)
            {
                //MuafIstDersler m = new MuafIstDersler();
                List<IcerikKararlari> i = ctx.IcerikKararlaris.Where(x => x.AlinanDersID == mf.AlinanDersID && x.MuafIstDersID == mf.MuafIstDersID).ToList();

                int nihaiKarar = 0;

                if (i.Count() > 0)
                {
                    if (i[0].KararUye1 == 0 || i[0].KararUye2 == 0 || i[0].KararUye3 == 0)
                    {
                        nihaiKarar = 0; //Değerlendirilmemiş    
                    }
                    else
                    {
                        if (i[0].KararUye1 + i[0].KararUye2 + i[0].KararUye3 > 1)
                        {
                            nihaiKarar = 1; //Uygun
                        }
                        else
                        {
                            nihaiKarar = -1;// Uygun Değil
                        }
                    }
                }
                else
                {
                    nihaiKarar = 0; //Değerlendirilmemiş 
                }

                mf.DanismanUygunluk = 0;
                mf.IcerikUygunluk = nihaiKarar;
                mf.KrediUygunluk = 0;
                mf.NotUygunluk = 0;               
             

                ctx.MuafIstDerslers.Add(mf);
                ctx.SaveChanges();

                if (i.Count() == 0)
                {
                    IcerikKararlari ik = new IcerikKararlari();
                    ik.MuafIstDersID = mf.MuafIstDersID;
                    ik.AlinanDersID = mf.AlinanDersID;
                    ik.KararUye1 = 0;
                    ik.KararUye2 = 0;
                    ik.KararUye3 = 0;
                    ik.KararTarihi = DateTime.Now;
                    ik.KararUniID = UyeInfo.KullaniciUniID;
                    ik.KararFakulteID = UyeInfo.KullaniciFakulteID;
                    ik.KararBolumID = UyeInfo.KullaniciBolumID;
                    ik.CokluGrupID = (int)mf.CokluGrupID;

                    ctx.IcerikKararlaris.Add(ik);
                    ctx.SaveChanges();
                }

                TempData["Durum"] = "Ekle";
                TempData["Text"] = "Tamam";

                ViewBag.MuafBasvID = mf.MuafBasvID;
                return RedirectToAction("Index", new { ID = mf.MuafBasvID });
            }
            else
            {
                ViewBag.MuafBasvID = mf.MuafBasvID;
                TempData["Kayitlimi"] = "Kayitli";
                TempData["Durum"] = "Hata";
                TempData["Text"] = "Bu ders daha önce eklenmiş!";
                return RedirectToAction("Index", new { ID = mf.MuafBasvID });
            }
        }

        public JsonResult TamamlaDersKodu(string search, int uniid, int fktid, int blmid)
        {
            List<DersKoduAdi> di = ctx.DersIcerikleris.Where(x =>x.AlinanUniID== uniid && x.AlinanFakulteID==fktid && x.AlinanBolumID== blmid && x.AlinanDersKodu.Contains(search)).Select(x => new DersKoduAdi {
                AlinanDersKodu = x.AlinanDersKodu,
                AlinanDersAdi = x.AlinanDersAdi                
            }).ToList();

            return new JsonResult { Data = di, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult TamamlaDersAdi(string search, int uniid, int fktid, int blmid)
        {
            List<DersKoduAdi> di = ctx.DersIcerikleris.Where(x => x.AlinanUniID == uniid && x.AlinanFakulteID == fktid && x.AlinanBolumID == blmid && x.AlinanDersAdi.Contains(search)).Select(x => new DersKoduAdi
            {
                AlinanDersKodu = x.AlinanDersKodu,
                AlinanDersAdi = x.AlinanDersAdi
            }).ToList();

            return new JsonResult { Data = di, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}