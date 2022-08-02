using System;
using System.Collections.Generic;

namespace Muafiyet.Models
{
    public partial class UyelikTalep
    {
        public int UyelikTalepID { get; set; }
        public string TalepEdenAdi { get; set; }
        public string TalepEdenSoyadi { get; set; }
        public string TalepEdenMail { get; set; }
        public int TalepEdenUniID { get; set; }
        public int TalepEdenFakulteID { get; set; }
        public int TalepEdenBolumID { get; set; }
        public System.DateTime TalepTarihi { get; set; }
        public int TalepCevap { get; set; }
        public virtual Universiteler Universiteler { get; set; }
    }
}
