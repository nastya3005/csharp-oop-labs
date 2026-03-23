using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public class MagicShieldMod : BaseMod
{
    private bool _shieldActive = true;

    public MagicShieldMod(ICreature creature) : base(creature) { }

    public override void TakeDamage(int damage)
    {
        if (_shieldActive && damage > 0)
        {
            _shieldActive = false;
            return;
        }

        base.TakeDamage(damage);
    }

    protected override BaseMod CreateModifier(ICreature creature)
    {
        return new MagicShieldMod(creature);
    }
}