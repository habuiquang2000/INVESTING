namespace DataService.Utils;

public class Uuid
{
    public static string Generate =>
        Guid.NewGuid().ToString();
    public static string GenerateWithDateTime =>
        $"{(new DateTime()).GetHashCode()}{Generate}";
}
