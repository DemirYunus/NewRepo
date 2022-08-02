using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class NotTiplari
    {
        public int[] NotTipID { get; set; }
        public string[] NotTipi { get; set; }

        public NotTiplari()
        {
            NotTipID = new int[3];
            NotTipi = new string[3];

            NotTipID[0] = 1; NotTipi[0] = "AA BA BB CB CC DC DD FD FF";
            NotTipID[1] = 2; NotTipi[1] = "A A- B+ B B- C+ C C- D+ D F";
            NotTipID[2] = 3; NotTipi[2] = "A+ A B+ B C+ C D+ D F";
        }

        public string NotDonustur(int mevcutTipID, int karsiTipID, string karsiNotu)
        {           
            Notlar mevcutNot = new Notlar(mevcutTipID);
            Notlar karsiNot = new Notlar(karsiTipID);

            double karsiKatsayisi = 0;
            for (int i = 0; i < karsiNot.Not.Length; i++)
            {
                if (karsiNotu==karsiNot.Not[i])
                {
                    karsiKatsayisi = karsiNot.NotKatsayiUst[i];
                    break;
                }
            }       

            string donusmusNot = "";

            for (int i = 0; i < mevcutNot.Not.Length; i++)
            {
                if (karsiKatsayisi>mevcutNot.NotKatsayiUst[i])
                {
                    if(i>0) donusmusNot = mevcutNot.Not[i-1].ToString();
                    else donusmusNot = mevcutNot.Not[i].ToString();
                    break;
                }
            }

            return donusmusNot;
        }

        public string CokluNotDonustur(int mevcutTipID, int karsiTipID, double toplamKatsayi, int dersSayisi)
        {
            string donusmusNot = "";
            double karsiKatsayi = toplamKatsayi / dersSayisi;

           Notlar notMuvcut = new Notlar(mevcutTipID);

                for (int i = 0; i < notMuvcut.Not.Length; i++)
                {
                    if (karsiKatsayi>notMuvcut.NotKatsayiUst[i])
                    {
                        if (i > 0) donusmusNot = notMuvcut.Not[i - 1].ToString();
                        else donusmusNot = notMuvcut.Not[i].ToString();
                        break;
                    }
                }        

            return donusmusNot;
        }
    }
}