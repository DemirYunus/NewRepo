using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Muafiyet.Models;
using System.Web.Security;
using Muafiyet.App_Classes;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using Newtonsoft.Json;

namespace Muafiyet.Controllers
{
    [Authorize]

    public class UyeController : Controller
    {
        // GET: Uye
        MuafContext ctx = new MuafContext();

        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            return View();
        }

        [AllowAnonymous]        
        [HttpPost]
        public ActionResult GirisYap(Kullanicilar k, string Hatirla)
        {
            Kullanicilar kl = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciEMail == k.KullaniciEMail && x.KullaniciSifre == k.KullaniciSifre);

            if (kl==null)
            {
                ViewBag.Mesaj = "Kullanıcı adı veya Şifre hatalı...";
                 return View();
            }
            else
            {
                if(kl.KullaniciRolID>3)
                {
                    ViewBag.Mesaj = "Giriş yetkiniz yok !";
                    return View();
                }
                else
                {
                    if (Hatirla == "on")
                    {
                        FormsAuthentication.RedirectFromLoginPage(k.KullaniciEMail, true);//true cookie aç
                    }
                    else
                    {
                        FormsAuthentication.RedirectFromLoginPage(k.KullaniciEMail, false);//true cookie açma
                    }

                    Data.Universite = ctx.Universitelers.ToList();
                    Data.Fakulte = ctx.Fakultelers.ToList();
                    Data.Bolum = ctx.Bolumlers.ToList();

                    UyeInfo.KullaniciID = kl.KullaniciID;
                    UyeInfo.KullaniciAdi = kl.KullaniciAdi;
                    UyeInfo.KullaniciSoyadi = kl.KullaniciSoyadi;
                    Unvanlar u = new Unvanlar();
                    string unv = "";
                    int unvID = 0;
                    for (int y = 0; y < u.unvanAdi.Length; y++)
                    {
                        if (kl.KullaniciUnvanID == u.unvanID[y])
                        {
                            unv = u.unvanAdi[y];
                            unvID = u.unvanID[y];
                            break;
                        }
                    }
                    UyeInfo.Unvan = unv;
                    UyeInfo.UnvanID = unvID;
                    UyeInfo.KullaniciEMail = kl.KullaniciEMail;
                    UyeInfo.KullaniciTlf = kl.KullaniciTlf;
                    UyeInfo.KullaniciDahili = kl.KullaniciDahili;
                    UyeInfo.KullaniciAdres = kl.KullaniciAdres;
                    UyeInfo.KullaniciSifre = kl.KullaniciSifre;
                    UyeInfo.RolID = (int)kl.KullaniciRolID;
                    Rollar r = new Rollar();
                    string ro = "";
                    for (int i = 0; i < r.RolID.Length; i++)
                    {
                        if (kl.KullaniciRolID == r.RolID[i])
                        {
                            ro = r.Rol[i];
                            break;
                        }
                    }
                    UyeInfo.Rol = ro;
                    UyeInfo.KullaniciUniID = (int)kl.KullaniciUniID;
                    UyeInfo.KullaniciFakulteID = (int)kl.KullaniciFakulteID;
                    UyeInfo.KullaniciBolumID = (int)kl.KullaniciBolumID;

                    List<MuafIstDersler> mf = ctx.MuafIstDerslers.Where(x => x.DanismanUygunluk == 0 || x.NotUygunluk == 0 || x.KrediUygunluk == 0).ToList();
                    Notification.BekleyenBasvuruBildiriSayisi = mf.Count;

                    List<IcerikKararlari> ic = null;
                    if (UyeInfo.Rol == "Komisyon Başkanı")
                    {
                        ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye1 == 0).ToList();
                    }
                    else if (UyeInfo.Rol == "Komisyon 1.Üye")
                    {
                        ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye2 == 0).ToList();
                    }
                    else if (UyeInfo.Rol == "Komisyon 2.Üye")
                    {
                        ic = ctx.IcerikKararlaris.Where(x => x.KararUniID == UyeInfo.KullaniciUniID && x.KararBolumID == UyeInfo.KullaniciBolumID && x.KararFakulteID == UyeInfo.KullaniciFakulteID && x.KararUye3 == 0).ToList();
                    }

