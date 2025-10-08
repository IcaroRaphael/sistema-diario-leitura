using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDiarioLeitura.Models;

[Table("usuario")]
public class Usuario
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Column("nome")]
    public string? Nome { get; set; }

    [Required]
    [StringLength(320)]
    [Column("email")]
    public string? Email { get; set; }

    [Required]
    [StringLength(255)]
    [Column("senha")]
    public string? Senha { get; set; }

    [Required]
    [Column("data_cadastro")]
    public DateTime DataCadastro { get; set; }
}
