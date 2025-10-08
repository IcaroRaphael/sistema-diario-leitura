namespace SistemaDiarioLeitura.Models;

public partial class StatusLeitura
{
    public sbyte Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Leitura> Leituras { get; set; } = new List<Leitura>();
}
