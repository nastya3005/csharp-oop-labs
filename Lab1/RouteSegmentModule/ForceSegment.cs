using Itmo.ObjectOrientedProgramming.Lab1.TrainModule;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSegmentModule;

public class ForceSegment : RouteSegment
{
    public ForceSegment(double length, double force)
    {
        Length = length;
        Force = force;
    }

    public double Length { get; }

    public double Force { get; }

    public override bool TryPassThrough(Train train, out double timeSpent)
    {
        if (!train.TryApplyForce(Force))
        {
            timeSpent = 0;
            return false;
        }

        bool result = train.TryPassDistance(Length, out timeSpent);

        train.TryApplyForce(0); // перестаем применять силу

        return result;
    }
}