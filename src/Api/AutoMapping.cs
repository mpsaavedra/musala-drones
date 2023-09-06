using Musala.Drones.Contracts.Dtos.Requests;
using Musala.Drones.Contracts.Dtos.Responses;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api;

public class AutoMapping : AutoMapper.Profile
{
    public AutoMapping()
    {
        CreateMap<Drone, ListDrone>();
        CreateMap<Drone, AvailableDrones>();
        CreateMap<Drone, RegisterDroneRequest>().ReverseMap();
        CreateMap<Drone, BatteryLevelInDroneResponse>()
            .ForMember(dst => dst.BatteryLevel, opts =>
                opts.MapFrom(src => src.BatteryCapacity));

        CreateMap<Medication, ListMedication>();
        CreateMap<Medication, MedicationInDrone>();
        CreateMap<Medication, MedicationLoaded>();
    }
}