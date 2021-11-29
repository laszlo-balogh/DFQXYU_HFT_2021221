using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTools;
using DFQXYU_HFT_2021221.Models;

namespace DFQXYU_HFT_2021221.Client
{
    class Menu
    {
        RestService restService;
        Movie mUpdate;
        Movie mCreate;
        Customer cUpdate;
        Customer cCreate;
        MovieRental rUpdate;
        MovieRental rCreate;
        Movie rMovieCreate;
        Customer rCustomerCreate;

        public Menu(RestService restService)
        {
            this.restService = restService;
            mUpdate = new Movie();
            mCreate = new Movie();
            cUpdate = new Customer();
            cCreate = new Customer();
            rUpdate = new MovieRental();
            rCreate = new MovieRental();
            rMovieCreate = new Movie();
            rCustomerCreate = new Customer();

        }

        public void Start()
        {
            var menu = new ConsoleMenu()
              .Add("List all ...", () => new ConsoleMenu()
                .Add("List all Movies", () => ListItems<Movie>(restService.Get<Movie>("movie")))
                .Add("List all Customers", () => ListItems<Customer>(restService.Get<Customer>("customer")))
                .Add("List all Rentals", () => ListItems<MovieRental>(restService.Get<MovieRental>("movierental")))
                .Add("Back", ConsoleMenu.Close).Show())
              .Add("One of them ...", () => new ConsoleMenu()
                .Add("Movie", () => OneItem("movie"))
                .Add("Customer", () => OneItem("customer"))
                .Add("Rental", () => OneItem("rental"))
                .Add("Back", ConsoleMenu.Close).Show())
              .Add("Update ...", () => new ConsoleMenu()
                .Add("Movie", () => new ConsoleMenu()
                    .Add("ID", () => UpdateItemM("ID"))
                    .Add("Title", () => UpdateItemM("Title"))
                    .Add("Year", () => UpdateItemM("Year"))
                    .Add("Producer", () => UpdateItemM("Producer"))
                    .Add("Location", () => UpdateItemM("Location"))
                    .Add("Price", () => UpdateItemM("Price"))
                    .Add("Update", () => UpdateItemM("Update"))
                    .Add("Back", ConsoleMenu.Close).Show())
                .Add("Customer", () => new ConsoleMenu()
                    .Add("ID", () => UpdateItemC("ID"))
                    .Add("Name", () => UpdateItemC("Name"))
                    .Add("BornDate", () => UpdateItemC("BornDate"))
                    .Add("Email", () => UpdateItemC("Email"))
                    .Add("PhoneNumber", () => UpdateItemC("PhoneNumber"))
                    .Add("RegularCustomer", () => UpdateItemC("RegularCustomer"))
                    .Add("Update", () => UpdateItemC("Update"))
                    .Add("Back", ConsoleMenu.Close).Show())
                .Add("Rental", () => new ConsoleMenu()
                .Add("ID", () => UpdateItemR("ID"))
                    .Add("StartDate", () => UpdateItemR("StartDate"))
                    .Add("EndDate", () => UpdateItemR("EndDate"))
                    .Add("MovieID", () => UpdateItemR("MovieID"))
                    .Add("CustomerID", () => UpdateItemR("CustomerID"))
                    .Add("Promotions", () => UpdateItemR("Promotions"))
                    .Add("Update", () => UpdateItemR("Update"))
                    .Add("Back", ConsoleMenu.Close).Show())
                .Add("Back", ConsoleMenu.Close).Show())
                .Add("Create ...", () => new ConsoleMenu()
                .Add("Movie", () => new ConsoleMenu()                    
                    .Add("Title", () => CreateItemM("Title"))
                    .Add("Year", () => CreateItemM("Year"))
                    .Add("Producer", () => CreateItemM("Producer"))
                    .Add("Location", () => CreateItemM("Location"))
                    .Add("Price", () => CreateItemM("Price"))
                    .Add("Create", () => CreateItemM("Create"))
                    .Add("Back", ConsoleMenu.Close).Show())
                .Add("Customer", () => new ConsoleMenu()
                    .Add("Name", () => CreateItemC("Name"))
                    .Add("BornDate", () => CreateItemC("BornDate"))
                    .Add("Email", () => CreateItemC("Email"))
                    .Add("PhoneNumber", () => CreateItemC("PhoneNumber"))
                    .Add("RegularCustomer", () => CreateItemC("RegularCustomer"))
                    .Add("Create", () => CreateItemC("Create"))
                    .Add("Back", ConsoleMenu.Close).Show())
                .Add("Rental", () => new ConsoleMenu()                
                    .Add("StartDate", () => CreateItemR("StartDate"))
                    .Add("EndDate", () => CreateItemR("EndDate"))
                    .Add("MovieID", () => CreateItemR("MovieID"))                  
                    .Add("CustomerID", () => CreateItemR("CustomerID"))                   
                    .Add("Promotions", () => CreateItemR("Promotions"))
                    .Add("Create", () => CreateItemR("Create"))
                    .Add("Back", ConsoleMenu.Close).Show())                 
                .Add("Back", ConsoleMenu.Close).Show())
                .Add("Delete ...", () => new ConsoleMenu()
                 .Add("Movie", () => DeleteItem("movie"))
                 .Add("Customer", () => DeleteItem("customer"))
                 .Add("Rental", () => DeleteItem("rental"))
                 .Add("Back", ConsoleMenu.Close).Show())
                .Add("Non-Cruds", () => new ConsoleMenu()
                .Add("RentalsByCustomerNames", () =>NonCrud("RentalsByCustomerNames"))
                .Add("RentalsWithIsRegularCustomer",()=>NonCrud("RentalsWithIsRegularCustomer"))
                .Add("RentalsWithBefore2000", ()=>NonCrud("RentalsWithBefore2000"))
                .Add("RentalsWithJamesCameronMovies", ()=>NonCrud("RentalsWithJamesCameronMovies"))
                .Add("RentalsWithNotJamesCameronAndCustomerBornDateIs2000", ()=>NonCrud("RentalsWithNotJamesCameronAndCustomerBornDateIs2000"))
                .Add("RentalsByLaci", ()=>NonCrud("RentalsByLaci"))
                .Add("RentalsCustomerBefore2000", ()=>NonCrud("RentalsCustomerBefore2000"))
                .Add("Back",ConsoleMenu.Close).Show())
              .Add("Exit", ConsoleMenu.Close);           

            menu.Show();

        }

