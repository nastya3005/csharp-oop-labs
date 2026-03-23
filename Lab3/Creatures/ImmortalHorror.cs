using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public class ImmortalHorror : Creature
{
    private bool _hasRevived;

    public ImmortalHorror() : base("ImmortalHorror", 4, 4)
    {
        _hasRevived = false;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (!IsAlive && !_hasRevived)
        {
            Health = 1;
            _hasRevived = true;
        }
    }

    public override ICreature Clone()
    {
        return new ImmortalHorror();
    }
}