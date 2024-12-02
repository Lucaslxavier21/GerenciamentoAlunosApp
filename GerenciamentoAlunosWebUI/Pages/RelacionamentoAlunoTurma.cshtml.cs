using Azure;
using ModeloAluno = GestaoDeAlunosApi.Models.AlunoModel;
using ModeloTurma = GestaoDeAlunosApi.Models.TurmaModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace GerenciamentoAlunosWebUI.Pages
{
    public class RelacionamentoAlunoTurmaModel : PageModel
    {
        public List<AlunoTurmaModel> AlunoTurma { get; set; } = new();
        public List<ModeloAluno> ListaDeAlunos { get; set; } = new();
        public List<ModeloTurma> ListaDeTurmas { get; set; } = new();

        private readonly IHttpClientFactory _httpClientFactory;

        public RelacionamentoAlunoTurmaModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
                    
            try
            {
                await ListarAlunos(client);
                await ListarTurmas(client);
                await ListarAlunosTurmas(client);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao acessar a API: {ex.Message}");
            }
        }

        private async Task ListarAlunos(HttpClient client)
        {
            string urlAlunos = "https://localhost:7003/api/AlunoTurma/ListarAlunos";
            var response = await client.GetAsync(urlAlunos);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                ListaDeAlunos = JsonSerializer.Deserialize<List<ModeloAluno>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao buscar dados da API.");
            }
        }
        private async Task ListarTurmas(HttpClient client)
        {
            string urlTurma = "https://localhost:7003/api/AlunoTurma/ListarTurmas";
            var response = await client.GetAsync(urlTurma);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                ListaDeTurmas = JsonSerializer.Deserialize<List<ModeloTurma>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao buscar dados da API.");
            }
        }
        private async Task ListarAlunosTurmas(HttpClient client)
        {
            string url = "https://localhost:7003/api/AlunoTurma/ListarRelacionamentos";
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                AlunoTurma = JsonSerializer.Deserialize<List<AlunoTurmaModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao buscar dados da API.");
            }
        }
    }
    public class AlunoTurmaModel
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Turma { get; set; }
    }

}
