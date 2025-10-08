namespace SistemaDiarioLeitura.Models;

public partial class Leitura
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public sbyte IdStatusLeitura { get; set; }

    public int IdLivro { get; set; }

    public sbyte? Nota { get; set; }

    public string? Comentario { get; set; }

    public DateOnly? DataInicio { get; set; }

    public DateOnly? DataFim { get; set; }

    public virtual Livro IdLivroNavigation { get; set; } = null!;

    public virtual StatusLeitura IdStatusLeituraNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
