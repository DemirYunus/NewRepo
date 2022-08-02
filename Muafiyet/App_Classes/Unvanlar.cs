using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class Unvanlar
    {
        public string[] unvanAdi { get; set; }
        public int[] unvanID { get; set; }

        public Unvanlar()
        {
            unvanAdi = new string[11];
            unvanID = new int[11];

            unvanAdi[0] = "Prof.Dr."; unvanID[0] = 1;
            unvanAdi[1] = "Doç.Dr."; unvanID[1] = 2;
            unvanAdi[2] = "Dr.Öğr.Üyesi"; unvanID[2] = 3;
            unvanAdi[3] = "Öğr.Grv.Dr."; unvanID[3] = 4;
            unvanAdi[4] = "Öğr.Grv."; unvanID[4] = 5;
            unvanAdi[5] = "Arş.Gör.Dr."; unvanID[5] = 6;
            unvanAdi[6] = "Uzman Dr."; unvanID[6] = 7;
            unvanAdi[7] = "Okutman Dr."; unvanID[7] = 8;
            unvanAdi[8] = "Arş.Gör."; unvanID[8] = 9;
            unvanAdi[9] = "Okutman"; unvanID[9] = 10;
            unvanAdi[10] = "Öğrenci"; unvanID[10] = 11;
        }
   
    }
}