using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;

public class SwapDecision : MiscDecision
{

    public SwapDecision() : base("Swap")
    {
    }

    public override ITurn GetAssociatedTurn(ITurn previousTurn) => new SwapTurn(previousTurn);

}
