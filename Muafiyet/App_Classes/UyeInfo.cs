using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class UyeInfo
    {
        public static int KullaniciID { get; set; }
        public static string Unvan { get; set; }
        public static int UnvanID { get; set; }
        public static string KullaniciAdi { get; set; }
        public static string KullaniciSoyadi { get; set; }
        public static string KullaniciTlf { get; set; }
        public static string KullaniciAdres { get; set; }
        public static string KullaniciEMail { get; set; }
        public static string KullaniciSifre { get; set; }
        public static int RolID { get; set; }
        public static string Rol { get; set; }
        public static string KullaniciDahili { get; set; }

        public static int KullaniciUniID { get; set; }
        public static int KullaniciFakulteID { get; set; }
        public static int KullaniciBolumID { get; set; }
    }
}