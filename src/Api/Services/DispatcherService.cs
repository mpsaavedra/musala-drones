using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Musala.Drones.Api.Repositories;
using Musala.Drones.Contracts.Dtos.Requests;
using Musala.Drones.Contracts.Dtos.Responses;
using Musala.Drones.Contracts.Enums;
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
    private readonly IValidator<Drone> _droneValidator;

    public DispatcherService(IMapper mapper, IDroneRepository droneRepository, 
        IDroneChargeRepository droneChargeRepository, IMedicationRepository medicationRepository, 
        IMedicationChargeRepository medicationChargeRepository, IValidator<Drone> droneValidator)
    {
        _mapper = mapper;
        _droneRepository = droneRepository;
        _droneChargeRepository = droneChargeRepository;
        _medicationRepository = medicationRepository;
        _medicationChargeRepository = medicationChargeRepository;
        _droneValidator = droneValidator;
    }

    public RegisterDroneResponse RegisterDrone(RegisterDroneRequest register)
    {
        var result = new RegisterDroneResponse();

        try
        {
            var drone = _mapper.Map<Drone>(register);
            var validation = _droneValidator.Validate(drone);
            if (!validation.IsValid)
            {
                result.Success = false;
                result.Errors = (from error in validation.Errors select error.ErrorMessage).ToList();
                return result;
            }

            var id = _droneRepository.Create(drone).Result;
            result.Id = id;
        }
        catch (Exception e)
        {
            result.Success = false;
            result.Message = e.Message;
        }

        return result;
    }

    public LoadDroneResponse LoadDrone(LoadDroneRequest loadDrone)
    {
        var result = new LoadDroneResponse();

        try
        {
            var drone = _droneRepository.GetEntity<Drone>().FirstOrDefault(x => x.Id == loadDrone.Id);

            if (drone == null)
            {
                result.Success = false;
                result.Message = $"Drone with Id {loadDrone.Id} is not registered";
                return result;
            }

            if (drone.State != DroneState.Idle)
            {
                result.Success = false;
                result.Message =
                    $"Drone with Id {loadDrone.Id} could not be load, is already loaded";
                return result;
            }

            if (drone.BatteryCapacity < 25)
            {
                result.Success = false;
                result.Message =
                    $"Drone with Id {loadDrone.Id} could not be load, battery level is below 25%";
                return result;
            }

            var medications = new List<MedicationCharge>();
            float medicineWeight = 0;
            foreach (var medId in loadDrone.MedicationIds)
            {
                if (medicineWeight > drone.WeightLimit)
                {
                    result.Success = false;
                    result.Message = "Medications load is above the drone weight limit";
                    return result;
                }

                var medicine = _medicationRepository.Query.FirstOrDefault(x => x.Id == medId);
                if (medicine == null)
                {
                    result.Success = false;
                    result.Errors.Add($"Medication with Id {medId} was not found");
                }
                else
                {
                    medicineWeight += medicine.Weight; // add medication weight
                    medications.Add(new MedicationCharge { Medication = medicine });
                    result.Medications.Add(_mapper.Map<MedicationLoaded>(medicine));
                }
            }

            if (!result.Success) return result;

            drone.DroneCharges.Add(new DroneCharge
            {
                MedicationCharges = medications
            });
            
            var res = _droneRepository.Update(drone.Id, drone).Result;
            if (!res)
            {
                result.Success = false;
                result.Message = $"Could not load medications in drone, please try again";
            }

        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return result;
    }

    public MedicationsInDroneResponse MedicationsInDrone(MedicationsInDroneRequest medicationsInDrone)
    {
        var result = new MedicationsInDroneResponse();
        try
        {
            var drone = _droneRepository.Query
                .Include(x => x.DroneCharges)
                .ThenInclude(x => x.MedicationCharges)
                .ThenInclude(x => x.Medication)
                .FirstOrDefault(x => x.Id == medicationsInDrone.Id);
            
            if (drone == null)
            {
                result.Success = false;
                result.Message = $"Drone with Id {medicationsInDrone.Id} is not registered";
                return result;
            }

            if (drone.State == DroneState.Idle)
            {
                result.Success = false;
                result.Message = $"Drone with Id {medicationsInDrone.Id} is not loaded";
                return result;
            }

            var loads = drone.DroneCharges;
            if (loads.Count == 0)
            {
                result.Success = false;
                result.Message = $"Drone with Id {medicationsInDrone.Id} has not charges";
                return result;
            }
            
            var medications = loads.Last();
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
            var drone = _droneRepository.Query.FirstOrDefault(x => x.Id == batteryLevel.Id);
            if (drone != null)
            {
                return _mapper.Map<BatteryLevelInDroneResponse>(drone);
            }

            result.Success = false;
            result.Message = $"Drone with Id {batteryLevel.Id} is not registered";
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