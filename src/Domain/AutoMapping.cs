using Musala.Drones.Contracts.Dtos.Requests;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Domain;

public class AutoMapping : AutoMapper.Profile
{
    public AutoMapping()
    {
        CreateMap<RegisterDroneRequest, Drone>();
    }
}