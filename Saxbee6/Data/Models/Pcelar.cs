using System;
using System.Collections.Generic;

namespace SaxBee6.Data.Models
{
    public partial class Pcelar
    {
        public Pcelar()
        {
            Pcelinjak = new HashSet<Pcelinjak>();
            Radovi = new HashSet<Radovi>();
        }

        public int IdPcelar { get; set; }
        public string OznakaPcelar { get; set; }
        public string ImeiPrezime { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }

        public virtual ICollection<Pcelinjak> Pcelinjak { get; set; }
        public virtual ICollection<Radovi> Radovi { get; set; }
    }
}
