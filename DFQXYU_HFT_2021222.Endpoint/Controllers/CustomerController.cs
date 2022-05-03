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
    public class CustomerController : ControllerBase
    {
        ICustomerLogic customerLogic;
        IHubContext<SignalRHub> hub;
        public CustomerController(ICustomerLogic customerLogic, IHubContext<SignalRHub> hub)
        {
            this.customerLogic = customerLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("CustomerCreated", customer);
        }

        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            customerLogic.Update(customer);
            this.hub.Clients.All.SendAsync("CustomerUpdated", customer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var customerToDelete = this.customerLogic.Read(id);
            customerLogic.Delete(id);
            this.hub.Clients.All.SendAsync("CustomerDeleted", customerToDelete);
        }
    }
}


