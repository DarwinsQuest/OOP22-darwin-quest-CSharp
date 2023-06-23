using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.AI;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp_Test.RaffaeleMarrazzo;

[TestFixture]
public class TestBattle
{
    private const uint DAMAGE_1 = 10;
    private const uint DAMAGE_2 = 15;
    private const uint HP_1 = 20;
    private const uint HP_2 = 25;
    private const uint HP_3 = 30;
    private const uint HP_4 = 35;
    private static readonly IGameEntity E1 = new Opponent("E1", new BasicAI());
    private static readonly IGameEntity E2 = new Opponent("E2", new BasicAI());

    [SetUp]
    public void AddBanions()
    {
        var neutralElement = new Neutral();
        IImmutableSet<IMove> b1Moves = ImmutableHashSet.Create<IMove>(new BasicMove(DAMAGE_1, "m1", neutralElement),
        new BasicMove(DAMAGE_2, "m2", neutralElement),
                new BasicMove(DAMAGE_1, "m3", neutralElement),
                new BasicMove(DAMAGE_2, "m4", neutralElement));
        IImmutableSet<IMove> b2Moves = ImmutableHashSet.Create<IMove>(new BasicMove(DAMAGE_1, "m1", neutralElement),
                new BasicMove(DAMAGE_2, "m2", neutralElement),
                new BasicMove(DAMAGE_1, "m3", neutralElement),
                new BasicMove(DAMAGE_2, "m4", neutralElement));
        IImmutableSet<IMove> b3Moves = ImmutableHashSet.Create<IMove>(new BasicMove(DAMAGE_1, "m1", neutralElement),
                new BasicMove(DAMAGE_2, "m2", neutralElement),
                new BasicMove(DAMAGE_1, "m3", neutralElement),
                new BasicMove(DAMAGE_2, "m4", neutralElement));
        IImmutableSet<IMove> b4Moves = ImmutableHashSet.Create<IMove>(new BasicMove(DAMAGE_1, "m1", neutralElement),
                new BasicMove(DAMAGE_2, "m2", neutralElement),
        new BasicMove(DAMAGE_1, "m3", neutralElement),
                new BasicMove(DAMAGE_2, "m4", neutralElement));
        IBanion b1 = new Banion(neutralElement, "b1", HP_1, new HashSet<IMove>(b1Moves));
        IBanion b2 = new Banion(neutralElement, "b2", HP_2, new HashSet<IMove>(b2Moves));
        IBanion b3 = new Banion(neutralElement, "b3", HP_3, new HashSet<IMove>(b3Moves));
        IBanion b4 = new Banion(neutralElement, "b4", HP_4, new HashSet<IMove>(b4Moves));
        E1.AddToInventory(b1);
        E1.AddToInventory(b2);
        E2.AddToInventory(b3);
        E2.AddToInventory(b4);
    }

    [Test]
    public void TestEquality()
    {
        var b1 = new BattleTile(E1, E2);
        var b2 = new BattleTile(E1, E2);
        Assert.That(b1, Is.EqualTo(b2));
        Assert.That(new BattleTile(E1, E2), !Is.EqualTo(new BattleTile(E2, E1)));
    }

    [Test]
    public void TestBattleStart()
    {
        var battle = new BattleTile(E2, E1);
        battle.NewBattle();
        bool nextTurn = battle.NextTurn();
        while (nextTurn)
        {
            nextTurn = battle.NextTurn();
        }
        if (battle.IsWinner(E2))
        {
            Assert.That(battle.NewBattle(), Is.False);
        }
        else
        {
            Assert.That(battle.NewBattle(), Is.True);
        }
    }

    [Test]
    public void TestBattleDevelopment()
    {
        var battle = new BattleTile(E1, E2);
        battle.NewBattle();
        bool nextTurn = battle.NextTurn();
        while (nextTurn)
        {
            nextTurn = battle.NextTurn();
        }
        var report = battle.BattleTurns;
        for (int i = 0; i < report.Count; i++)
        {
            var currentIteratingTurn = report[i];
            if (i % 2 == 0)
            {
                Assert.That(currentIteratingTurn.GetEntityOnTurn(), Is.EqualTo(E1));
                Assert.That(currentIteratingTurn.GetOtherEntity(), Is.EqualTo(E2));
            }
            else
            {
                Assert.That(currentIteratingTurn.GetEntityOnTurn(), Is.EqualTo(E2));
                Assert.That(currentIteratingTurn.GetOtherEntity(), Is.EqualTo(E1));
            }
            var otherEntityBanion = currentIteratingTurn.OtherEntityCurrentlyDeployedBanion();
            if (otherEntityBanion is not null
                && currentIteratingTurn is MoveTurn turn
                && !turn.Action.Item3.IsAlive
                && i + 1 < report.Count)
            {
                Assert.That(report[i + 1] is SwapTurn);
                var newBanion = ((SwapTurn)report[i + 1]).Action.Item2;
                if (newBanion is not null)
                {
                    Assert.That(newBanion, !Is.EqualTo(turn.Action.Item3));
                }
            }
        }
    }

    [Test]
    public void TestBattleWinner()
    {
        var battle = new BattleTile(E1, E2);
        Assert.That(battle.IsWinner(E1), Is.False);
        Assert.That(battle.IsWinner(E2), Is.False);
        battle.NewBattle();
        bool nextTurn = battle.NextTurn();
        while (nextTurn)
        {
            nextTurn = battle.NextTurn();
        }
        var lastTurn = battle.BattleTurns[battle.BattleTurns.Count - 1];
        var loser = lastTurn.GetOtherEntity();
        Assert.That(battle.IsWinner(loser), Is.False);
        Assert.That(battle.IsWinner(new Opponent("E3", new BasicAI())), Is.False);
    }

}
