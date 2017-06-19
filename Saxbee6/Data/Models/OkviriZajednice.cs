using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class OkviriZajednice
    {
        public OkviriZajednice()
        {
            PZajednica = new HashSet<PZajednica>();
        }

        public int IdOkviriZajednice { get; set; }
        public int? BrojOkviraZajednice { get; set; }
        public string OznakaOkvira { get; set; }
        public DateTime? DatumOkvira { get; set; }
        public int? VrstaOkviraFk { get; set; }

        public virtual ICollection<PZajednica> PZajednica { get; set; }
        public virtual VrsteOkvira VrstaOkviraFkNavigation { get; set; }
    }
}
