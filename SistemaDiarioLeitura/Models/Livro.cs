namespace SistemaDiarioLeitura.Models;

public partial class Livro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public short AnoPublicacao { get; set; }

    public virtual ICollection<Leitura> Leituras { get; set; } = new List<Leitura>();

    public virtual ICollection<Autor> IdAutors { get; set; } = new List<Autor>();

    public virtual ICollection<Genero> IdGeneros { get; set; } = new List<Genero>();
}
