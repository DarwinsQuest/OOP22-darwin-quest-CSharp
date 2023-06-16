using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni
{
    [TestFixture]
    public class TestBanion
    {
        private const uint BANION_HP = 100;
        private uint count;

        [Test]
        public void Stats()
        {
            IBanion banion = new Banion(new Neutral(), "testBanionStats", BANION_HP);
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
        }

        [Test]
        public void Observer()
        {
            IBanion banion = new Banion(new Neutral(), "testBanionObserver", 100);
            uint hpDecrease = 10;
            uint observableCalls = 3;

            Assert.That(banion.IsAlive, Is.True);
            banion.BanionChanged += Banion_BanionChanged1;
            banion.BanionChanged += Banion_BanionChanged2;
            banion.DecreaseHp(hpDecrease);
            banion.BanionChanged -= Banion_BanionChanged1;
            banion.DecreaseHp(hpDecrease);
            Assert.That(count, Is.EqualTo(observableCalls));
        }

        [Test]
        public void Identifier()
        {
            IBanion banion = new Banion(new Neutral(), "testBanionId", 100);
            Assert.That(new Banion(new Neutral(), "testBanionId", 100), Is.Not.EqualTo(banion));
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
