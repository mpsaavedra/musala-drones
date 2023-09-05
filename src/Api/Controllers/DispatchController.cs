using Microsoft.AspNetCore.Mvc;
using Musala.Drones.Api.Services;
using Musala.Drones.Contracts.Dtos.Requests;
using Musala.Drones.Contracts.Dtos.Responses;

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

    [HttpPost("[action]")]
    public ListDronesResponse ListDrones() => 
        _dispatcher.ListDrones();

    [HttpPost("[action]")]
    public ListMedicationsResponse ListMedications() => 
        _dispatcher.ListMedications();

    [HttpPost("[action]")]
    public MedicationsInDroneResponse MedicationsInDrone([FromBody] MedicationsInDroneRequest request) =>
        _dispatcher.MedicationsInDrone(request);

    [HttpPost("[action]")]
    public AvailableDronesResponse AvailableDronesForLoading() =>
        _dispatcher.ListAvailableDronesForLoading();

    [HttpPost("[action]")]
    public BatteryLevelInDroneResponse BatteryLevelInDrone([FromBody] BatteryLevelInDroneRequest request) =>
        _dispatcher.BatteryLevelOnDrone(request);

    [HttpPost("[action]")]
    public LoadDroneResponse LoadDrone([FromBody] LoadDroneRequest request) =>
        _dispatcher.LoadDrone(request);

    [HttpPost("[action]")]
    public RegisterDroneResponse RegisterDrone([FromBody] RegisterDroneRequest request) =>
        _dispatcher.RegisterDrone(request);


}