using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Controllers;

[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase {
  private readonly ILogger<ShoppingCartController> _logger;

  public ShoppingCartController(ILogger<ShoppingCartController> logger) {
    _logger = logger;
  }

  [HttpGet]
  public async Task<IActionResult> OnGetAsync()
  {
    await Task.Delay(200);
    _logger.LogInformation("OnGetAsync()");
    return Ok();
  }

  [HttpPost]
  public async Task<IActionResult> OnPostAsync([FromBody] ShoppingCartItem shoppingCartItem)
  {
    await Task.Delay(200);
    _logger.LogInformation(shoppingCartItem.ToString());
    return Ok(shoppingCartItem);
  }

  public record ShoppingCartItem(
    int ProductCatalogId,
    string ProductName,
    string ProductDescription
  ) { }
}