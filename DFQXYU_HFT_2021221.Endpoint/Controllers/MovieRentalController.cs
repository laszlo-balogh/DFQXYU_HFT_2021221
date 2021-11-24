using DFQXYU_HFT_2021221.Logic;
using DFQXYU_HFT_2021221.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieRentalController : ControllerBase
    {
        IMovieReantalLogic rentalLogic;
        public MovieRentalController(IMovieReantalLogic rentalLogic)
        {
            this.rentalLogic = rentalLogic;
        }
        [HttpGet]
        public IEnumerable<MovieRental> Get()
        {
            return rentalLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public MovieRental Get(int id)
        {
            return rentalLogic.Read(id);
        }

        [HttpPost]
        public void Post([FromBody] MovieRental rental)
        {
            rentalLogic.Create(rental);
        }

        [HttpPut]
        public void Put([FromBody] MovieRental rental)
        {
            rentalLogic.Update(rental);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            rentalLogic.Delete(id);
        }
    }
}