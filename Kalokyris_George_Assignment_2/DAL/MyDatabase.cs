using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Kalokyris_George_Assignment_2.Models;

namespace Kalokyris_George_Assignment_2.DAL
{
    public class MyDatabase : DbContext
    {

        public MyDatabase() : base("LocalConnection")
        {

        }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

}