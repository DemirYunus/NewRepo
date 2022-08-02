using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class Fakultelar
    {
        public int[] FakulteID { get; set; }
        public string[] Fakulte { get; set; }

        public Fakultelar()
        {
            FakulteID = new int[18];
            Fakulte = new string[18];

            FakulteID[0] = 1; Fakulte[0] = "İlahiyat Fakültesi";
            FakulteID[1] = 2; Fakulte[1] = "İletişim Fakültesi";
            FakulteID[2] = 3; Fakulte[2] = "İslami İlimler Fakültesi";
            FakulteID[3] = 4; Fakulte[3] = "İşletme Fakültesi";
            FakulteID[4] = 5; Fakulte[4] = "Mimarlık Fakültesi";
            FakulteID[5] = 6; Fakulte[5] = "Mühendislik Fakültesi";
            FakulteID[6] = 7; Fakulte[6] = "Mühendislik ve Doğa Bilimleri Fakültesi";
            FakulteID[7] = 8; Fakulte[7] = "Mühendislik-Mimarlık Fakültesi";
            FakulteID[8] = 9; Fakulte[8] = "Sağlık Bilimleri Fakültesi";
            FakulteID[9] = 10; Fakulte[9] = "Siyasal Bilgiler Fakültesi";
            FakulteID[10] = 11; Fakulte[10] = "Spor Bilimleri Fakültesi";
            FakulteID[11] = 12; Fakulte[11] = "Teknoloji Fakültesi";
            FakulteID[12] = 13; Fakulte[12] = "Tıp Fakültesi";
            FakulteID[13] = 14; Fakulte[13] = "Turizm Fakültesi";
            FakulteID[14] = 15; Fakulte[14] = "Uygulamalı Bilimler Fakültesi";
            FakulteID[15] = 16; Fakulte[15] = "Veteriner Fakültesi";
            FakulteID[16] = 17; Fakulte[16] = "Ziraat Fakültesi";
            FakulteID[17] = 18; Fakulte[17] = "Ziraat ve Doğa Bilimleri Fakültesi";
        }

    }
}