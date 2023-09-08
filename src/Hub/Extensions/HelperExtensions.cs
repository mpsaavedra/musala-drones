using System.Text;

namespace Musala.Drones.Hub.Extensions;

public static class HelperExtensions
{
    public static string JoinAsString(this List<string> source, string separator = ", ", bool carrierReturn = true)
    {
        var result = new StringBuilder();
        foreach (var line in source)
        {
            var carrier = carrierReturn ? "\n" : "";
            result.Append($"{line}{carrier}");
        }
        return result.ToString();
    }
}