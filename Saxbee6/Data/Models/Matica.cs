using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class Matica
    {
        public Matica()
        {
            PZajednica = new HashSet<PZajednica>();
        }

        public int IdMatice { get; set; }
        public string OznakaMatice { get; set; }
        public string OpisMatice { get; set; }
        public DateTime? GodisteMatice { get; set; }
        public bool? Aktivna { get; set; }
        public int? IdPorijekloFk { get; set; }

        public virtual ICollection<PZajednica> PZajednica { get; set; }
        public virtual Porijeklo IdPorijekloFkNavigation { get; set; }
    }
}
