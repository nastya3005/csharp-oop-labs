using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public class MimicChest : Creature
{
    public MimicChest() : base("MimicChest", 1, 1) { }

    public override void AttackCreature(ICreature target)
    {
        if (!IsAlive || !target.IsAlive)
            return;

        int newAttack = Math.Max(Attack, target.Attack);
        int newHealth = Math.Max(Health, target.Health);

        Attack = newAttack;
        Health = newHealth;

        base.AttackCreature(target);
    }

    public override ICreature Clone()
    {
        return new MimicChest();
    }
}