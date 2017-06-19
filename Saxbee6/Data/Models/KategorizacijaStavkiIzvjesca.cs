using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class KategorizacijaStavkiIzvjesca
    {
        public KategorizacijaStavkiIzvjesca()
        {
            Porijeklo = new HashSet<Porijeklo>();
            VrstaRizvjesca = new HashSet<VrstaRizvjesca>();
        }

        public int IdKategorizacijaStavkiIzvjesca { get; set; }
        public string OznakaKategorije { get; set; }
        public string Opis { get; set; }
        public string Napomena { get; set; }
        public int? IdPorijekloFk { get; set; }

        public virtual ICollection<Porijeklo> Porijeklo { get; set; }
        public virtual ICollection<VrstaRizvjesca> VrstaRizvjesca { get; set; }
    }
}
