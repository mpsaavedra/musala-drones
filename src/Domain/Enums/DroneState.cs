namespace Musala.Drones.Domain.Enums;

public enum DroneState : byte
{
    Idle = 2, 
    Loading, 
    Loaded, 
    Delivering, 
    Delivered, 
    Returning
}