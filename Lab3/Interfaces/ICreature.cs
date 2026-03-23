namespace Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

public interface ICreature : ICreaturePrototype
{
    string Name { get; }

    int Attack { get; set; }

    int Health { get; set; }

    bool IsAlive { get; }

    void AttackCreature(ICreature target);

    void TakeDamage(int damage);

    void Modify(int attackMod, int healthMod);
}