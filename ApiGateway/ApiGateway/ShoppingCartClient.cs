using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ApiGateway.Clients;

public interface IShoppingCartClient
{
  Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds);
}

public class ShoppingCartClient : IShoppingCartClient
{
  private readonly HttpClient _client;
  private readonly ILogger<ShoppingCartClient> _logger;
  private static string _shoppingCartBaseUrl;

  public ShoppingCartClient(ILogger<ShoppingCartClient> logger, IConfiguration config, HttpClient client)
  {
    _shoppingCartBaseUrl = config["ShoppingCartBaseUrl"];
    _logger = logger;
    // Console.WriteLine(_shoppingCartBaseUrl);
    client.BaseAddress = new Uri(_shoppingCartBaseUrl);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    _client = client;
  }

  public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds)
  {
    using var response = await RequestProductFromProductCatalog(productCatalogueIds);
    return await ConvertToShoppingCartItems(response);
  }

  private async Task<HttpResponseMessage> RequestProductFromProductCatalog(int[] productCatalogIds)
  {
    _logger.LogInformation(_shoppingCartBaseUrl);
    return await _client.GetAsync("shoppingcart");
  }

  private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage response)
  {
    response.EnsureSuccessStatusCode();
    return new List<ShoppingCartItem>();
    var products = await
      JsonSerializer.DeserializeAsync<List<ShoppingCartItem>>(
        await response.Content.ReadAsStreamAsync(),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
      ?? new();
    return products
      .Select(p =>
        new ShoppingCartItem(
          p.ProductCatalogId,
          p.ProductName,
          p.ProductDescription
      ));
  }
}
public record ShoppingCartItem(int ProductCatalogId, string ProductName, string ProductDescription);
