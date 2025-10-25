using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebServiceProject.Models;

namespace WebServiceProject.Services
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7024/");
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _client.GetFromJsonAsync<List<Product>>("api/products");
        }

        public async Task CreateProductAsync(Product product)
        {
            await _client.PostAsJsonAsync("api/products", product);
        }
    }
}