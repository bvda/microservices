using Microsoft.AspNetCore.Mvc;

using ApiGateway.Clients;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
  private readonly ILogger<ShoppingCartController> _logger;
  private readonly IShoppingCartClient _shoppingCartClient;
  public  ShoppingCartController(ILogger<ShoppingCartController> logger, IShoppingCartClient shoppingCartClient)
  {
    _logger = logger;
    _shoppingCartClient = shoppingCartClient;
  }

  [HttpGet]
  public async Task<IActionResult> GetAsync()
  {
    await _shoppingCartClient.GetShoppingCartItems(new int[] {1 , 2});
    return Ok();
  }
}