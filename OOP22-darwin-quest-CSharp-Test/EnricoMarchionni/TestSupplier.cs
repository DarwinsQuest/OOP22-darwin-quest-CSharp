using OOP22_darwin_quest_CSharp.EnricoMarchionni.Difficulty;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni;

internal class TestSupplier : IPositiveIntSupplier
{
    private const int MAX = 4;
    private readonly Random random = new Random();

    public uint Max => MAX;

    public uint Next()
    {
        return (uint) random.Next(1, MAX + 1);
    }
}
