using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AssetFlow.API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required  (ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}