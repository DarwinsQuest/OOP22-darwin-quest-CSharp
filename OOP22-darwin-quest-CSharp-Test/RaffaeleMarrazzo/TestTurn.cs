using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.AI;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp_Test.RaffaeleMarrazzo;

[TestFixture]
public class TestTurn
{
    private const int SIZE = 200;
    private const uint HP = 100;
    private const uint DAMAGE_1 = 20;
    private const uint DAMAGE_2 = 15;
    private readonly IGameEntity E1 = new Opponent("E1", new BasicAI());
    private readonly IGameEntity E2 = new Opponent("E2", new BasicAI());

    [OneTimeSetUp]
    public void AddBanions()
    {
        var fire = new ImmutableElement("Fire", ImmutableHashSet.Create<string>(), ImmutableHashSet.Create<string>());
        var water = new ImmutableElement("Water", ImmutableHashSet.Create<string>(), ImmutableHashSet.Create<string>());
        var grass = new ImmutableElement("Grass", ImmutableHashSet.Create<string>(), ImmutableHashSet.Create<string>());
        var air = new ImmutableElement("Air", ImmutableHashSet.Create<string>(), ImmutableHashSet.Create<string>());
        IImmutableSet<IMove> b1Moves = ImmutableHashSet.Create<IMove>(new BasicMove(DAMAGE_1, "m1", fire),
                new BasicMove(DAMAGE_2, "m2", fire),
                new BasicMove(DAMAGE_1, "m3", fire),
                new BasicMove(DAMAGE_2, "m4", fire));
        IImmutableSet<IMove> b2Moves = ImmutableHashSet.Create<IMove>(new BasicMove(DAMAGE_1, "m1", water),
                new BasicMove(DAMAGE_2, "m2", water),
                new BasicMove(DAMAGE_1, "m3", water),
                new BasicMove(DAMAGE_2, "m4", water));
        IImmutableSet<IMove> b3Moves = ImmutableHashSet.Create<IMove>(new BasicMove(DAMAGE_1, "m1", grass),
                new BasicMove(DAMAGE_2, "m2", grass),
                new BasicMove(DAMAGE_1, "m3", grass),
                new BasicMove(DAMAGE_2, "m4", grass));
        IImmutableSet<IMove> b4Moves = ImmutableHashSet.Create<IMove>(new BasicMove(DAMAGE_1, "m1", air),
                new BasicMove(DAMAGE_2, "m2", air),
                new BasicMove(DAMAGE_1, "m3", air),
                new BasicMove(DAMAGE_2, "m4", air));
        IBanion b1 = new Banion(fire, "b1", HP, new HashSet<IMove>(b1Moves));
        IBanion b2 = new Banion(water, "b2", HP, new HashSet<IMove>(b2Moves));
        IBanion b3 = new Banion(grass, "b3", HP, new HashSet<IMove>(b3Moves));
        IBanion b4 = new Banion(air, "b4", HP, new HashSet<IMove>(b4Moves));
        E1.AddToInventory(b1);
        E1.AddToInventory(b2);
        E2.AddToInventory(b3);
        E2.AddToInventory(b4);
    }

    [Test]
    public void TestDeployTurnCreation()
    {
        ITurn turn = new DeployTurn(E1, E2);
        Assert.Throws<ArgumentNullException>(() => new DeployTurn(null, E2));
        Assert.Throws<ArgumentNullException>(() => new DeployTurn(E1, null));
        Assert.Throws<ArgumentNullException>(() => new DeployTurn(null));
        Assert.Throws<ArgumentException>(() => new DeployTurn(turn));
        turn.PerformAction();
        var deployTurn = new DeployTurn(turn);
        deployTurn.PerformAction();
        Assert.That(deployTurn.GetEntityOnTurn(), Is.EqualTo(turn.GetOtherEntity()));
        Assert.That(deployTurn.GetOtherEntity(), Is.EqualTo(turn.GetEntityOnTurn()));
        Assert.That(deployTurn.OtherEntityCurrentlyDeployedBanion(), Is.EqualTo(turn.OnTurnCurrentlyDeployedBanion()));
    }

    [Test]
    public void TestMoveTurnCreation()
    {
        ITurn turn = new DeployTurn(E1, E2);
        Assert.Throws<ArgumentNullException>(() => new MoveTurn(null));
        Assert.Throws<ArgumentException>(() => new MoveTurn(turn));
        turn.PerformAction();
        Assert.Throws<ArgumentException>(() => new MoveTurn(turn));
        var deployTurn = DoDeployTurns();
        var moveTurn = new MoveTurn(deployTurn);
        moveTurn.PerformAction();
        Assert.That(moveTurn.GetEntityOnTurn(), Is.EqualTo(deployTurn.GetOtherEntity()));
        Assert.That(moveTurn.GetOtherEntity(), Is.EqualTo(deployTurn.GetEntityOnTurn()));
        Assert.That(moveTurn.OnTurnCurrentlyDeployedBanion(), Is.EqualTo((deployTurn.OtherEntityCurrentlyDeployedBanion())));
        Assert.That(moveTurn.OtherEntityCurrentlyDeployedBanion(), Is.EqualTo(deployTurn.OnTurnCurrentlyDeployedBanion()));
    }

