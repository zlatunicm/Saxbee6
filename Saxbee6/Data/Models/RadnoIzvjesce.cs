using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class RadnoIzvjesce
    {
        public RadnoIzvjesce()
        {
            PZajednica = new HashSet<PZajednica>();
            Radovi = new HashSet<Radovi>();
        }

        public int IdIzvjesca { get; set; }
        public string OznakaIzvjesca { get; set; }
        public int? IdVrstaIzvjescaFk { get; set; }

        public virtual ICollection<PZajednica> PZajednica { get; set; }
        public virtual ICollection<Radovi> Radovi { get; set; }
        public virtual VrstaRizvjesca IdVrstaIzvjescaFkNavigation { get; set; }
    }
}
