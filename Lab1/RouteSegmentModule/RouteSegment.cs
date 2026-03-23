using Itmo.ObjectOrientedProgramming.Lab1.TrainModule;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteSegmentModule;

public abstract class RouteSegment
{
    public abstract bool TryPassThrough(Train train, out double timeSpent);
}