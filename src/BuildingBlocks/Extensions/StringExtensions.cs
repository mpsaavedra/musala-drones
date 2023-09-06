using System.Text.RegularExpressions;

namespace Musala.Drones.BuildingBlocks.Extensions;

public static class StringExtensions
{
    public static bool IsValidMedicationName(this string name) =>
        Regex.IsMatch(name, @"^[a-zA-Z\d-_]+$");

    
    public static bool IsValidMedicationCode(this string name) =>
        Regex.IsMatch(name, @"^[A-Z\d_]+$");
}