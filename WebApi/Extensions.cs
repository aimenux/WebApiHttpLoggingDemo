namespace Api;

public static class Extensions
{
    public static bool IgnoreCaseContains(this string input, string part)
    {
        return input?.Contains(part, StringComparison.OrdinalIgnoreCase) == true;
    }
}