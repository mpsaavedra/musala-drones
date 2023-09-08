using System.Text;
using Musala.Drones.Contracts.Dtos.Requests;
using Musala.Drones.Contracts.Dtos.Responses;
using Musala.Drones.Contracts.Enums;
using Musala.Drones.Hub.Extensions;
using Musala.Drones.Hub.Models;

namespace Musala.Drones.Hub.Services;

public interface IDroneHubService
{
    Task ExecuteAsync();

    // Task<RegisterDroneResponse?> RegisterDrone(RegisterDroneRequest request);
    // Task<LoadDroneResponse?> LoadDrone(LoadDroneRequest request);
    // Task<MedicationsInDroneResponse?> GetMedicationsInDrone(MedicationsInDroneRequest request);
    // Task<AvailableDronesResponse?> GetListAvailableDronesForLoading();
    // Task<BatteryLevelInDroneResponse?> GetBatteryLevelInDrone(BatteryLevelInDroneRequest request);
    // Task<ListDronesResponse?> GetDroneList(DroneStatusRequest request);
    // Task<ListMedicationsResponse?> GetListMedications();

    Task<DroneStatusResponse> GetDroneStatus(DroneStatusRequest request);
}

public class DroneHubService : IDroneHubService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<DroneHubService> _logger;
    private Dictionary<int, Drone> _drones = new();
    private Dictionary<int, Medication> _medications = new();
    private int _count = 0;
    public int _tickCount = 0;
    private bool _validLoad = true;
    private bool _idle = true;

    public DroneHubService(IApiClient apiClient, ILogger<DroneHubService> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        _tickCount++;
        await SimulateDroneDeliveries();

        if (_tickCount < 5)
            return;
        else
            _tickCount = 0;

        _count++;

        switch (_count)
        {
            case 1:
            {
                await GetDroneList();
                break;
            }
            case 2:
            case 3:
            {
                var idx = new Random().Next(_drones.Count - 1);
                await GetDroneStatus(new DroneStatusRequest { Id = idx });
                break;
            }
            case 4:
            {
                await GetListMedications();
                break;
            }
            case 5:
            case 6:
            {
                await GetListAvailableDronesForLoading();
                break;
            }
            case 7:
            {
                var idx = new Random().Next(1, _drones.Count);
                await GetMedicationsInDrone(idx);
                break;
            }
            case 8:
            {
                await LoadDrone();
                break;
            }
            case 9:
            {
                await RegisterDrone();
                break;
            }
            case 10:
                _count = 0;
                break;
        }
    }

    public async Task<DroneStatusResponse> GetDroneStatus(DroneStatusRequest request)
    {
        // this method has the intention to emulate the status notification message from
        // a drone, server request the drone to get status and this method returns it

        if (!_drones.Keys.Contains(request.Id)) return await Task.FromResult(new DroneStatusResponse());

        var drone = _drones[request.Id];
        _logger.LogInformation($"drone {drone.Id} status: battery level {drone.BatteryLevel}, state {drone.State}");
        return await Task.FromResult(new DroneStatusResponse
        {
            Battery = (long)_drones[request.Id].BatteryLevel,
            State = (int)_drones[request.Id].State
        });
    }

    private async Task SimulateDroneDeliveries()
    {
        foreach (var drone in _drones.Values)
        {
            switch (drone.State)
            {
                case DroneState.Idle:
                {
                    if (drone.BatteryLevel < 100)
                        drone.BatteryLevel += 5;
                    break;
                }
                case DroneState.Delivering:
                {
                    drone.DeliveringTime += 10;

                    if (drone.BatteryLevel > 0 && drone.DeliveringTime < 100)
                        drone.BatteryLevel -= 5;

                    if (drone.DeliveringTime >= 150)
                    {
                        await _apiClient.DroneHasDelivered(new DroneHasDeliveredRequest{Id = drone.Id});
                        drone.DeliveringTime = 0;
                    }

                    break;
                }
            }
        }
    }

    private async Task<RegisterDroneResponse?> RegisterDrone()
    {
        _logger.LogInformation("RegisterDrone");
        
        var request = new RegisterDroneRequest
        {
            Model = DroneModel.CruiserWeight,
            SerialNumber = $"DRONE_{_drones.Keys.Count + 1}",
            State = DroneState.Idle,
            BatteryCapacity = 100,
            WeightLimit = 500,
        };
        var response = await _apiClient.RegisterDrone(request);
        if (response == null || !response.Success)
        {
            if(response != null)
                _logger.LogError($"drone could not be register. {response.Message}, {response.Errors.JoinAsString()}");
            return null;
        }
        _logger.LogInformation("Drone has been register");
        await GetDroneList();
        return response;
    }

    private async Task<LoadDroneResponse?> LoadDrone()
    {
        _logger.LogInformation("LoadDrone");
        var res = new LoadDroneResponse();
        var drones = _drones.Values.Where(x => (x.State == DroneState.Idle) == _idle);
        if (!drones.Any())
        {
            _logger.LogInformation("There is no drone available to load");
            return null;
        }

        var drone = drones.First();
        var request = new LoadDroneRequest();
        request.Id = drone.Id;
        float weight = 0;
        for (var i = 0; i < 15; i++)
        {
            var medication = GetMedication();
            var load = _validLoad
                ? drone.WeightLimit > weight + medication.Weight
                : drone.WeightLimit < weight + medication.Weight;
            if (!load) continue;
            request.MedicationIds.Add(medication.Id);
            weight += medication.Weight;
        }

        var response = await _apiClient.LoadDrone(request);
        _idle = !_idle;
        if (response == null || !response.Success)
        {
            if (response != null)
                _logger.LogError($"{response.Message}\n{response.Errors.JoinAsString()}");
            return null;
        }

        _logger.LogInformation("Drone loaded");
        await GetDroneList();
        _validLoad = !_validLoad;
        return res;
    }

    private async Task<MedicationsInDroneResponse?> GetMedicationsInDrone(int id)
    {
        _logger.LogInformation("GetMedicationsInDrone");
        var res = await _apiClient.MedicationsInDrone(new MedicationsInDroneRequest { Id = id });
        if (res == null) return null;

        var info = new StringBuilder();
        info.Append($"Medications in drone {id}\n");
        if (res.Medications.Count == 0)
        {
            info.Append("Drone has no medications loaded");
            _logger.LogInformation(info.ToString());
            return null;
        }

        info.Append("Id\t Weight\t\tName\n");
        foreach (var med in res.Medications)
        {
            var idx = med.Id < 10 ? $" {med.Id}" : $"{med.Id}";
            info.Append($"      {idx}\t  {med.Weight}\t   {med.Name}\n");
        }

        _logger.LogInformation(info.ToString());
        return res;
    }

    private async Task<AvailableDronesResponse?> GetListAvailableDronesForLoading()
    {
        _logger.LogInformation("GetListAvailableDronesForLoading");
        var drones = await _apiClient.AvailableDronesForLoading();
        if (drones == null) return null;
        var info = new StringBuilder();
        info.Append("\tAvailable drones to load\n");
        if (drones.AvailableDrones.Count == 0)
        {
            info.Append("There is no available drones\n");
            _logger.LogInformation(info.ToString());
            return null;
        }

        info.Append(" Id \t Serial Number\t Battery Level\n");
        foreach (var drone in drones.AvailableDrones)
        {
            info.Append($"  {drone.Id}\t   {drone.SerialNumber}\t\t{drone.BatteryCapacity}\n");
        }

        _logger.LogInformation(info.ToString());
        return drones;
    }

    private async Task<BatteryLevelInDroneResponse?> GetBatteryLevelInDrone(BatteryLevelInDroneRequest request)
    {
        _logger.LogInformation("GetBatteryLevelInDrone");
        var res = await _apiClient.BatteryLevelInDrone(request);
        if (res == null) return null;
        var info = new StringBuilder();
        info.Append($" drone: {request.Id}, battery level:{res.BatteryLevel}");
        _logger.LogInformation(info.ToString());
        return res;
    }

    private async Task<ListDronesResponse?> GetDroneList()
    {
        _logger.LogInformation("GetDroneList");
        var drones = await _apiClient.GetListDrones();
        if (drones == null) return null;
        var listInfo = new StringBuilder();
        _drones.Clear();
        listInfo.Append("\tDrone list\n");
        listInfo.Append(" Id \t Serial Number\t Battery Level\t  State\n");
        foreach (var drone in drones.Drones)
        {
            listInfo.Append($"  {drone.Id}\t   {drone.SerialNumber}\t\t{drone.BatteryCapacity}\t{drone.State.ToString()}\n");
            _drones.Add(drone.Id, new Drone
            {
                Id = drone.Id,
                BatteryLevel = drone.BatteryCapacity,
                State = drone.State,
                WeightLimit = drone.WeightLimit
            });
        }

        _logger.LogInformation(listInfo.ToString());

        return drones;
    }

    private async Task<ListMedicationsResponse?> GetListMedications()
    {
        _logger.LogInformation("GetListMedications");
        var res = await _apiClient.GetListMedications();
        if (res == null) return null;

        _medications.Clear();
        var info = new StringBuilder();
        info.Append("Id\t Weight\t\tName\n");
        foreach (var med in res.Medications)
        {
            _medications.Add(med.Id, new Medication { Id = med.Id, Name = med.Name, Weight = med.Weight });
            var id = med.Id < 10 ? $" {med.Id}" : $"{med.Id}";
            info.Append($"      {id}\t  {med.Weight}\t   {med.Name}\n");
        }

        _logger.LogInformation(info.ToString());

        return res;
    }

    private Medication GetMedication()
    {
        var id = new Random().Next(1, _medications.Count);
        return _medications[id];
    }
}