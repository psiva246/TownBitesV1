namespace TownBites.CustomerApp.Helpers;

public static class PreferencesHelper
{
    private const string TokenKey = "jwt_token";
    private const string UserNameKey = "user_name";
    private const string UserIdKey = "user_id";

    public static void SaveToken(string token)
    {
        Preferences.Default.Set(TokenKey, token);
    }

    public static string GetToken()
    {
        return Preferences.Default.Get(TokenKey, string.Empty);
    }

    public static void SaveUser(string name, int id)
    {
        Preferences.Default.Set(UserNameKey, name);
        Preferences.Default.Set(UserIdKey, id);
    }

    public static string GetUserName()
    {
        return Preferences.Default.Get(UserNameKey, string.Empty);
    }

    public static int GetUserId()
    {
        return Preferences.Default.Get(UserIdKey, 0);
    }

    public static void Logout()
    {
        Preferences.Default.Clear();
    }
}