using Itmo.ObjectOrientedProgramming.Lab3.Interfaces;

namespace Itmo.ObjectOrientedProgramming.Lab3.Creatures;

public class Creature : ICreature
{
    public string Name { get; protected set; }

    public int Attack { get; set; }

    public int Health { get; set; }

    public bool IsAlive => Health > 0;

    public Creature(string name, int attack, int health)
    {
        if (attack < 0) throw new ArgumentException("Attack cannot be less than 0");
        if (health <= 0) throw new ArgumentException("Health must be positive");

        Name = name;
        Attack = attack;
        Health = health;
    }

    public virtual void AttackCreature(ICreature target)
    {
        if (!IsAlive || Attack <= 0 || !target.IsAlive)
            return;

        target.TakeDamage(Attack);
    }

    public virtual void TakeDamage(int damage)
    {
        if (!IsAlive || damage <= 0)
            return;

        Health -= damage;
    }

    public virtual void Modify(int attackMod, int healthMod)
    {
        Attack += attackMod;
        Health += healthMod;

        if (Attack < 0) Attack = 0;
    }

    public override string ToString()
    {
        return $"({Attack}/{Health})";
    }

    public virtual ICreature Clone()
    {
        return new Creature(Name, Attack, Health);
    }
}