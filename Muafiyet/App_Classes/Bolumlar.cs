using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class Bolumlar
    {
        public int[] BolumID { get; set; }
        public string[] Bolum { get; set; }

        public Bolumlar()
        {
            BolumID = new int[8];
            Bolum = new string[8];

            BolumID[0] = 1; Bolum[0] = "Bilgisayar Mühendisliği";
            BolumID[1] = 2; Bolum[1] = "Çevre Mühendisliği";
            BolumID[2] = 3; Bolum[2] = "Elektrik-Elektronik Mühendisliği";
            BolumID[3] = 4; Bolum[3] = "Endüstri Mühendisliği";
            BolumID[4] = 5; Bolum[4] = "İnşaat Mühendisliği";
            BolumID[5] = 6; Bolum[5] = "Kimya Mühendisliği";
            BolumID[6] = 7; Bolum[6] = "Makine Mühendisliği";
            BolumID[7] = 8; Bolum[7] = "Metalurji-Malzeme Mühendisliği";
           
        }

    }
}