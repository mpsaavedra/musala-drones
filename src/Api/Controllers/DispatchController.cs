using Microsoft.AspNetCore.Mvc;
using Musala.Drones.Api.Repositories;
using Musala.Drones.Api.Services;
using Musala.Drones.Domain.Dtos.Requests;
using Musala.Drones.Domain.Dtos.Responses;

namespace Musala.Drones.Api.Controllers;

[Route("[controller]")]
public class DispatchController : ControllerBase
{
    private readonly ILogger<DispatchController> _logger;
    private readonly IDispatcherService _dispatcher;

    public DispatchController(ILogger<DispatchController> logger, IDispatcherService dispatcher)
    {
        _logger = logger;
        _dispatcher = dispatcher;
    }

    [HttpGet("[action]")]
    public ListDronesResponse ListDrones() => 
        _dispatcher.ListDrones();

    [HttpGet("[action]")]
    public ListMedicationsResponse ListMedications() => 
        _dispatcher.ListMedications();

    [HttpGet("[action]")]
    public MedicationsInDroneResponse MedicationsInDrone([FromQuery] MedicationsInDroneRequest request) =>
        _dispatcher.MedicationsInDrone(request);

    [HttpGet("[action]")]
    public AvailableDronesResponse AvailableDronesForLoading() =>
        _dispatcher.ListAvailableDronesForLoading();

    [HttpGet("[action]")]
    public BatteryLevelInDroneResponse BatteryLevelInDrone([FromQuery] BatteryLevelInDroneRequest request) =>
        _dispatcher.BatteryLevelOnDrone(request);

    [HttpPost("[action]")]
    public LoadDroneResponse LoadDrone([FromBody] LoadDroneRequest request) =>
        _dispatcher.LoadDrone(request);

    [HttpPost("[action]")]
    public RegisterDroneResponse RegisterDrone([FromBody] RegisterDroneRequest request) =>
        _dispatcher.RegisterDrone(request);


}