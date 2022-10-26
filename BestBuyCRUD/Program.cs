using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyCRUD
{
    public class Program
    {
        #region Configuration

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder() // this builds a configuration
                .SetBasePath(Directory.GetCurrentDirectory()) // setting a path from a file for the app settings. .GetCurrentDirectory is the method for the path.
                .AddJsonFile("appssettings.json") // telling which file to use.
                .Build(); // Building the configuration.

            string connString = config.GetConnectionString("DefaultConnection"); // Reading the contents of the appsettings.json AKA the connection string.
           IDbConnection conn = new MySqlConnection(connString); //IdbConnection is helping us connect to a database.


            #endregion
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);
            DapperProductRepository productRepository = new DapperProductRepository(conn);

            Console.WriteLine("Hello user. Here are the current departments:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();
            var depos = repo.GetAllDepartments(); // Will show you everything inside of departments.
            Print(depos); // Going to show you everything inside of the table.

            Console.WriteLine("Do you want to add a Department?");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What is the name of your new department?");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());// Showing you that you actually put a record in the department table.
            }

            Console.WriteLine("Here are the current products:");
            Console.WriteLine("Please press enter . . .");
            Console.ReadLine();
            var products = productRepository.GetAllProducts();
            Print2(products);

            Console.WriteLine("Do you want to add a new product?");
            string userAnswer = Console.ReadLine();

            if(userAnswer.ToLower() == "yes")
            {
                Console.WriteLine($"What is the new product name?");
                var prodName = Console.ReadLine();

                Console.WriteLine($"What is the new product's price?");
                var price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine($"What is the new product's category id?");
                var categoryID = Convert.ToInt32(Console.ReadLine());

                productRepository.CreateProduct(prodName, price, categoryID);
            }




            Console.WriteLine("Have a great day.");



            Console.ReadLine();

        }
        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"ID: {depo.DepartmentID} Name: {depo.Name}");
            }
        }

        private static void Print2(IEnumerable<Product> prods)
        {
            foreach(var product in prods)
            {
                Console.WriteLine($"ProductID: {product.ProductID} Name: {product.Name} Price: {product.Price} CategoryID: {product.CategoryID} OnSale: {product.OnSale} StockLevel {product.StockLevel}");
            }

        }
    }
}
