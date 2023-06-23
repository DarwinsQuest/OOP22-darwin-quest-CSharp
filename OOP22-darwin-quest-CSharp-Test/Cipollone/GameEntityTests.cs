using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.AI;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp_Test.Cipollone;

[TestFixture]
public class GameEntityTests
{
    private const string Name1 = "Alice";
    private const string Name2 = "Bob";
    private const uint MoveDamage = 10;
    private const uint Hp = 100;
    private static readonly IAI Ai = new BasicAI();
    private static readonly IElement Neutral = new Neutral();
    private static readonly ISet<IMove> Moves = new HashSet<IMove>()
    {
        new BasicMove(MoveDamage, "1", Neutral),
        new BasicMove(MoveDamage, "2", Neutral),
        new BasicMove(MoveDamage, "3", Neutral),
        new BasicMove(MoveDamage, "4", Neutral)
    };
    private static readonly IList<IBanion> Banions = new List<IBanion>()
    {
        new Banion(Neutral, Name2, Hp, Moves),
        new Banion(Neutral, Name2, Hp, Moves),
        new Banion(Neutral, Name2, Hp, Moves),
    };

    private static IEnumerable<TestCaseData> GetEntitiesAndBanions
    {
        get
        {
            yield return new TestCaseData(new Player(Name1), Banions[0], Banions[1], Banions[2]);
            yield return new TestCaseData(new Opponent(Name2, Ai), Banions[0], Banions[1], Banions[2]);
        }
    }

    private static IEnumerable<TestCaseData> GetPlayerThenOpponent
    {
        get
        {
            yield return new TestCaseData(new Player(Name1));
            yield return new TestCaseData(new Opponent(Name2, Ai));
        }
    }

    private static IEnumerable<TestCaseData> GetEntitiesForEqualityTest
    {
        get
        {
            yield return new TestCaseData(
                new Player(Name1),
                new Player(Name2),
                new Player(Name1));
            yield return new TestCaseData(
                new Opponent(Name1, Ai),
                new Opponent(Name2, Ai),
                new Opponent(Name1, Ai));
        }
    }

    [Test]
    public void CreatePlayerTest()
    {
        List<string> nicknames = new()
        {
            "",
            "  ",
            "123",
            "1" + Name1,
            "!" + Name2,
            "@@foo",
            "bar%",
            "€uro",
            "_a",
            "b_",
            "__c_",
            "d__",
            "Amélie",
            "Walter White",
            "JessePinkman ",
            "魚"
        };
        nicknames.ForEach(s => Assert.Throws<ArgumentException>(() => new Player(s)));  
    }

    [Test]
    public void CreateOpponentTest()
    {
        List<string> nicknames = new() {"", "  "};
        nicknames.ForEach(s => Assert.Throws<ArgumentException>(() => new Opponent(s, Ai)));
    }

    [Test]
    public void NicknameTest()
    {
        IGameEntity p = new Player(Name1);
        IGameEntity o = new Opponent(Name2, Ai);
        Assert.Multiple(() =>
        {
            Assert.That(p.Name, Is.EqualTo(Name1));
            Assert.That(o.Name, Is.EqualTo(Name2));
        });
    }

    [Test, TestCaseSource(nameof(GetPlayerThenOpponent))]
    public void AddToInventoryTest(IGameEntity entity)
    {
        // Add collection test.
        Assert.Multiple(() =>
        {
            Assert.That(entity.AddToInventory(Banions[0]), Is.True);
            Assert.That(entity.AddToInventory(Banions[1]), Is.True);
            Assert.That(Banions.ToList().GetRange(0, 2), Is.EqualTo(entity.GetInventory()));
        });
        // Add element test.
        Assert.Multiple(() =>
        {
            Assert.That(entity.AddToInventory(Banions), Is.True);
            Assert.That(Banions, Is.EqualTo(entity.GetInventory()));
        });
    }

