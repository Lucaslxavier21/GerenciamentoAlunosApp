using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GerenciamentoAlunosWebUI.Pages
{
    public class AlunoModel : PageModel
    {
        public List<Aluno> Alunos { get; set; } = new();

        private readonly IHttpClientFactory _httpClientFactory;

        public AlunoModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // URL do endpoint da API
            string url = "https://localhost:7003/api/Aluno/ListarAlunos";

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Alunos = JsonSerializer.Deserialize<List<Aluno>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Erro ao buscar dados da API.");
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao acessar a API: {ex.Message}");
            }
        }
    }

    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
