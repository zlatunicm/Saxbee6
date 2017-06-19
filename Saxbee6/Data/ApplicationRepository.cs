using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaxBee6.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaxBee6.Data
{
    public class ApplicationRepository: IApplicationRepository
    {

        private SaxBeeDBContext _context;

        public ApplicationRepository(SaxBeeDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Pcelinjak> SviPcelinjaci()
        {

            return _context.Pcelinjak.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public IEnumerable<PZajednica> GetZajednicaById(int id)
        {
            return _context.PZajednica.FromSql("SELECT * FROM dbo.[P.Zajednica] WHERE ID_Pcelinjaka_FK={0}", id)
                .ToList();
            //SELECT* FROM dbo.[P.Zajednica] WHERE ID_Pcelinjaka_FK = 1;
        }

        public IEnumerable<Pcelinjak> GetPcelinjakById(int id)
        {
            return _context.Pcelinjak.FromSql("SELECT * FROM dbo.[Pcelinjak] WHERE ID_Pcelinjak={0}", id)
                .ToList();
            //SELECT* FROM dbo.[P.Zajednica] WHERE ID_Pcelinjaka_FK = 1;
        }

        public void UpdatePcelinjak(Pcelinjak p)
        {
            var staripcelinjak = _context.Pcelinjak.Where(x => x.IdPcelinjak == p.IdPcelinjak).FirstOrDefault();
            staripcelinjak.Naziv = p.Naziv;
            staripcelinjak.Opis = p.Opis;
            staripcelinjak.OznakaPcelinjaka = p.OznakaPcelinjaka;
            _context.Pcelinjak.Update(staripcelinjak);
            _context.SaveChanges();


           //_context.Pcelinjak.FromSql($"UPDATE dbo.[pcelinjak] SET Oznaka_Pcelinjaka='{p.OznakaPcelinjaka}',Naziv='{p.Naziv}',Opis='{p.Opis}' WHERE ID_Pcelinjak={p.IdPcelinjak}");
        }

        public void AddPcelinjak(Pcelinjak pcelinjak)
        {
            _context.Add(pcelinjak);
        }

        public IEnumerable<object> PocetnaIzvjesca()
        {
            return _context.PZajednica.FromSql("SELECT dbo.radovi.Oznaka_Rada, dbo.Pcelar.Oznaka_Pcelar, dbo.Pcelinjak.Oznaka_Pcelinjaka , dbo.[P.Zajednica].Oznaka_Zajednice, dbo.Vrsta_RIzvjesca.Tip_Izvjesca FROM Radovi JOIN Pcelar ON Radovi.ID_Pcelar_FK = pcelar.ID_Pcelar JOIN Pcelinjak ON Radovi.ID_Pcelinjak_FK = Pcelinjak.ID_Pcelinjak JOIN[P.Zajednica] ON Radovi.ID_Zajednice_FK =[P.Zajednica].ID_Zajednice JOIN RadnoIzvjesce ON Radovi.ID_RadnoIzvjesce = RadnoIzvjesce.ID_Izvjesca JOIN Vrsta_RIzvjesca ON RadnoIzvjesce.ID_Vrsta_Izvjesca_FK = Vrsta_RIzvjesca.ID_Vrsta_Izvjesca ")
                .ToList();
        }
    }
}
