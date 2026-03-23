using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Game;

public class Catalog
{
    private readonly CreatureFactory _factory = new CreatureFactory();

    public ICreature GetCreature(string name)
    {
        return name switch
        {
            "BattleAnalyst" => _factory.CreateBattleAnalyst(),
            "ViciousFighter" => _factory.CreateViciousFighter(),
            "MimicChest" => _factory.CreateMimicChest(),
            "ImmortalHorror" => _factory.CreateImmortalHorror(),
            "AmuletMaster" => _factory.CreateAmuletMaster(),
            _ => throw new ArgumentException($"Creature {name} not found in catalog"),
        };
    }

    public CreatureBuilder GetCreatureBuilder(string name)
    {
        return new CreatureBuilder(GetCreature(name));
    }

    public IEnumerable<string> GetAvailableCreatures()
    {
        return new[] { "BattleAnalyst", "ViciousFighter", "MimicChest", "ImmortalHorror", "AmuletMaster" };
    }
}