using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public class AttackMasteryMod : BaseMod
{
    public AttackMasteryMod(ICreature creature) : base(creature) { }

    public override void AttackCreature(ICreature target)
    {
        if (!IsAlive || !target.IsAlive || Attack <= 0)
            return;

        base.AttackCreature(target);

        if (target.IsAlive && Attack > 0)
        {
            base.AttackCreature(target);
        }
    }

    protected override BaseMod CreateModifier(ICreature creature)
    {
        return new AttackMasteryMod(creature);
    }
}
