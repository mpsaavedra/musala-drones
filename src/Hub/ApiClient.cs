using Musala.Drones.Contracts.Dtos.Requests;
using Musala.Drones.Contracts.Dtos.Responses;

namespace Musala.Drones.Hub;

public interface IApiClient
{
    Task<ListDronesResponse?> GetListDrones();
    Task<ListMedicationsResponse?> GetListMedications();
    Task<MedicationsInDroneResponse?> MedicationsInDrone(MedicationsInDroneRequest request);
    Task<AvailableDronesResponse?> AvailableDronesForLoading();
    Task<BatteryLevelInDroneResponse?> BatteryLevelInDrone(BatteryLevelInDroneRequest request);
    Task<LoadDroneResponse?> LoadDrone(LoadDroneRequest request);
    Task<RegisterDroneResponse?> RegisterDrone(RegisterDroneRequest request);

    Task<DroneHasDeliveredResponse?> DroneHasDelivered(DroneHasDeliveredRequest request);
}

public class ApiClient : IApiClient
{
    private readonly HttpClient _client;

    public ApiClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<ListDronesResponse?> GetListDrones()
    {
        using var response = await _client.PostAsync("Dispatch/ListDrones/", null);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ListDronesResponse>()!;
        return null;
    }

    public async Task<ListMedicationsResponse?> GetListMedications()
    {
        using var response = await _client.PostAsync("Dispatch/ListMedications/", null);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<ListMedicationsResponse>();
        return null;
    }

    public async Task<MedicationsInDroneResponse?> MedicationsInDrone(MedicationsInDroneRequest request)
    {
        using var response = await _client.PostAsJsonAsync("Dispatch/MedicationsInDrone", request);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<MedicationsInDroneResponse>();
        return null;
    }

    public async Task<AvailableDronesResponse?> AvailableDronesForLoading()
    {
        using var response = await _client.PostAsync("Dispatch/AvailableDronesForLoading", null);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<AvailableDronesResponse>();
        return null;
    }

    public async Task<BatteryLevelInDroneResponse?> BatteryLevelInDrone(BatteryLevelInDroneRequest request)
    {
        using var response = await _client.PostAsJsonAsync("Dispatch/BatteryLevelInDrone", request);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<BatteryLevelInDroneResponse>();
        return null;
    }

    public async Task<LoadDroneResponse?> LoadDrone(LoadDroneRequest request)
    {
        using var response = await _client.PostAsJsonAsync("Dispatch/LoadDrone", request);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<LoadDroneResponse>();
        return null;
    }

    public async Task<RegisterDroneResponse?> RegisterDrone(RegisterDroneRequest request)
    {
        using var response = await _client.PostAsJsonAsync("Dispatch/RegisterDrone", request);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<RegisterDroneResponse>();
        return null;
    }

    public async Task<DroneHasDeliveredResponse?> DroneHasDelivered(DroneHasDeliveredRequest request)
    {
        using var response = await _client.PostAsJsonAsync("Dispatch/DroneHasDelivered", request);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<DroneHasDeliveredResponse>();
        return null;
    }
}