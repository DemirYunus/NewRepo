using Microsoft.Reporting.WebForms;
using Muafiyet.App_Classes;
using Muafiyet.App_DataSet;
using Muafiyet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    [Authorize]
    public class BasvurularController : Controller
    {
        // GET: Basvurular
        MuafContext ctx = new MuafContext();
        public ActionResult Index(int? filtreID)
        {
            TempData["Denklik"] = 0;

            List<MuafiyetBasvurulari> bs = null;
            List<MuafiyetBasvurulari> bsKopya = null;

            if (UyeInfo.KullaniciEMail== "adminMuaf")
            {
                //View_Basvurular
                bs = ctx.MuafiyetBasvurularis.ToList();               
            }
            else
            {
    //View_Basvurular
             bs = ctx.MuafiyetBasvurularis.Where(x => x.Kullanicilar.KullaniciUniID == UyeInfo.KullaniciUniID && x.Kullanicilar.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.Kullanicilar.KullaniciBolumID == UyeInfo.KullaniciBolumID && x.Denklik==0).ToList();               
            }
                

            List<MuafIstDersler> mf = null;
            foreach (MuafiyetBasvurulari b in bs)
            {
                string sonuc = "Tamamlandı";

                mf = ctx.MuafIstDerslers.Where(x => x.MuafBasvID == b.MuafBasvuruID).ToList();
                if (mf.Count==0) sonuc = "Ders Seçilmemiş";
                else
                {
                    foreach (MuafIstDersler md in mf)
                    {
                        if (md.NotUygunluk == 0 || md.KrediUygunluk == 0 || md.IcerikUygunluk == 0 || md.DanismanUygunluk == 0)
                        {
                            sonuc = "Değerlendiriliyor";
                            break;
                        }
                    }
                }
                b.Durum = sonuc;                       
            }

            if (UyeInfo.KullaniciEMail == "adminMuaf")
            {                
                bsKopya =bs.Where(x => x.Durum == "Değerlendiriliyor").ToList();
            }
            else
            {                
                bsKopya = bs.Where(x => x.Kullanicilar.KullaniciUniID == UyeInfo.KullaniciUniID && x.Kullanicilar.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.Kullanicilar.KullaniciBolumID == UyeInfo.KullaniciBolumID && x.Denklik == 0 && x.Durum == "Değerlendiriliyor").ToList();
            }

            //ViewBag.Unvanlar = u;


            if (filtreID == 0)//Hepsi
            {
                return View(bs);
            }
            else if(filtreID==null) // İlk Açılış Süreci Devam Edenler
            {
                return View(bsKopya);
            }
            else //|| filtreID==-1 Süreci Devam Edenler
            {
                return View(bsKopya);
            }
        }

        public ActionResult Denklik(int? filtreID)
        {
            TempData["Denklik"] = 1;
            List<MuafiyetBasvurulari> dbs = null;
            List<MuafiyetBasvurulari> dbsKopya = null;

            if (UyeInfo.KullaniciEMail == "adminMuaf")
            {
                //View_Basvurular
                dbs = ctx.MuafiyetBasvurularis.ToList();

            }
            else
            {

                //View_Basvurular 
                dbs = ctx.MuafiyetBasvurularis.Where(x => x.Kullanicilar.KullaniciUniID == UyeInfo.KullaniciUniID && x.Kullanicilar.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.Kullanicilar.KullaniciBolumID == UyeInfo.KullaniciBolumID && x.Denklik == 1).ToList();

                dbs = ctx.MuafiyetBasvurularis.Where(x => x.Kullanicilar.KullaniciUniID == UyeInfo.KullaniciUniID && x.Kullanicilar.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.Kullanicilar.KullaniciBolumID == UyeInfo.KullaniciBolumID && x.Denklik == 1).ToList();

            }


            List<MuafIstDersler> mf = null;
            foreach (MuafiyetBasvurulari b in dbs)
            {
                string sonuc = "Tamamlandı";

                mf = ctx.MuafIstDerslers.Where(x => x.MuafBasvID == b.MuafBasvuruID).ToList();
                if (mf.Count == 0) sonuc = "Ders Seçilmemiş";
                else
                {
                    foreach (MuafIstDersler md in mf)
                    {
                        if (md.NotUygunluk == 0 || md.KrediUygunluk == 0 || md.IcerikUygunluk == 0 || md.DanismanUygunluk == 0)
                        {
                            sonuc = "Değerlendiriliyor";
                            break;
                        }
                    }
                }
                b.Durum = sonuc;
            }

            //ViewBag.Unvanlar = u;
            dbsKopya = dbs.Where(x => x.Durum == "Değerlendiriliyor" || x.Durum == "Ders Seçilmemiş").ToList();

            if (filtreID == 0)
            {
                return View(dbs);
            }
            else //Süreci Devam Edenler
            {
                return View(dbsKopya);
            }
        }

        public ActionResult Ekle()
        {                
            ViewBag.Universiteler = Data.Universite;                  

            return View();
        }

        public ActionResult EkleDenklik()
        {
            ViewBag.Universiteler = Data.Universite;

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(MuafiyetBasvurulari m)
        {            
            ViewBag.Universiteler = Data.Universite;


            m.BasvuruTarihi = DateTime.Now;

            ctx.MuafiyetBasvurularis.Add(m);
                ctx.SaveChanges();                

                return RedirectToAction("Index","DersSecimi", new{ID= m.MuafBasvuruID });
         
        }

        [HttpPost]
        public string Sil(int ID)
        {

            string sonuc = "";
            

            List<MuafIstDersler> mf = ctx.MuafIstDerslers.Where(x => x.MuafBasvID == ID).ToList();

            if (mf.Count == 0)
            {
                MuafiyetBasvurulari m = ctx.MuafiyetBasvurularis.FirstOrDefault(x => x.MuafBasvuruID == ID);
                ctx.MuafiyetBasvurularis.Remove(m);

                try
                {
                    ctx.SaveChanges();
                    sonuc= "başarılı";
                }
                catch (Exception)
                {

                    sonuc= "hata";
                }
            }
            else
            {
                foreach (MuafIstDersler md in mf)
                {
                    if (md.NotUygunluk == 0 || md.KrediUygunluk == 0 || md.IcerikUygunluk == 0 || md.DanismanUygunluk == 0)
                    {
                        sonuc= "silinemez";
                    }
                    else
                    {
                        try
                        {
                            ctx.SaveChanges();
                            sonuc= "başarılı";
                        }
                        catch (Exception)
                        {

                            sonuc= "hata";
                        }
                    }
                }
            }

            return sonuc;
        }

        [HttpGet]
        public string KullaniciyiGetir(string okulno)
        {
            Kullanicilar v = ctx.Kullanicilars.Where(a => a.KullaniciOkulNo == okulno).FirstOrDefault();
          
            string jsonString = JsonConvert.SerializeObject(v, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore          
             
            });

            return  jsonString;            
        }

        [HttpGet]
        public ActionResult KullaniciGetir(string okulno)
        {
            Kullanicilar v = ctx.Kullanicilars.Where(a => a.KullaniciOkulNo == okulno).FirstOrDefault();
            if (v != null)
            {
             
                return Json(new { KullaniciAdi = v.KullaniciAdi, KullaniciSoyadi = v.KullaniciSoyadi, KullaniciTlf = v.KullaniciTlf, KullaniciEMail = v.KullaniciEMail, KullaniciID = v.KullaniciID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Guncelle(int ID)
        {
            ViewBag.Universiteler = Data.Universite;           

            MuafiyetBasvurulari m = ctx.MuafiyetBasvurularis.FirstOrDefault(x => x.MuafBasvuruID == ID);
            return View(m);
        }

        //[HttpPost]
        //public ActionResult Guncelle(View_Basvurular bs)
        //{
        //    MuafiyetBasvurulari m= ctx.MuafiyetBasvurularis.First(x => x.MuafBasvuruID == bs.MuafBasvuruID);
        //    m.OgrenciID = bs.OgrenciID;
        //    m.KayitSekliID = bs.KayitSekliID;
        //    m.MuafIsUniID = bs.MuafIsUniID;
        //    m.MuaIstFaktID = bs.MuaIstFaktID;
        //    m.MuafIsBolumID = bs.MuafIsBolumID;

        //    ctx.SaveChanges();

        //    ViewBag.AdSoyad = bs.KullaniciAdi + " " + bs.KullaniciSoyadi;
        //    ViewBag.UniFaktBlm = bs.MuafIsUniID + " " + bs.Fakulte + " " + bs.Bolum;

        //    return RedirectToAction("Index", "DersSecimi");
        //}


        [AllowAnonymous]
        public ActionResult KmsnRaporu(int ID)
        {
            ReportViewer ReportViewer1 = new ReportViewer();
            //ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.SizeToReportContent = true;

            MuafContext ctx = new MuafContext();
            List<MuafIstDersler> rpr = ctx.MuafIstDerslers.Where(x => x.MuafBasvID == ID).OrderBy(x=>x.CokluGrupID).ToList();
           
           MuafiyetBasvurulari mm = ctx.MuafiyetBasvurularis.FirstOrDefault(x => x.MuafBasvuruID == ID);

            TempData["Denklik"] = mm.Denklik;

           Ayarlar a = null;
            if (UyeInfo.KullaniciUniID!=0)
            {
                List<DersIcerikleri> bl = ctx.DersIcerikleris.Where(x => x.AlinanUniID == UyeInfo.KullaniciUniID && x.AlinanFakulteID == UyeInfo.KullaniciFakulteID && x.AlinanBolumID == UyeInfo.KullaniciBolumID).ToList();

                a = ctx.Ayarlars.FirstOrDefault(x => x.AyarUniID == UyeInfo.KullaniciUniID && x.AyarFakulteID == UyeInfo.KullaniciFakulteID && x.AyarBolumID == UyeInfo.KullaniciBolumID);
            }
            else
            {
                List<DersIcerikleri> bl = ctx.DersIcerikleris.Where(x => x.AlinanUniID == mm.Kullanicilar.KullaniciUniID && x.AlinanFakulteID == mm.Kullanicilar.KullaniciFakulteID && x.AlinanBolumID == mm.Kullanicilar.KullaniciBolumID).ToList();

                a = ctx.Ayarlars.FirstOrDefault(x => x.AyarUniID == mm.Kullanicilar.KullaniciUniID && x.AyarFakulteID == mm.Kullanicilar.KullaniciFakulteID && x.AyarBolumID == mm.Kullanicilar.KullaniciBolumID);
            }   

            NotTiplari nt = new NotTiplari();

            decimal AKTS1 = a.AKTS1;
            decimal AKTS2 = a.AKTS2;
            decimal AKTS3 = a.AKTS3;
            decimal AKTS4 = a.AKTS14;

            decimal kredi1 = a.Kredi1;
            decimal kredi2 = a.Kredi2;
            decimal kredi3 = a.Kredi3;
            decimal kredi4 = a.Kredi4;

            decimal basariliAKTS = 0;
            decimal basariliKREDI = 0;

            //foreach (DersIcerikleri r in bl)
            //{
            //    if (r.Ders_SinifID == 1)
            //    {
            //        AKTS1 = AKTS1 + (int)r.AKTS;
            //    }
            //    else if (r.Ders_SinifID == 2)
            //    {
            //        AKTS2 = AKTS2 + (int)r.AKTS;
            //    }
            //    else if (r.Ders_SinifID == 3)
            //    {
            //        AKTS3 = AKTS3 + (int)r.AKTS;
            //    }
            //    if (r.Ders_SinifID == 4)
            //    {
            //        AKTS4 = AKTS4 + (int)r.AKTS;
            //    }
            //}


            dsKmsnRaporu ds = new dsKmsnRaporu();
            DataTable dt = ds.Tables.Add("dtKmsnRaporu");

            dt.Columns.Add("KullaniciOkulNo");
            dt.Columns.Add("KullaniciAdi");
            dt.Columns.Add("KullaniciSoyadi");
            dt.Columns.Add("BasvuruTarihi");
            dt.Columns.Add("Universite");
            dt.Columns.Add("Fakulte");
            dt.Columns.Add("Bolum");
            dt.Columns.Add("DersAdi");
            dt.Columns.Add("Kredi");          
            dt.Columns.Add("AlinanDersAdi");
            dt.Columns.Add("Kredisi");
            dt.Columns.Add("AlinanDersNotu");
            dt.Columns.Add("MuafDers");
            dt.Columns.Add("Gerekce");
            dt.Columns.Add("MevcutUni");
            dt.Columns.Add("MevcutFakt");
            dt.Columns.Add("MevcutBlm");
            dt.Columns.Add("MuafKredi");
            dt.Columns.Add("AKTS");
            dt.Columns.Add("AlinanAKTS");
            dt.Columns.Add("DonusmusNot");
            dt.Columns.Add("MuafAKTS");        
            dt.Columns.Add("DersKodu");
            dt.Columns.Add("AlinanDersKodu");
            dt.Columns.Add("KayitSekliID");

            string faklte1 = "";
            string bolm1 = "";
            string faklte = "";
            string bolm = "";

            foreach (MuafIstDersler r in rpr)
            {
                foreach (Fakulteler ff in Data.Fakulte)
                {
                    if (ff.FakulteID== r.DersIcerikleri1.AlinanFakulteID)
                    {
                        faklte1 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ff.Fakulte.ToLower());
                        break;
                    }
                }
                foreach (Bolumler bb in Data.Bolum)
                {
                    if (bb.BolumID == r.DersIcerikleri1.AlinanBolumID)
                    {
                        bolm1 = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bb.Bolum.ToLower());
                        break;
                    }
                }
                foreach (Fakulteler ff in Data.Fakulte)
                {
                    if (ff.FakulteID == r.DersIcerikleri.AlinanFakulteID)
                    {
                        faklte = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ff.Fakulte.ToLower());
                        break;
                    }
                }
                foreach (Bolumler bb in Data.Bolum)
                {
                    if (bb.BolumID == r.DersIcerikleri.AlinanBolumID)
                    {
                        bolm = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(bb.Bolum.ToLower());
                        break;
                    }
                }              
                break;
            }

            int sayac = 0;

            List<MuafIstDersler> listGruplu = null; int grupNo = -1;
            foreach (MuafIstDersler r in rpr)
            {
                if (grupNo != r.CokluGrupID)
                {
                    DataRow myRow = dt.NewRow();
                    dt.Rows.Add(myRow);
                    dt.Rows[sayac][0] = r.MuafiyetBasvurulari.Kullanicilar.KullaniciOkulNo;
                    dt.Rows[sayac][1] = r.MuafiyetBasvurulari.Kullanicilar.KullaniciAdi;
                    dt.Rows[sayac][2] = r.MuafiyetBasvurulari.Kullanicilar.KullaniciSoyadi;
                    dt.Rows[sayac][3] = r.MuafiyetBasvurulari.BasvuruTarihi.ToShortDateString();
                    dt.Rows[sayac][4] = r.DersIcerikleri1.Universiteler.Universite;
                    dt.Rows[sayac][5] = faklte1;
                    dt.Rows[sayac][6] = bolm1;
                    dt.Rows[sayac][7] = r.DersIcerikleri.AlinanDersAdi;
                    dt.Rows[sayac][8] = r.DersIcerikleri.AlinanKredisi;
                    dt.Rows[sayac][22] = r.DersIcerikleri.AlinanDersKodu;                    
                    dt.Rows[sayac][24] = r.MuafiyetBasvurulari.KayitSekliID;

                    if (r.CokluGrupID > 0)
                    {
                        string dersAdi = "";
                        string kredisi = "";
                        string dersNotu = "";
                        string dersNotuDonusmus = "";
                        double notKatsayi = 0;
                        double topNotKatsayi = 0;
                        string aktss = "";
                        string dersKodu = "";

                        listGruplu = ctx.MuafIstDerslers.Where(x => x.CokluGrupID == r.CokluGrupID).ToList();
                        Notlar n = new Notlar(mm.NotTipiID);

                        foreach (MuafIstDersler item in listGruplu)
                        {
                            dersAdi = dersAdi + item.DersIcerikleri1.AlinanDersAdi.ToString() + "\n";
                            kredisi = kredisi + item.DersIcerikleri1.AlinanKredisi.ToString() + "\n";                  
                            aktss = aktss + item.DersIcerikleri1.AlinanAKTS + "\n";
                            dersNotu = dersNotu + item.AlinanDersNotu + "\n";
                            dersKodu = dersKodu + item.DersIcerikleri1.AlinanDersKodu.ToString() + "\n";

                            for (int i = 0; i < n.Not.Length; i++)
                            {
                                if (r.AlinanDersNotu==n.Not[i])
                                {
                                    notKatsayi = n.NotKatsayiUst[i];
                                    break;
                                }
                            }
                            topNotKatsayi = topNotKatsayi + notKatsayi;                        
                        }                       

                        dt.Rows[sayac][9] = dersAdi;
                        dt.Rows[sayac][10] = kredisi;
                        dt.Rows[sayac][11] = dersNotu;
                        dt.Rows[sayac][19] = aktss;
                        dt.Rows[sayac][23] = dersKodu;

                        if (mm.Denklik == 0)  dersNotuDonusmus = nt.CokluNotDonustur(a.NotTipiID, mm.NotTipiID, topNotKatsayi, listGruplu.Count);
                        dt.Rows[sayac][20] = dersNotuDonusmus;
                        dt.Rows[sayac][18] = r.DersIcerikleri.AlinanAKTS;

                        grupNo = (int)r.CokluGrupID;
                    }
                    else
                    {
                        dt.Rows[sayac][9] = r.DersIcerikleri1.AlinanDersAdi;
                        dt.Rows[sayac][10] = r.DersIcerikleri1.AlinanKredisi;
                        dt.Rows[sayac][11] = r.AlinanDersNotu;
                        dt.Rows[sayac][18] = r.DersIcerikleri.AlinanAKTS;
                        dt.Rows[sayac][19] = r.DersIcerikleri1.AlinanAKTS;
                        dt.Rows[sayac][23] = r.DersIcerikleri1.AlinanDersKodu;

                        if (a.NotTipiID == mm.NotTipiID)//mevcut not sis --> alinan ders üni not sis
                        {
                            dt.Rows[sayac][20] = r.AlinanDersNotu;
                        }
                        else
                        {
                            if (mm.Denklik == 0)  dt.Rows[sayac][20] = nt.NotDonustur(a.NotTipiID, mm.NotTipiID, r.AlinanDersNotu);
                        }
                    }

                    if (r.DanismanUygunluk == 0 || r.KrediUygunluk == 0 || r.NotUygunluk == 0 || r.IcerikUygunluk == 0)
                    {
                        dt.Rows[sayac][12] = "";
                        dt.Rows[sayac][13] = "";
                        dt.Rows[sayac][17] = "0";
                        dt.Rows[sayac][21] = "0";
                    }
                    else
                    {
                        if (r.KrediUygunluk == -1 || r.NotUygunluk == -1 || r.IcerikUygunluk == -1 || r.DanismanUygunluk == -1)
                        {
                            dt.Rows[sayac][12] = "";
                            dt.Rows[sayac][17] = "0";
                            dt.Rows[sayac][21] = "0";

                            string durum = "";
                            if (r.KrediUygunluk == -1)
                            {
                                durum = "Kredi yetersiz";
                            }
                            if (r.NotUygunluk == -1)
                            {
                                durum = durum + "\nNotu yetersiz";
                            }
                            if (r.IcerikUygunluk == -1)
                            {
                                durum = durum + "\nİçerik uygun değil";
                            }
                            if (r.DanismanUygunluk == -1)
                            {
                                durum = durum + "\nDanışmanca uygun değil";
                            }

                            dt.Rows[sayac][13] = durum;
                        }
                        else
                        {
                            basariliAKTS = basariliAKTS + r.DersIcerikleri.AlinanAKTS;
                            basariliKREDI = basariliKREDI + r.DersIcerikleri.AlinanKredisi;

                            dt.Rows[sayac][12] = r.DersIcerikleri.AlinanDersAdi;
                            dt.Rows[sayac][13] = "";
                            dt.Rows[sayac][17] = r.DersIcerikleri.AlinanKredisi;
                            dt.Rows[sayac][21] = r.DersIcerikleri.AlinanAKTS;
                        }
                    }
                    dt.Rows[sayac][14] = r.DersIcerikleri.Universiteler.Universite;

                    dt.Rows[sayac][15] = faklte;
                    dt.Rows[sayac][16] = bolm;

                    sayac = sayac + 1;
                 
                }              
            }

            string intibakSinifi = "";
            if (a.AKTS_Kredi==2)
            {
                if ((double)basariliAKTS * 0.7 < (double)AKTS2)// Dinamik olmalı !!!!!!!!!!!!!!!!!!!!!
                {
                    intibakSinifi = "1.Sınıf";
                }
                else if ((double)AKTS2  < (double)basariliAKTS * 0.7 && (double)basariliAKTS * 0.7 <= (double)(AKTS3 + AKTS2) )
                {
                    intibakSinifi = "2.Sınıf";
                }
                else if ((double)(AKTS3 + AKTS2)  < (double)basariliAKTS * 0.7 && (double)basariliAKTS * 0.7 <= (double)(AKTS4 + AKTS2 + AKTS3))
                {
                    intibakSinifi = "3.Sınıf";
                }
                else if ((double)(AKTS4 + AKTS2 + AKTS3)  < (double)basariliAKTS * 0.7)
                {
                    intibakSinifi = "4.Sınıf";
                }
            }
            else
            {
                if (basariliKREDI < kredi1 / 2)// %50 Dinamik olmalı !!!!!!!!!!!!!!!!!!!!!
                {
                    intibakSinifi = "1.Sınıf";
                }
                else if (kredi1 / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2) / 2)
                {
                    intibakSinifi = "2.Sınıf";
                }
                else if ((kredi1 + kredi2) / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2 + kredi3) / 2)
                {
                    intibakSinifi = "3.Sınıf";
                }
                else if ((kredi1 + kredi2 + kredi3) / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2 + kredi3 + kredi4) / 2)
                {
                    intibakSinifi = "4.Sınıf";
                }
            }

           List<Kullanicilar> kl = ctx.Kullanicilars.Where(x => x.KullaniciUniID == UyeInfo.KullaniciUniID && x.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.KullaniciBolumID == UyeInfo.KullaniciBolumID && x.KullaniciRolID < 4).ToList();

            string kmsnBaskani= "Belirlenmemiş";
            string kmsn1Uye = "Belirlenmemiş";
            string kmsn2Uye = "Belirlenmemiş";
          
            Unvanlar uv = new Unvanlar();
           
            foreach (Kullanicilar item in kl)
            {
                if (item.KullaniciRolID==1)
                {
                    string unvan = "";

                    for (int i = 0; i < uv.unvanAdi.Length; i++)
                    {
                        if (uv.unvanID[i]==item.KullaniciUnvanID)
                        {
                            unvan = uv.unvanAdi[i];
                            break;
                        }
                    }
                    kmsnBaskani = unvan+" "+item.KullaniciAdi + " " + item.KullaniciSoyadi;
                }
                else if(item.KullaniciRolID == 2)
                {
                    string unvan = "";

                    for (int i = 0; i < uv.unvanAdi.Length; i++)
                    {
                        if (uv.unvanID[i] == item.KullaniciUnvanID)
                        {
                            unvan = uv.unvanAdi[i];
                            break;
                        }
                    }

                    kmsn1Uye = unvan+" "+item.KullaniciAdi + " " + item.KullaniciSoyadi;
                }
                else if (item.KullaniciRolID == 3)
                {
                    string unvan = "";

                    for (int i = 0; i < uv.unvanAdi.Length; i++)
                    {
                        if (uv.unvanID[i] == item.KullaniciUnvanID)
                        {
                            unvan = uv.unvanAdi[i];
                            break;
                        }
                    }

                    kmsn2Uye = unvan + " " + item.KullaniciAdi + " " + item.KullaniciSoyadi;
                }
            }
           

            ReportDataSource rds = new ReportDataSource("dsKmsnRaporu", ds.Tables[0]); //
            if(mm.Denklik==0) ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Reports/KmsnRaporu-BTU.rdlc");
            else ReportViewer1.LocalReport.ReportPath = Server.MapPath("/Reports/KmsnRaporuDenklik.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.Refresh();

            ReportParameter[] parms = new ReportParameter[4];
            parms[0] = new ReportParameter("Intibak", intibakSinifi);
            parms[1] = new ReportParameter("KmsnBaskani", kmsnBaskani);
            parms[2] = new ReportParameter("KmsnUye1", kmsn1Uye);
            parms[3] = new ReportParameter("KmsnUye2", kmsn2Uye);
      
            ReportViewer1.LocalReport.SetParameters(parms);
            ReportViewer1.LocalReport.Refresh();

            ViewBag.Report = ReportViewer1;

           
            return View();
        }

    }
}