namespace Kalokyris_George_Assignment_2.Migrations
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Kalokyris_George_Assignment_2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Kalokyris_George_Assignment_2.DAL.MyDatabase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Kalokyris_George_Assignment_2.DAL.MyDatabase context)
        {

            Category c1 = new Category() { Title = "IT" };
            Category c2 = new Category() { Title = "Cloud Services" };
            Category c3 = new Category() { Title = "Programming" };
            Category c4 = new Category() { Title = "Databases" };
            Category c5 = new Category() { Title = "UX" };
            Category c6 = new Category() { Title = "Project Management" };
            Category c7 = new Category() { Title = "Operation Systems" };
            Category c8 = new Category() { Title = "Mathematics & Statistics" };

            context.Categories.AddOrUpdate(x => x.Title, c1, c2, c3, c4, c5, c6, c7, c8);
           
            context.Trainers.AddOrUpdate(x => new { x.FirstName, x.LastName },

            new Trainer() { FirstName = "Michael", LastName = "Scott", HireDate = DateTime.Parse("2017-1-10"), isAvailable = true, Salary = 120000, Categories = new Collection <Category>() { c1, c2, c3 } },
            new Trainer() { FirstName = "Dwight", LastName = "Shrute", HireDate = DateTime.Parse("2017-2-3"), isAvailable = false, Salary = 90000, Categories = new Collection<Category>() { c4, c5, c6 } },
            new Trainer() { FirstName = "Pam", LastName = "Beasly", HireDate = DateTime.Parse("2016-12-19"), isAvailable = false, Salary = 50000, Categories = new Collection<Category>() { c7, c8 } },
            new Trainer() { FirstName = "Andy", LastName = "Bernard", HireDate = DateTime.Parse("2015-4-3"), isAvailable = true, Salary = 90000, Categories = new Collection<Category>() { c1, c2, c3 } },
            new Trainer() { FirstName = "Jim", LastName = "Halpert", HireDate = DateTime.Parse("2014-3-9"), isAvailable = true, Salary = 75000, Categories = new Collection<Category>() { c4, c5, c6 } },
            new Trainer() { FirstName = "Kelly", LastName = "Kapoor", HireDate = DateTime.Parse("2013-2-9"), isAvailable = false, Salary = 85000, Categories = new Collection<Category>() { c7 } },
            new Trainer() { FirstName = "Angela", LastName = "Martin", HireDate = DateTime.Parse("2011-3-10"), isAvailable = false, Salary = 50000, Categories = new Collection<Category>() { c3, c2, c1 } },
            new Trainer() { FirstName = "Toby", LastName = "Flenderson", HireDate = DateTime.Parse("2018-2-9"), isAvailable = true, Salary = 55000, Categories = new Collection<Category>() { c1, c2, c8 } },
            new Trainer() { FirstName = "Erin", LastName = "Hannon", HireDate = DateTime.Parse("2019-8-12"), isAvailable = false, Salary = 45000, Categories = new Collection<Category>() { c1, c2, c3 } },
            new Trainer() { FirstName = "Stanley", LastName = "Hudson", HireDate = DateTime.Parse("2018-5-2"), isAvailable = true, Salary = 150000, Categories = new Collection<Category>() { c2, c3, c4 } }
            );

           
           
            
            




        }
    }
}
