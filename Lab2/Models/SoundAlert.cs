namespace Itmo.ObjectOrientedProgramming.Lab2.Models;

public class SoundAlert : AAlert
{
    private readonly int _frequency;
    private readonly int _duration;

    public SoundAlert(int frequency = 1000, int duration = 500)
    {
        _frequency = frequency;
        _duration = duration;
    }

    public override void TriggerAlert()
    {
        if (OperatingSystem.IsWindows())
        {
            Console.Beep(_frequency, _duration);
        }
        else
        {
            Console.WriteLine("Beeep!!");
        }
    }
}