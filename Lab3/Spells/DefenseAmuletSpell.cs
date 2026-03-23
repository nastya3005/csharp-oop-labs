using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public class DefenseAmuletSpell : ISpell
{
    public ICreature Apply(ICreature creature)
    {
        return new MagicShieldMod(creature);
    }
}