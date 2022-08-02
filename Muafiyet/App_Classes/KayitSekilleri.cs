using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class KayitSekilleri
    {
        public int[] KayitSekliID { get; set; }
        public string[] KayitSekli { get; set; }

        public KayitSekilleri()
        {
            KayitSekliID = new int[3];
            KayitSekli = new string[3];

            KayitSekliID[0] = 1; KayitSekli[0] = "Merkezi Yerleştirme";
            KayitSekliID[1] = 2; KayitSekli[1] = "Dikey Geçiş";
            KayitSekliID[2] = 3; KayitSekli[2] = "Yaz Okulu";
            KayitSekliID[3] = 4; KayitSekli[3] = "Merkezi Yerleştirme ile Yatay Geçiş";
            KayitSekliID[4] = 5; KayitSekli[4] = "Kurum içi Yatay Geçiş";
            KayitSekliID[5] = 6; KayitSekli[5] = "Kurumlar Arası Yatay Geçiş";
            KayitSekliID[6] = 7; KayitSekli[6] = "Af";
        }
    }
}