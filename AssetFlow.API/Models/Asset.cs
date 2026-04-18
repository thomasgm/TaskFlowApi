using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetFlow.API.Models
{
    public class Asset
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome do ativo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        [Required(ErrorMessage = "A data de aquisição é obrigatória.")]
        public DateTime AcquisitionDate { get; set; }
        
        [Required(ErrorMessage = "O valor do ativo é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Value { get; set; }
        
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public Category? Category { get; set; }

        public string Status { get; set; }
    }
}