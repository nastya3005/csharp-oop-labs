using Itmo.ObjectOrientedProgramming.Lab1.TrainModule;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSegmentModule;

public class RegularSegment : RouteSegment
{
    public RegularSegment(double length)
    {
        Length = length;
    }

    public double Length { get; }

    public override bool TryPassThrough(Train train, out double timeSpent)
    {
        return train.TryPassDistance(Length, out timeSpent);
    }
}