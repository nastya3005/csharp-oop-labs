using Lab5.Application.Common.Interfaces;

namespace Lab5.Infrastructure.Settings;

public class SettingsProvider : ISettingsProvider
{
    private string AdminPassword { get; }

    private int SessionTimeoutMinutes { get; }

    public SettingsProvider(string adminPassword, int sessionTimeoutMinutes)
    {
        AdminPassword = adminPassword;
        SessionTimeoutMinutes = sessionTimeoutMinutes;
    }

    public string GetAdminPassword()
    {
        return AdminPassword;
    }

    public int GetSessionTimeoutMinutes()
    {
        return SessionTimeoutMinutes;
    }
}