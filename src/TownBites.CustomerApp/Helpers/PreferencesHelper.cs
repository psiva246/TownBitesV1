namespace TownBites.CustomerApp.Helpers;

public static class PreferencesHelper
{
    private const string JwtKey = "jwt";

    public static void SaveToken(string token)
    {
        Preferences.Default.Set(JwtKey, token);
    }

    public static string GetToken()
    {
        return Preferences.Default.Get(JwtKey, string.Empty);
    }

    public static void Clear()
    {
        Preferences.Default.Remove(JwtKey);
    }

    public static bool IsLoggedIn()
    {
        return !string.IsNullOrWhiteSpace(GetToken());
    }
}