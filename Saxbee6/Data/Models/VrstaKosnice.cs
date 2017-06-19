using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class VrstaKosnice
    {
        public VrstaKosnice()
        {
            PZajednica = new HashSet<PZajednica>();
        }

        public int IdVrstaKosnice { get; set; }
        public string OznakaVrsteKosnice { get; set; }
        public string Naziv { get; set; }
        public bool? Aktivan { get; set; }
        public bool? Nastavljaca { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<PZajednica> PZajednica { get; set; }
    }
}
