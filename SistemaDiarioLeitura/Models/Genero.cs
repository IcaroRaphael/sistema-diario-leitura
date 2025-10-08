namespace SistemaDiarioLeitura.Models;

public partial class Genero
{
    public sbyte Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Livro> IdLivros { get; set; } = new List<Livro>();
}
