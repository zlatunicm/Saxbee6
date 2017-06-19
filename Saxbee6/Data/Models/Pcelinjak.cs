using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class Pcelinjak
    {
        public Pcelinjak()
        {
            PZajednica = new HashSet<PZajednica>();
            Radovi = new HashSet<Radovi>();
        }

        public int IdPcelinjak { get; set; }
        public string OznakaPcelinjaka { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int? IdGeoPolozajFk { get; set; }
        public int? IdPcelaraFkVlasnik { get; set; }

        public virtual ICollection<PZajednica> PZajednica { get; set; }
        public virtual ICollection<Radovi> Radovi { get; set; }
        public virtual GeoPolozaj IdGeoPolozajFkNavigation { get; set; }
        public virtual Pcelar IdPcelaraFkVlasnikNavigation { get; set; }
    }
}
