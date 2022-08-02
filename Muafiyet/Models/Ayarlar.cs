using System;
using System.Collections.Generic;

namespace Muafiyet.Models
{
    public partial class Ayarlar
    {
        public int ID { get; set; }
        public int NotTipiID { get; set; }
        public int AyarUniID { get; set; }
        public int AyarFakulteID { get; set; }
        public int AyarBolumID { get; set; }
        public decimal AKTS1 { get; set; }
        public decimal AKTS2 { get; set; }
        public decimal AKTS3 { get; set; }
        public decimal AKTS14 { get; set; }
        public decimal Kredi1 { get; set; }
        public decimal Kredi2 { get; set; }
        public decimal Kredi3 { get; set; }
        public decimal Kredi4 { get; set; }
        public int AKTS_Kredi { get; set; }
        public virtual Universiteler Universiteler { get; set; }
    }
}
