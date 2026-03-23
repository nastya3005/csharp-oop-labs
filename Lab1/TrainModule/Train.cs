namespace Itmo.ObjectOrientedProgramming.Lab1.TrainModule;

public class Train
{
    public Train(double mass, double maxForce, double precision = 0.1)
    {
        if (mass <= 0)
            throw new ArgumentException("Mass must be positive");
        if (maxForce <= 0)
            throw new ArgumentException("Max force must be positive");
        if (precision <= 0)
            throw new ArgumentException("Precision must be positive");

        Mass = mass;
        MaxForce = maxForce;
        Precision = precision;
        CurrentSpeed = 0;
        Acceleration = 0;
    }

    public double Mass { get; }

    public double MaxForce { get; }

    public double Precision { get; }

    public double CurrentSpeed { get; private set; }

    public double Acceleration { get; private set; }

    public bool TryApplyForce(double force)
    {
        if (Math.Abs(force) > MaxForce)
        {
            return false;
        }

        Acceleration = force / Mass;
        return true;
    }

    public bool TryPassDistance(double distance, out double timeSpent)
    {
        timeSpent = 0;
        double remainingDistance = distance;
        double currentSpeed = CurrentSpeed;
        double acceleration = Acceleration;

        if (currentSpeed == 0 && acceleration == 0 && distance > 0)
        {
            return false;
        }

        while (remainingDistance > 0)
        {
            double newSpeed = currentSpeed + (acceleration * Precision);

            if (newSpeed < 0)
            {
                return false;
            }

            double distanceThisStep = newSpeed * Precision;

            if (distanceThisStep >= remainingDistance)
            {
                remainingDistance = 0;
            }
            else
            {
                remainingDistance -= distanceThisStep;
            }

            timeSpent += Precision;

            currentSpeed = newSpeed;
        }

        CurrentSpeed = currentSpeed;
        return true;
    }

    public void Reset()
    {
        CurrentSpeed = 0;
        Acceleration = 0;
    }
}