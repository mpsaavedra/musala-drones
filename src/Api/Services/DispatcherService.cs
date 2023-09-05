using AutoMapper;
using Musala.Drones.Api.Repositories;
using Musala.Drones.Domain.Dtos.Requests;
using Musala.Drones.Domain.Dtos.Responses;
using Musala.Drones.Domain.Enums;
using Musala.Drones.Domain.Models;

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
    private readonly IMapper _mapper;
    private readonly IDroneRepository _droneRepository;
    private readonly IDroneChargeRepository _droneChargeRepository;
    private readonly IMedicationRepository _medicationRepository;
    private readonly IMedicationChargeRepository _medicationChargeRepository;

    public DispatcherService(IMapper mapper, IDroneRepository droneRepository, 
        IDroneChargeRepository droneChargeRepository, IMedicationRepository medicationRepository, 
        IMedicationChargeRepository medicationChargeRepository)
    {
        _mapper = mapper;
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
        var result = new MedicationsInDroneResponse();
        try
        {
            var drone = _droneRepository.Query.FirstOrDefault(x => x.SerialNumber.Equals(medicationsInDrone.SerialNumber));
            if (drone == null)
            {
                result.Success = false;
                result.Message = $"Drone with serial number {medicationsInDrone.SerialNumber} is not registered";
            }

            if (drone.State == DroneState.Idle)
            {
                result.Success = false;
                result.Message = $"Drone with serial number {medicationsInDrone.SerialNumber} is not loaded";
            }

            var medications = drone.DroneCharges.Last();
            foreach (var charge in medications.MedicationCharges)
            {
                var medication = _mapper.Map<MedicationInDrone>(charge.Medication);
                result.Medications.Add(medication);
            }
        }
        catch (Exception e)
        {
            result.Success = false;
            result.Message = e.Message;
        }

        return result;
    }

    public AvailableDronesResponse ListAvailableDronesForLoading()
    {
        var result = new AvailableDronesResponse();
        try
        {
            var drones = _droneRepository.Query
                .Where(x => x.State == DroneState.Idle && x.BatteryCapacity > 25);
            foreach (var drone in drones)
            {
                result.AvailableDrones.Add(_mapper.Map<AvailableDrones>(drone));
            }
        }
        catch (Exception e)
        {
            result.Success = false;
            result.Message = e.Message;
        }

        return result;
    }

    public BatteryLevelInDroneResponse BatteryLevelOnDrone(BatteryLevelInDroneRequest batteryLevel)
    {
        var result = new BatteryLevelInDroneResponse();
        try
        {
            var drone = _droneRepository.Query.FirstOrDefault(x => x.SerialNumber.Equals(batteryLevel.SerialNumber));
            if (drone != null)
            {
                return _mapper.Map<BatteryLevelInDroneResponse>(drone);
            }

            result.Success = false;
            result.Message = $"Drone with serial number {batteryLevel.SerialNumber} is not registered";
            return result;
        }
        catch (Exception e)
        {
            result.Success = false;
            result.Message = e.Message;
        }

        return result;
    }

    public ListDronesResponse ListDrones()
    {
        var result = new ListDronesResponse();
        try
        {
            var drones = _droneRepository.Query;
            foreach (var drone in drones)
            {
                result.Drones.Add(_mapper.Map<ListDrone>(drone));
            }
        }
        catch (Exception e)
        {
            result.Success = false;
            result.Message = e.Message;
        }
        return result;
    }

    public ListMedicationsResponse ListMedications()
    {
        var result = new ListMedicationsResponse();
        try
        {
            var medications = _medicationRepository.Query;
            foreach (var medication in medications)
            {
                result.Medications.Add(_mapper.Map<ListMedication>(medication));
            }
        }
        catch (Exception e)
        {
            result.Success = false;
            result.Message = e.Message;
        }
        return result;
    }
}