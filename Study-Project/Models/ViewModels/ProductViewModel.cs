using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Study_Project.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Obrigatório preenchimento do campo!")]
        public string Description { get; set; }

        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Obrigatório preenchimento do campo!")]
        public float Value { get; set; }

        [DisplayName("Categoria")]
        public Category Category { get; set; }

        [DisplayName("Todas as Categoria")]
        public List<Category> CategorySelect { get; set; }

        public ProductViewModel(List<Category> categorySelect)
        {
            CategorySelect = categorySelect;
        }

        public ProductViewModel()
        {
        }
    }
}
