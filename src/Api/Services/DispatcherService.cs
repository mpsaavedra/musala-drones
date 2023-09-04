using Musala.Drones.Api.Repositories;
using Musala.Drones.Domain.Dtos.Requests;
using Musala.Drones.Domain.Dtos.Responses;

namespace Musala.Drones.Api.Services;


public interface IDispatcherService
{
    RegisterDroneResponse RegisterDrone(RegisterDroneRequest register);

    LoadDroneResponse LoadDrone(LoadDroneRequest loadDrone);

    MedicationsInDroneResponse MedicationsInDrone(MedicationsInDroneRequest medicationsInDrone);

    AvailableDronesResponse ListAvailableDronesForLoading();

    BatteryLevelInDroneResponse BatteryLevelOnDrone(BatteryLevelInDroneRequest batteryLevel);

    ListDronesResponse ListDrones();

    ListMedicationsResponse ListMedications();
}

public class DispatcherService : IDispatcherService
{
    private readonly IDroneRepository _droneRepository;
    private readonly IDroneChargeRepository _droneChargeRepository;
    private readonly IMedicationRepository _medicationRepository;
    private readonly IMedicationChargeRepository _medicationChargeRepository;

    public DispatcherService(IDroneRepository droneRepository, IDroneChargeRepository droneChargeRepository,
        IMedicationRepository medicationRepository, IMedicationChargeRepository medicationChargeRepository)
    {
        _droneRepository = droneRepository;
        _droneChargeRepository = droneChargeRepository;
        _medicationRepository = medicationRepository;
        _medicationChargeRepository = medicationChargeRepository;
    }

    public RegisterDroneResponse RegisterDrone(RegisterDroneRequest register)
    {
        throw new NotImplementedException();
    }

    public LoadDroneResponse LoadDrone(LoadDroneRequest loadDrone)
    {
        throw new NotImplementedException();
    }

    public MedicationsInDroneResponse MedicationsInDrone(MedicationsInDroneRequest medicationsInDrone)
    {
        throw new NotImplementedException();
    }

    public AvailableDronesResponse ListAvailableDronesForLoading()
    {
        throw new NotImplementedException();
    }

    public BatteryLevelInDroneResponse BatteryLevelOnDrone(BatteryLevelInDroneRequest batteryLevel)
    {
        throw new NotImplementedException();
    }

    public ListDronesResponse ListDrones()
    {
        throw new NotImplementedException();
    }

    public ListMedicationsResponse ListMedications()
    {
        throw new NotImplementedException();
    }
}