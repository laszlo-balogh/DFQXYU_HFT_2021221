using DFQXYU_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCRUDController : ControllerBase
    {
        IMovieReantalLogic logic;
        public NonCRUDController(IMovieReantalLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<object> RentalsByCustomerNames()
        {
            return logic.RentalsByCustomerNames();
        }
        [HttpGet]
        public IEnumerable<object> RentalsWithBefore2000()
        {
            return logic.RentalsWithBefore2000();
        }
        [HttpGet]
        public IEnumerable<object> RentalsWithIsRegularCustomer()
        {
            return logic.RentalsWithIsRegularCustomer();
        }
        [HttpGet]
        public IEnumerable<object> RentalsWithJamesCameronMovies()
        {
            return logic.RentalsWithJamesCameronMovies();
        }
        [HttpGet]
        public IEnumerable<object> RentalsWithNotJamesCameronAndCustomerBornDateIs2000()
        {
            return logic.RentalsWithNotJamesCameronAndCustomerBornDateIs2000();
        }
        [HttpGet]
        public IEnumerable<object> RentalsByLaci()
        {
            return logic.RentalsByLaci();
        }
        [HttpGet]
        public IEnumerable<object> RentalsCustomerBefore2000()
        {
            return logic.RentalsCustomerBefore2000();
        }
    }
}
