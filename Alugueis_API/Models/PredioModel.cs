namespace Alugueis_API.Models
{
    public class Predio
    {
        public int CodPredio { get; set; }
        public int QtdAndares { get; set; }
        public string Endereco { get; set; }
        public string NomePredio { get; set; }
        public ICollection<Apto> Aptos { get; set; }

    }
}
