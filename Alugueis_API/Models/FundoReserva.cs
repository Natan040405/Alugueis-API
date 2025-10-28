namespace alugueis_api.Models
{
    public class FundoReserva
    {
        public int CodFundo {  get; set; }
        public string Descricao { get; set; }
        public float ValoreMeta { get; set; }
        public int PrazoMeses { get; set; }
        public DateTime dataInicio { get; set; }
        public float ValorMensal {get => ValoreMeta / PrazoMeses;}
        public int ativo { get; set; }
    }
}
