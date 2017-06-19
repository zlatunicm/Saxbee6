using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SaxBee6.ViewModels
    {
        public class PcelinjakViewModel
        {
           
            public int IdPcelinjak { get; set; }
            public string OznakaPcelinjaka { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public int? IdGeoPolozajFk { get; set; }
            public int? IdPcelaraFkVlasnik { get; set; }
           

        }
    }



