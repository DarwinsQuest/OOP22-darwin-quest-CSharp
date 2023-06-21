using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

public interface IMoveTurn : ITurn
{
    Tuple<IMove, IBanion, IBanion> Action { get; }

}
