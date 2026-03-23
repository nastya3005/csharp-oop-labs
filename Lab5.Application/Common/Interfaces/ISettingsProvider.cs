namespace Lab5.Application.Common.Interfaces;

public interface ISettingsProvider
{
    string GetAdminPassword();

    int GetSessionTimeoutMinutes();
}