    [Test]
    public void TestSwapTurnCreation()
    {
        ITurn turn = new DeployTurn(E1, E2);
        Assert.Throws<ArgumentNullException>(() => new SwapTurn(null));
        Assert.Throws<ArgumentException>(() => new SwapTurn(turn));
        turn.PerformAction();
        Assert.Throws<ArgumentException>(() => new SwapTurn(turn));
        var deployTurn = DoDeployTurns();
        var swapTurn = new SwapTurn(deployTurn);
        swapTurn.PerformAction();
        Assert.That(swapTurn.GetEntityOnTurn(), Is.EqualTo(deployTurn.GetOtherEntity()));
        Assert.That(swapTurn.GetOtherEntity(), Is.EqualTo(deployTurn.GetEntityOnTurn()));
        Assert.That(swapTurn.OtherEntityCurrentlyDeployedBanion(), Is.EqualTo(deployTurn.OnTurnCurrentlyDeployedBanion()));
    }

    [Test]
    public void TestEquality()
    {
        var turns = Enumerable.Repeat<IDeployTurn>(new DeployTurn(E1, E2), SIZE).ToList();
        turns.ForEach(t1 => turns.Where(t2 => t2 != t1).ToList().ForEach(t3 => Assert.That(t1, !Is.EqualTo(t3))));
    }

    [Test]
    public void TestBanionGetters()
    {
        ITurn turn = new DeployTurn(E1, E2);
        Assert.Throws<InvalidOperationException>(() => turn.OnTurnCurrentlyDeployedBanion());
        Assert.Throws<InvalidOperationException>(() => turn.OtherEntityCurrentlyDeployedBanion());
        turn.PerformAction();
        Assert.DoesNotThrow(() => turn.OnTurnCurrentlyDeployedBanion());
        Assert.DoesNotThrow(() => turn.OtherEntityCurrentlyDeployedBanion());
    }

    [Test]
    public void TestDeployTurnDevelopment()
    {
        var turn = new DeployTurn(E1, E2);
        turn.PerformAction();
        IBanion deployedBanion = turn.Action;
        AssertBanionEquality(turn.OnTurnCurrentlyDeployedBanion()!, deployedBanion);
    }

    [Test]
    public void TestMoveTurnDevelopment()
    {
        var previousTurn = DoDeployTurns();
        var turn = new MoveTurn(previousTurn);
        var passiveBanionBeforeAction = previousTurn.OnTurnCurrentlyDeployedBanion()?.Copy();
        var activeBanion = previousTurn.OtherEntityCurrentlyDeployedBanion();
        turn.PerformAction();
        var actionDone = turn.Action;
        var chosenMove = (IDamageMove)actionDone.Item1;
        AssertBanionEquality(turn.OnTurnCurrentlyDeployedBanion()!, actionDone.Item2);
        AssertBanionEquality(turn.OtherEntityCurrentlyDeployedBanion()!, actionDone.Item3);
        Assert.That(chosenMove.ComputeDamage(actionDone.Item2, actionDone.Item3), Is.EqualTo(passiveBanionBeforeAction?.Hp - actionDone.Item3.Hp));
    }

    [Test]
    public void TestSwapTurnDevelopment()
    {
        var previousTurn = DoDeployTurns();
        var swapTurn = new SwapTurn(previousTurn);
        IBanion oldBanion = previousTurn.OtherEntityCurrentlyDeployedBanion()!;
        swapTurn.PerformAction();
        var banions = swapTurn.Action;
        AssertBanionEquality(oldBanion, banions.Item1);
        if (banions.Item2 is not null)
        {
            AssertBanionEquality(swapTurn.OnTurnCurrentlyDeployedBanion()!, banions.Item2);
        }
    }

    private IDeployTurn DoDeployTurns()
    {
        var turn1 = new DeployTurn(E1, E2);
        turn1.PerformAction();
        var turn2 = new DeployTurn(turn1);
        turn2.PerformAction();
        return turn2;
    }

    private void AssertBanionEquality(IBanion b1, IBanion b2)
    {
        Assert.That(b1.Element, Is.EqualTo(b2.Element));
        Assert.That(b1.Name, Is.EqualTo(b2.Name));
        Assert.That(b1.MaxHp, Is.EqualTo(b2.MaxHp));
    }

}
