public static class DataExtractor
{
    public static string ExtractString(string line, int start, int length)
    {
        return line.Substring(start - 1, length).Trim();
    }

    public static int ExtractInt(string line, int start, int length)
    {
        return int.Parse(ExtractString(line, start, length));
    }

    public static decimal ExtractDecimal(string line, int start, int length)
    {
        return decimal.Parse(ExtractString(line, start, length));
    }

    public static DateTime ExtractDate(string line, int start, int length)
    {
        string dateString = ExtractString(line, start, length);
        return DateTime.ParseExact(dateString, "ddMMMyy", System.Globalization.CultureInfo.InvariantCulture);
    }
}