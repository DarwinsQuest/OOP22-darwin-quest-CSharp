using OOP22_darwin_quest_CSharp.Cipollone.Die;

namespace OOP22_darwin_quest_CSharp_Test.Cipollone;

[TestFixture]
public class DieTest
{
    private const int Faces4 = 4;
    private const int Faces6 = 6;
    private static IEnumerable<TestCaseData> GetAllDice
    {
        get
        {
            yield return new TestCaseData(
                new Die(),
                new Die { Faces = Faces4 },
                new Die { Faces = Faces6 },
                new Die { Faces = Faces4 });
        }
    }

    private static IEnumerable<TestCaseData> GetDice
    {
        get
        {
            yield return new TestCaseData(new Die());
            yield return new TestCaseData(new Die { Faces = Faces4 });
            yield return new TestCaseData(new Die { Faces = Faces6 });
            yield return new TestCaseData(new Die { Faces = Faces4 });
        }
    }

    [Test]
    public void DieCreationTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(new Die().Faces, Is.EqualTo(Faces6));
            Assert.That(new Die { Faces = Faces4 }.Faces, Is.EqualTo(Faces4));
            Assert.Throws<ArgumentException>(() => new Die { Faces = 0 });
            Assert.Throws<ArgumentException>(() => new Die { Faces = 2 });
            Assert.Throws<ArgumentException>(() => new Die { Faces = 5 });
        });
    }

    [Test, TestCaseSource(nameof(GetAllDice))]
    public void DieEqualityTest(Die d1, Die d2, Die d3, Die d4)
    {
        Assert.Multiple(() =>
        {
            Assert.That(d1, !Is.EqualTo(d2));
            Assert.That(d1, Is.EqualTo(d3));
            Assert.That(d2, Is.EqualTo(d4));
            Assert.That(d3, !Is.EqualTo(d4));
        });
    }

    [Test, TestCaseSource(nameof(GetAllDice))]
    public void DieFacesTest(Die d1, Die d2, Die d3, Die d4)
    {
        Assert.Multiple(() =>
        {
            Assert.That(d1.Faces, Is.EqualTo(Faces6));
            Assert.That(d2.Faces, Is.EqualTo(Faces4));
            Assert.That(d3.Faces, Is.EqualTo(Faces6));
            Assert.That(d4.Faces, Is.EqualTo(Faces4));
        });
    }

    [Test, TestCaseSource(nameof(GetDice))]
    public void DieValueTest(Die die)
    {
        const int times = 10;
        Enumerable.Range(0, times)
            .ToList()
            .ForEach(_ => Assert.That(die.Value, Is.LessThanOrEqualTo(die.Faces)));
    }
    
}
