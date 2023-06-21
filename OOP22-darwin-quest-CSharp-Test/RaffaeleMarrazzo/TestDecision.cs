using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;

namespace OOP22_darwin_quest_CSharp_Test.RaffaeleMarrazzo;

[TestFixture]
public class TestDecision
{
    [Test]
    public void TestEquality()
    {
        IDecision d1 = new MoveDecision();
        IDecision d2 = new SwapDecision();
        IDecision d3 = new MoveDecision();
        IDecision d4 = new SwapDecision();
        Assert.That(d1, !Is.EqualTo(d2));
        Assert.That(d1, Is.EqualTo(d3));
        Assert.That(d2, Is.EqualTo(d4));
    }
}
