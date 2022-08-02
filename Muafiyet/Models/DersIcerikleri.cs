using System;
using System.Collections.Generic;

namespace Muafiyet.Models
{
    public partial class DersIcerikleri
    {
        public DersIcerikleri()
        {
            this.IcerikKararlaris = new List<IcerikKararlari>();
            this.IcerikKararlaris1 = new List<IcerikKararlari>();
            this.MuafIstDerslers = new List<MuafIstDersler>();
            this.MuafIstDerslers1 = new List<MuafIstDersler>();
        }

        public int AlinanDersID { get; set; }
        public int AlinanUniID { get; set; }
        public int AlinanFakulteID { get; set; }
        public int AlinanBolumID { get; set; }
        public string AlinanDersKodu { get; set; }
        public string AlinanDersAdi { get; set; }
        public decimal AlinanKredisi { get; set; }
        public int AlinanAKTS { get; set; }
        public string IcerikLinki { get; set; }
        public string IcerikNotu { get; set; }
        public string IcerikResimYolu { get; set; }
        public virtual Universiteler Universiteler { get; set; }
        public virtual ICollection<IcerikKararlari> IcerikKararlaris { get; set; }
        public virtual ICollection<IcerikKararlari> IcerikKararlaris1 { get; set; }
        public virtual ICollection<MuafIstDersler> MuafIstDerslers { get; set; }
        public virtual ICollection<MuafIstDersler> MuafIstDerslers1 { get; set; }
    }
}
