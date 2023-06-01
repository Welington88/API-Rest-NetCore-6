using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.NET.Models;

[Table("Colaborador")]
public class Colaborador
{
    public Colaborador()
    {
    }
    [Key]
    public int Matricula { get; set; }

    [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
    [StringLength(50, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 2)]
    public String Nome { get; set; }

    [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
    [Display(Name = "Endereço")]
    [StringLength(50, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 2)]
    public String Endereco { get; set; }

    [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
    [DisplayFormat(DataFormatString = "{0:(###)-####-####}", ApplyFormatInEditMode = true)]
    [StringLength(20, ErrorMessage = " O Campo {0} pode ter no máximo {1} e minimo {2} caracteres ", MinimumLength = 2)]
    [DataType(DataType.PhoneNumber)]
    public String Telefone { get; set; }

    [Display(Name = "Admissão")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
    public DateTime Admissao { get; set; }

    [Display(Name = "Salário")]
    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
    [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
    public float Salario { get; set; }

    [Display(Name = "É Gestor")]
    [Required(ErrorMessage = " O Campo {0} é Obrigatório!")]
    public Boolean Gestor { get; set; }

    [ForeignKey("Setor")]
    [Display(Name = "Código Setor")]
    public int IdSetor { get; set; }
    [JsonIgnore]
    public virtual Setor Setor { get; set; }

    [ForeignKey("Cargo")]
    [Display(Name = "Código Cargo")]
    [JsonIgnore]
    public int IdCargo { get; set; }
    [JsonIgnore]
    public virtual Cargo Cargo { get; set; }

}
