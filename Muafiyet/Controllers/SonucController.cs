using Muafiyet.App_Classes;
using Muafiyet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Muafiyet.Controllers
{
    public class SonucController : Controller
    {
        // GET: Sonuc
        MuafContext ctx = new MuafContext();
        public ActionResult TopluSonuclar(IcerikKararlari tps)
        {
            Data.Universite = ctx.Universitelers.ToList();
            Data.Fakulte = ctx.Fakultelers.ToList();
            Data.Bolum = ctx.Bolumlers.ToList();

            List<Universiteler> uni = ctx.Universitelers.ToList();            

            ViewBag.Universiteler = uni;         
            ViewBag.Sonuc = "";
           
                List<IcerikKararlari> ts = null;
            if (tps.KararUniID == 0)
            {
                ts = ctx.IcerikKararlaris.Where(x => x.KararUniID == 0 && x.KararFakulteID == 0 && x.KararBolumID == 0).ToList();

                ViewBag.Sonuc = "";
            }
            else
            {
                ts = ctx.IcerikKararlaris.Where(x => x.KararUniID == tps.KararUniID && x.KararFakulteID == tps.KararFakulteID && x.KararBolumID == tps.KararBolumID).ToList();

                Universiteler u = ctx.Universitelers.FirstOrDefault(x => x.UniversiteID == tps.KararUniID);
                ViewBag.MevcutUni = u.Universite.ToString().Split(new[] { " ÜNİVERSİTESİ" }, StringSplitOptions.None)[0];

                if (ts.Count == 0) ViewBag.Sonuc = "KayitYok";
                else ViewBag.Sonuc = "KayitVar";
            }
                return View(ts);               
        }

        public ActionResult TopluSonuclarTarih(int? UniID, int? FakulteID, string tarih)
        {
            Data.Universite = ctx.Universitelers.ToList();
            Data.Fakulte = ctx.Fakultelers.ToList();
            Data.Bolum = ctx.Bolumlers.ToList();
            DataTable dt = new DataTable();
            int uniID = 0;
            int fakulteiID = 0;

            List<Universiteler> uni = ctx.Universitelers.ToList();
            ViewBag.Universiteler = uni;

            ViewBag.Durum = "İlkAçış";

            if (UniID != null) uniID =Convert.ToInt32(UniID);
            if (FakulteID != null) fakulteiID = Convert.ToInt32(FakulteID);

            Tablo(dt, uniID, fakulteiID, tarih);

            return View(dt);
        }

        public ActionResult TopluSonuclarTarihGoster()//
        {
            Data.Universite = ctx.Universitelers.ToList();
            Data.Fakulte = ctx.Fakultelers.ToList();
            Data.Bolum = ctx.Bolumlers.ToList();
            DateTime bsTarihi=DateTime.Now;
            bsTarihi = bsTarihi.AddDays(-2);
            DataTable dt = new DataTable();

            int UniID = 24;
            int FakulteID = 75;
           

            if (UniID != 0 && FakulteID != 0)
            {
               // DateTime bsTarihi = Convert.ToDateTime(tarih);

                List<MuafIstDersler> rpr = ctx.MuafIstDerslers.Where(x =>x.MuafiyetBasvurulari.BasvuruTarihi >= bsTarihi &&  x.MuafiyetBasvurulari.Kullanicilar.KullaniciUniID == UniID && x.MuafiyetBasvurulari.Kullanicilar.KullaniciFakulteID == FakulteID).OrderBy(x => x.CokluGrupID).ToList();

                Ayarlar a = null;

                List<DersIcerikleri> bl = ctx.DersIcerikleris.Where(x => x.AlinanUniID == UyeInfo.KullaniciUniID && x.AlinanFakulteID == UyeInfo.KullaniciFakulteID && x.AlinanBolumID == UyeInfo.KullaniciBolumID).ToList();
                

                NotTiplari nt = new NotTiplari();

                //a = ctx.Ayarlars.FirstOrDefault(x => x.AyarUniID == UyeInfo.KullaniciUniID && x.AyarFakulteID == UyeInfo.KullaniciFakulteID && x.AyarBolumID == UyeInfo.KullaniciBolumID);

                //decimal AKTS1 = a.AKTS1;
                //decimal AKTS2 = a.AKTS2;
                //decimal AKTS3 = a.AKTS3;
                //decimal AKTS4 = a.AKTS14;

                //decimal kredi1 = a.Kredi1;
                //decimal kredi2 = a.Kredi2;
                //decimal kredi3 = a.Kredi3;
                //decimal kredi4 = a.Kredi4;

                decimal basariliAKTS = 0;
                decimal basariliKREDI = 0;


                

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

                string faklte1 = "";
                string bolm1 = "";
                string faklte = "";
                string bolm = "";

                foreach (MuafIstDersler r in rpr)
                {
                    foreach (Fakulteler ff in Data.Fakulte)
                    {
                        if (ff.FakulteID == r.DersIcerikleri1.AlinanFakulteID)
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

                        if (r.CokluGrupID > 0)
                        {
                            string dersAdi = "";
                            string kredisi = "";
                            string dersNotu = "";
                            string dersNotuDonusmus = "";
                            double notKatsayi = 0;
                            double topNotKatsayi = 0;
                            string aktss = "";

                            listGruplu = ctx.MuafIstDerslers.Where(x => x.CokluGrupID == r.CokluGrupID).ToList();
                            Notlar n = new Notlar(r.MuafiyetBasvurulari.NotTipiID);

                            foreach (MuafIstDersler item in listGruplu)
                            {
                                dersAdi = dersAdi + item.DersIcerikleri1.AlinanDersAdi.ToString() + "\n";
                                kredisi = kredisi + item.DersIcerikleri1.AlinanKredisi.ToString() + "\n";
                                aktss = aktss + item.DersIcerikleri1.AlinanAKTS + "\n";
                                dersNotu = dersNotu + item.AlinanDersNotu + "\n";

                                for (int i = 0; i < n.Not.Length; i++)
                                {
                                    if (r.AlinanDersNotu == n.Not[i])
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

                            if (r.MuafiyetBasvurulari.Denklik == 0) dersNotuDonusmus = nt.CokluNotDonustur(1, r.MuafiyetBasvurulari.NotTipiID, topNotKatsayi, listGruplu.Count);
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

                            if (1 == r.MuafiyetBasvurulari.NotTipiID)//mevcut not sis --> alinan ders üni not sis
                            {
                                dt.Rows[sayac][20] = r.AlinanDersNotu;
                            }
                            else
                            {
                                if (r.MuafiyetBasvurulari.Denklik == 0) dt.Rows[sayac][20] = nt.NotDonustur(1, r.MuafiyetBasvurulari.NotTipiID, r.AlinanDersNotu);
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

                //string intibakSinifi = "";
                //if (a.AKTS_Kredi == 1)
                //{
                //    if (basariliAKTS < AKTS1 / 2)
                //    {
                //        intibakSinifi = "1.Sınıf";
                //    }
                //    else if (AKTS1 / 2 < basariliAKTS && basariliAKTS <= (AKTS1 + AKTS2) / 2)
                //    {
                //        intibakSinifi = "2.Sınıf";
                //    }
                //    else if ((AKTS1 + AKTS2) / 2 < basariliAKTS && basariliAKTS <= (AKTS1 + AKTS2 + AKTS3) / 2)
                //    {
                //        intibakSinifi = "3.Sınıf";
                //    }
                //    else if ((AKTS1 + AKTS2 + AKTS3) / 2 < basariliAKTS && basariliAKTS <= (AKTS1 + AKTS2 + AKTS3 + AKTS4) / 2)
                //    {
                //        intibakSinifi = "4.Sınıf";
                //    }
                //}
                //else
                //{
                //    if (basariliKREDI < kredi1 / 2)
                //    {
                //        intibakSinifi = "1.Sınıf";
                //    }
                //    else if (kredi1 / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2) / 2)
                //    {
                //        intibakSinifi = "2.Sınıf";
                //    }
                //    else if ((kredi1 + kredi2) / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2 + kredi3) / 2)
                //    {
                //        intibakSinifi = "3.Sınıf";
                //    }
                //    else if ((kredi1 + kredi2 + kredi3) / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2 + kredi3 + kredi4) / 2)
                //    {
                //        intibakSinifi = "4.Sınıf";
                //    }
                //}
                ViewData["Data"] = dt;
            }
            ViewBag.Durum = "SorguAçış";
            return RedirectToAction("TopluSonuclarTarih", new { data = dt });
        }

        public ActionResult TopluSonuclarGoster(IcerikKararlari tps)
        {
            List<Universiteler> uni = ctx.Universitelers.ToList();      

            ViewBag.Universiteler = uni;        

            List<IcerikKararlari> ts = null;
            if (tps.KararUniID == 0)
            {
                ts = ctx.IcerikKararlaris.Where(x => x.KararUniID == 0 && x.KararFakulteID == 0 && x.KararBolumID == 0).ToList();
            }
            else
            {
                ts = ctx.IcerikKararlaris.Where(x => x.KararUniID == tps.KararUniID && x.KararFakulteID == tps.KararFakulteID && x.KararBolumID == tps.KararBolumID).ToList();
            }

            if (ts.Count==0) ViewBag.Sonuc = "KayitYok";
            else ViewBag.Sonuc = "KayitVar";

            return RedirectToAction("TopluSonuclar", new { data = ts} );
            //string jsonString = JsonConvert.SerializeObject(ts, Formatting.None, new JsonSerializerSettings()
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            //return  jsonString;            
        }

        private void Tablo(DataTable dt, int UniID, int FakulteID, string tarih)
        {          
            Data.Universite = ctx.Universitelers.ToList();
            Data.Fakulte = ctx.Fakultelers.ToList();
            Data.Bolum = ctx.Bolumlers.ToList();
           
            //tarih = String.Format("{0:u}", Convert.ToDateTime(tarih));
            DateTime bsTarihi = DateTime.Now;
            //bsTarihi = bsTarihi.AddDays(-2);

            if (tarih == null) tarih = DateTime.Now.ToShortDateString();
            bsTarihi = DateTime.Parse(tarih);
            int day = bsTarihi.Date.Month;
            int month= bsTarihi.Date.Day;
            int year= bsTarihi.Date.Year;

            //int UniID = 24;
            //int FakulteID = 75;

            if (UniID != 0 && FakulteID != 0)
            {
                // DateTime bsTarihi = Convert.ToDateTime(tarih);

                List<MuafIstDersler> rpr = ctx.MuafIstDerslers.Where(x => x.MuafiyetBasvurulari.BasvuruTarihi.Day >=day && x.MuafiyetBasvurulari.BasvuruTarihi.Month >= month && x.MuafiyetBasvurulari.BasvuruTarihi.Year >= year && x.MuafiyetBasvurulari.Kullanicilar.KullaniciUniID == UniID && x.MuafiyetBasvurulari.Kullanicilar.KullaniciFakulteID == FakulteID).OrderBy(x => x.CokluGrupID).ToList();                

                Ayarlar a = null;

                List<DersIcerikleri> bl = ctx.DersIcerikleris.Where(x => x.AlinanUniID == UyeInfo.KullaniciUniID && x.AlinanFakulteID == UyeInfo.KullaniciFakulteID && x.AlinanBolumID == UyeInfo.KullaniciBolumID).ToList();


                NotTiplari nt = new NotTiplari();

                //a = ctx.Ayarlars.FirstOrDefault(x => x.AyarUniID == UyeInfo.KullaniciUniID && x.AyarFakulteID == UyeInfo.KullaniciFakulteID && x.AyarBolumID == UyeInfo.KullaniciBolumID);

                //decimal AKTS1 = a.AKTS1;
                //decimal AKTS2 = a.AKTS2;
                //decimal AKTS3 = a.AKTS3;
                //decimal AKTS4 = a.AKTS14;

                //decimal kredi1 = a.Kredi1;
                //decimal kredi2 = a.Kredi2;
                //decimal kredi3 = a.Kredi3;
                //decimal kredi4 = a.Kredi4;

                decimal basariliAKTS = 0;
                decimal basariliKREDI = 0;

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

                string faklte1 = "";
                string bolm1 = "";
                string faklte = "";
                string bolm = "";

                foreach (MuafIstDersler r in rpr)
                {
                    foreach (Fakulteler ff in Data.Fakulte)
                    {
                        if (ff.FakulteID == r.DersIcerikleri1.AlinanFakulteID)
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

                        if (r.CokluGrupID > 0)
                        {
                            string dersAdi = "";
                            string kredisi = "";
                            string dersNotu = "";
                            string dersNotuDonusmus = "";
                            double notKatsayi = 0;
                            double topNotKatsayi = 0;
                            string aktss = "";

                            listGruplu = ctx.MuafIstDerslers.Where(x => x.CokluGrupID == r.CokluGrupID).ToList();
                            Notlar n = new Notlar(r.MuafiyetBasvurulari.NotTipiID);

                            foreach (MuafIstDersler item in listGruplu)
                            {
                                dersAdi = dersAdi + item.DersIcerikleri1.AlinanDersAdi.ToString() + "\n";
                                kredisi = kredisi + item.DersIcerikleri1.AlinanKredisi.ToString() + "\n";
                                aktss = aktss + item.DersIcerikleri1.AlinanAKTS + "\n";
                                dersNotu = dersNotu + item.AlinanDersNotu + "\n";

                                for (int i = 0; i < n.Not.Length; i++)
                                {
                                    if (r.AlinanDersNotu == n.Not[i])
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

                            if (r.MuafiyetBasvurulari.Denklik == 0) dersNotuDonusmus = nt.CokluNotDonustur(1, r.MuafiyetBasvurulari.NotTipiID, topNotKatsayi, listGruplu.Count);
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

                            if (1 == r.MuafiyetBasvurulari.NotTipiID)//mevcut not sis --> alinan ders üni not sis
                            {
                                dt.Rows[sayac][20] = r.AlinanDersNotu;
                            }
                            else
                            {
                                if (r.MuafiyetBasvurulari.Denklik == 0) dt.Rows[sayac][20] = nt.NotDonustur(1, r.MuafiyetBasvurulari.NotTipiID, r.AlinanDersNotu);
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

                //string intibakSinifi = "";
                //if (a.AKTS_Kredi == 1)
                //{
                //    if (basariliAKTS < AKTS1 / 2)
                //    {
                //        intibakSinifi = "1.Sınıf";
                //    }
                //    else if (AKTS1 / 2 < basariliAKTS && basariliAKTS <= (AKTS1 + AKTS2) / 2)
                //    {
                //        intibakSinifi = "2.Sınıf";
                //    }
                //    else if ((AKTS1 + AKTS2) / 2 < basariliAKTS && basariliAKTS <= (AKTS1 + AKTS2 + AKTS3) / 2)
                //    {
                //        intibakSinifi = "3.Sınıf";
                //    }
                //    else if ((AKTS1 + AKTS2 + AKTS3) / 2 < basariliAKTS && basariliAKTS <= (AKTS1 + AKTS2 + AKTS3 + AKTS4) / 2)
                //    {
                //        intibakSinifi = "4.Sınıf";
                //    }
                //}
                //else
                //{
                //    if (basariliKREDI < kredi1 / 2)
                //    {
                //        intibakSinifi = "1.Sınıf";
                //    }
                //    else if (kredi1 / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2) / 2)
                //    {
                //        intibakSinifi = "2.Sınıf";
                //    }
                //    else if ((kredi1 + kredi2) / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2 + kredi3) / 2)
                //    {
                //        intibakSinifi = "3.Sınıf";
                //    }
                //    else if ((kredi1 + kredi2 + kredi3) / 2 < basariliKREDI && basariliKREDI <= (kredi1 + kredi2 + kredi3 + kredi4) / 2)
                //    {
                //        intibakSinifi = "4.Sınıf";
                //    }
                //}              
            }
    }

    public ActionResult Sonuclar(string ogrNo)
        {
            Data.Universite = ctx.Universitelers.ToList();
            Data.Fakulte = ctx.Fakultelers.ToList();
            Data.Bolum = ctx.Bolumlers.ToList();

            List<Universiteler> uni = ctx.Universitelers.ToList();

            ViewBag.Universiteler = uni;
            ViewBag.Sonuc = "";
            List<MuafiyetBasvurulari> bs = null;

            if (ogrNo != null)
            {
                bs = ctx.MuafiyetBasvurularis.Where(x => x.Kullanicilar.KullaniciOkulNo == ogrNo).ToList();

                if (bs.Count == 0)
                {
                    ViewBag.Sonuc = "KayitYok";
                }
                else
                {
                    List<MuafIstDersler> mf = null;
                    foreach (MuafiyetBasvurulari b in bs)
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
                    ViewBag.Sonuc = "KayitVar";
                }
            }                     

            return View(bs);
        }
    }
}