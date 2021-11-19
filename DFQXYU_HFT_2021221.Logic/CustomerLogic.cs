using DFQXYU_HFT_2021221.Models;
using DFQXYU_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFQXYU_HFT_2021221.Logic
{
    public class CustomerLogic : ICustomerLogic
    {
        ICustomerRepository customerRepo;

        public CustomerLogic(ICustomerRepository customerRepo)
        {
            this.customerRepo = customerRepo;
        }
        public void Create(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            else if (customer.Name == null)
            {
                throw new ArgumentException("Name cannot be null");
            }
            else if (customer.BornDate == default)
            {
                throw new ArgumentException("Born date cannot be empty");
            }
            else if (customer.Email == null)
            {
                throw new ArgumentException("Email cannot be null");
            }
            else if (customer.Email.Contains('@'))
            {
                string[] array = customer.Email.Split('@');
                if (array.Length > 2)
                {
                    throw new ArgumentException("Wrong email format");
                }
                else if (!array[1].Contains('.'))
                {
                    throw new ArgumentException("Wrong email format");
                }
            }
            else if (customer.PhoneNumber.ToString().Length < 9 || customer.PhoneNumber.ToString().Length > 9)
            {
                throw new ArgumentException("Wrong phone number format");
            }
            else if (customer.PhoneNumber<0)
            {
                throw new ArgumentException("Wrong phone number format");
            }
            else
            {
               this.customerRepo.Create(customer);
            }
        }

        public void Delete(int id)
        {
            if (id < 1)
            {
                throw new IndexOutOfRangeException("Minimum id value is 1");
            }
            this.customerRepo.Delete(id);
        }

        public Customer Read(int id)
        {
            if (id < 1)
            {
                throw new IndexOutOfRangeException("Minimum id value is 1");
            }
            return this.customerRepo.Read(id);
        }

        public IEnumerable<Customer> ReadAll()
        {
            return this.customerRepo.ReadAll();
        }

        public void Update(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer");
            }
            else if (customer.Name == null)
            {
                throw new ArgumentException("Name cannot be null");
            }
            else if (customer.BornDate == default)
            {
                throw new ArgumentException("Born date cnnot be empty");
            }
            else if (customer.Email == null)
            {
                throw new ArgumentException("Email cannot be null");
            }
            else if (customer.Email.Contains('@'))
            {
                string[] array = customer.Email.Split('@');
                if (array.Length > 2)
                {
                    throw new ArgumentException("Wrong email format");
                }
                else if (!array[1].Contains('.'))
                {
                    throw new ArgumentException("Wrong email format");
                }
            }
            else if (customer.PhoneNumber.ToString().Length < 9 || customer.PhoneNumber.ToString().Length > 9)
            {
                throw new ArgumentException("Wrong phone number format");
            }
            else if (customer.PhoneNumber < 0)
            {
                throw new ArgumentException("Wrong phone number format");
            }
            this.customerRepo.Update(customer);
        }
    }
}
