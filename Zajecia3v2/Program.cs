using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
//using Microsoft.EntityFrameworkCore;

namespace Zajecia3v2
{
    public class Customer
    {
        public int customerID { get; set; }
        public string customerName { get; set; }

        public string phone { get; set; }

    }


    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }

    public class CustomerContext2 : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .Property(x => x.customerID)
                .IsRequired();


            modelBuilder.Entity<Customer>()
                .Property(x => x.customerName)
                .HasMaxLength(255);

            modelBuilder.Entity<Customer>()
                .Property(x => x.phone)
                .HasMaxLength(255);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CustomerContext())
            {
                //CODE FIRST   
                //Dodaj do bazy
                Customer x = new Customer { customerID = 1, customerName = "Artur", phone = "888888888" };
                db.Customers.Add(x);
                db.SaveChanges();

                //Pobierz z bazy
                var query = from x1 in db.Customers
                            orderby x1.customerID
                            select x1;

                Console.WriteLine("Klienci:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.customerID + " " + item.customerName + " " + item.phone);
                }

                //DATABASE FIRST


                using (var db2 = new CustomerContext2())
                {
                    var result = (from a in db2.Customers
                                  select new { a.customerID, a.customerName, a.phone }).OrderBy(xx => xx.customerID).ToList();

                    foreach (var item in result)
                    {
                        Console.WriteLine(item.customerID + " " + item.customerName + " " + item.phone);
                    }

                }
                Console.ReadLine();
            }
        }

    }
}