        public void ListItems<T>(List<T> items)
        {
            Console.Clear();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        public void OneItem(string item)
        {
            int id = 0;
            Console.Clear();
            if (item == "movie")
            {
                Console.WriteLine("MovieID: ");
                id = int.Parse(Console.ReadLine());
                Console.WriteLine(restService.Get<Movie>(id, "movie"));
            }
            else if (item == "customer")
            {
                Console.WriteLine("CustomerID: ");
                id = int.Parse(Console.ReadLine());
                Console.WriteLine(restService.Get<Customer>(id, "customer"));
            }
            else
            {
                Console.WriteLine("MovieRentalID: ");
                id = int.Parse(Console.ReadLine());
                Console.WriteLine(restService.Get<MovieRental>(id, "movierental"));
            }
            Console.ReadLine();
        }
        public void DeleteItem(string item)
        {
            int id = 0;
            Console.Clear();
            if (item == "movie")
            {
                Console.WriteLine("MovieID: ");
                id = int.Parse(Console.ReadLine());
                restService.Delete(id, "movie");
            }
            else if (item == "customer")
            {
                Console.WriteLine("CustomerID: ");
                id = int.Parse(Console.ReadLine());
                restService.Delete(id, "customer");
            }
            else
            {
                Console.WriteLine("MovieRentalID: ");
                id = int.Parse(Console.ReadLine());
                restService.Delete(id, "movierental");
            }
        }

        public void UpdateItemM(string property)
        {
            Console.Clear();
            if (property == "ID")
            {
                Console.WriteLine($"MovieID: {mUpdate.MovieID}\r");
                Console.Write("New ID: ");
                int id = int.Parse(Console.ReadLine());
                mUpdate.MovieID = id;

            }
            else if (property == "Title")
            {
                Console.WriteLine($"Title: {mUpdate.MovieTitle}\r");
                Console.Write("New Title: ");
                string t = Console.ReadLine();
                mUpdate.MovieTitle = t;
            }
            else if (property == "Year")
            {
                Console.WriteLine($"Year: {mUpdate.Year}\r");
                Console.Write("New Year: ");
                int y = int.Parse(Console.ReadLine());
                mUpdate.Year = y;
            }
            else if (property == "Producer")
            {
                Console.WriteLine($"Producer: {mUpdate.Producer}\r");
                Console.Write("New Producer: ");
                string p = Console.ReadLine();
                mUpdate.Producer = p;
            }
            else if (property == "Location")
            {
                Console.WriteLine($"Location: {mUpdate.Location}\r");
                Console.Write("New Location: ");
                string l = Console.ReadLine();
                mUpdate.Location = l;
            }
            else if (property == "Price")
            {
                Console.WriteLine($"Price {mUpdate.Price}\r");
                Console.Write($"New Price: ");
                int pri = int.Parse(Console.ReadLine());
                mUpdate.Price = pri;
            }
            else
            {
                restService.Put<Movie>(mUpdate, "movie");
            }

        }
        public void UpdateItemC(string property)
        {
            Console.Clear();
            if (property == "ID")
            {
                Console.WriteLine($"CustomerID: {cUpdate.CustomerID}\r");
                Console.Write("New ID: ");
                int id = int.Parse(Console.ReadLine());
                cUpdate.CustomerID = id;

            }
            else if (property == "Name")
            {
                Console.WriteLine($"Name: {cUpdate.Name}\r");
                Console.Write("New Name: ");
                string t = Console.ReadLine();
                cUpdate.Name = t;
            }
            else if (property == "BornDate")
            {
                Console.WriteLine($"BornDate: {cUpdate.BornDate}\r");
                Console.Write("New BornDate: ");
                string[] year = Console.ReadLine().Split("-");
                DateTime y = new DateTime(int.Parse(year[0]), int.Parse(year[1]), int.Parse(year[2]));
                cUpdate.BornDate = y;
            }
            else if (property == "Email")
            {
                Console.WriteLine($"Email: {cUpdate.Email}\r");
                Console.Write("New Email: ");
                string p = Console.ReadLine();
                cUpdate.Email = p;
            }
            else if (property == "PhoneNumber")
            {
                Console.WriteLine($"PhoneNumber: {cUpdate.PhoneNumber}\r");
                Console.Write("New PhoneNumber: ");
                int l = int.Parse(Console.ReadLine());
                cUpdate.PhoneNumber = l;
            }
            else if (property == "RegularCustomer")
            {
                Console.WriteLine($"RegularCustomer {cUpdate.RegularCustomer}\r");
                Console.Write($"New Price: ");
                bool pri = bool.Parse(Console.ReadLine());
                cUpdate.RegularCustomer = pri;
            }
            else
            {
                restService.Put<Customer>(cUpdate, "customer");
            }

        }
        public void UpdateItemR(string property)
        {
            Console.Clear();
            if (property == "ID")
            {
                Console.WriteLine($"RentalID: {rUpdate.RentalID}\r");
                Console.Write("New ID: ");
                int id = int.Parse(Console.ReadLine());
                rUpdate.RentalID = id;

            }
            else if (property == "StartDate")
            {
                Console.WriteLine($"StartDate: {rUpdate.StartDate}\r");
                Console.Write("New StartDate: ");
                string[] year = Console.ReadLine().Split("-");
                DateTime y = new DateTime(int.Parse(year[0]), int.Parse(year[1]), int.Parse(year[2]));
                rUpdate.StartDate = y;
            }
            else if (property == "EndDate")
            {
                Console.WriteLine($"EndDate: {rUpdate.EndDate}\r");
                Console.Write("New EndDate: ");
                string[] year = Console.ReadLine().Split("-");
                DateTime y = new DateTime(int.Parse(year[0]), int.Parse(year[1]), int.Parse(year[2]));
                rUpdate.EndDate = y;
            }
            else if (property == "MovieID")
            {
                Console.WriteLine($"MovieID: {rUpdate.MovieID}\r");
                Console.Write("New MovieID: ");
                int p = int.Parse(Console.ReadLine());
                rUpdate.MovieID = p;
            }
            else if (property == "CustomerID")
            {
                Console.WriteLine($"CustomerID: {rUpdate.CustomerID}\r");
                Console.Write("New CustomerID: ");
                int l = int.Parse(Console.ReadLine());
                rUpdate.CustomerID = l;
            }
            else if (property == "Promotions")
            {
                Console.WriteLine($"RegularCustomer {rUpdate.Promotions}\r");
                Console.Write($"New Price: ");
                bool pri = bool.Parse(Console.ReadLine());
                rUpdate.Promotions = pri;
            }
            else
            {
                restService.Put<MovieRental>(rUpdate, "movierental");
            }

        }
        public void CreateItemM(string property)
        {
            Console.Clear();
            if (property == "ID")
            {
                Console.WriteLine($"MovieID: {mCreate.MovieID}\r");
                Console.Write("New ID: ");
                int id = int.Parse(Console.ReadLine());
                mCreate.MovieID = id;

            }
            else if (property == "Title")
            {
                Console.WriteLine($"Title: {mCreate.MovieTitle}\r");
                Console.Write("New Title: ");
                string t = Console.ReadLine();
                mCreate.MovieTitle = t;
            }
            else if (property == "Year")
            {
                Console.WriteLine($"Year: {mCreate.Year}\r");
                Console.Write("New Year: ");
                int y = int.Parse(Console.ReadLine());
                mCreate.Year = y;
            }
            else if (property == "Producer")
            {
                Console.WriteLine($"Producer: {mCreate.Producer}\r");
                Console.Write("New Producer: ");
                string p = Console.ReadLine();
                mCreate.Producer = p;
            }
            else if (property == "Location")
            {
                Console.WriteLine($"Location: {mCreate.Location}\r");
                Console.Write("New Location: ");
                string l = Console.ReadLine();
                mCreate.Location = l;
            }
            else if (property == "Price")
            {
                Console.WriteLine($"Price {mCreate.Price}\r");
                Console.Write($"New Price: ");
                int pri = int.Parse(Console.ReadLine());
                mCreate.Price = pri;
            }
            else
            {
                restService.Post<Movie>(mCreate, "movie");
            }

        }
        public void CreateItemC(string property)
        {
            Console.Clear();
            if (property == "ID")
            {
                Console.WriteLine($"CustomerID: {cCreate.CustomerID}\r");
                Console.Write("New ID: ");
                int id = int.Parse(Console.ReadLine());
                cCreate.CustomerID = id;

            }
            else if (property == "Name")
            {
                Console.WriteLine($"Name: {cCreate.Name}\r");
                Console.Write("New Name: ");
                string t = Console.ReadLine();
                cCreate.Name = t;
            }
            else if (property == "BornDate")
            {
                Console.WriteLine($"BornDate: {cCreate.BornDate}\r");
                Console.Write("New BornDate: ");
                string[] year = Console.ReadLine().Split("-");
                DateTime y = new DateTime(int.Parse(year[0]), int.Parse(year[1]), int.Parse(year[2]));
                cCreate.BornDate = y;
            }
            else if (property == "Email")
            {
                Console.WriteLine($"Email: {cCreate.Email}\r");
                Console.Write("New Email: ");
                string p = Console.ReadLine();
                cCreate.Email = p;
            }
            else if (property == "PhoneNumber")
            {
                Console.WriteLine($"PhoneNumber: {cCreate.PhoneNumber}\r");
                Console.Write("New PhoneNumber: ");
                int l = int.Parse(Console.ReadLine());
                cCreate.PhoneNumber = l;
            }
            else if (property == "RegularCustomer")
            {
                Console.WriteLine($"RegularCustomer {cCreate.RegularCustomer}\r");
                Console.Write($"New Price: ");
                bool pri = bool.Parse(Console.ReadLine());
                cCreate.RegularCustomer = pri;
            }
            else
            {
                restService.Post<Customer>(cCreate, "customer");
            }

        }
        public void CreateItemR(string property)
        {
            Console.Clear();
            if (property == "ID")
            {
                Console.WriteLine($"RentalID: {rCreate.RentalID}\r");
                Console.Write("New ID: ");
                int id = int.Parse(Console.ReadLine());
                rCreate.RentalID = id;

            }
            else if (property == "StartDate")
            {
                Console.WriteLine($"StartDate: {rCreate.StartDate}\r");
                Console.Write("New StartDate: ");
                string[] year = Console.ReadLine().Split("-");
                DateTime y = new DateTime(int.Parse(year[0]), int.Parse(year[1]), int.Parse(year[2]));
                rCreate.StartDate = y;
            }
            else if (property == "EndDate")
            {
                Console.WriteLine($"EndDate: {rCreate.EndDate}\r");
                Console.Write("New EndDate: ");
                string[] year = Console.ReadLine().Split("-");
                DateTime y = new DateTime(int.Parse(year[0]), int.Parse(year[1]), int.Parse(year[2]));
                rCreate.EndDate = y;
            }
            else if (property == "MovieID")
            {
                Console.WriteLine($"MovieID: {rCreate.MovieID}\r");
                Console.Write("New MovieID: ");
                int p = int.Parse(Console.ReadLine());
                rCreate.MovieID = p;
            }
            else if (property == "CustomerID")
            {
                Console.WriteLine($"CustomerID: {rCreate.CustomerID}\r");
                Console.Write("New CustomerID: ");
                int l = int.Parse(Console.ReadLine());
                rCreate.CustomerID = l;
            }
            else if (property == "Promotions")
            {
                Console.WriteLine($"RegularCustomer {rCreate.Promotions}\r");
                Console.Write($"New Price: ");
                bool pri = bool.Parse(Console.ReadLine());
                rCreate.Promotions = pri;
            }
            else
            {
                //rCreate.Movie = rMovieCreate;
                //rCreate.Customer = rCustomerCreate;
                restService.Post<MovieRental>(rCreate, "movierental");
            }

        }
        public void NonCrud(string property)
        {
            Console.Clear();
            if (property== "RentalsByCustomerNames")
            {
                var v = restService.Get<object>("noncrud/RentalsByCustomerNames");
                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
            }
            else if (property== "RentalsWithIsRegularCustomer")
            {
                var v = restService.Get<object>("noncrud/RentalsWithIsRegularCustomer");
                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
            }
            else if (property== "RentalsWithBefore2000")
            {
                var v = restService.Get<object>("noncrud/RentalsWithBefore2000");
                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
            }
            else if (property== "RentalsWithJamesCameronMovies")
            {
                var v = restService.Get<object>("noncrud/RentalsWithJamesCameronMovies");
                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
            }
            else if (property== "RentalsWithNotJamesCameronAndCustomerBornDateIs2000")
            {
                var v = restService.Get<object>("noncrud/RentalsWithNotJamesCameronAndCustomerBornDateIs2000");
                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
            }
            else if (property== "RentalsByLaci")
            {
                var v = restService.Get<object>("noncrud/RentalsByLaci");
                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
            }
            else if (property== "RentalsCustomerBefore2000")
            {
                var v = restService.Get<object>("noncrud/RentalsCustomerBefore2000");
                foreach (var item in v)
                {
                    Console.WriteLine(item);
                }
            }
            Console.ReadLine();
        }   
    }
}
