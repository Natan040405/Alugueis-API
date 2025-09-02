namespace Alugueis_API.Models
{
    public class DespesaRateio
    {
        public int CodDespesa { get; set; }
        public int CodApto { get; set; }
        public float VlrRateio { get; set; }
        public Despesa Despesa { get; set; }
        public Apto Apto { get; set; }
    }
}
