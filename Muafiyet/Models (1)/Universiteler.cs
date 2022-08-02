using System;
using System.Collections.Generic;

namespace Muafiyet.Models
{
    public partial class Universiteler
    {
        public Universiteler()
        {
            this.Ayarlars = new List<Ayarlar>();
            this.DersIcerikleris = new List<DersIcerikleri>();
            this.IcerikKararlaris = new List<IcerikKararlari>();
            this.Kullanicilars = new List<Kullanicilar>();
            this.MuafiyetBasvurularis = new List<MuafiyetBasvurulari>();
        }

        public int UniversiteID { get; set; }
        public string Universite { get; set; }
        public virtual ICollection<Ayarlar> Ayarlars { get; set; }
        public virtual ICollection<DersIcerikleri> DersIcerikleris { get; set; }
        public virtual ICollection<IcerikKararlari> IcerikKararlaris { get; set; }
        public virtual ICollection<Kullanicilar> Kullanicilars { get; set; }
        public virtual ICollection<MuafiyetBasvurulari> MuafiyetBasvurularis { get; set; }
    }
}
