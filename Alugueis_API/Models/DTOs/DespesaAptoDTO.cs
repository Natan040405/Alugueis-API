namespace alugueis_api.Models.DTOs
{
    public class DespesaAptoDTO
    {
        public int CodDespesa { get; set; }
        public int? CodApto { get; set; }
        public int CodTipoDespesa { get; set; }
        public double VlrTotalDespesa { get; set; }
        public DateTime CompetenciaMes { get; set; }
        public int Compartilhado { get; set; }
    }
}
