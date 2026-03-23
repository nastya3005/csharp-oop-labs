namespace Itmo.ObjectOrientedProgramming.Lab3.Game;

public class Battle
{
    private const int MaxBattleRounds = 100;

    public BattleResult Fight(PlayerTable table1, PlayerTable table2)
    {
        int rounds = 0;

        while (rounds < MaxBattleRounds)
        {
            if (ExecuteAttack(table1, table2))
                return BattleResult.Player1Win;

            if (ExecuteAttack(table2, table1))
                return BattleResult.Player2Win;

            if ((!table1.HasAttackers && !table2.HasTargets) ||
                (!table2.HasAttackers && !table1.HasTargets))
            {
                return BattleResult.Draw;
            }

            rounds++;
        }

        return BattleResult.Draw;
    }

    private bool ExecuteAttack(PlayerTable attackerTable, PlayerTable defenderTable)
    {
        Interfaces.ICreature? attacker = attackerTable.GetRandomAttacker();
        Interfaces.ICreature? target = defenderTable.GetRandomTarget();

        if (attacker != null && target != null)
        {
            attacker.AttackCreature(target);

            if (!defenderTable.HasAliveCreatures())
                return true;
        }

        return false;
    }
}

public enum BattleResult
{
    Player1Win,
    Player2Win,
    Draw,
}