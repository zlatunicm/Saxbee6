using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saxbee6.ViewModels
{
    public class PzajednicaViewModel
    {
        public int IdZajednice { get; set; }
        public string OznakaZajednice { get; set; }
        public int? BrojUlica { get; set; }
        public bool? Uzmiljena { get; set; }
        public string Opis { get; set; }
        public int? IdIzvjescaFk { get; set; }
        public int? IdPorijekloFk { get; set; }
        public int? IdMaticeFk { get; set; }
        public int? IdOkviriZajedniceFk { get; set; }
        public int? IdPcelinjakaFk { get; set; }
        public int? IdVrstaKosniceFk { get; set; }
    }
}
