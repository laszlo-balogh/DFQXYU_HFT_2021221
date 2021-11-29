using DFQXYU_HFT_2021221.Models;
using DFQXYU_HFT_2021221;
using System;
using System.Linq;

using System.Collections.Generic;

namespace DFQXYU_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DFQXYU_HFT_2021221_Database.mdf;Integrated Security=True            

            RestService restService = new RestService("http://localhost:47417");

            Menu m = new Menu(restService);
            m.Start();
        }       
    }
}
