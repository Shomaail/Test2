using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BL.Data;

/// <summary>
/// Summary description for Application
/// </summary>
public static class PromotionApplication
{
    private const int SaltByteSize = 128;
    private const int HashByteSize = 128;
    private const int HasingIterationsCount = 10101;

    public static string specialCharacters = "[\"/_+|<>~;'#%^&*]";
    
    public static bool IsDependentTask(int taskID)
    {
        BAL bal = new BAL();
        if (bal.GetTaskDependency().Where(a => a.ChildTaskID == taskID).Count() > 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }
    public static bool ValidateMailingAddress(string mailingAddress)
    {
        string ma = mailingAddress.ToLower();
        if (             !ma.Contains("univ") &&
                         !ma.Contains("üniversite") && !ma.Contains("yliopisto") &&
                         !ma.Contains("uniwersytet") && !ma.Contains("Tech") &&
                         !ma.Contains("institu") &&
                         !ma.Contains("mit") && !ma.Contains("uoit") && !ma.Contains("school") &&
                         !ma.Contains("polytechnique") && !ma.Contains("scole") && !ma.Contains("جامعه") &&
                         !ma.Contains("imperial college"))
        { return false; }
        else
        { return true; }
    }
    public static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
    {
        int minHashLenght = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
        var xor = firstHash.Length ^ secondHash.Length;
        for (int i = 0; i < minHashLenght; i++)
            xor |= firstHash[i] ^ secondHash[i];
        return 0 == xor;
    }
    internal static byte[] ComputeHash(string password, byte[] salt, int iterations = HasingIterationsCount, int hashByteSize = HashByteSize)
    {
        using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt))
        {
            hashGenerator.IterationCount = iterations;
            return hashGenerator.GetBytes(hashByteSize);
        }
    }
    internal static byte[] GenerateSalt(int saltByteSize = SaltByteSize)
    {
        using (RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[saltByteSize];
            saltGenerator.GetBytes(salt);
            return salt;
        }
    }

    public static string SetTitle(string Title)
    {
        if (Title.ToLower().Contains("mr"))
        {
            Title = "Mr. ";
        }
        else if (Title.ToLower().Contains("ms"))
        {
            Title = "Ms. ";
        }
        else if (Title == "" || Title.ToLower().Contains("dr") || Title.ToLower().Contains("prof"))
        {
            Title = "Dr. ";
        }
        else if (Title.ToLower().Contains("miss"))
        {
            Title = "Miss. ";
        }
        else if (Title.ToLower().Contains("mrs"))
        {
            Title = "Mrs. ";
        }
        return Title;
    }
    public static string GetNextRank(Employee user)
    {
        if(string.IsNullOrEmpty(user.Rank))
        {
            return "";
        }
        switch (user.Rank.ToLower().Trim())
        {
            case "visiting associate professor":
                return RankProfessorial.Associate_Professor.ToString().Replace("_", " ");
            case "assistant professor":
                return RankProfessorial.Associate_Professor.ToString().Replace("_", " ");
            case "associate professor":
                return RankProfessorial.Professor.ToString().Replace("_", " ");
            case "visiting professor":
                return RankProfessorial.Professor.ToString().Replace("_", " ");
            default:
                return "";
        }
    }    
    public static string GetSalutation(string rank)
    {
        switch (rank)
        {
            case "Professor": return "Dr. ";
            case "Associate Professor": return "Dr. ";
            case "Assistant Professor": return "Dr. ";
            case "Visiting Associate Professor": return "Dr. ";
            case "Visiting Assistant Professor": return "Dr. ";
            default: return "Mr. ";
        }
    }
    public static bool IsValidEmail(this string email)
    {

        var r = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");

        return !string.IsNullOrEmpty(email) && r.IsMatch(email);
    }
    public static bool IsHTML(string text)
    {
        bool result = false;
        //   var regexItem = new Regex("<(?(?=!--)!--[\\s\\S]*--|(?(?=\\?)\\?[\\s\\S]*\\?|(?(?=\\/)\\/[^.-\\d][^\\/\\]'\"[!#$%&()*+,;<=>?@^`{|}~ ]*|[^.-\\d][^\\/\\]'\"[!#$%&()*+,;<=>?@^`{|}~ ]*(?:\\s[^.-\\d][^\\/\\]'\"[!#$%&()*+,;<=>?@^`{|}~ ]*(?:=(?:\"[^\"]*\"|'[^']*'|[^'\"<\\s]*))?)*)\\s?\\/?))>");
        var pattern = @"<[^>]*>";
        var regex = new Regex(pattern);
        if (regex.IsMatch(text))
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

}