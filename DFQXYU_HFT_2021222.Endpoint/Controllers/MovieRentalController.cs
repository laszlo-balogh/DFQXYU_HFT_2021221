using DFQXYU_HFT_2021221.Logic;
using DFQXYU_HFT_2021221.Models;
using DFQXYU_HFT_2021222.Endpoint.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieRentalController : ControllerBase
    {
        IMovieReantalLogic rentalLogic;
        IHubContext<SignalRHub> hub;
        public MovieRentalController(IMovieReantalLogic rentalLogic, IHubContext<SignalRHub> hub)
        {
            this.rentalLogic = rentalLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("MovieRentalCreated", rental);
        }

        [HttpPut]
        public void Put([FromBody] MovieRental rental)
        {
            rentalLogic.Update(rental);
            this.hub.Clients.All.SendAsync("MovieRentalUpdated", rental);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var rentalToDelete = rentalLogic.Read(id);
            rentalLogic.Delete(id);
            this.hub.Clients.All.SendAsync("MovieRentalDeleted", rentalToDelete);
        }
    }
}