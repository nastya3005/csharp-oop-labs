using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public class AmuletMaster : Creature
{
    public AmuletMaster() : base("AmuletMaster", 5, 2) { }

    public override ICreature Clone()
    {
        return new AmuletMaster();
    }
}