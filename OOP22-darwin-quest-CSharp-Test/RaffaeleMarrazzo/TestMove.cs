using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp_Test.RaffaeleMarrazzo;

[TestFixture]
public class TestMove
{
    private const uint MIN_DAMAGE_INFLICTED = 1;
    private const uint ILLEGAL_BASE_DAMAGE = 0;
    private const uint LEGAL_BASE_DAMAGE_1 = 20;
    private const uint LEGAL_BASE_DAMAGE_2 = 10;
    private const uint HP = 100;
    private const String MOVE_NAME_1 = "Fireball";
    private const String MOVE_NAME_2 = "Splash";
    private readonly IElement neutral = new Neutral();

    [Test]
    public void TestMoveCreation()
    {
        Assert.Throws<ArgumentException>(() => new BasicMove(ILLEGAL_BASE_DAMAGE, MOVE_NAME_1, neutral));
        Assert.Throws<ArgumentException>(() => new BasicMove(LEGAL_BASE_DAMAGE_1, "", neutral));
        Assert.Throws<ArgumentException>(() => new BasicMove(LEGAL_BASE_DAMAGE_1, "  ", neutral));

        IDamageMove move = new BasicMove(LEGAL_BASE_DAMAGE_1, MOVE_NAME_1, neutral);
        Assert.That(move.BaseDamage, Is.EqualTo(LEGAL_BASE_DAMAGE_1));
        Assert.That(move.Element, Is.EqualTo(neutral));
        Assert.That(move.Name, Is.EqualTo(MOVE_NAME_1));
    }

    [Test]
    public void TestComputeDamage()
    {
        var damageMove = new BasicMove(LEGAL_BASE_DAMAGE_2, "move", new Air());
        var banions = CreateBanions();
        foreach (var banion in banions)
        {
            Assert.That(damageMove.ComputeDamage(banion), Is.GreaterThanOrEqualTo(MIN_DAMAGE_INFLICTED));
        }
    }

    [Test]
    public void TestPerformDamage()
    {
        var damageMove = new BasicMove(LEGAL_BASE_DAMAGE_2, "move", new Air());
        var banions = CreateBanions();
        foreach (var banion in banions)
        {
            var banionHpBeforeMove = banion.Hp;
            damageMove.Perform(banion);
            Assert.That(damageMove.ComputeDamage(banion), Is.EqualTo(banionHpBeforeMove - banion.Hp));
        }
    }

    [Test]
    public void TestEquality()
    {
        IDamageMove m1 = new BasicMove(LEGAL_BASE_DAMAGE_1, MOVE_NAME_1, neutral);
        IDamageMove m2 = new BasicMove(LEGAL_BASE_DAMAGE_2, MOVE_NAME_1, neutral);
        IDamageMove m3 = new BasicMove(LEGAL_BASE_DAMAGE_2, MOVE_NAME_1, new Air());
        IDamageMove m4 = new BasicMove(LEGAL_BASE_DAMAGE_1, MOVE_NAME_1, neutral);
        IDamageMove m5 = new BasicMove(LEGAL_BASE_DAMAGE_1, MOVE_NAME_2, neutral);

        Assert.That(m1, Is.EqualTo(m4));
        Assert.That(m1, !Is.EqualTo(m2));
        Assert.That(m2, !Is.EqualTo(m3));
        Assert.That(m1, !Is.EqualTo(m5));
    }

    private static IList<IBanion> CreateBanions()
    {
        var fire = new ImmutableElement("Fire", ImmutableHashSet.Create<string>(), ImmutableHashSet.Create<string>());
        var water = new ImmutableElement("Water", ImmutableHashSet.Create<string>(), ImmutableHashSet.Create<string>());
        var grass = new ImmutableElement("Grass", ImmutableHashSet.Create<string>(), ImmutableHashSet.Create<string>());
        var air = new ImmutableElement("Air", ImmutableHashSet.Create<string>(), ImmutableHashSet.Create<string>());
        IImmutableSet<IMove> b1Moves = ImmutableHashSet.Create<IMove>(new BasicMove(LEGAL_BASE_DAMAGE_1, "m1", fire),
                new BasicMove(LEGAL_BASE_DAMAGE_2, "m2", fire),
                new BasicMove(LEGAL_BASE_DAMAGE_1, "m3", fire),
                new BasicMove(LEGAL_BASE_DAMAGE_2, "m4", fire));
        IImmutableSet<IMove> b2Moves = ImmutableHashSet.Create<IMove>(new BasicMove(LEGAL_BASE_DAMAGE_1, "m1", water),
                new BasicMove(LEGAL_BASE_DAMAGE_2, "m2", water),
                new BasicMove(LEGAL_BASE_DAMAGE_1, "m3", water),
                new BasicMove(LEGAL_BASE_DAMAGE_2, "m4", water));
        IImmutableSet<IMove> b3Moves = ImmutableHashSet.Create<IMove>(new BasicMove(LEGAL_BASE_DAMAGE_1, "m1", grass),
                new BasicMove(LEGAL_BASE_DAMAGE_2, "m2", grass),
                new BasicMove(LEGAL_BASE_DAMAGE_1, "m3", grass),
                new BasicMove(LEGAL_BASE_DAMAGE_2, "m4", grass));
        IImmutableSet<IMove> b4Moves = ImmutableHashSet.Create<IMove>(new BasicMove(LEGAL_BASE_DAMAGE_1, "m1", air),
                new BasicMove(LEGAL_BASE_DAMAGE_2, "m2", air),
                new BasicMove(LEGAL_BASE_DAMAGE_1, "m3", air),
                new BasicMove(LEGAL_BASE_DAMAGE_2, "m4", air));
        IBanion b1 = new Banion(fire, "b1", HP, new HashSet<IMove>(b1Moves));
        IBanion b2 = new Banion(water, "b2", HP, new HashSet<IMove>(b2Moves));
        IBanion b3 = new Banion(grass, "b3", HP, new HashSet<IMove>(b3Moves));
        IBanion b4 = new Banion(air, "b4", HP, new HashSet<IMove>(b4Moves));
        return new List<IBanion> { b1, b2, b3, b4 };
    }

}
