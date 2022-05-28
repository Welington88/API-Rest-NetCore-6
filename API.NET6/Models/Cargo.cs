using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.NET6.Models
{
    [Table("Cargo")]
    public class Cargo
    {
        public Cargo()
        {
        }
        [Key]
        public int IdCargo { get; set; }
        [Display(Name = "Cargo")]
        [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
        [StringLength(50, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 2)]
        public String Descricao { get; set; }

        [ForeignKey("Setor")]
        [Display(Name = "Código Setor")]
        public int IdSetor { get; set; }
        public virtual Setor Setor { get; set; }

        [JsonIgnore]
        public virtual ICollection<Colaborador> Colaboradores { get; set; }
    }
}
