using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class Rollar
    {
        public int[] RolID { get; set; }
        public string[] Rol { get; set; }
        public string[] RolAciklama { get; set; }

        public Rollar()
        {
            RolID = new int[6];
            Rol = new string[6];
            RolAciklama = new string[6];

            RolID[0] = 1; Rol[0] = "Komisyon Başkanı"; RolAciklama[0] = "Açıklama";
            RolID[1] = 2; Rol[1] = "Komisyon 1.Üye"; RolAciklama[1] = "Açıklama";
            RolID[2] = 3; Rol[2] = "Komisyon 2.Üye"; RolAciklama[2] = "Açıklama";
            RolID[3] = 4; Rol[3] = "Danışman"; RolAciklama[3] = "Açıklama";
            RolID[4] = 5; Rol[4] = "Öğretim Üyesi"; RolAciklama[3] = "Açıklama";
            RolID[5] = 6; Rol[5] = "Öğrenci"; RolAciklama[4] = "Açıklama";
          
        }
    }

 
}