using Itmo.ObjectOrientedProgramming.Lab1.TrainModule;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSegmentModule;

public class StationSegment : RouteSegment
{
    public StationSegment(double speedLimit, double boardingTime, double unboardingTime)
    {
        SpeedLimit = speedLimit;
        BoardingTime = boardingTime;
        UnboardingTime = unboardingTime;
    }

    public double SpeedLimit { get; }

    public double BoardingTime { get; }

    public double UnboardingTime { get; }

    public override bool TryPassThrough(Train train, out double timeSpent)
    {
        timeSpent = 0;

        if (train.CurrentSpeed > SpeedLimit)
        {
            return false;
        }

        timeSpent += BoardingTime + UnboardingTime;

        return true;
    }
}