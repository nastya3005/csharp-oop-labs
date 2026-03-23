using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public class BattleAnalyst : Creature
{
    private int _battleAttackBonus = 0;

    public BattleAnalyst() : base("BattleAnalyst", 2, 4) { }

    public override void AttackCreature(ICreature target)
    {
        if (IsAlive && target.IsAlive)
        {
            _battleAttackBonus += 2;
            Attack += 2;
        }

        base.AttackCreature(target);
    }

    public void ResetBattleStats()
    {
        Attack -= _battleAttackBonus;
        _battleAttackBonus = 0;
    }

    public override ICreature Clone()
    {
        return new BattleAnalyst();
    }
}