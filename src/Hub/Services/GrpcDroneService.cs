using Grpc.Core;
using Musala.Drones.Hub;

namespace Musala.Drones.Hub.Services;

public class GrpcDroneService : DronesHub.DronesHubBase
{
    private readonly ILogger<GrpcDroneService> _logger;
    private readonly IDroneHubService _hub;

    public GrpcDroneService(ILogger<GrpcDroneService> logger, IDroneHubService hub)
    {
        _logger = logger;
        _hub = hub;
    }

    public override async Task<DroneStatusResponse> GetDroneStatus(DroneStatusRequest request,
        ServerCallContext context) => await _hub.GetDroneStatus(request);
}