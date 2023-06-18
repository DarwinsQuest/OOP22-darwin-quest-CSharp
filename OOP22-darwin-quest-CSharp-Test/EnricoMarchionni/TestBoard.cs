using OOP22_darwin_quest_CSharp.EnricoMarchionni.World;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni;

[TestFixture]
public class TestBoard
{

    [Test]
    public void Movement()
    {
        uint levels = 10;
        var board = new Board(levels, new TestSupplier());
        uint pos = 0;

        while (board.Move().HasValue)
        {
            pos = board.Pos;
        }

        Assert.Multiple(() =>
        {
            Assert.That(pos, Is.EqualTo(board.LastPos));
            Assert.That(board.CanMove, Is.False);
        });
    }
}
