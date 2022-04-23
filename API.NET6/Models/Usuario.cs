using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.NET6.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public Usuario()
        {
        }

        [Key]
        [JsonIgnore]
        public int IdUsuario { get; set; }

        [Required]
        [Column("Nome", Order = 2, TypeName = "varchar(50)")]
        public String Nome { get; set; }

        [Required]
        public String Senha { get; set; }

    }

}
