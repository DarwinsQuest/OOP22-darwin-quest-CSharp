using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni;

[TestFixture]
internal class TestBanion
{
    private const uint BANION_HP = 100;
    private uint count;

    private readonly ISet<IMove> _moves = new HashSet<IMove>()
    {
        new BasicMove(5, "move1", new Neutral()),
        new BasicMove(5, "move2", new Neutral()),
        new BasicMove(5, "move3", new Neutral()),
        new BasicMove(5, "move4", new Neutral()),
    };

    [Test]
    public void Stats()
    {
        IBanion banion = new Banion(new Neutral(), "testBanionStats", BANION_HP, _moves);
        Assert.That(banion.IsAlive, Is.True);

        uint hpDelta = 10;
        banion.DecreaseHp(hpDelta);
        Assert.That(banion.Hp, Is.EqualTo(BANION_HP - hpDelta));

        banion.IncreaseHp(hpDelta);
        Assert.That(banion.Hp, Is.EqualTo(BANION_HP));

        banion.DecreaseHp(hpDelta);
        banion.SetHpToMax();
        Assert.That(banion.Hp, Is.EqualTo(banion.MaxHp));

        banion.DecreaseHp(banion.Hp);
        Assert.That(banion.IsAlive, Is.False);

        // OVERFLOW tests

        banion.IncreaseHp(BANION_HP);
        Assert.That(banion.IsAlive, Is.True);

        banion.IncreaseHp(uint.MaxValue);
        Assert.That(banion.Hp, Is.EqualTo(banion.MaxHp));

        banion.DecreaseHp(banion.Hp);
        Assert.That(banion.IsAlive, Is.False);

        banion.IncreaseHp(BANION_HP);
        Assert.That(banion.IsAlive, Is.True);

        banion.DecreaseHp(uint.MaxValue);
        Assert.Multiple(() =>
        {
            Assert.That(banion.IsAlive, Is.False);
            Assert.That(banion.Hp, Is.EqualTo(IBanion.MIN_HP));
        });

        Assert.Throws(typeof(ArgumentException), () => banion.IncreaseHp(0));
        Assert.Throws(typeof(ArgumentException), () => banion.DecreaseHp(0));
    }

    [Test]
    public void Moves()
    {
        Assert.Throws<ArgumentException>(() => new Banion(
            new Air(),
            "testBanionStats",
            BANION_HP,
            new HashSet<IMove>()
                {
                    new BasicMove(5, "move1", new Neutral()),
                    new BasicMove(5, "move2", new Neutral()),
                    new BasicMove(5, "move3", new Electro()),
                    new BasicMove(5, "move4", new Neutral()),
                }));
    }

    [Test]
    public void Observer()
    {
        IBanion banion = new Banion(new Neutral(), "testBanionObserver", 100, _moves);
        uint hpDecrease = 10;
        uint observableCalls = 4;

        Assert.That(banion.IsAlive, Is.True);
        banion.EventBanionChanged += Banion_BanionChanged1;
        banion.EventBanionChanged += Banion_BanionChanged2;
        banion.DecreaseHp(hpDecrease); // 2 calls
        banion.EventBanionChanged -= Banion_BanionChanged1;
        banion.DecreaseHp(hpDecrease); // 1 call
        banion.MaxHp = 10; // 1 call
        Assert.Multiple(() =>
        {
            Assert.That(banion.Hp, Is.EqualTo(banion.MaxHp));
            Assert.That(count, Is.EqualTo(observableCalls));
        });
    }

    [Test]
    public void Identifier()
    {
        IBanion banion = new Banion(new Neutral(), "testBanionId", 100, _moves);
        Assert.That(new Banion(new Neutral(), "testBanionId", 100, _moves), Is.Not.EqualTo(banion));
    }

    private void Banion_BanionChanged1(object? sender, IBanion e)
    {
        count++;
    }

    private void Banion_BanionChanged2(object? sender, IBanion e)
    {
        count++;
    }
}
