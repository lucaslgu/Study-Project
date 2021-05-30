using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Expense> Expenses { get; set; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
