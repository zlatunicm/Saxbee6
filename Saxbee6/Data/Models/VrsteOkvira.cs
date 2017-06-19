using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class VrsteOkvira
    {
        public VrsteOkvira()
        {
            OkviriZajednice = new HashSet<OkviriZajednice>();
        }

        public int IdVrsteOkvira { get; set; }
        public string OpisOkvira { get; set; }

        public virtual ICollection<OkviriZajednice> OkviriZajednice { get; set; }
    }
}
