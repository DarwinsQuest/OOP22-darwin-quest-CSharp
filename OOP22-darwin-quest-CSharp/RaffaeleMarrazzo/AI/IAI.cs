using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.AI;
public interface IAI
{
    IBanion DecideBanionDeployment(ICollection<IBanion> banions);

    IMove DecideMoveSelection(ICollection<IMove> moves);

    IBanion? DecideBanionSwap(ICollection<IBanion> banions);

    IDecision GetDecision();
}
