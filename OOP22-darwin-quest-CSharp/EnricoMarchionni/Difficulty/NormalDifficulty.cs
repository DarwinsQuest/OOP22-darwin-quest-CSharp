using System.ComponentModel;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.World;
using OOP22_darwin_quest_CSharp_Test.EnricoMarchionni;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Difficulty;

/*
 * This class is not the same as in java because this "partial" implementation
 * permitted me to implement OOP22_darwin_quest_CSharp.EnricoMarchionni.Engine.
 */
[Description("Normal")]
public class NormalDifficulty : IDifficulty
{
    private const uint LEVELS = 10;

    public NormalDifficulty()
    {
        Board = new Board(LEVELS, new BasicSupplier());
    }

    public IBoard Board { get; }
}
