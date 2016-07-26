using System;

public static class EnumParser {

    public static string ParseAbbreviation(Enum e)
    {
        string text = ParseUppercase(e);
        string[] res = text.Split(' ');
        string ret = "";
        foreach (string s in res)
        {
            ret += s.Substring(0, 1);
        }
        return ret;
    }

    public static string ParseUppercase(Enum e)
    {
        return ParseStartCase(e).ToUpper();
    }

    public static string ParseLowercase(Enum e)
    {
        return ParseStartCase(e).ToLower();
    }

    public static string ParseSentenceCase(Enum e)
    {
        string ret = ParseLowercase(e);
        char first = char.ToUpper(ret[0]);
        ret = ret.Substring(1);
        return first + ret;       
    }

    public static string ParseStartCase(Enum e)
    {
        string s = e.ToString();
        bool first = true;
        string ret = "";
        foreach (char c in s)
        {
            if (char.IsUpper(c))
            {
                if (!first)
                {
                    ret += " ";
                }
                else first = false;
            }
            ret += c;
        }
        return ret;
    }

  
}
