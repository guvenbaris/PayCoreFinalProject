using System.Security.Cryptography;
using System.Text;

namespace PayCore.Application.Utilities.Hash;

public class HashingHelper
{
    public static string CreatePasswordHash(string password,string email)
    {
        using MD5 md5 = MD5.Create();
        password += email;

        byte[] input = Encoding.ASCII.GetBytes(password);
        byte[] hash = md5.ComputeHash(input);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        };
        return sb.ToString();
    }
    public static bool VerifyPasswordHash(string loginPassword, string existingPassword)
    {
        for (int i = 0; i < existingPassword.Length; i++)
        {
            if (loginPassword[i] != existingPassword[i])
            {
                return false;
            }
        }
        return true;
    }
}
