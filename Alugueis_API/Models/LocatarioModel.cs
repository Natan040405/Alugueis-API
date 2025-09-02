using System.ComponentModel.DataAnnotations;

namespace Alugueis_API.Models
{
    public class Locatario
    {
        public int Idade {  get; set; }
        public int TemPet {  get; set; }
        public int QtdDependentes { get; set; }
        public string NomeLocatario { get; set; }
        public string Cpf { get; set; }
        public string EnderecoUltimoImovel { get; set; }
        public ICollection<Locacao> Locacoes { get; set; }

    }
}
