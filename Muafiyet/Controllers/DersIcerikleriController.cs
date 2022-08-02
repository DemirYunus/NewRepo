using Muafiyet.App_Classes;
using Muafiyet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;


namespace Muafiyet.Controllers
{
    [Authorize]
    public class DersIcerikleriController : Controller
    {
        // GET: DersIcerikleri
        MuafContext ctx = new MuafContext();
        string durum = "";
      
        public ActionResult Index(int? filtreID)
        {
            List<DersIcerikleri> di = null;
            if (filtreID == -1)//Bölüm
            {             
                    di = ctx.DersIcerikleris.Where(x => x.AlinanBolumID == UyeInfo.KullaniciBolumID).OrderBy(x => x.AlinanFakulteID).OrderBy(x => x.AlinanBolumID).ToList();           
            }
            else if (filtreID == -2)//Fakülte
            {
                di = ctx.DersIcerikleris.Where(x => x.AlinanFakulteID == UyeInfo.KullaniciFakulteID).OrderBy(x => x.AlinanFakulteID).OrderBy(x => x.AlinanBolumID).ToList();
            }
            else if (filtreID == -3)//Üniversite
            {
                di = ctx.DersIcerikleris.Where(x => x.AlinanUniID == UyeInfo.KullaniciUniID).OrderBy(x => x.AlinanFakulteID).OrderBy(x => x.AlinanBolumID).ToList();
            }
            else if (filtreID == 0)//Tümü
            {
                di = ctx.DersIcerikleris.OrderBy(x => x.AlinanFakulteID).OrderBy(x => x.AlinanBolumID).ToList();
            }
            else
            {
                di = ctx.DersIcerikleris.Where(x => x.AlinanUniID == UyeInfo.KullaniciUniID && x.AlinanFakulteID == UyeInfo.KullaniciFakulteID && x.AlinanBolumID == UyeInfo.KullaniciBolumID).OrderBy(x => x.AlinanFakulteID).OrderBy(x => x.AlinanBolumID).ToList();
            }
            //List<DersIcerikleri> di = ctx.DersIcerikleris.ToList();//Ekleyen uni fakt bkm bilgileri olmalı

            List<Universiteler> uni = ctx.Universitelers.ToList();
            ViewBag.Universiteler = uni;

            return View(di);
        }

        public ActionResult IndexFiltre(int? filtreID)
        {
            List<DersIcerikleri> di = null;
            if (filtreID==-1)//Bölüm
            {
                di = ctx.DersIcerikleris.Where(x => x.AlinanBolumID == UyeInfo.KullaniciBolumID).ToList();
            }
            else if (filtreID == -2)//Fakülte
            {
                di = ctx.DersIcerikleris.Where(x => x.AlinanFakulteID == UyeInfo.KullaniciFakulteID).ToList(); 
            }
            else if (filtreID == -3)
            {
                di = ctx.DersIcerikleris.Where(x => x.AlinanUniID == UyeInfo.KullaniciUniID).ToList();
            }
            else
            {
                di = ctx.DersIcerikleris.ToList();
            }

            List<Universiteler> uni = ctx.Universitelers.ToList();
            ViewBag.Universiteler = uni;
            
            return View(di);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(DersIcerikleri di, HttpPostedFileBase file)
        {
            try
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

                            TempData["Durum"] = "Ekle";
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
                    TempData["Durum"] = "Ekle";
                    TempData["Text"] = di.AlinanDersAdi.ToString();
                }

                di.AlinanDersKodu = di.AlinanDersKodu.ToUpper();
                di.AlinanDersAdi = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(di.AlinanDersAdi.ToLower());

                ctx.DersIcerikleris.Add(di);
                ctx.SaveChanges();

                return RedirectToAction("Index");
                //return RedirectToAction("Index","DersSecimi", new { ID = MuafBasvuruID });// mf.MuafBasvID
            }
            catch (Exception)
            {

                return RedirectToAction("UygunOlmayanFormat", "Hata");
            }
           
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guncelle(DersIcerikleri di, HttpPostedFileBase file)
        {
            try
            {
   //if (ModelState.IsValid)
            //{
            DersIcerikleri d = ctx.DersIcerikleris.First(x => x.AlinanDersID == di.AlinanDersID);
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

                        TempData["Durum"] = "Güncelle";
                        TempData["Text"] = di.AlinanDersAdi.ToString();

                        if (d.IcerikResimYolu!=null && d.IcerikResimYolu != "")
                        {
                            string eskiDosyaAdi = d.IcerikResimYolu.ToString();
                            System.IO.File.Delete(Path.Combine(Server.MapPath("~/images"), eskiDosyaAdi));
                        }

                        d.IcerikResimYolu = yeniDosyaAdi;
                    }
                    else
                    {
                        TempData["Durum"] = "Hata-Guncelle";
                        TempData["Text"] = di.AlinanDersAdi.ToString();
                    }
                }
            }
            else
            {
                TempData["Durum"] = "Güncelle";
                TempData["Text"] = di.AlinanDersAdi.ToString();
            }

           