                    List<Kullanicilar> kk = ctx.Kullanicilars.Where(x => x.KullaniciUniID == UyeInfo.KullaniciUniID && x.KullaniciBolumID == UyeInfo.KullaniciBolumID && x.KullaniciFakulteID == UyeInfo.KullaniciFakulteID && x.KullaniciUnvanID != 11).ToList();

                    Notification.BaskanYokmi = 1;
                    Notification.Uye1Yokmi = 1;
                    Notification.Uye2Yokmi = 1;

                    foreach (Kullanicilar item in kk)
                    {
                        if (item.KullaniciRolID == 1)
                        {
                            Notification.BaskanYokmi = 0;
                        }
                        if (item.KullaniciRolID == 2)
                        {
                            Notification.Uye1Yokmi = 0;
                        }
                        if (item.KullaniciRolID == 3)
                        {
                            Notification.Uye2Yokmi = 0;
                        }
                    }

                    Notification.BekleyenIcerikBildiriSayisi = ic.Count;
                    Notification.BekleyenToplamSayisi = Notification.BekleyenIcerikBildiriSayisi + Notification.BekleyenBasvuruBildiriSayisi + Notification.BaskanYokmi + Notification.Uye1Yokmi + Notification.Uye2Yokmi;
                  
                }
                return RedirectToAction("Panel", "Acilis");

            }
           
        }

        [AllowAnonymous]
        public ActionResult UyeOl()
        { 
            Data.Universite= ctx.Universitelers.ToList();
            Data.Fakulte= ctx.Fakultelers.ToList();
            Data.Bolum = ctx.Bolumlers.ToList();

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult UyeOl(UyelikTalep u)
        {
            Kullanicilar kayitliKullanici = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciUniID == u.TalepEdenUniID && x.KullaniciFakulteID == u.TalepEdenFakulteID && x.KullaniciBolumID == u.TalepEdenBolumID);

            List<Universiteler> uni = ctx.Universitelers.ToList();
            List<Fakulteler> fkt = ctx.Fakultelers.ToList();
            List<Bolumler> blm = ctx.Bolumlers.ToList();

            string universite = "";
            string fakulte = "";
            string bolum = "";

            if (kayitliKullanici==null)
            { 
                try
                {
                    UyelikTalep ut = ctx.UyelikTaleps.FirstOrDefault(x => x.TalepEdenUniID == u.TalepEdenUniID && x.TalepEdenFakulteID == u.TalepEdenFakulteID && x.TalepEdenBolumID == u.TalepEdenBolumID);

                    if (ut == null)//Daha önce başvurusu var mı
                    {

                        ViewBag.Mesaj = "Başarılı";

                        MailMessage posta = new MailMessage();
                        posta.From = new MailAddress("destek@muaff.com", "MUAF");
                        posta.To.Add(new MailAddress(u.TalepEdenMail));
                        posta.To.Add(new MailAddress("demiry@atauni.edu.tr"));
                        posta.Subject = "Muaf Üyelik İşlemleri";

                        foreach (Universiteler item in uni)
                        {
                            if (item.UniversiteID==u.TalepEdenUniID)
                            {
                                universite = item.Universite;
                                break;
                            }
                        }
                        foreach (Fakulteler item in fkt)
                        {
                            if (item.FakulteID == u.TalepEdenFakulteID)
                            {
                                fakulte = item.Fakulte;
                                break;
                            }
                        }
                        foreach (Bolumler item in blm)
                        {
                            if (item.BolumID == u.TalepEdenBolumID)
                            {
                                bolum = item.Bolum;
                                break;
                            }
                        }


                        posta.IsBodyHtml = true;
                        string body = createEmailBody(u.TalepEdenAdi, u.TalepEdenSoyadi, universite, fakulte, bolum, "index.html", "");

                        posta.Body = body;

                        //"<p>Sayın " + u.TalepEdenAdi + " " + u.TalepEdenSoyadi + ",<br /></p> <p>Üyelik talebiniz alınmıştır.<br /></p> <p>Talebiniz onaylandıktan sonra MUAF'ı kullanabilirsiniz.</p><p>İyi Çalışmalar...</p>";


                        AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                        LinkedResource lr = new LinkedResource(Server.MapPath("~/Content_Mail/images/MUAFHeader.png"), MediaTypeNames.Image.Jpeg);
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

                        u.TalepTarihi = DateTime.Now;
                        u.TalepCevap = 0;

                        ctx.UyelikTaleps.Add(u);
                        ctx.SaveChanges();

                        //FileStream fs = new FileStream(Server.MapPath("~/Files/Hususlar.pdf"), FileMode.Open, FileAccess.Read);
                        //Attachment a = new Attachment(Server.MapPath("~/Files/Hususlar.pdf"));
                        //posta.Attachments.Add(a);

                        //string str = "<html><body><h1>Picture</h1><br/><img src =\"cid:image1\"></body></html>";
                        //AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
                        //LinkedResource lr = new LinkedResource(Server.MapPath("~/images/product3.png"), MediaTypeNames.Image.Jpeg);
                        //lr.ContentId = "image1";
                        //av.LinkedResources.Add(lr);
                        //objeto_mail.AlternateViews.Add(av);
                    }
                    else
                    {
                        ViewBag.Mesaj = "Tekrar Başvuru";
                        ViewBag.Text = ut.TalepEdenMail;
                    }

                }
                catch (Exception exp)
                {
                    ViewBag.Mesaj = "Mail Hatası";
                }              
            }
            else
            {
                ViewBag.Mesaj = "Bu bölüm sisteme kayıtlı. " + kayitliKullanici.KullaniciEMail + " adresinden bilgi alabilirsiniz.";
            }
          
            ViewBag.Universiteler = uni;
            return View();
        }

        private string createEmailBody(string ad, string soyad, string uni, string fakulte, string bolum, string template, string sifre)
        {

            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(Server.MapPath("~/Content_Mail/"+ template)))

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

        public ActionResult UyelikOnay()
        {
            List<UyelikTalep> u = ctx.UyelikTaleps.OrderBy(x=>x.TalepCevap).ToList();
            return View(u);
        }
        [HttpPost]
        public ActionResult UyelikKabul(int uyeID)
        {
            UyelikTalep u = ctx.UyelikTaleps.FirstOrDefault(x => x.UyelikTalepID == uyeID);

            Kullanicilar kayitliKullanici = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciUniID == u.TalepEdenUniID && x.KullaniciFakulteID == u.TalepEdenFakulteID && x.KullaniciBolumID == u.TalepEdenBolumID);

            if (kayitliKullanici == null)
            {
                try
                {
                    Kullanicilar k = new Kullanicilar();

                    Random r = new Random();
                    string sifre = r.Next(1000, 9999).ToString();

                    k.KullaniciOkulNo = "0";
                    k.KullaniciAdi = u.TalepEdenAdi;
                    k.KullaniciSoyadi = u.TalepEdenSoyadi;
                    k.KullaniciTlf = "0";
                    k.KullaniciEMail = u.TalepEdenMail;
                    k.KullaniciSifre = sifre;
                    k.KullaniciUnvanID = 1;
                    k.KullaniciUniID = u.TalepEdenUniID;
                    k.KullaniciFakulteID = u.TalepEdenFakulteID;
                    k.KullaniciBolumID = u.TalepEdenBolumID;
                    k.KullaniciRolID = 1;                  
                    k.AktifMi = 1;

                    ctx.Kullanicilars.Add(k);
                    ctx.SaveChanges();

                    u.TalepCevap = 1;
                    ctx.SaveChanges();

                    Ayarlar a = new Ayarlar();
                    a.NotTipiID = 1;
                    a.AyarUniID = u.TalepEdenUniID;
                    a.AyarFakulteID = u.TalepEdenFakulteID;
                    a.AyarBolumID = u.TalepEdenBolumID;
                    ctx.Ayarlars.Add(a);
                    a.AKTS1 = 30;
                    a.AKTS2 = 30;
                    a.AKTS3 = 30;
                    a.AKTS14 = 30;
                    a.Kredi1 = 20;
                    a.Kredi2 = 20;
                    a.Kredi3 = 20;
                    a.Kredi4 = 20;
                    a.AKTS_Kredi = 1;

                    ctx.SaveChanges();

                    TempData["Durum"] = "Başarılı";

                    MailMessage posta = new MailMessage();
                    posta.From = new MailAddress("destek@muaff.com", "MUAF");
                    posta.To.Add(new MailAddress(k.KullaniciEMail));
                    posta.Subject = "Muaf Üyelik İşlemleri";

                    posta.IsBodyHtml = true;
                    string body = createEmailBody(u.TalepEdenAdi, u.TalepEdenSoyadi, "", "", "", "Kabul.html", sifre);

                    posta.Body = body;                    

                    AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    LinkedResource lr = new LinkedResource(Server.MapPath("~/Content_Mail/images/Kabul.png"), MediaTypeNames.Image.Jpeg);
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

                    //FileStream fs = new FileStream(Server.MapPath("~/Files/Hususlar.pdf"), FileMode.Open, FileAccess.Read);
                    //Attachment a = new Attachment(Server.MapPath("~/Files/Hususlar.pdf"));
                    //posta.Attachments.Add(a);

                    //string str = "<html><body><h1>MUAF</h1><br/><img src =\"cid:image1\"></body></html>";
                    //AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
                    //LinkedResource lr = new LinkedResource(Server.MapPath("~/images/product3.png"), MediaTypeNames.Image.Jpeg);
                    //lr.ContentId = "image1";
                    //av.LinkedResources.Add(lr);
                    //posta.AlternateViews.Add(av);                 
                }
                catch (Exception exp)
                {
                    TempData["Durum"] = "Mail Hatası";
                }
            }
            else
            {
                TempData["Durum"]="Kayıtlı";
                TempData["Text"]= kayitliKullanici.KullaniciEMail + " adresinden bilgi alabilirsiniz.";
            }
          
            return RedirectToAction("UyelikOnay");
        }

        public ActionResult UyelikRed(int uyeID)
        {
            UyelikTalep u = ctx.UyelikTaleps.FirstOrDefault(x => x.UyelikTalepID == uyeID);
            List<Universiteler> uni = ctx.Universitelers.ToList();
            List<Fakulteler> fkt = ctx.Fakultelers.ToList();
            List<Bolumler> blm = ctx.Bolumlers.ToList();

            string universite = "";
            string fakulte = "";
            string bolum = "";

            TempData["Durum"] = "Red";

            try
            {
                MailMessage posta = new MailMessage();
                posta.From = new MailAddress("destek@muaff.com", "MUAF");
                posta.To.Add(new MailAddress(u.TalepEdenMail));
                posta.Subject = "Muaf Üyelik İşlemleri";

                foreach (Universiteler item in uni)
                {
                    if (item.UniversiteID == u.TalepEdenUniID)
                    {
                        universite = item.Universite;
                        break;
                    }
                }
                foreach (Fakulteler item in fkt)
                {
                    if (item.FakulteID == u.TalepEdenFakulteID)
                    {
                        fakulte = item.Fakulte;
                        break;
                    }
                }
                foreach (Bolumler item in blm)
                {
                    if (item.BolumID == u.TalepEdenBolumID)
                    {
                        bolum = item.Bolum;
                        break;
                    }
                }


                posta.IsBodyHtml = true;
                string body = createEmailBody(u.TalepEdenAdi, u.TalepEdenSoyadi,universite, fakulte, bolum, "Red.html", "");

                posta.Body = body;

                AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                LinkedResource lr = new LinkedResource(Server.MapPath("~/Content_Mail/images/Red.png"), MediaTypeNames.Image.Jpeg);
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

                u.TalepCevap = -1;
                ctx.SaveChanges();
            }
            catch (Exception)
            {

                TempData["Durum"] = "Mail Hatası";
            }

            return RedirectToAction("UyelikOnay");
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acilis");
        }

        public ActionResult Profil()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProfilGuncelle(Kullanicilar k)
        {
            Kullanicilar kk = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciID == k.KullaniciID);

            kk.KullaniciUnvanID = k.KullaniciUnvanID;
            UyeInfo.UnvanID = k.KullaniciUnvanID;
            Unvanlar u = new Unvanlar();
            string unv = "";      
            for (int y = 0; y < u.unvanAdi.Length; y++)
            {
                if (k.KullaniciUnvanID == u.unvanID[y])
                {
                    unv = u.unvanAdi[y];                 
                    break;
                }
            }
            UyeInfo.Unvan = unv;
            kk.KullaniciAdi = k.KullaniciAdi;
            UyeInfo.KullaniciAdi = k.KullaniciAdi;
            kk.KullaniciSoyadi = k.KullaniciSoyadi;
            UyeInfo.KullaniciSoyadi = k.KullaniciSoyadi;
            kk.KullaniciEMail = k.KullaniciEMail;
            UyeInfo.KullaniciEMail = k.KullaniciEMail;
            kk.KullaniciTlf = k.KullaniciTlf;
            UyeInfo.KullaniciTlf = k.KullaniciTlf;
            kk.KullaniciDahili = k.KullaniciDahili;
            UyeInfo.KullaniciDahili = k.KullaniciDahili;

            ctx.SaveChanges();

            TempData["Durum"] = "Profil";
            TempData["Text"] = "";

            return RedirectToAction("Profil", "Uye");
        }

        public ActionResult SifreGuncelle(Kullanicilar k, string mevcutSifre)
        {
            Kullanicilar kk = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciID == k.KullaniciID);

            if (mevcutSifre!=kk.KullaniciSifre)
            {
                TempData["Durum"] = "HataMevcutSifre";
                TempData["Text"] = "Mevcut şifrenizi hatalı girdiniz !";

                return RedirectToAction("Profil", "Uye");
            }
            else
            {
                kk.KullaniciSifre = k.KullaniciSifre;

                ctx.SaveChanges();

                TempData["Durum"] = "Şifre";
                TempData["Text"] = "";

                return RedirectToAction("Profil", "Uye");
            }
         
        }

        public ActionResult Sabitler()
        {
            List<Universiteler> uni = ctx.Universitelers.ToList();
            List<Fakulteler> fkt = ctx.Fakultelers.ToList();
            List<Bolumler> blm = ctx.Bolumlers.ToList();

            ViewBag.Universiteler = uni;
            ViewBag.Fakulteler = fkt;
            ViewBag.Bolumler = blm;

            return View();
        }

        [HttpPost]
        public ActionResult UniGuncelle(Universiteler uv)
        {
            Universiteler u = ctx.Universitelers.FirstOrDefault(x=>x.UniversiteID==uv.UniversiteID);
            u.Universite = uv.Universite;
            ctx.SaveChanges();

            return RedirectToAction("Sabitler", "Uye");
        }

        [HttpPost]
        public ActionResult FakulteGuncelle(Fakulteler fk)
        {
            Fakulteler f = ctx.Fakultelers.FirstOrDefault(x => x.FakulteID == fk.FakulteID);
            f.Fakulte = fk.Fakulte;
            ctx.SaveChanges();

            return RedirectToAction("Sabitler", "Uye");
        }

        [HttpPost]
        public ActionResult BolumGuncelle(Bolumler bl)
        {
            Bolumler b = ctx.Bolumlers.FirstOrDefault(x => x.BolumID == bl.BolumID);
            b.Bolum = bl.Bolum;
            ctx.SaveChanges();

            return RedirectToAction("Sabitler", "Uye");
        }

        [HttpPost]
        [AllowAnonymous]
        public string SifreSifirla(string email)
        {
            string sonuc = "";
            try
            {
                Kullanicilar kayitliKullanici = ctx.Kullanicilars.FirstOrDefault(x => x.KullaniciEMail == email);

                if (kayitliKullanici==null)
                {
                    sonuc = "kayitliDegil";
                }
                else
                {
                    Random r = new Random();
                    string sifre = r.Next(1000, 9999).ToString();

                    kayitliKullanici.KullaniciSifre = sifre;
                    ctx.SaveChanges();                   

                    MailMessage posta = new MailMessage();
                    posta.From = new MailAddress("destek@muaff.com", "MUAF");
                    posta.To.Add(new MailAddress(kayitliKullanici.KullaniciEMail));
                    posta.Subject = "Muaf Üyelik İşlemleri";

                    posta.IsBodyHtml = true;
                    string body = createEmailBody(kayitliKullanici.KullaniciAdi, kayitliKullanici.KullaniciSoyadi, "", "", "", "SifreSifirla.html", sifre);

                    posta.Body = body;

                    AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
                    LinkedResource lr = new LinkedResource(Server.MapPath("~/Content_Mail/images/SifreSifirla.png"), MediaTypeNames.Image.Jpeg);
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

                    sonuc = "başarılı";
                }                            
            }
            catch (Exception)
            {
               
                TempData["Durum"] = "Mail Hatası";

                sonuc = "hata";
            }
            //string jsonString = JsonConvert.SerializeObject(sonuc, Formatting.None, new JsonSerializerSettings()
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            //});

            //return jsonString;
             return sonuc;
        }

        public ActionResult TanitimMailiAt()
        {
            return View();
        }

        [HttpPost]        
        public ActionResult TanitimMailiAt(string email)
        {           
            try
            {
                MailMessage posta = new MailMessage();
                posta.From = new MailAddress("destek@muaff.com", "MUAF");
                posta.To.Add(new MailAddress(email));
                posta.Subject = "Muaff Tanıtım";

                posta.IsBodyHtml = true;
                string body = createEmailBody("", "", "", "", "", "Tanitim.html", "");

                posta.Body = body;

                AlternateView av = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                LinkedResource lr1 = new LinkedResource(Server.MapPath("~/Content_Mail/images/uecretsiz.png"), MediaTypeNames.Image.Jpeg);
                lr1.ContentId = "image1";
                av.LinkedResources.Add(lr1);

                LinkedResource lr2 = new LinkedResource(Server.MapPath("~/Content_Mail/images/server.png"), MediaTypeNames.Image.Jpeg);
                lr2.ContentId = "image2";
                av.LinkedResources.Add(lr2);

                LinkedResource lr3 = new LinkedResource(Server.MapPath("~/Content_Mail/images/computer.png"), MediaTypeNames.Image.Jpeg);
                lr3.ContentId = "image3";
                av.LinkedResources.Add(lr3);

                LinkedResource lr4 = new LinkedResource(Server.MapPath("~/Content_Mail/images/repeat.png"), MediaTypeNames.Image.Jpeg);
                lr4.ContentId = "image4";
                av.LinkedResources.Add(lr4);

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

                TempData["Durum"] = "Başarılı";
            }

            catch (Exception)
            {

                TempData["Durum"] = "Mail Hatası";                
            }
            //string jsonString = JsonConvert.SerializeObject(sonuc, Formatting.None, new JsonSerializerSettings()
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            //});

            //return jsonString;
            return RedirectToAction("TanitimMailiAt", "Uye");
        }
    }
}