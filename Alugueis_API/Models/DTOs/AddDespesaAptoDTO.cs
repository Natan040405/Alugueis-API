namespace alugueis_api.Models.DTOs
{
    public class AddDespesaAptoDTO
    {
        public int CodDespesa { get; set; }
        public int? CodApto { get; set; }
        public int CodTipoDespesa { get; set; }
        public float VlrTotalDespesa { get; set; }
        public DateTime CompetenciaMes { get; set; }
    }
}
