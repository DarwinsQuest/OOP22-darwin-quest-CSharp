using OOP22_darwin_quest_CSharp.Cipollone.Evolution;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;
using static OOP22_darwin_quest_CSharp.Cipollone.Evolution.EvolvableBanion;

namespace OOP22_darwin_quest_CSharp_Test.Cipollone;

[TestFixture]
public class LinearEvolutionTest
{
    private const string Name = "Bob";
    private const uint DefaultHp = 10;
    private const uint MaxHp = 30;
    private const double HpMultiplier = 0.15;
    private const uint MoveDamage = 10;
    private BanionStats _lastRecord = null!;
    private static readonly IElement Neutral = new Neutral();
    private static readonly ISet<IMove> Moves = new HashSet<IMove>()
    {
        new BasicMove(MoveDamage, "1", Neutral),
        new BasicMove(MoveDamage, "2", Neutral),
        new BasicMove(MoveDamage, "3", Neutral),
        new BasicMove(MoveDamage, "4", Neutral)
    };
    private static readonly BanionStats DefaultRecord = new(1, DefaultHp, DefaultHp);

    private static IEnumerable<TestCaseData> GetDefaultBanion
    {
        get
        {
            yield return new TestCaseData(new EvolvableBanion(Neutral, Name, DefaultHp, Moves));
        }
    }

    [Test, TestCaseSource(nameof(GetDefaultBanion))]
    public void EvolveTest(EvolvableBanion b)
    {
        Assert.That(b.Evolve(banion => banion.Level > 10), Is.False);
        AssertRecordEquals(DefaultRecord, b);
        _lastRecord = new BanionStats(2, AddPercentage(DefaultRecord.Hp), AddPercentage(DefaultRecord.MaxHp));
        Assert.That(b.Evolve(banion => banion.Level == 1), Is.True);
        AssertRecordEquals(_lastRecord, b);
        Assert.That(b.Evolve(_ => false), Is.False);
        AssertRecordEquals(_lastRecord, b);
        _lastRecord = new BanionStats(3, AddPercentage(_lastRecord.Hp), AddPercentage(_lastRecord.MaxHp));
        Assert.That(b.Evolve(banion => banion.Level % 2 == 0), Is.True);
        AssertRecordEquals(_lastRecord, b);
        Assert.That(b.Evolve(banion => banion.Level % 2 == 0), Is.False);
        AssertRecordEquals(_lastRecord, b);
    }

    [Test, TestCaseSource(nameof(GetDefaultBanion))]
    public void EvolveToLevelTest(EvolvableBanion b)
    {
        _lastRecord = new BanionStats(10, MaxHp, MaxHp);
        Assert.That(b.EvolveToLevel(10, banion => banion.Level <= 10), Is.True);
        AssertRecordEquals(_lastRecord, b);
        // Banion reached MaxHp. HP Cannot further evolve.
        _lastRecord = new BanionStats(15, MaxHp, MaxHp);
        Assert.That(b.EvolveToLevel(15, banion => banion.Level < 15), Is.True);
        AssertRecordEquals(_lastRecord, b);
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentException>(() => b.EvolveToLevel(5, _ => true));
            Assert.That(b.EvolveToLevel(20, _ => false), Is.False);
        });
        AssertRecordEquals(_lastRecord, b);
    }

    [Test, TestCaseSource(nameof(GetDefaultBanion))]
    public void RollbackTest(EvolvableBanion b)
    {
        Assert.That(b.EvolveToLevel(10, banion => banion.Level % 2 == 0), Is.False);
        AssertRecordEquals(DefaultRecord, b);
    }

    [Test, TestCaseSource(nameof(GetDefaultBanion))]
    public void IncreaseXpTest(EvolvableBanion b)
    {
        Assert.That(b.Xp, Is.EqualTo(0));
        b.IncreaseXp(5);
        Assert.That(b.Xp, Is.EqualTo(5));
        AssertRecordEquals(DefaultRecord, b);
        _lastRecord = new BanionStats(2, AddPercentage(DefaultRecord.Hp), AddPercentage(DefaultRecord.MaxHp));
        b.IncreaseXp(20);
        Assert.That(b.Xp, Is.EqualTo(5));
        AssertRecordEquals(_lastRecord, b);
        b.IncreaseXp(15);
        Assert.That(b.Xp, Is.EqualTo(20));
        AssertRecordEquals(_lastRecord, b);
        _lastRecord = new BanionStats(3, AddPercentage(_lastRecord.Hp), AddPercentage(_lastRecord.MaxHp));
        b.IncreaseXp(5);
        Assert.That(b.Xp, Is.EqualTo(5));
        AssertRecordEquals(_lastRecord, b);
        Assert.Throws<ArgumentException>(() => b.IncreaseXp(0));
    }

    private void AssertRecordEquals(BanionStats record, EvolvableBanion banion)
    {
        var currentRecord = new BanionStats(banion.Level, banion.Hp, banion.MaxHp);
        Assert.That(record, Is.EqualTo(currentRecord));
    }

    private uint AddPercentage(uint stat)
    {
        const double increase = 1 + HpMultiplier;
        return (uint)Math.Round(stat * increase);
    }
    
}
