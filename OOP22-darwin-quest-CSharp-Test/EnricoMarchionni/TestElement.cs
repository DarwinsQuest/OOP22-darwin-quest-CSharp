using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni;

[TestFixture]
internal class TestElement
{

    [Test]
    public void Miscellaneus()
    {
        var air = new Air();
        var electro = new Electro();

        Assert.Multiple(() =>
        {
            Assert.That(air.IsWeaker(air), Is.True);
            Assert.That(electro.IsStronger(electro), Is.True);
            Assert.That(electro.Name, Is.EqualTo("Electro"));
        });
    }
}
