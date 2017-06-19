using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class VrstaRizvjesca
    {
        public VrstaRizvjesca()
        {
            RadnoIzvjesce = new HashSet<RadnoIzvjesce>();
        }

        public int IdVrstaIzvjesca { get; set; }
        public string TipIzvjesca { get; set; }
        public string OpisIzvjesca { get; set; }
        public int? IdKategorizacijaStavkiIzvjescaFk { get; set; }

        public virtual ICollection<RadnoIzvjesce> RadnoIzvjesce { get; set; }
        public virtual KategorizacijaStavkiIzvjesca IdKategorizacijaStavkiIzvjescaFkNavigation { get; set; }
    }
}
