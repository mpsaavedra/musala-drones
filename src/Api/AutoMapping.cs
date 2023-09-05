using Musala.Drones.Contracts.Dtos.Responses;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api;

public class AutoMapping : AutoMapper.Profile
{
    public AutoMapping()
    {
        CreateMap<Drone, ListDrone>();
        CreateMap<Drone, AvailableDrones>();
        CreateMap<Drone, BatteryLevelInDroneResponse>()
            .ForMember(dst => dst.BatteryLevel, opts =>
                opts.MapFrom(src => src.BatteryCapacity))
            .ForMember(dst => dst.SerialNumber, opts =>
                opts.MapFrom(src => src.SerialNumber));
        
        CreateMap<Medication, ListMedication>();
        CreateMap<Medication, MedicationInDrone>();
    }
}