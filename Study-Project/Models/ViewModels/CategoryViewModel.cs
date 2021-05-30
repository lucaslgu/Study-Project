using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Obrigatório preenchimento do campo!")]
        public string Name { get; set; }
    }
}
