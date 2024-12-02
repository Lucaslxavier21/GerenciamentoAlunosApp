namespace GestaoDeAlunosApi.Models
{
    public class TurmaModel
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Turma { get; set; }
        public int Ano { get; set; }
        public bool Ativo { get; set; }
    }
}
