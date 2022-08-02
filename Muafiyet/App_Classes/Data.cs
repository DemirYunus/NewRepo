using Muafiyet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Muafiyet.App_Classes
{
    public class Data
    {
        public static List<Universiteler> Universite { get; set; }
        public static List<Fakulteler> Fakulte { get; set; }
        public static List<Bolumler> Bolum { get; set; }
    }
}