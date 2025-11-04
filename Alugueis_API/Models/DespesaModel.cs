namespace alugueis_api.Models
{
    public class Despesa
    {
        public int CodDespesa { get; set; }
        public int CodTipoDespesa { get; set; }
        public TipoDespesa TipoDespesa { get; set; }
        public float VrlTotalDespesa { get; set; }
        public DateTime DataDespesa { get; set; }
        public DateTime CompetenciaMes { get; set; }
        public ICollection<DespesaRateio> Rateios { get; set; }
    }
}
