using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace Itmo.ObjectOrientedProgramming.Lab3.Game;

public class PlayerTable
{
    private const int MaxCreaturesOnTable = 7;

    private readonly List<ICreature> _creatures = new List<ICreature>();

    public ReadOnlyCollection<ICreature> Creatures => _creatures.AsReadOnly();

    public int CreatureCount => _creatures.Count;

    public bool IsFull => _creatures.Count >= MaxCreaturesOnTable;

    public bool HasTargets => GetTargets().Any();

    public bool HasAttackers => GetAttackers().Any();

    public bool AddCreature(ICreature? creaturePrototype)
    {
        if (IsFull || creaturePrototype == null)
            return false;

        ICreature creatureCopy = creaturePrototype.Clone();
        _creatures.Add(creatureCopy);
        return true;
    }

    public bool RemoveCreature(ICreature creature)
    {
        return _creatures.Remove(creature);
    }

    public void ClearTable()
    {
        _creatures.Clear();
    }

    public IEnumerable<ICreature> GetAttackers()
    {
        return _creatures.Where(creature => creature.IsAlive && creature.Attack > 0).ToList();
    }

    public IEnumerable<ICreature> GetTargets()
    {
        return _creatures.Where(creature => creature.IsAlive).ToList();
    }

    public ICreature? GetRandomAttacker()
    {
        var attackers = GetAttackers().ToList();
        return attackers.Count > 0 ? attackers[RandomNumberGenerator.GetInt32(attackers.Count)] : null;
    }

    public ICreature? GetRandomTarget()
    {
        var targets = GetTargets().ToList();
        return targets.Count > 0 ? targets[RandomNumberGenerator.GetInt32(targets.Count)] : null;
    }

    public bool HasAliveCreatures()
    {
        return _creatures.Any(creature => creature.IsAlive);
    }

    public void ResetBattleCreatures()
    {
        foreach (BattleAnalyst creature in _creatures.OfType<BattleAnalyst>())
        {
            creature.ResetBattleStats();
        }

        foreach (ViciousFighter fighter in _creatures.OfType<ViciousFighter>())
        {
            fighter.ResetBattleStats();
        }
    }
}