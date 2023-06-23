using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.EnricoMarchionni;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni;

[TestFixture]
internal class TestEngine
{

    [Test]
    public void Difficulties()
    {
        var player = new Player("John");
        var engine = new Engine(player);
        Assert.That(engine.Difficulties, Is.EqualTo(ImmutableSortedSet.Create("Normal")));

        foreach (var diff in engine.Difficulties)
        {
            var tmp_engine = new Engine(player);
            Assert.Multiple(() =>
            {
                Assert.Throws(typeof(InvalidOperationException), () => { var board = tmp_engine.Board; });
                Assert.That(tmp_engine.StartGame(diff), Is.True);
            });
        }
    }
}
