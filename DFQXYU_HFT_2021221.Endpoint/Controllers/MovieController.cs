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
    public class MovieController : ControllerBase
    {
        IMovieLogic movieLogic;
        public MovieController(IMovieLogic movieLogic)
        {
            this.movieLogic = movieLogic;
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
        }

        [HttpPut]
        public void Put([FromBody] Movie movie)
        {
            movieLogic.Update(movie);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            movieLogic.Delete(id);
        }
    }
}
