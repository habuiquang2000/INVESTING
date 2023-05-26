namespace DataService.Utils;

public class Uuid
{
    public static string Generate
    {
        get
        {
            Guid guid = Guid.NewGuid();
            string uuid = guid.ToString();
            return uuid;
        }
    }
    public static string GenerateWithDateTime
    {
        get
        {
            DateTime dateTime = new();
            return $"{dateTime.GetHashCode()}{Generate}";
        }
    }
}
