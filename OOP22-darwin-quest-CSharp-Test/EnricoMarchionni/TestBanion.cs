using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni
{
    [TestFixture]
    public class TestBanion
    {
        private readonly IBanion banion = new Banion(new Neutral(), "testBanionObserver", 100);
        private uint count;

        [Test]
        public void Observer()
        {
            uint hpDecrease = 10;
            uint observableCalls = 2;

            Assert.That(banion.IsAlive, Is.True);
            banion.BanionChanged += Banion_BanionChanged1;
            banion.BanionChanged += Banion_BanionChanged2;
            banion.DecreaseHp(hpDecrease);
            Assert.That(count, Is.EqualTo(observableCalls));
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
}
