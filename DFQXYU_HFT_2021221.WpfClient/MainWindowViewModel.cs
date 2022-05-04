using DFQXYU_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DFQXYU_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Movie> Movies { get; set; }
        public RestCollection<Customer> Customers { get; set; }
        public RestCollection<MovieRental> MovieRentals { get; set; }

        private Movie selectedMovie;
        private Customer selectedCustomer;
        private MovieRental selectedMovieRental;

        public Movie SelectedMovie
        {
            get { return selectedMovie; }
            set
            {
                if (value != null)
                {
                    selectedMovie = new Movie()
                    {
                        MovieID = value.MovieID,
                        MovieTitle = value.MovieTitle,
                        Producer = value.Producer,
                        Year = value.Year,
                        Location = value.Location,
                        Price = value.Price,
                    };
                    OnPropertyChanged();
                    (DeleteMovieCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                if (value != null)
                {
                    selectedCustomer = new Customer()
                    {                        
                        CustomerID = value.CustomerID,
                        Name = value.Name,
                        BornDate = value.BornDate,
                        Email = value.Email,
                        PhoneNumber = value.PhoneNumber,
                        RegularCustomer = value.RegularCustomer,
                        Rentals = value.Rentals,                        
                    };
                    OnPropertyChanged();
                    (DeleteCustomerCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public MovieRental SelectedMovieRental
        {
            get { return selectedMovieRental; }
            set
            {
                if (value != null)
                {
                    selectedMovieRental = new MovieRental()
                    {
                        RentalID = value.RentalID,
                        Customer = value.Customer,
                        CustomerID = value.CustomerID,
                        Movie = value.Movie,                        
                        MovieID = value.MovieID,
                        StartDate = value.StartDate,
                        EndDate = value.EndDate,
                        Promotions = value.Promotions                        
                    };
                    OnPropertyChanged();
                    (DeleteMovieRentalCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }



        public ICommand CreateMovieCommand { get; set; }
        public ICommand DeleteMovieCommand { get; set; }
        public ICommand UpdateMovieCommand { get; set; }

        public ICommand CreateCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }
        public ICommand UpdateCustomerCommand { get; set; }

        public ICommand CreateMovieRentalCommand { get; set; }
        public ICommand DeleteMovieRentalCommand { get; set; }
        public ICommand UpdateMovieReantalCommand { get; set; }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Movies = new RestCollection<Movie>("http://localhost:47417/", "movie", "hub");
                Customers = new RestCollection<Customer>("http://localhost:47417/", "customer", "hub");
                MovieRentals = new RestCollection<MovieRental>("http://localhost:47417/", "movierental", "hub");

                this.CreateMovieCommand = new RelayCommand(() =>
                {
                    Movies.Add(new Movie()
                    {
                        MovieID = SelectedMovie.MovieID,
                        MovieTitle = SelectedMovie.MovieTitle,
                        Producer = SelectedMovie.Producer,
                        Year = SelectedMovie.Year,
                        Location = SelectedMovie.Location,
                        Price = SelectedMovie.Price,
                    });
                });

                this.CreateCustomerCommand = new RelayCommand(() =>
                {
                    Customers.Add(new Customer()
                    {
                        CustomerID = SelectedCustomer.CustomerID,
                        Name = SelectedCustomer.Name,
                        BornDate = SelectedCustomer.BornDate,
                        Email = SelectedCustomer.Email,
                        PhoneNumber = SelectedCustomer.PhoneNumber,
                        RegularCustomer = SelectedCustomer.RegularCustomer,
                        Rentals = SelectedCustomer.Rentals,
                    });
                });

                this.CreateMovieRentalCommand = new RelayCommand(() =>
                {
                    MovieRentals.Add(new MovieRental()
                    {
                        RentalID = SelectedMovieRental.RentalID,
                        Customer = SelectedMovieRental.Customer,
                        CustomerID = SelectedMovieRental.CustomerID,
                        Movie = SelectedMovieRental.Movie,
                        MovieID = SelectedMovieRental.MovieID,
                        StartDate = SelectedMovieRental.StartDate,
                        EndDate = SelectedMovieRental.EndDate,
                        Promotions = SelectedMovieRental.Promotions
                    });
                });

                this.UpdateMovieCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Movies.Update(SelectedMovie);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                this.UpdateCustomerCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Customers.Update(SelectedCustomer);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                this.UpdateMovieReantalCommand = new RelayCommand(() =>
                {
                    try
                    {
                        MovieRentals.Update(SelectedMovieRental);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                this.DeleteMovieCommand = new RelayCommand(() =>
                {
                    Movies.Delete(SelectedMovie.MovieID);
                    SelectedMovie = new Movie();

                },
                () =>
                {
                    return SelectedMovie != null && SelectedMovie.MovieID != 0;
                });

                this.DeleteCustomerCommand = new RelayCommand(() =>
                {
                    Customers.Delete(SelectedCustomer.CustomerID);
                    SelectedCustomer = new Customer();

                },
                () =>
                {
                    return selectedCustomer != null && SelectedCustomer.CustomerID != 0;
                });

                this.DeleteMovieRentalCommand = new RelayCommand(() =>
                {
                    MovieRentals.Delete(SelectedMovieRental.RentalID);
                    SelectedMovieRental = new MovieRental();

                },
               () =>
               {
                   return SelectedMovieRental != null && SelectedMovieRental.RentalID != 0;
               });

            }
            SelectedMovie = new Movie();
            SelectedCustomer = new Customer();
            SelectedMovieRental = new MovieRental();
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

    }
}
