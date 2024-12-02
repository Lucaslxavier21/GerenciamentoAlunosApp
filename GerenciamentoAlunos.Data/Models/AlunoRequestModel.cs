namespace GestaoDeAlunosApi.Models
{
    public class AlunoRequestModel
    {
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}