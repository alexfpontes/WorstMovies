using Microsoft.AspNetCore.Mvc;
using WorstMovie.Services.Interfaces;

namespace WorstMovie.Controllers;

[ApiController]
[Route("[controller]")]
public class ProducersController(IProducerRangeAwardsService _producerRangeAwardsService,
ILogger<ProducersController> _logger) : ControllerBase
{
    [HttpGet("/producer/range-awards")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _producerRangeAwardsService.GetRangeAwards();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, null);
            return BadRequest();
        }
    }
}
