using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spells;

public class MagicMirrorSpell : ISpell
{
    public ICreature Apply(ICreature creature)
    {
        int originalAttack = creature.Attack;
        int originalHealth = creature.Health;

        creature.Attack = originalHealth;
        creature.Health = originalAttack;
        return creature;
    }
}