using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP22_darwin_quest_CSharp.EnricoMarchionni;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni;

[TestFixture]
internal class TestEngine
{

    [Test]
    public void Difficulties()
    {
        var engine = new Engine();
        Assert.That(engine.Difficulties, Is.EqualTo(ImmutableSortedSet.Create("Normal")));

        foreach (var diff in engine.Difficulties)
        {
            var tmp_engine = new Engine();
            Assert.Multiple(() =>
            {
                Assert.Throws(typeof(InvalidOperationException), () => { var board = tmp_engine.Board; });
                Assert.That(tmp_engine.StartGame(diff), Is.True);
            });
        }
    }
}
