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
    public class MovieController : ControllerBase
    {
        IMovieLogic movieLogic;
        IHubContext<SignalRHub> hub;
        public MovieController(IMovieLogic movieLogic, IHubContext<SignalRHub> hub)
        {
            this.movieLogic = movieLogic;
            this.hub = hub;
        }
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return movieLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return movieLogic.Read(id);
        }

        [HttpPost]
        public void Post([FromBody] Movie movie)
        {
            movieLogic.Create(movie);
            this.hub.Clients.All.SendAsync("MovieCreated", movie);
        }

        [HttpPut]
        public void Put([FromBody] Movie movie)
        {
            movieLogic.Update(movie);
            this.hub.Clients.All.SendAsync("MovieUpdated", movie);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movieToDelete = this.movieLogic.Read(id);
            movieLogic.Delete(id);
            this.hub.Clients.All.SendAsync("MovieDeleted", movieToDelete);
        }
    }
}
