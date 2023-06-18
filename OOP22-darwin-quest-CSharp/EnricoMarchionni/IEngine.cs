using OOP22_darwin_quest_CSharp.EnricoMarchionni.World;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni;

public interface IEngine
{
    IReadOnlySet<string> Difficulties { get; }

    bool StartGame(string difficulty);

    IBoard Board { get; }

    bool IsGameOver();
}
