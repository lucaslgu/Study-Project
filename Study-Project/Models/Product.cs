using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public Category Category { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
