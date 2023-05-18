using System.Text;
using System.Text.RegularExpressions;

namespace MediaManager.API.Helpers;

public static class ExtensionMethods
{
    public static string GenerateMoniker(this string phrase)
    {
        string str = RemoveAccent(phrase).ToLower();
        // invalid chars
        str = Regex.Replace(str, @"[^-.a-z0-9\s]", "");

        // convert multiple spaces into one space   
        str = Regex.Replace(str, @"\s+", " ").Trim();

        // cut and trim 
        var extension = Path.GetExtension(str);
        var baseName = str;
        if (!String.IsNullOrWhiteSpace(extension))
        {
            baseName = str.Substring(0, str.IndexOf(extension));
        }

        baseName = baseName.Substring(0, baseName.Length <= 45 ? baseName.Length : 45).Trim();
        str = Regex.Replace(baseName, @"\s", "-"); // hyphens   
        return str + extension;
    }

    public static string RemoveAccent(string txt)
    {
        // Encoding ascii = Encoding.ASCII;
        Encoding unicode = Encoding.Unicode;

        byte[] bytes = unicode.GetBytes(txt);
        return Encoding.ASCII.GetString(bytes);
    }
}
