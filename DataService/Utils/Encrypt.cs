using System.Security.Cryptography;
using System.Text;

namespace DataService.Utils;

#region Encode And Compare
public static class Encrypt
{
    static readonly string saltStr = @"3c6851e2e42c296bc75270ca2fe92c97";
    static string GetSecretKey()
        => string.IsNullOrEmpty(saltStr)
        ? ""
        : saltStr;

    public static string GenSalt(string pwd, string? key = null)
    {
        string gen = string.Empty;
        foreach (char c in pwd)
        {
            gen += c;
            foreach (char s in key ?? GetSecretKey())
            {
                gen += c + s;
            }

            gen += c;
        }
        return gen;

    }
    public static string GenMD5(string pwd)
    {
        MD5 md5Hasher = MD5.Create();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(pwd));

        StringBuilder gen = new();
        foreach (int i in data) gen.Append(i.ToString("x2"));

        return gen.ToString();
    }

    public static string GenSaltMD5(string pwd, string key = "")
    {
        string gen = GenSalt(pwd, key);
        gen = GenMD5(gen);
        return gen;
    }
    public static bool CompareGenSaltMD5(string pwd, string hash)
        => GenSaltMD5(pwd) == hash;
}
#endregion
