using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class Porijeklo
    {
        public Porijeklo()
        {
            Matica = new HashSet<Matica>();
        }

        public int IdPorijeklo { get; set; }
        public string OznakaPorijekla { get; set; }
        public int? IdVrstaPorijeklaFk { get; set; }

        public virtual ICollection<Matica> Matica { get; set; }
        public virtual KategorizacijaStavkiIzvjesca IdVrstaPorijeklaFkNavigation { get; set; }
        public virtual VrstaPorijekla IdVrstaPorijeklaFk1 { get; set; }
    }
}
