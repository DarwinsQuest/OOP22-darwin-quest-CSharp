using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp_Test.RaffaeleMarrazzo;

[TestFixture]
public class TestMove
{
    private const uint ILLEGAL_BASE_DAMAGE = 0;
    private const uint LEGAL_BASE_DAMAGE_1 = 20;
    private const uint LEGAL_BASE_DAMAGE_2 = 10;
    private const String MOVE_NAME_1 = "Fireball";
    private const String MOVE_NAME_2 = "Splash";
    private readonly IElement neutral = new Neutral();

    [Test]
    public void TestMoveCreation()
    {
        Assert.Throws<ArgumentException>(() => new BasicMove(ILLEGAL_BASE_DAMAGE, MOVE_NAME_1, neutral));
        Assert.Throws<ArgumentException>(() => new BasicMove(LEGAL_BASE_DAMAGE_1, "", neutral));
        Assert.Throws<ArgumentException>(() => new BasicMove(LEGAL_BASE_DAMAGE_1, "  ", neutral));
        Assert.Throws<ArgumentException>(() => new BasicMove(LEGAL_BASE_DAMAGE_1, null, neutral));
        Assert.Throws<ArgumentNullException>(() => new BasicMove(LEGAL_BASE_DAMAGE_1, MOVE_NAME_1, null));

        IDamageMove move = new BasicMove(LEGAL_BASE_DAMAGE_1, MOVE_NAME_1, neutral);
        Assert.That(move.BaseDamage, Is.EqualTo(LEGAL_BASE_DAMAGE_1));
        Assert.That(move.Element, Is.EqualTo(neutral));
        Assert.That(move.Name, Is.EqualTo(MOVE_NAME_1));
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

}
