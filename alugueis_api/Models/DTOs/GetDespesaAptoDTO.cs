namespace alugueis_api.Models.DTOs
{
    public class GetDespesaAptoDTO
    {
        public int CodDespesa { get; set; }
        public int CodTipoDespesa { get; set; }
        public string NomeTipoDespesa { get; set; }
        public float VlrTotalDespesa { get; set; }
        public DateTime DataDespesa { get; set; }
        public DateTime CompetenciaMes { get; set; }
        public int Compartilhado { get; set; }

        public GetDespesaAptoDTO(int codDespesa, int codTipoDespesa, string nomeTipoDespesa, float vlrTotalDespesa, DateTime dataDespesa, DateTime competenciaMes, int compartilhado) 
        {
            CodDespesa = codDespesa;
            CodTipoDespesa = codTipoDespesa;
            NomeTipoDespesa = nomeTipoDespesa;
            VlrTotalDespesa = vlrTotalDespesa;
            DataDespesa = dataDespesa;
            CompetenciaMes = competenciaMes;
            Compartilhado = compartilhado;
        }
    }
}
