using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
  private readonly ILogger<ShoppingCartController> _logger;
  public  ShoppingCartController(ILogger<ShoppingCartController> logger)
  {
    _logger = logger;
  }

  [HttpGet]
  public async Task<IActionResult> GetAsync()
  {
    await Task.Delay(100);
    return Ok();
  }
}