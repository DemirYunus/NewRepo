using System;
using System.Collections.Generic;

namespace Muafiyet.Models
{
    public partial class MuafiyetBasvurulari
    {
        public MuafiyetBasvurulari()
        {
            this.MuafIstDerslers = new List<MuafIstDersler>();
        }

        public int MuafBasvuruID { get; set; }
        public int OgrenciID { get; set; }
        public int KayitSekliID { get; set; }
        public int MuafIsUniID { get; set; }
        public int MuaIstFaktID { get; set; }
        public int MuafIsBolumID { get; set; }
        public System.DateTime BasvuruTarihi { get; set; }
        public string Durum { get; set; }
        public int NotTipiID { get; set; }
        public virtual Kullanicilar Kullanicilar { get; set; }
        public virtual ICollection<MuafIstDersler> MuafIstDerslers { get; set; }
        public virtual Universiteler Universiteler { get; set; }
    }
}
