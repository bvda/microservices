using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductListController : ControllerBase
{
  private readonly ILogger<ProductListController> _logger;
  public  ProductListController(ILogger<ProductListController> logger)
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