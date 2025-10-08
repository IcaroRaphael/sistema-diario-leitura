namespace SistemaDiarioLeitura.Models;

public partial class Autor
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public DateOnly DataNascimento { get; set; }

    public string? Biografia { get; set; }

    public virtual ICollection<Livro> IdLivros { get; set; } = new List<Livro>();
}
