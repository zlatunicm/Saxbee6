using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class PZajednica
    {
        public PZajednica()
        {
            Radovi = new HashSet<Radovi>();
        }

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

        public virtual ICollection<Radovi> Radovi { get; set; }
        public virtual RadnoIzvjesce IdIzvjescaFkNavigation { get; set; }
        public virtual Matica IdMaticeFkNavigation { get; set; }
        public virtual OkviriZajednice IdOkviriZajedniceFkNavigation { get; set; }
        public virtual Pcelinjak IdPcelinjakaFkNavigation { get; set; }
        public virtual VrstaKosnice IdVrstaKosniceFkNavigation { get; set; }
    }
}
