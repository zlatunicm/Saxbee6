using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class GeoPolozaj
    {
        public GeoPolozaj()
        {
            Pcelinjak = new HashSet<Pcelinjak>();
        }

        public int IdGeoPolozaj { get; set; }
        public int? GeografskaSirina { get; set; }
        public int? GeografskaDuzina { get; set; }

        public virtual ICollection<Pcelinjak> Pcelinjak { get; set; }
    }
}
