using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public class ViciousFighter : Creature
{
    private readonly int _baseAttack;
    private int _battleAttackBonus = 0;

    public ViciousFighter() : base("ViciousFighter", 1, 6)
    {
        _baseAttack = 1;
    }

    public override void TakeDamage(int damage)
    {
        if (!IsAlive || damage <= 0)
            return;

        int healthBefore = Health;
        base.TakeDamage(damage);

        if (IsAlive && healthBefore > Health)
        {
            _battleAttackBonus = _baseAttack;
            Attack += _battleAttackBonus;
        }
    }

    public void ResetBattleStats()
    {
        Attack -= _battleAttackBonus;
        _battleAttackBonus = 0;
    }

    public override ICreature Clone()
    {
        return new ViciousFighter();
    }
}