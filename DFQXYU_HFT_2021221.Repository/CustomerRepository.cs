using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        MovieDbContext db;
        public CustomerRepository(MovieDbContext db)
        {
            this.db = db;
        }
        public void Create(Customer customer)
        {
            db.Add(customer);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Remove(Read(id));
            db.SaveChanges();
        }

        public Customer Read(int id)
        {
            return db.Customers.FirstOrDefault(t => t.CustomerID == id);
        }

        public IQueryable<Customer> ReadAll()
        {
            return db.Customers;
        }

        public void Update(Customer customer)
        {
            var oldCustomer = Read(customer.CustomerID);
            oldCustomer.BornDate = customer.BornDate;
            oldCustomer.Email = customer.Email;
            oldCustomer.Name = customer.Name;
            oldCustomer.PhoneNumber = customer.PhoneNumber;
            oldCustomer.Rentals = customer.Rentals;
            db.SaveChanges();
        }
    }
}
