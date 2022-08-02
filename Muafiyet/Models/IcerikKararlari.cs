using System;
using System.Collections.Generic;

namespace Muafiyet.Models
{
    public partial class IcerikKararlari
    {
        public int ID { get; set; }
        public int MuafIstDersID { get; set; }
        public int AlinanDersID { get; set; }
        public int KararUye1 { get; set; }
        public int KararUye2 { get; set; }
        public int KararUye3 { get; set; }
        public System.DateTime KararTarihi { get; set; }
        public int KararUniID { get; set; }
        public int KararFakulteID { get; set; }
        public int KararBolumID { get; set; }
        public int CokluGrupID { get; set; }
        public virtual DersIcerikleri DersIcerikleri { get; set; }
        public virtual DersIcerikleri DersIcerikleri1 { get; set; }
        public virtual Universiteler Universiteler { get; set; }
    }
}