    [Test, TestCaseSource(nameof(GetPlayerThenOpponent))]
    public void UpdateInventoryBaseTest(IGameEntity entity)
    {
        // Updating the initial inventory to contain all electro banions.
        Assert.That(entity.AddToInventory(Banions), Is.True);
        List<IBanion?> removed = Enumerable.Range(0, Banions.Count)
            .Select(i => entity.UpdateInventory(Banions[i], new Banion(new Electro(), Name2, Hp, Moves)))
            .ToList();
        Assert.Multiple(() =>
        {
            Assert.That(entity.GetInventory().All(banion => banion.Element.Equals(new Electro())), Is.True);
            Assert.That(Banions.ToList().GetRange(0, Banions.Count), Is.EqualTo(removed));
        });
    }

    [Test, TestCaseSource(nameof(GetEntitiesAndBanions))]
    public void UpdateInventorySameBanionTest(IGameEntity entity, IBanion b1, IBanion b2, IBanion b3)
    {
        Assert.Multiple(() =>
        {
            Assert.That(entity.AddToInventory(b1), Is.True);
            Assert.That(entity.AddToInventory(b1), Is.False);
            Assert.That(entity.AddToInventory(new List<IBanion> {b2, b3}), Is.True);
            Assert.That(entity.AddToInventory(new List<IBanion> {b1, b2, b3}), Is.False);
            Assert.That(entity.AddToInventory(new List<IBanion> {b1, b2, b3, new Banion(Neutral, Name2, Hp, Moves)}), Is.True);
        });
    }
    
    [Test, TestCaseSource(nameof(GetEntitiesAndBanions))]
    public void NonExistingBanionInventoryTest(IGameEntity entity, IBanion b1, IBanion b2, IBanion b3)
    {
        IBanion? retrievedBanion = entity.UpdateInventory(b1, b2);
        Assert.That(retrievedBanion, Is.Null);
    }
    
    [Test, TestCaseSource(nameof(GetEntitiesAndBanions))]
    public void InventoryContainsNewBanionTest(IGameEntity entity, IBanion b1, IBanion b2, IBanion b3)
    {
        entity.AddToInventory(new List<IBanion> { b1, b2 });
        IBanion? retrievedBanion = entity.UpdateInventory(b1, b2);
        Assert.That(retrievedBanion, Is.Null);
    }
    
    [Test, TestCaseSource(nameof(GetEntitiesAndBanions))]
    public void InventoryDoesNotContainOldBanionTest(IGameEntity entity, IBanion b1, IBanion b2, IBanion b3)
    {
        IBanion? retrievedBanion = entity.UpdateInventory(b3, new Banion(Neutral, Name2, Hp, Moves));
        Assert.That(retrievedBanion, Is.Null);
    }

    [Test, TestCaseSource(nameof(GetPlayerThenOpponent))]
    public void IsOutOfBanionsTest(IGameEntity entity)
    {
        Assert.Multiple(() =>
        {
            Assert.That(entity.AddToInventory(Banions), Is.True);
            Assert.That(entity.IsOutOfBanions, Is.False);
        });
        for (int i = 0; i < Banions.Count; i++)
        {
            var currentBanion = Banions[i];
            currentBanion.DecreaseHp(currentBanion.Hp);
            entity.UpdateInventory(currentBanion, currentBanion);
            if (i == Banions.Count - 1)
            {
                Assert.That(entity.IsOutOfBanions, Is.True);
            }
            else
            {
                Assert.That(entity.IsOutOfBanions, Is.False);
            }
        }
        Banions.ToList().ForEach(b => b.SetHpToMax());
    }

    [Test, TestCaseSource(nameof(GetEntitiesForEqualityTest))]
    public void EqualityTest(IGameEntity e1, IGameEntity e2, IGameEntity e3)
    {
        Assert.Multiple(() =>
        {
            Assert.That(e1, Is.EqualTo(e3));
            Assert.That(e1, !Is.EqualTo(e2));
        });
    }
}
