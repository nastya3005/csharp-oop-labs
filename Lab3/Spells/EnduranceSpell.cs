using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public class EnduranceSpell : ISpell
{
    public ICreature Apply(ICreature creature)
    {
        creature.Modify(0, 5);
        return creature;
    }
}