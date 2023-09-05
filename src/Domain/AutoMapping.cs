using Musala.Drones.Domain.Dtos.Requests;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Domain;

public class AutoMapping : AutoMapper.Profile
{
    public AutoMapping()
    {
        CreateMap<RegisterDroneRequest, Drone>();
    }
}