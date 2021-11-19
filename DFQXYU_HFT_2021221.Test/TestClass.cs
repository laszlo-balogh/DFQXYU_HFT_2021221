using DFQXYU_HFT_2021221.Data;
using DFQXYU_HFT_2021221.Repository;
using DFQXYU_HFT_2021221.Logic;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFQXYU_HFT_2021221.Models;

namespace DFQXYU_HFT_2021221.Test
{
    [TestFixture]
    public class TestClass
    {
        MovieRentalLogic movieRentalLogic;
        public TestClass()
        {
            Mock<IMovieRentalRepository> mockMovieREntalRepository = new Mock<IMovieRentalRepository>();
            Mock<IMovieRepository> mockMovieRepository = new Mock<IMovieRepository>();
            Mock<ICustomerRepository> mockCustomerRepository = new Mock<ICustomerRepository>();

            Movie fakeMovie = new Movie()
            {
                MovieTitle = "Troy",
                Year = 2004,
                Producer = "Wolfgang Petersen",
                Location = "USA",
                Price = 2000
            };
            Movie fakeMovie2 = new Movie()
            {
                MovieTitle = "Pirates of the Caribbean",
                Year = 2003,
                Producer = "Gore Verbinski",
                Location = "USA",
                Price = 4000
            };
            Customer fakeCustomer = new Customer()
            {
                Name = "Kiss Dániel",
                BornDate = new DateTime(1999, 08, 11),
                Email = "kiss.daniel@gmail.com",
                PhoneNumber = 203334567,
                RegularCustomer = false
            };
            Customer fakeCustomer2 = new Customer()
            {
                Name = "Nagy Géza",
                BornDate = new DateTime(2005, 07, 31),
                Email = "nagy.geza@gmail.com",
                PhoneNumber = 203334568,
                RegularCustomer = false
            };            

            mockMovieREntalRepository.Setup(
                t => t.Create(It.IsAny<MovieRental>())
            );
            mockMovieRepository.Setup(
                t => t.Create(It.IsAny<Movie>())
            );
            mockCustomerRepository.Setup(
                t => t.Create(It.IsAny<Customer>())
            );
            mockMovieREntalRepository.Setup(t => t.ReadAll()
            ).Returns(
                new List<MovieRental>()
                {
                    new MovieRental()
                    {
                        Promotions = false,
                        Movie=fakeMovie,
                        Customer=fakeCustomer
                    },
                    new MovieRental()
                    {
                        Promotions=false,
                        Movie=fakeMovie2,
                        Customer=fakeCustomer2
                    }
                }.AsQueryable()
                );
            mockMovieRepository.Setup(t => t.ReadAll()
            ).Returns(
                new List<Movie>() { fakeMovie, fakeMovie2 }.AsQueryable()
                );
            mockCustomerRepository.Setup(t => t.ReadAll()
            ).Returns(
                new List<Customer>() {fakeCustomer,fakeCustomer2 }.AsQueryable()
                );
            movieRentalLogic = new MovieRentalLogic(mockMovieREntalRepository.Object,
                mockMovieRepository.Object,mockCustomerRepository.Object);
        }

        [TestCase(true, "Troy",2004, "Wolfgang Petersen","USA",2000)]
        [TestCase(false, "Troy",2004, "Wolfgang Petersen","USA",2000)]
        public void CreateMovieTest(bool result,string movieTitle,int year,string producer, string location,int price)
        {

        }
    }
}
