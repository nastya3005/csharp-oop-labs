using Itmo.ObjectOrientedProgramming.Lab3.Creatures;
using Itmo.ObjectOrientedProgramming.Lab3.Game;
using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;
using Itmo.ObjectOrientedProgramming.Lab3.Spells;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab3.Tests;

public class TestsCases
{
    [Fact]
    public void BattleAnalyst_Attack_IncreasesAttackBonus()
    {
        var analyst = new BattleAnalyst();
        var target = new Creature("Test", 1, 10);
        int initialAttack = analyst.Attack;

        analyst.AttackCreature(target);

        Assert.Equal(initialAttack + 2, analyst.Attack);
    }

    [Fact]
    public void ViciousFighter_TakeDamage_DoublesAttack()
    {
        var fighter = new ViciousFighter();
        int initialAttack = fighter.Attack;

        fighter.TakeDamage(1);

        Assert.Equal(initialAttack * 2, fighter.Attack);
    }

    [Fact]
    public void MimicChest_Attack_CopiesStrongerStats()
    {
        var mimic = new MimicChest();
        var target = new Creature("Strong", 5, 10);

        mimic.AttackCreature(target);

        Assert.Equal(5, mimic.Attack);
        Assert.Equal(10, mimic.Health);
    }

    [Fact]
    public void ImmortalHorror_FirstDeath_Revives()
    {
        var horror = new ImmortalHorror();

        horror.TakeDamage(4);

        Assert.True(horror.IsAlive);
        Assert.Equal(1, horror.Health);
    }

    [Fact]
    public void ImmortalHorror_SecondDeath_Permanent()
    {
        var horror = new ImmortalHorror();
        horror.TakeDamage(4);

        horror.TakeDamage(1);

        Assert.False(horror.IsAlive);
    }

    [Fact]
    public void AmuletMaster_HasInitialModifiers()
    {
        var factory = new CreatureFactory();
        Interfaces.ICreature master = factory.CreateAmuletMaster();

        var testCreature = new Creature("Test", 10, 10);
        master.AttackCreature(testCreature);

        Assert.True(master.IsAlive);
    }

    [Fact]
    public void Creature_Clone_CreatesIndependentCopy()
    {
        var original = new Creature("Test", 3, 3);

        Interfaces.ICreature clone = original.Clone();
        original.TakeDamage(2);

        Assert.Equal(3, clone.Health);
        Assert.Equal(1, original.Health);
    }

    [Fact]
    public void Creature_Modify_ChangesStatsCorrectly()
    {
        var creature = new Creature("Test", 2, 5);

        creature.Modify(3, -2);

        Assert.Equal(5, creature.Attack);
        Assert.Equal(3, creature.Health);
    }

    [Fact]
    public void MagicShieldMod_BlocksFirstDamage()
    {
        var creature = new Creature("Test", 2, 5);
        var shielded = new MagicShieldMod(creature);

        shielded.TakeDamage(100);

        Assert.True(shielded.IsAlive);
        Assert.Equal(5, shielded.Health);
    }

    [Fact]
    public void AttackMasteryMod_AttacksTwice_WhenTargetSurvives()
    {
        var attacker = new AttackMasteryMod(new Creature("Attacker", 2, 10));
        var target = new Creature("Target", 1, 10);
        int initialHealth = target.Health;

        attacker.AttackCreature(target);

        Assert.Equal(initialHealth - 4, target.Health);
    }

    [Fact]
    public void MultipleMagicShields_BlockMultipleTimes()
    {
        var creature = new Creature("Test", 2, 5);
        var doubleShielded = new MagicShieldMod(new MagicShieldMod(creature));

        doubleShielded.TakeDamage(100);
        doubleShielded.TakeDamage(100);

        Assert.True(doubleShielded.IsAlive);
        Assert.Equal(5, doubleShielded.Health);
    }

    [Fact]
    public void Modifier_Clone_PreservesBehavior()
    {
        var original = new MagicShieldMod(new Creature("Test", 2, 5));

        Interfaces.ICreature clone = original.Clone();

        original.TakeDamage(100);
        clone.TakeDamage(100);

        Assert.True(original.IsAlive);
        Assert.True(clone.IsAlive);
    }

    [Fact]
    public void StrengthSpell_IncreasesAttackBy5()
    {
        var creature = new Creature("Test", 2, 5);
        var spell = new StrengthSpell();

        Interfaces.ICreature result = spell.Apply(creature);

        Assert.Equal(7, result.Attack);
    }

    [Fact]
    public void MagicMirrorSpell_SwapsAttackAndHealth()
    {
        var creature = new Creature("Test", 3, 8);
        var spell = new MagicMirrorSpell();

        Interfaces.ICreature result = spell.Apply(creature);

        Assert.Equal(8, result.Attack);
        Assert.Equal(3, result.Health);
    }

    [Fact]
    public void DefenseAmuletSpell_AddsMagicShield()
    {
        var creature = new Creature("Test", 2, 5);
        var spell = new DefenseAmuletSpell();

        Interfaces.ICreature result = spell.Apply(creature);

        result.TakeDamage(100);
        Assert.True(result.IsAlive);
    }

