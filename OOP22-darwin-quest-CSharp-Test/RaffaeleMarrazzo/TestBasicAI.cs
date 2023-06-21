using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.AI;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp_Test.RaffaeleMarrazzo;

[TestFixture]
public class TestBasicAI
{
    private const int SEED = 353_232;
    private const uint N_ITERATIONS = 100;
    private const uint BANION_HP = 100;
    private const uint MOVE_DAMAGE = 10;

    private readonly IAI ai = new BasicAI();
    private static readonly ISet<IMove> moves = ImmutableHashSet.CreateRange<IMove>(new[] { new BasicMove(MOVE_DAMAGE, "1", new Neutral()),
        new BasicMove(MOVE_DAMAGE, "2", new Neutral()),
        new BasicMove(MOVE_DAMAGE, "3", new Neutral()),
        new BasicMove(MOVE_DAMAGE, "4", new Neutral()) });

    [Test]
    public void TestDeployBanion()
    {
        ISet<IBanion> banions = ImmutableHashSet.CreateRange<IBanion>(new[] { new Banion(new Neutral(), "b1", BANION_HP, moves), 
            new Banion(new Neutral(), "b2", BANION_HP, moves), 
            new Banion(new Neutral(), "b3", BANION_HP, moves)});
        IBanion deployedBanion = ai.DecideBanionDeployment(banions);
        Assert.That(banions.Contains(deployedBanion), Is.True);
    }

    [Test]
    public void TestMoveSelection()
    {
        IMove chosenMove = ai.DecideMoveSelection(moves);
        Assert.That(moves.Contains(chosenMove), Is.True);
    }

    [Test]
    public void TestBanionSwap()
    {
        IBanion b1 = new Banion(new Neutral(), "b1", BANION_HP, moves);
        IBanion b2 = new Banion(new Neutral(), "b2", BANION_HP, moves);
        ISet<IBanion> banions = ImmutableHashSet.CreateRange<IBanion>(new[] { b1, b2 });
        var deployedBanion1 = ai.DecideBanionSwap(banions);
        Assert.That(deployedBanion1 is not null && banions.Contains(deployedBanion1), Is.True);
        b2.DecreaseHp(b2.Hp);
        var deployedBanion2 = ai.DecideBanionSwap(banions);
        Assert.That(deployedBanion2 is not null && deployedBanion2.Equals(b1));
        b1.DecreaseHp(b1.Hp);
        var deployedBanion3 = ai.DecideBanionSwap(banions);
        Assert.That(deployedBanion3 is null, Is.True);
    }

    [Test]
    public void TestGetDecision()
    {
        IAI ai1 = new BasicAI(SEED);
        IAI ai2 = new BasicAI(SEED);
        ICollection<IDecision> decisions1 = new HashSet<IDecision>();
        ICollection<IDecision> decisions2 = new HashSet<IDecision>();
        for (uint i = 1; i <= N_ITERATIONS; i++)
        {
            decisions1.Add(ai1.GetDecision());
            decisions2.Add(ai2.GetDecision());
        }
        Assert.That(decisions1, Is.EqualTo(decisions2));
    }
}
