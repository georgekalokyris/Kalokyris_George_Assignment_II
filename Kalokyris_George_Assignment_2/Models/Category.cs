using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kalokyris_George_Assignment_2.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Title { get; set; }

        //Navigation Properties

        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}