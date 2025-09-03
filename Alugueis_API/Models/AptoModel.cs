namespace alugueis_api.Models
{
    public class Apto
    {
        public int CodApto { get; set; }
        public int CodPredio { get; set; }
        public Predio Predio { get; set; }
        public int Andar { get; set; }
        public int QtdQuartos { get; set; }
        public int QtdBanheros { get; set; }
        public float MetrosQuadrados { get; set; }
        public ICollection<Locacao> Locacoes { get; set; }
        public ICollection<DespesaRateio> DespesasRateio { get; set; }
    }
}
