using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public class StrengthSpell : ISpell
{
    public ICreature Apply(ICreature creature)
    {
        creature.Modify(5, 0);
        return creature;
    }
}