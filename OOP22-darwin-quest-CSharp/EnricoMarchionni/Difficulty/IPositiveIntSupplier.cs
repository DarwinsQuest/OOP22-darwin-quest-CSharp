namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Difficulty;

public interface IPositiveIntSupplier
{
    uint Max { get; }

    uint Next();
}