            d.AlinanUniID = di.AlinanUniID;
            d.AlinanFakulteID = di.AlinanFakulteID;
            d.AlinanBolumID = di.AlinanBolumID;
            d.AlinanDersAdi = di.AlinanDersAdi;
            d.AlinanDersKodu = di.AlinanDersKodu;
            d.AlinanKredisi = di.AlinanKredisi;
            d.IcerikLinki = di.IcerikLinki;           
            d.IcerikNotu = di.IcerikNotu;
            d.AlinanAKTS = di.AlinanAKTS;

            //TempData["Durum"] = "Güncelle";
            //TempData["Text"] = di.AlinanDersAdi.ToString();

            ctx.SaveChanges();
            return RedirectToAction("Index");
            //return "başarılı";
            //}
            //else
            //{
            //    ViewBag.Hata = "Kaydedilemedi";
            //    //return View();
            //    return "hata";
            //}
            }
            catch (Exception)
            {

                return RedirectToAction("UygunOlmayanFormat", "Hata");
            }         

        }

        public string Sil(int DersID)
        {
            DersIcerikleri bl = ctx.DersIcerikleris.FirstOrDefault(x => x.AlinanDersID == DersID);
            ctx.DersIcerikleris.Remove(bl);

            List<MuafIstDersler> drs = ctx.MuafIstDerslers.Where(x => x.MuafIstDersID == DersID || x.AlinanDersID == DersID).ToList();

            string sonuc = "";
            if (drs.Count==0)
            {
                try
                {
                    ctx.SaveChanges();
                    //return JavaScript(String.Format("Toast({0})", "basarili"));
                    TempData["Durum"] = "Sil";
                    TempData["Text"] = bl.AlinanDersAdi.ToString();
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
                sonuc = "silinemez";
            }

            return sonuc; ;
        }

        public ActionResult GetirIcerikDegelendirme(int? filtreID)
        {
            List<Universiteler> uni = ctx.Universitelers.ToList();
            ViewBag.Universiteler = uni;         


            List<IcerikKararlari> ic=null;

            if (UyeInfo.Rol == "Komisyon Başkanı")
            {
                if (filtreID == 0)//Tümü
                {
                    ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararBolumID == UyeInfo.KullaniciBolumID).OrderBy(x => x.CokluGrupID).ToList();
                }
                else //Değerlendirilmeyenler
                {
                    ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararUye1 == 0).OrderBy(x => x.CokluGrupID).ToList();
                }
            }
            else if (UyeInfo.Rol == "Komisyon 1.Üye")
            {
                if (filtreID == 0)//Tümü
                {
                    ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararBolumID == UyeInfo.KullaniciBolumID).OrderBy(x => x.CokluGrupID).ToList();
                }
                else //Değerlendirilmeyenler
                {
                    ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararUye2 == 0).OrderBy(x => x.CokluGrupID).ToList();
                }
            }
            else if (UyeInfo.Rol == "Komisyon 2.Üye")
            {
                if (filtreID == 0)//Tümü
                {
                    ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararBolumID == UyeInfo.KullaniciBolumID).OrderBy(x => x.CokluGrupID).ToList();
                }
                else //Değerlendirilmeyenler
                {
                    
                    ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararUye3 == 0).OrderBy(x => x.CokluGrupID).ToList();
                }
            }
            if (ic.Count!=0)
            {
                ViewBag.MevcutUni = ic[0].Universiteler.Universite.ToString().Split(new[] { " ÜNİVERSİTESİ" }, StringSplitOptions.None)[0];

                ViewBag.KarsiUni = ic[0].DersIcerikleri1.Universiteler.Universite.ToString().Split(new[] { " ÜNİVERSİTESİ" }, StringSplitOptions.None)[0];
            }
                return View(ic);
            
        }

        public void Uygun(int ID)
        {           
            IcerikKararlari ic = ctx.IcerikKararlaris.FirstOrDefault(x => x.ID == ID);

            if (ic.CokluGrupID>0)
            {
                List<IcerikKararlari> ii = ctx.IcerikKararlaris.Where(x => x.CokluGrupID == ic.CokluGrupID).ToList();
                foreach (IcerikKararlari item in ii)
                {
                    if (UyeInfo.Rol == "Komisyon Başkanı") item.KararUye1 = 1;
                    else if (UyeInfo.Rol == "Komisyon 1.Üye") item.KararUye2 = 1;
                    else if (UyeInfo.Rol == "Komisyon 2.Üye") item.KararUye3 = 1;
                    ctx.SaveChanges();
                }
            }
            else
            {
                if (UyeInfo.Rol == "Komisyon Başkanı") ic.KararUye1 = 1;
                else if (UyeInfo.Rol == "Komisyon 1.Üye") ic.KararUye2 = 1;
                else if (UyeInfo.Rol == "Komisyon 2.Üye") ic.KararUye3 = 1;
                ctx.SaveChanges();
            }         

            #region İçerik karara bağlandıktan sonra muaf istenen derslere bağlanıyor

            IcerikKararlari i = ctx.IcerikKararlaris.FirstOrDefault(x => x.ID == ID);

            int nihaiKarar = 0;
           
                if (i.KararUye1 == 0 || i.KararUye2 == 0 || i.KararUye3 == 0)
                {
                    nihaiKarar = 0; //Değerlendirilmemiş    
                }
                else
                {
                    if (i.KararUye1 + i.KararUye2 + i.KararUye3 > 1)
                    {
                        nihaiKarar = 1; //Uygun
                    }
                    else
                    {
                        nihaiKarar = -1;// Uygun Değil
                    }
                }

            if (nihaiKarar != 0)
            {
                List<MuafIstDersler> mf = ctx.MuafIstDerslers.Where(y => y.MuafIstDersID == i.MuafIstDersID && y.AlinanDersID == i.AlinanDersID).ToList();

                foreach (MuafIstDersler s in mf)
                {
                    MuafIstDersler m = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == s.ID);
                    m.IcerikUygunluk = nihaiKarar;
                    ctx.SaveChanges();
                }
           
            }
            #endregion

            #region notification güncelleniyor

            List<IcerikKararlari> ick = null;
            if (UyeInfo.Rol == "Komisyon Başkanı")
            {
                ick = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye1 == 0).ToList();
            }
            else if (UyeInfo.Rol == "Komisyon 1.Üye")
            {
                ick = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye2 == 0).ToList();
            }
            else if (UyeInfo.Rol == "Komisyon 2.Üye")
            {
                ick = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye3 == 0).ToList();
            }
            Notification.BekleyenIcerikBildiriSayisi = ick.Count;       

            List<MuafIstDersler> mff = ctx.MuafIstDerslers.Where(x => x.DanismanUygunluk == 0 || x.NotUygunluk == 0 || x.KrediUygunluk == 0).ToList();
            Notification.BekleyenBasvuruBildiriSayisi = mff.Count;
            Notification.BekleyenToplamSayisi = Notification.BekleyenIcerikBildiriSayisi + Notification.BekleyenBasvuruBildiriSayisi;

            #endregion

            TempData["Durum"] = "Kabul";
            TempData["Text"] = ic.DersIcerikleri1.AlinanDersAdi.ToString();
        }

        public void UygunDegil(int ID)
        {
            IcerikKararlari ic = ctx.IcerikKararlaris.FirstOrDefault(x => x.ID == ID);
            if (ic.CokluGrupID > 0)
            {
                List<IcerikKararlari> ii = ctx.IcerikKararlaris.Where(x => x.CokluGrupID == ic.CokluGrupID).ToList();
                foreach (IcerikKararlari item in ii)
                {
                    if (UyeInfo.Rol == "Komisyon Başkanı") item.KararUye1 = 1;
                    else if (UyeInfo.Rol == "Komisyon 1.Üye") item.KararUye2 = 1;
                    else if (UyeInfo.Rol == "Komisyon 2.Üye") item.KararUye3 = 1;
                    ctx.SaveChanges();
                }
            }
            else
            {
                if (UyeInfo.Rol == "Komisyon Başkanı") ic.KararUye1 = -1;
                else if (UyeInfo.Rol == "Komisyon 1.Üye") ic.KararUye2 = -1;
                else if (UyeInfo.Rol == "Komisyon 2.Üye") ic.KararUye3 = -1;
                ctx.SaveChanges();
            }
          

            #region İçerik karara bağlandıktan sonra muaf istenen derslere bağlanıyor

            IcerikKararlari i = ctx.IcerikKararlaris.FirstOrDefault(x => x.ID == ID);

            int nihaiKarar = 0;

            if (i.KararUye1 == 0 || i.KararUye2 == 0 || i.KararUye3 == 0)
            {
                nihaiKarar = 0; //Değerlendirilmemiş    
            }
            else
            {
                if (i.KararUye1 + i.KararUye2 + i.KararUye3 > 1)
                {
                    nihaiKarar = 1; //Uygun
                }
                else
                {
                    nihaiKarar = -1;// Uygun Değil
                }
            }

            if (nihaiKarar != 0)
            {
                List<MuafIstDersler> mf = ctx.MuafIstDerslers.Where(y => y.MuafIstDersID == i.MuafIstDersID && y.AlinanDersID == i.AlinanDersID).ToList();

                foreach (MuafIstDersler s in mf)
                {
                    MuafIstDersler m = ctx.MuafIstDerslers.FirstOrDefault(x => x.ID == s.ID);
                    m.IcerikUygunluk = nihaiKarar;
                    ctx.SaveChanges();
                }

            }
            #endregion                   

            #region notification güncelleniyor

            List<IcerikKararlari> ick = null;
            if (UyeInfo.Rol == "Komisyon Başkanı")
            {
                ick = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye1 == 0).ToList();
            }
            else if (UyeInfo.Rol == "Komisyon 1.Üye")
            {
                ick = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye2 == 0).ToList();
            }
            else if (UyeInfo.Rol == "Komisyon 2.Üye")
            {
                ick = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye3 == 0).ToList();
            }
            Notification.BekleyenIcerikBildiriSayisi = ick.Count; 

            List<MuafIstDersler> mff = ctx.MuafIstDerslers.Where(x => x.DanismanUygunluk == 0 || x.NotUygunluk == 0 || x.KrediUygunluk == 0).ToList();
            Notification.BekleyenBasvuruBildiriSayisi = mff.Count;
            Notification.BekleyenToplamSayisi = Notification.BekleyenIcerikBildiriSayisi + Notification.BekleyenBasvuruBildiriSayisi;



            #endregion

            TempData["Durum"] = "Red";
            TempData["Text"] = ic.DersIcerikleri1.AlinanDersAdi.ToString();
        }

        public string UyeUyar()
        {
            string sonuc="Basarılı";
            //return sonuc;

            try
            {
                List<Kullanicilar> kl = ctx.Kullanicilars.Where(x => x.KullaniciUniID == UyeInfo.KullaniciUniID && x.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.KullaniciBolumID == UyeInfo.KullaniciBolumID && x.KullaniciRolID < 4).ToList();                             

                Unvanlar uv = new Unvanlar();

                foreach (Kullanicilar item in kl)
                {
                    string unvan = "";
                    string uyeBilgi = "";

                    for (int i = 0; i < uv.unvanAdi.Length; i++)
                    {
                        if (uv.unvanID[i] == item.KullaniciUnvanID)
                        {
                            unvan = uv.unvanAdi[i];
                            break;
                        }
                    }

                    uyeBilgi = unvan + " " + item.KullaniciAdi + " " + item.KullaniciSoyadi;
                    MailIslemleri(item.KullaniciEMail, uyeBilgi);                   
                    break;
                }              
            }
            catch (Exception)
            {
                sonuc = "Başarısız"; 
            }
            return sonuc;
        }

        private string createEmailBody(string ad, string soyad, string uni, string fakulte, string bolum, string template, string sifre)
        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(Server.MapPath("~/Content_Mail/" + template)))

            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{ad}", ad); //replacing the required things  

            body = body.Replace("{soyad}", soyad);

            body = body.Replace("{uni}", uni);

            body = body.Replace("{fakulte}", fakulte);

            body = body.Replace("{bolum}", bolum);

            body = body.Replace("{sifre}", sifre);

            return body;
        }

        private void MailIslemleri(string mailAdresi, string uyeBilgisi)
        {
            MailMessage posta = new MailMessage();
            posta.From = new MailAddress("destek@muaff.com", "MUAF");
            posta.To.Add(new MailAddress(mailAdresi));
            posta.Subject = "Muaff-İçerik Değerlendirme";

            posta.IsBodyHtml = true;
            string body = createEmailBody(uyeBilgisi, "", "", "", "", "uyeUyar.html", "");

            posta.Body = body;

            AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            LinkedResource lr = new LinkedResource(Server.MapPath("~/Content_Mail/images/Uyari.png"), MediaTypeNames.Image.Jpeg);
            lr.ContentId = "image1";
            av.LinkedResources.Add(lr);
            posta.AlternateViews.Add(av);

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "mail.muaff.com";//smtp.internal.mycompany.com
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("destek@muaff.com", "YUnus2325");
            client.EnableSsl = false;
            client.Send(posta);
        }

    }
}