namespace SistemaDeCadastroDeCliente.Models
{
    public class EnderecoResponse
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public bool? Erro { get; set; }
    }
}
