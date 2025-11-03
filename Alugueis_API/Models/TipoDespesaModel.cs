namespace alugueis_api.Models
{
    public class TipoDespesa
    {
        public int CodTipoDespesa { get; set; }
        public string NomeTipoDespesa { get; set; }
        public int Compartilhado { get; set; }
        public ICollection<Despesa> Despesas { get; set; }
    }
}
