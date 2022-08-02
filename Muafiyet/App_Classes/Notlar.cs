using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class Notlar
    {
        public int[] NotID { get; set; }
        public string[] Not { get; set; }
        public double[] NotKatsayiUst { get; set; }

        public Notlar(int NotTipiID)
        {
            if (NotTipiID==1)
            {
                NotID = new int[11];
                Not = new string[11];
                NotKatsayiUst = new double[11];

                NotID[0] = 1; Not[0] = "AA"; NotKatsayiUst[0] = 4;
                NotID[1] = 2; Not[1] = "BA"; NotKatsayiUst[1] = 3.5;
                NotID[2] = 3; Not[2] = "BB"; NotKatsayiUst[2] = 3;
                NotID[3] = 4; Not[3] = "CB"; NotKatsayiUst[3] = 2.5;
                NotID[4] = 5; Not[4] = "CC"; NotKatsayiUst[4] = 2;
                NotID[5] = 6; Not[5] = "DC"; NotKatsayiUst[5] = 1.5;
                NotID[6] = 7; Not[6] = "DD"; NotKatsayiUst[6] = 1;
                NotID[7] = 8; Not[7] = "FD"; NotKatsayiUst[7] = 0.5;
                NotID[8] = 9; Not[8] = "FF"; NotKatsayiUst[8] = 0;
                NotID[9] = 10; Not[9] = "G";  NotKatsayiUst[9] = -1;
                NotID[10] = 11; Not[10] = "K"; NotKatsayiUst[10] = -2;
            }
            else if (NotTipiID == 2)
            {
                NotID = new int[13];
                Not = new string[13];
                NotKatsayiUst = new double[13];

                NotID[0] = 1; Not[0] = "A"; NotKatsayiUst[0] =4;
                NotID[1] = 2; Not[1] = "A-"; NotKatsayiUst[1] = 3.7;
                NotID[2] = 3; Not[2] = "B+"; NotKatsayiUst[2] = 3.3;
                NotID[3] = 4; Not[3] = "B"; NotKatsayiUst[3] = 3;
                NotID[4] = 5; Not[4] = "B-"; NotKatsayiUst[4] = 2.7;
                NotID[5] = 6; Not[5] = "C+"; NotKatsayiUst[5] = 2.3;
                NotID[6] = 7; Not[6] = "C"; NotKatsayiUst[6] = 2;
                NotID[7] = 8; Not[7] = "C-"; NotKatsayiUst[7] = 1.7;
                NotID[8] = 9; Not[8] = "D+"; NotKatsayiUst[8] = 1.3;
                NotID[9] = 10; Not[9] = "D"; NotKatsayiUst[9] = 1;
                NotID[10] = 11; Not[10] = "F"; NotKatsayiUst[10] = 0;
                NotID[11] = 12; Not[11] = "G"; NotKatsayiUst[11] = -1;
                NotID[12] = 13; Not[12] = "K"; NotKatsayiUst[12] = -2;
            }
            else if (NotTipiID == 3)
            {
                NotID = new int[11];
                Not = new string[11];
                NotKatsayiUst = new double[11];

                NotID[0] = 1; Not[0] = "A+"; NotKatsayiUst[0] = 4;
                NotID[1] = 2; Not[1] = "A"; NotKatsayiUst[1] = 3.75;
                NotID[2] = 3; Not[2] = "B+"; NotKatsayiUst[2] = 3.5;
                NotID[3] = 4; Not[3] = "B"; NotKatsayiUst[3] = 3;
                NotID[4] = 5; Not[4] = "C+"; NotKatsayiUst[4] = 2.5;
                NotID[5] = 6; Not[5] = "C"; NotKatsayiUst[5] = 2;
                NotID[6] = 7; Not[6] = "D+"; NotKatsayiUst[6] = 1.5;
                NotID[7] = 8; Not[7] = "D"; NotKatsayiUst[7] = 1;
                NotID[8] = 9; Not[8] = "F"; NotKatsayiUst[8] = 0;
                NotID[9] = 10; Not[9] = "G"; NotKatsayiUst[9] = -1;
                NotID[10] = 11; Not[10] = "K"; NotKatsayiUst[10] = -2;
            }
        }
    }
}