using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

public interface ISwapTurn : ITurn
{
    Tuple<IBanion, IBanion?> Action { get; }

}
