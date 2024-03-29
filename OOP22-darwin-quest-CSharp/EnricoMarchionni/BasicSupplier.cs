using OOP22_darwin_quest_CSharp.EnricoMarchionni.Difficulty;

namespace OOP22_darwin_quest_CSharp_Test.EnricoMarchionni;

/*
 * This class doesn't exist in the java project.
 * I added this class here mainly for test purposes.
 */
public class BasicSupplier : IPositiveIntSupplier
{
    private const int MAX = 4;
    private readonly Random random = new();

    public uint Max => MAX;

    public uint Next()
    {
        return (uint) random.Next(1, MAX + 1);
    }
}
