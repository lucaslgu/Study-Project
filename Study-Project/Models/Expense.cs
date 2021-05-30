using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime DateInclusion { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
        public float TotalSpending { get; set; }
        public bool Active { get; set; }
    }
}
