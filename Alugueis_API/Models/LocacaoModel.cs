namespace alugueis_api.Models
{
    public class Locacao
    {
        public int CodLocacao {  get; set; }
        public int CodApto { get; set; }
        public string Cpf { get; set; }
        public float VlrAluguel { get; set; }
        public float VlrCausao { get; set; }
        public DateTime DataIncio { get; set; }
        public DateTime DataFim { get; set; }
        public Locatario Locatario { get; set; }
        public Apto Apto { get; set; }
    }
}
