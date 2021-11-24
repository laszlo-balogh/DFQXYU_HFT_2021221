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
        MovieLogic movieLogic;
        CustomerLogic customerLogic;
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
                new List<Customer>() { fakeCustomer, fakeCustomer2 }.AsQueryable()
                );
            movieRentalLogic = new MovieRentalLogic(mockMovieREntalRepository.Object,
                mockMovieRepository.Object, mockCustomerRepository.Object);
            movieLogic = new MovieLogic(mockMovieRepository.Object);
            customerLogic = new CustomerLogic(mockCustomerRepository.Object);
        }

        [TestCase(true, "Troy", 2004, "Wolfgang Petersen", "USA", 2000)]
        [TestCase(true, "D", 2004, "Wolfgang Petersen", "USA", null)]
        [TestCase(false, null, 2004, "Wolfgang Petersen", "USA", 2000)]
        [TestCase(false, "A", -2004, "Wolfgang Petersen", "USA", 2000)]
        [TestCase(false, "B", 20040, "Wolfgang Petersen", "USA", 2000)]
        [TestCase(false, "C", 2004, "Wolfgang Petersen", "USA", -2000)]
        [TestCase(false, "E", 2004, "Wolfgang Petersen", "USA", -200)]
        public void CreateMovieTest(bool result, string movieTitle, int year, string producer, string location, int price)
        {
            if (result)
            {
                Assert.That(
                    () =>
                    {
                        movieLogic.Create(
                            new Movie()
                            {
                                MovieTitle = movieTitle,
                                Year = year,
                                Producer = producer,
                                Location = location,
                                Price = price
                            });
                    }, Throws.Nothing
                    );
            }
            else
            {
                Assert.That(
                    () =>
                    {
                        movieLogic.Create(
                            new Movie()
                            {
                                MovieTitle = movieTitle,
                                Year = year,
                                Producer = producer,
                                Location = location,
                                Price = price
                            });
                    }, Throws.Exception
                    );
            }
        }


        [TestCase(true, "1Kiss Béla", "2000-11-22", "xyz@gmail.com", 203334567, false)]
        [TestCase(false, "", "2000-11-22", "xyz@gmail.com", 203334567, false)]
        [TestCase(false, null, "2000-11-22", "xyz@gmail.com", 203334567, false)]
        [TestCase(false, "2Kiss Béla", default, "xyz@gmail.com", 203334567, false)]
        [TestCase(false, "3Kiss Béla", "2000-11-11", "xyzgmail.com", 203334567, false)]
        [TestCase(false, "4Kiss Béla", "2000-11-11", "xyz@gmailcom", 203334567, false)]
        [TestCase(false, "5Kiss Béla", "2000-11-11", "xyz@gmail.com", -13334567, false)]
        [TestCase(false, "6Kiss Béla", "2000-11-11", "xyz@gmail.com", 13334567, false)]
        [TestCase(false, "7Kiss Béla", "2000-11-11", "xyz@gmail.com", null, false)]
        public void CreateCustomerTest(bool result, string name, DateTime bornDate, string email, int phoneNumber, bool regularCustomer)
        {
            if (result)
            {
                Assert.That(
                    () =>
                    {
                        customerLogic.Create(
                            new Customer()
                            {
                                Name = name,
                                BornDate = bornDate,
                                Email = email,
                                PhoneNumber = phoneNumber,
                                RegularCustomer = regularCustomer
                            });
                    }, Throws.Nothing
                    );
            }
            else
            {
                Assert.That(
                    () =>
                    {
                        customerLogic.Create(
                            new Customer()
                            {
                                Name = name,
                                BornDate = bornDate,
                                Email = email,
                                PhoneNumber = phoneNumber,
                                RegularCustomer = regularCustomer
                            });
                    }, Throws.Exception
                    );
            }
        }

        static Customer cc = new Customer()
        {
            Name = "Kiss Dániel",
            BornDate = new DateTime(1999, 08, 11),
            Email = "kiss.daniel@gmail.com",
            PhoneNumber = 203334567,
            RegularCustomer = false
        };

        [TestCase(false, null)]
        public void CreateRentalNullMovieTest(bool result, Movie movie)
        {
            if (result)
            {
                Assert.That(
                    () =>
                    {
                        movieRentalLogic.Create(
                            new MovieRental()
                            {
                                Movie = movie,
                                Customer = new Customer()
                                {
                                    Name = "Kiss Dániel",
                                    BornDate = new DateTime(1999, 08, 11),
                                    Email = "kiss.daniel@gmail.com",
                                    PhoneNumber = 203334567,
                                    RegularCustomer = false
                                }
                            });
                    }, Throws.Nothing
                    );

            }
            else
            {
                Assert.That(
                    () =>
                    {
                        movieRentalLogic.Create(
                           new MovieRental()
                           {
                               Movie = movie,
                               Customer = new Customer()
                               {
                                   Name = "Kiss Dániel",
                                   BornDate = new DateTime(1999, 08, 11),
                                   Email = "kiss.daniel@gmail.com",
                                   PhoneNumber = 203334567,
                                   RegularCustomer = false
                               }

                           });
                    }, Throws.Exception
                    );
            }
        }
        [TestCase(false, null)]
        public void CreateRentalNullCustomerTest(bool result, Customer customer)
        {
            if (result)
            {
                Assert.That(
                    () =>
                    {
                        movieRentalLogic.Create(
                            new MovieRental()
                            {
                                Movie = new Movie()
                                {
                                    MovieTitle = "Troy",
                                    Year = 2004,
                                    Producer = "Wolfgang Petersen",
                                    Location = "USA",
                                    Price = 2000
                                },
                                Customer = customer
                            });
                    }, Throws.Nothing
                    );

            }
            else
            {
                Assert.That(
                    () =>
                    {
                        movieRentalLogic.Create(
                           new MovieRental()
                           {
                               Movie = new Movie()
                               {
                                   MovieTitle = "Troy",
                                   Year = 2004,
                                   Producer = "Wolfgang Petersen",
                                   Location = "USA",
                                   Price = 2000
                               },
                               Customer = customer

                           });
                    }, Throws.Exception
                    );
            }
        }
        [TestCase(null)]
        public void MovieIsNullTest(Movie movie)
        {
            Assert.That(
                () =>
                {
                    movieLogic.Create(movie);
                }, Throws.Exception
                );
        }
        [TestCase(null)]
        public void MovieRentalIsNullTest(MovieRental rental)
        {
            Assert.That(
                () =>
                {
                    movieRentalLogic.Create(rental);
                }, Throws.Exception
                );
        }
        [TestCase(null)]
        public void CustomerIsNullTest(Customer customer)
        {
            Assert.That(
                () =>
                {
                    customerLogic.Create(customer);
                }, Throws.Exception
                );
        }

        [Test]
        public void TestRentalsWithBefore2000()
        {
            var result = movieRentalLogic.RentalsWithBefore2000().ToArray();
            Assert.That(result[0], Is.EqualTo(new { 
            //new Movie() { MovieID = 1},
            }));
        }
    }

}
