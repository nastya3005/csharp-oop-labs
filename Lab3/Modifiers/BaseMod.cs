using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public abstract class BaseMod : ICreature
{
    private readonly ICreature _creature;

    protected BaseMod(ICreature creature)
    {
        _creature = creature;
    }

    public string Name => _creature.Name;

    public int Attack
    {
        get => _creature.Attack;
        set => _creature.Attack = value;
    }

    public int Health
    {
        get => _creature.Health;
        set => _creature.Health = value;
    }

    public bool IsAlive => _creature.IsAlive;

    public virtual void AttackCreature(ICreature target)
    {
        _creature.AttackCreature(target);
    }

    public virtual void TakeDamage(int damage)
    {
        _creature.TakeDamage(damage);
    }

    public virtual void Modify(int attackMod, int healthMod)
    {
        _creature.Modify(attackMod, healthMod);
    }

    public virtual ICreature Clone()
    {
        ICreature clonedCreature = _creature.Clone();
        return CreateModifier(clonedCreature);
    }

    protected abstract BaseMod CreateModifier(ICreature creature);
}