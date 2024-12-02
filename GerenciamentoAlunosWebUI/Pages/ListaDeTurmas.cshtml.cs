using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace GerenciamentoAlunosWebUI.Pages
{
    public class TurmaModel : PageModel
    {
        public List<TurmaEntidade> Turmas { get; set; } = new();

        private readonly IHttpClientFactory _httpClientFactory;

        public TurmaModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // URL do endpoint da API
            string url = "https://localhost:7003/api/Turma/ListarTurmas";

            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Turmas = JsonSerializer.Deserialize<List<TurmaEntidade>>(json, new JsonSerializerOptions
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

    public class TurmaEntidade
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Turma { get; set; }
        public int Ano { get; set; }
        public bool Ativo { get; set; }
    }
}
