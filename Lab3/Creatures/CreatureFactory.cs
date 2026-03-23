using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public class CreatureFactory
{
    public ICreature CreateCreature<T>() where T : ICreature, new()
    {
        return new T();
    }

    public ICreature CreateBattleAnalyst() => new BattleAnalyst();

    public ICreature CreateAmuletMaster()
    {
        ICreature amuletMaster = new AmuletMaster();
        amuletMaster = new MagicShieldMod(amuletMaster);
        amuletMaster = new AttackMasteryMod(amuletMaster);
        return amuletMaster;
    }

    public ICreature CreateMimicChest() => new MimicChest();

    public ICreature CreateViciousFighter() => new ViciousFighter();

    public ICreature CreateImmortalHorror() => new ImmortalHorror();
}