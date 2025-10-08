namespace alugueis_api.Models.DTOs
{
    public class AptoListDTO
    {
        public int CodApto { get; set; }
        public int CodPredio { get; set; }
        public int Andar { get; set; }
        public int QtdQuartos { get; set; }
        public int QtdBanheiros { get; set; }
        public int MetrosQuadrados { get; set; }
        public string NomePredio { get; set; }
    }
}