    [Fact]
    public void Catalog_GetCreature_ReturnsCorrectType()
    {
        var catalog = new Catalog();

        Interfaces.ICreature analyst = catalog.GetCreature("BattleAnalyst");
        Interfaces.ICreature fighter = catalog.GetCreature("ViciousFighter");

        Assert.IsType<BattleAnalyst>(analyst);
        Assert.IsType<ViciousFighter>(fighter);
    }

    [Fact]
    public void Catalog_InvalidName_ThrowsException()
    {
        var catalog = new Catalog();

        Assert.Throws<ArgumentException>(() => catalog.GetCreature("InvalidCreature"));
    }

    [Fact]
    public void CreatureBuilder_WithMethods_ModifyCreature()
    {
        var catalog = new Catalog();
        CreatureBuilder builder = catalog.GetCreatureBuilder("BattleAnalyst");

        Interfaces.ICreature customCreature = builder
            .WithAttack(10)
            .WithHealth(20)
            .WithModifier<MagicShieldMod>()
            .Creature;

        Assert.Equal(10, customCreature.Attack);
        Assert.Equal(20, customCreature.Health);
    }

    [Fact]
    public void CreatureBuilder_CreateCustom_CreatesNewCreature()
    {
        var baseCreature = new Creature("Base", 1, 1);
        var builder = new CreatureBuilder(baseCreature);

        Interfaces.ICreature custom = builder
            .WithAttack(15)
            .WithHealth(25)
            .WithModifier<AttackMasteryMod>()
            .Creature;

        Assert.Equal(15, custom.Attack);
        Assert.Equal(25, custom.Health);
    }

    [Fact]
    public void PlayerTable_AddCreature_RespectsMaxLimit()
    {
        var table = new PlayerTable();
        var creature = new Creature("Test", 1, 1);

        for (int i = 0; i < 7; i++)
        {
            table.AddCreature(creature);
        }

        bool eighthAdded = table.AddCreature(creature);
        Assert.False(eighthAdded);
        Assert.Equal(7, table.CreatureCount);
    }

    [Fact]
    public void PlayerTable_GetRandomAttacker_ReturnsValidAttacker()
    {
        var table = new PlayerTable();
        var attacker = new Creature("Attacker", 3, 5);
        var deadCreature = new Creature("Dead", 2, 1);
        var zeroAttack = new Creature("Zero", 0, 5);

        table.AddCreature(attacker);
        table.AddCreature(deadCreature);
        table.AddCreature(zeroAttack);

        Interfaces.ICreature? randomAttacker = table.GetRandomAttacker();

        Assert.NotNull(randomAttacker);
        Assert.True(randomAttacker.IsAlive);
        Assert.True(randomAttacker.Attack > 0);
    }

    [Fact]
    public void PlayerTable_ResetBattleStats_ClearsTemporaryBonuses()
    {
        var table = new PlayerTable();
        var analyst = new BattleAnalyst();
        var fighter = new ViciousFighter();

        table.AddCreature(analyst);
        table.AddCreature(fighter);

        BattleAnalyst tableAnalyst = table.Creatures.OfType<BattleAnalyst>().First();
        ViciousFighter tableFighter = table.Creatures.OfType<ViciousFighter>().First();

        tableAnalyst.AttackCreature(new Creature("Target", 1, 10));
        tableFighter.TakeDamage(1);

        int analystBonusAttack = tableAnalyst.Attack;
        int fighterBonusAttack = tableFighter.Attack;

        table.ResetBattleCreatures();

        Assert.True(tableAnalyst.Attack < analystBonusAttack);
        Assert.True(tableFighter.Attack < fighterBonusAttack);
    }

    [Fact]
    public void Battle_Fight_PlayerWins_WhenOpponentDies()
    {
        var battle = new Battle();
        var table1 = new PlayerTable();
        var table2 = new PlayerTable();

        table1.AddCreature(new Creature("Strong", 10, 10));
        table2.AddCreature(new Creature("Weak", 1, 1));

        BattleResult result = battle.Fight(table1, table2);

        Assert.Equal(BattleResult.Player1Win, result);
    }

    [Fact]
    public void Battle_Fight_Draw_WhenNoAttackers()
    {
        var battle = new Battle();
        var table1 = new PlayerTable();
        var table2 = new PlayerTable();

        table1.AddCreature(new Creature("NoAttack1", 0, 10));
        table2.AddCreature(new Creature("NoAttack2", 0, 10));

        BattleResult result = battle.Fight(table1, table2);

        Assert.Equal(BattleResult.Draw, result);
    }

    [Fact]
    public void Battle_Fight_AlternatesTurns()
    {
        var battle = new Battle();
        var table1 = new PlayerTable();
        var table2 = new PlayerTable();

        table1.AddCreature(new Creature("P1Creature", 1, 150));
        table2.AddCreature(new Creature("P2Creature", 1, 150));

        BattleResult result = battle.Fight(table1, table2);

        Assert.Equal(BattleResult.Draw, result);
    }
}