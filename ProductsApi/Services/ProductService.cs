using ProductsApi.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace ProductsApi.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetStringAsync("https://fakestoreapi.in/api/products");

            var responseObject = JsonConvert.DeserializeObject<ResponseWrapper>(response);

            return responseObject.Products;
        }

        public async Task CreateProductAsync(Product product)
        {
            var jsonProduct = JsonConvert.SerializeObject(new
            {
                title = product.title,
                brand = product.brand,
                model = product.model,
                color = product.color,
                category = product.category,
                discount = product.discount
            });

            var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://fakestoreapi.in/api/products", content);

            response.EnsureSuccessStatusCode();
        }
    }

    public class ResponseWrapper
    {
        public List<Product> Products { get; set; }
    }
}
