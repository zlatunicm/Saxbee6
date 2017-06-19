using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class VrstaPorijekla
    {
        public VrstaPorijekla()
        {
            Porijeklo = new HashSet<Porijeklo>();
        }

        public int IdVrstaPorijekla { get; set; }
        public string OpisPorijekla { get; set; }
        public string KlasaPorijekla { get; set; }

        public virtual ICollection<Porijeklo> Porijeklo { get; set; }
    }
}
