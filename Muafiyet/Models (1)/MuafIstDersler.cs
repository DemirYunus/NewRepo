using System;
using System.Collections.Generic;

namespace Muafiyet.Models
{
    public partial class MuafIstDersler
    {
        public int ID { get; set; }
        public int MuafBasvID { get; set; }
        public int MuafIstDersID { get; set; }
        public int AlinanDersID { get; set; }
        public string AlinanDersNotu { get; set; }
        public int DanismanUygunluk { get; set; }
        public int IcerikUygunluk { get; set; }
        public int NotUygunluk { get; set; }
        public int KrediUygunluk { get; set; }
        public virtual DersIcerikleri DersIcerikleri { get; set; }
        public virtual DersIcerikleri DersIcerikleri1 { get; set; }
        public virtual MuafiyetBasvurulari MuafiyetBasvurulari { get; set; }
    }
}
