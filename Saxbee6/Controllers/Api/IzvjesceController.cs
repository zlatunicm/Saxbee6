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

namespace Saxbee6.Controllers.Api
{
    [Route("api/izvjesce")]
    public class IzvjesceController:Controller
    {
        private IApplicationRepository _repository;

        public IzvjesceController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Izvjesca()
        {
            var results = _repository.PocetnaIzvjesca();

            return Ok(Mapper.Map<IEnumerable<pIzvjescaViewModel>>(results));
        }

    }
}
