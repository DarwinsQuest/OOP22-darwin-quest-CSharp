using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;

public class MoveDecision : MiscDecision
{

    public MoveDecision() : base("Move")
    {
    }

    public override ITurn GetAssociatedTurn(ITurn previousTurn) => new MoveTurn(previousTurn);

}
