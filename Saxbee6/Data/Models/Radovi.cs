using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class Radovi
    {
        public int IdRadovi { get; set; }
        public string OznakaRada { get; set; }
        public DateTime? DatumPocetakRada { get; set; }
        public DateTime? DatumZavrsetakRada { get; set; }
        public int? IdPcelarFk { get; set; }
        public int? IdPcelinjakFk { get; set; }
        public int? IdZajedniceFk { get; set; }
        public int? IdRadnoIzvjesce { get; set; }

        public virtual Pcelar IdPcelarFkNavigation { get; set; }
        public virtual Pcelinjak IdPcelinjakFkNavigation { get; set; }
        public virtual RadnoIzvjesce IdRadnoIzvjesceNavigation { get; set; }
        public virtual PZajednica IdZajedniceFkNavigation { get; set; }
    }
}
