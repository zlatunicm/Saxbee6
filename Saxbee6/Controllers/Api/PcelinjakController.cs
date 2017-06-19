using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Saxbee6.ViewModels;
using SaxBee6.Data;
using SaxBee6.Data.Models;
using SaxBee6.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaxBee6.Controllers.Api
{
    [Route("api/pcelinjak")]
    public class PcelinjakController : Controller
    {
        

        private IApplicationRepository _repository;

        public PcelinjakController(IApplicationRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("")]
        public IActionResult GetAllPcelinjak()
        {
            var results = _repository.SviPcelinjaci();
          
            return Ok(Mapper.Map<IEnumerable<PcelinjakViewModel>>(results));
         
        }

        [HttpGet("{id:int}")]
        public IActionResult GetZajednicaByPcelinjak(int id)
        {

            var results = _repository.GetZajednicaById(id);

            return Ok(Mapper.Map<IEnumerable<PzajednicaViewModel>>(results));

        }

        [HttpGet("jedan/{id:int}")]
        public IActionResult GetPcelinjakById(int id)
        {

            var results = _repository.GetPcelinjakById(id);

            return Ok(Mapper.Map<IEnumerable<PcelinjakViewModel>>(results));

        }


        [HttpPost("/update")]
        public async Task<IActionResult> UpdatePcelinjakById([FromBody]PcelinjakViewModel ThePcelinjak)
        {
            var updatePcelinjak = Mapper.Map<Pcelinjak>(ThePcelinjak);

            _repository.UpdatePcelinjak(updatePcelinjak);

            if (await _repository.SaveChangesAsync())
            {
                return Created($"api/pcelinjak/update/{updatePcelinjak.Naziv}", Mapper.Map<PcelinjakViewModel>(updatePcelinjak));
            }

            return BadRequest("Nema Pcelinjaka");

        }




        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]PcelinjakViewModel thePcelinjak)
        {
            var newPcelinjak = Mapper.Map<Pcelinjak>(thePcelinjak);

            _repository.AddPcelinjak(newPcelinjak);

            if(await _repository.SaveChangesAsync())
            {
                return Created($"api/pcelinjak/{thePcelinjak.Naziv}", Mapper.Map<PcelinjakViewModel>(newPcelinjak));
            }

            return BadRequest("Nema Pcelinjaka");
        }

    }
}
