using Microsoft.AspNetCore.Mvc;
using SaxBee6.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaxBee6.Data
{
    public interface IApplicationRepository
    {
        IEnumerable<Pcelinjak> SviPcelinjaci();

        void AddPcelinjak(Pcelinjak pcelinjak);

        IEnumerable<Pcelinjak> GetPcelinjakById(int id);

        void UpdatePcelinjak (Pcelinjak p);

        Task<bool> SaveChangesAsync();

        IEnumerable<PZajednica> GetZajednicaById(int id);
       
        IEnumerable<object> PocetnaIzvjesca();
    }
}