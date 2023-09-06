using System.Data;
using FluentValidation;
using Musala.Drones.Domain.Models;

namespace Musala.Drones.Api.Validators;

public class DroneValidator : AbstractValidator<Drone>
{
    public DroneValidator()
    {
        RuleFor(x => x.SerialNumber).Length(0, 100);
        RuleFor(x => x.WeightLimit).LessThan(501);
        RuleFor(x => x.BatteryCapacity).LessThan(101);
        // RuleFor(x +> x.)
    }
}