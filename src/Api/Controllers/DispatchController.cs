using Microsoft.AspNetCore.Mvc;
using Musala.Drones.Api.Repositories;

namespace Musala.Drones.Api.Controllers;

[Route("[controller]")]
public class DispatchController : ControllerBase
{
    private readonly ILogger<DispatchController> _logger;
    private readonly IDroneRepository _dronerRepository;

    public DispatchController(ILogger<DispatchController> logger, IDroneRepository dronerRepository)
    {
        _logger = logger;
        _dronerRepository = dronerRepository;
    }

    [HttpGet("[action]")]
    public IActionResult ListDrones()
    {
        return Ok("This is ok");
    }
}