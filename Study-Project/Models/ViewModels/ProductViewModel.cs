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
        [Required(ErrorMessage = "Obrigatório preenchimento do campo!")]
        public float Value { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Obrigatório preenchimento do campo!")]
        public int CategoryId { get; set; }
    }
}
