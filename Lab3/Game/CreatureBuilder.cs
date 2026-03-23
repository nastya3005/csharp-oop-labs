using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;
using Activator = System.Activator;

namespace Itmo.ObjectOrientedProgramming.Lab3.Game;

public class CreatureBuilder
{
    public ICreature Creature { get; set; }

    public CreatureBuilder(ICreature creature)
    {
        Creature = creature;
    }

    public CreatureBuilder WithAttack(int attack)
    {
        Creature.Attack = attack;
        return this;
    }

    public CreatureBuilder WithHealth(int health)
    {
        Creature.Health = health;
        return this;
    }

    public CreatureBuilder WithModifier<T>() where T : BaseMod
    {
        if (Activator.CreateInstance(typeof(T), Creature) is T modifier)
        {
            Creature = modifier;
        }

        return this;
    }

    public CreatureBuilder WithSpell(ISpell spell)
    {
        if (spell != null)
        {
            Creature = spell.Apply(Creature);
        }

        return this;
    }
}