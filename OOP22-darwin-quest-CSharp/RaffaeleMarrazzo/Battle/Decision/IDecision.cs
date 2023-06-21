using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;

public interface IDecision
{
    ITurn GetAssociatedTurn(ITurn previousTurn);

}
