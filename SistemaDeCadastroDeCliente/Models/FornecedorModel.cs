using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeCadastroDeCliente.Models
{
    public class FornecedorModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome não pode estar vazio")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe um cnpj válido")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ deve ter exatamente 14 caracteres.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter exatamente 14 dígitos numéricos.")]
        public string CNPJ { get; set; }

        [Required]
        public string Segmento { get; set; }

        [Required(ErrorMessage = "Informe um CEP válido.")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter exatamente 8 caracteres.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "O CEP deve conter exatamente 8 dígitos numéricos.")]
        public string CEP { get; set; }

        [StringLength(255)]
        public string Endereco { get; set; }

        [NotMapped]
        public IFormFile Foto { get; set; }

        public string FotoPath { get; set; }
        public string FotoUrl { get; set; }
    }
}
