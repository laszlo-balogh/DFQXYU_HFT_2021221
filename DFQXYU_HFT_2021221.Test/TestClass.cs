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
        Mock<IMovieRentalRepository> mockMovieREntalRepository;
        Mock<IMovieRepository> mockMovieRepository;
        Mock<ICustomerRepository> mockCustomerRepository;
        public TestClass()
        {
            mockMovieREntalRepository = new Mock<IMovieRentalRepository>(MockBehavior.Loose);
            mockMovieRepository = new Mock<IMovieRepository>(MockBehavior.Loose);
            mockCustomerRepository = new Mock<ICustomerRepository>(MockBehavior.Loose);

            Movie fakeMovie = new Movie()
            {
                MovieID = 1,
                MovieTitle = "Troy",
                Year = 1999, // 2004
                Producer = "Wolfgang Petersen",
                Location = "USA",
                Price = 2000
            };
            Movie fakeMovie2 = new Movie()
            {
                MovieID = 2,
                MovieTitle = "Pirates of the Caribbean",
                Year = 2003,
                Producer = "James Cameron", //Gore Verbinski
                Location = "USA",
                Price = 4000
            };
            Customer fakeCustomer = new Customer()
            {
                CustomerID = 1,
                Name = "Kiss Laci",
                BornDate = new DateTime(2000, 08, 11),
                Email = "kiss.daniel@gmail.com",
                PhoneNumber = 203334567,
                RegularCustomer = false
            };
            Customer fakeCustomer2 = new Customer()
            {
                CustomerID = 2,
                Name = "Nagy Géza",
                BornDate = new DateTime(1999, 07, 31),
                Email = "nagy.geza@gmail.com",
                PhoneNumber = 203334568,
                RegularCustomer = true
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
                        RentalID=1,
                        Promotions = false,
                        Movie=fakeMovie,
                        Customer=fakeCustomer,
                        MovieID=fakeMovie.MovieID,
                        CustomerID=fakeCustomer.CustomerID
                    },
                    new MovieRental()
                    {
                        RentalID=2,
                        Promotions=false,
                        Movie=fakeMovie2,
                        Customer=fakeCustomer2,
                        MovieID=fakeMovie2.MovieID,
                        CustomerID=fakeCustomer2.CustomerID

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
            mockMovieRepository.Setup(t => t.Read(1)).Returns(fakeMovie);
            mockMovieRepository.Setup(t => t.Delete(It.IsAny<int>()));


            mockCustomerRepository.Setup(t => t.Read(1)).Returns(fakeCustomer);
            mockCustomerRepository.Setup(t => t.Delete(It.IsAny<int>()));

            mockMovieREntalRepository.Setup(t=>t.Delete(It.IsAny<int>()));
            mockMovieREntalRepository.Setup(t=>t.Read(1)).Returns(new MovieRental()
            {
                RentalID = 1,
                Promotions = false,
                Movie = fakeMovie,
                Customer = fakeCustomer,
                MovieID = fakeMovie.MovieID,
                CustomerID = fakeCustomer.CustomerID
            });

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

        [TestCase(null)]
        public void CreateMovieIsNullTest(Movie movie)
        {
            Assert.That(
                () =>
                {
                    movieLogic.Create(movie);
                }, Throws.Exception
                );
        }
        [TestCase(null)]
        public void CreateMovieRentalIsNullTest(MovieRental rental)
        {
            Assert.That(
                () =>
                {
                    movieRentalLogic.Create(rental);
                }, Throws.Exception
                );
        }
        [TestCase(null)]
        public void CreateCustomerIsNullTest(Customer customer)
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
            ;

            Assert.That(result[0].GetHashCode(), Is.EqualTo(new { RentalID = 1, Name = "Troy", Year = 1999 }.GetHashCode()

            ));
        }

        [Test]
        public void TestRentalsWithNotJamesCameronAndCustomerBornDateIs2000()
        {
            var result = movieRentalLogic.RentalsWithNotJamesCameronAndCustomerBornDateIs2000().ToArray();
            Assert.That(result[0].GetHashCode(), Is.EqualTo(new { CustomerName = "Kiss Laci", RentalID = 1 }.GetHashCode()));
        }

        [Test]
        public void TestRentalsByLaci()
        {
            var result = movieRentalLogic.RentalsByLaci().ToArray();
            Assert.That(result[0].GetHashCode(), Is.EqualTo(new
            {
                RentalID = 1,
                Name = "Kiss Laci",
                Movie = "Troy"
            }.GetHashCode()
            ));
        }


        [Test]
        public void TestRentalsCustomerBefore2000()
        {
            var result = movieRentalLogic.RentalsCustomerBefore2000().ToArray();
            Assert.That(result[0].GetHashCode, Is.EqualTo(new
            {
                RentalID = 2,
                Name = "Nagy Géza",
                BornDate = new DateTime(1999, 07, 31),
                Movie = "Pirates of the Caribbean"
            }.GetHashCode()
            ));
        }

        [Test]
        public void TestRentalsWithJamesCameronMovies()
        {
            var result = movieRentalLogic.RentalsWithJamesCameronMovies().ToArray();
            Assert.That(result[0].GetHashCode(), Is.EqualTo(new
            {
                MovieID = 2,
                MovieTitle = "Pirates of the Caribbean",
                Year = 2003,
                Price = 4000,
                RentalID = 2
            }.GetHashCode()
            ));
        }

        [TestCase(true, 1, "Troy", 2004, "Wolfgang Petersen", "USA", 2000)]
        [TestCase(true, 1, "D", 2004, "Wolfgang Petersen", "USA", null)]
        [TestCase(false, 1, null, 2004, "Wolfgang Petersen", "USA", 2000)]
        [TestCase(false, 1, "A", -2004, "Wolfgang Petersen", "USA", 2000)]
        [TestCase(false, 1, "B", 20040, "Wolfgang Petersen", "USA", 2000)]
        [TestCase(false, 1, "C", 2004, "Wolfgang Petersen", "USA", -2000)]
        [TestCase(false, 1, "E", 2004, "Wolfgang Petersen", "USA", -200)]
        [TestCase(false, 100, "E", 2004, "Wolfgang Petersen", "USA", 2000)]
        public void UpdateMovieTest(bool result, int id, string movieTitle, int year, string producer, string location, int price)
        {
            if (result)
            {
                Assert.That(
                    () =>
                    {
                        movieLogic.Update(
                            new Movie()
                            {
                                MovieID = id,
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
                        movieLogic.Update(
                            new Movie()
                            {
                                MovieID = id,
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

        [TestCase(true, 1, "1Kiss Béla", "2000-11-22", "xyz@gmail.com", 203334567, false)]
        [TestCase(false, 1, "", "2000-11-22", "xyz@gmail.com", 203334567, false)]
        [TestCase(false, 1, null, "2000-11-22", "xyz@gmail.com", 203334567, false)]
        [TestCase(false, 1, "2Kiss Béla", default, "xyz@gmail.com", 203334567, false)]
        [TestCase(false, 1, "3Kiss Béla", "2000-11-11", "xyzgmail.com", 203334567, false)]
        [TestCase(false, 1, "4Kiss Béla", "2000-11-11", "xyz@gmailcom", 203334567, false)]
        [TestCase(false, 1, "5Kiss Béla", "2000-11-11", "xyz@gmail.com", -13334567, false)]        
        [TestCase(false, 1, "7Kiss Béla", "2000-11-11", "xyz@gmail.com", null, false)]
        [TestCase(false, 100, "8Kiss Béla", "2000-11-22", "xyz@gmail.com", 203334567, false)]
        public void UpdateCustomerTest(bool result, int id, string name, DateTime bornDate, string email, int phoneNumber, bool regularCustomer)
        {
            if (result)
            {
                Assert.That(
                    () =>
                    {
                        customerLogic.Update(
                            new Customer()
                            {
                                CustomerID = id,
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
                        customerLogic.Update(
                            new Customer()
                            {
                                CustomerID = id,
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

        [TestCase(1)]
        public void ReadMovieTest(int id)
        {
            var v = movieLogic.Read(1);
            this.mockMovieRepository.Verify(x => x.Read(id), Times.Once);
        }

        [TestCase(1)]
        public void ReadCustomerTest(int id)
        {             
            var v = customerLogic.Read(1);
            this.mockCustomerRepository.Verify(x => x.Read(id), Times.Once);
        }

        [TestCase(1)]
        public void DeleteMovieRentalTest(int id)
        {
            movieRentalLogic.Delete(id);
            this.mockMovieREntalRepository.Verify(x => x.Delete(id), Times.Once);
        }
        
    }
}
