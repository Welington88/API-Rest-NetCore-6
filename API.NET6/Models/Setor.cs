using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.NET6.Models
{
    [Table("Setor")]
    public class Setor
    {
        public Setor()
        {
        }
        [Key]
        public int IdSetor { get; set; }

        [Display(Name = "Setor")]
        [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
        [StringLength(50, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 2)]
        public String Descricao { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cargo> Cargos { get; set; }

        [JsonIgnore]
        public virtual ICollection<Colaborador> Colaboradores { get; set; }
    }
}
