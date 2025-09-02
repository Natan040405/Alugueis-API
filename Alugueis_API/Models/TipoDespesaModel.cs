namespace Alugueis_API.Models
{
    public class TipoDespesa
    {
        public int CodTipo { get; set; }
        public string NomeTipo { get; set; }
        public int Compartilhado { get; set; }
        public ICollection<Despesa> Despesas { get; set; }
    }
}
