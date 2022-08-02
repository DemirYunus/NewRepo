using System;
using System.Collections.Generic;

namespace Muafiyet.Models
{
    public partial class Kullanicilar
    {
        public Kullanicilar()
        {
            this.MuafiyetBasvurularis = new List<MuafiyetBasvurulari>();
        }

        public int KullaniciID { get; set; }
        public string KullaniciOkulNo { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
        public string KullaniciTlf { get; set; }
        public string KullaniciDahili { get; set; }
        public string KullaniciAdres { get; set; }
        public string KullaniciEMail { get; set; }
        public string KullaniciSifre { get; set; }
        public int KullaniciUnvanID { get; set; }
        public int KullaniciUniID { get; set; }
        public int KullaniciFakulteID { get; set; }
        public int KullaniciBolumID { get; set; }
        public int KullaniciRolID { get; set; }
        public int AktifMi { get; set; }
        public virtual Universiteler Universiteler { get; set; }
        public virtual ICollection<MuafiyetBasvurulari> MuafiyetBasvurularis { get; set; }
    }
}
