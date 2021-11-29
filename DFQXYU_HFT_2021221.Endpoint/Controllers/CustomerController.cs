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
    public class CustomerController : ControllerBase
    {
        ICustomerLogic customerLogic;
        public CustomerController(ICustomerLogic customerLogic)
        {
            this.customerLogic = customerLogic;
        }
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return customerLogic.ReadAll();
        }

        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return customerLogic.Read(id);
        }

        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            customerLogic.Create(customer);
        }

        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            customerLogic.Update(customer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            customerLogic.Delete(id);
        }
    }
}


