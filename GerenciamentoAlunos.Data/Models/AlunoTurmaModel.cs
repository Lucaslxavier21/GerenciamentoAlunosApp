namespace GestaoDeAlunosApi.Models
{
    public class AlunoTurmaModel
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Turma { get; set; }
        public List<AlunoModel> ListaDeAlunos { get; set; }
        public List<TurmaModel> ListaDeTurmas { get; set; }
    }
}
