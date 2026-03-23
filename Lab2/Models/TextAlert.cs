namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class TextAlert : AAlert
{
    private readonly string _alertMessage;

    public TextAlert(string alertMessage = "⚠️ ALERT: Suspicious message detected!")
    {
        _alertMessage = alertMessage;
    }

    public override void TriggerAlert()
    {
        Console.WriteLine(_alertMessage);
    }
}