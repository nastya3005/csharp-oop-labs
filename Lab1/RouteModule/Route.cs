using Itmo.ObjectOrientedProgramming.Lab1.RouteSegmentModule;
using Itmo.ObjectOrientedProgramming.Lab1.TrainModule;

namespace Itmo.ObjectOrientedProgramming.Lab1.RouteModule;

public class Route
{
    private readonly List<RouteSegment> segments;

    public Route(double maxFinalSpeed)
    {
        segments = new List<RouteSegment>();
        MaxFinalSpeed = maxFinalSpeed;
    }

    public double MaxFinalSpeed { get; }

    public void AddSegment(RouteSegment segment)
    {
        segments.Add(segment);
    }

    public bool TryPassRoute(Train train, out double totalTime)
    {
        totalTime = 0;

        train.Reset();

        foreach (RouteSegment segment in segments)
        {
            if (!segment.TryPassThrough(train, out double segmentTime))
            {
                return false;
            }

            totalTime += segmentTime;
        }

        if (train.CurrentSpeed > MaxFinalSpeed)
        {
            return false;
        }

        return true;
    }
}