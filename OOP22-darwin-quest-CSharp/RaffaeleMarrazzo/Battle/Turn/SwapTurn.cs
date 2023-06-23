using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

public class SwapTurn : AbstractTurn, ISwapTurn
{
    private readonly IBanion _oldBanion;
    private IBanion? _newBanion;
    public Tuple<IBanion, IBanion?> Action
    {
        get
        {
            if (HasBeenDone)
            {
                return new Tuple<IBanion, IBanion?>(_oldBanion, _newBanion);
            }
            else
            {
                throw new InvalidOperationException("The action has not been done yet.");
            }
        }
    }

    public SwapTurn(ITurn previousTurn) : base(previousTurn)
    {
        if (previousTurn.OtherEntityCurrentlyDeployedBanion is null)
        {
            throw new ArgumentException("The entity not on turn in previousTurn must have a currently deployed banion.");
        }
        else
        {
            _oldBanion = previousTurn.OtherEntityCurrentlyDeployedBanion!.Copy();
            _newBanion = null;
        }
    }

    public override string ToString()
    {
        if (HasBeenDone)
        {
            return "SwapTurnImpl[ " + EntityOnTurn.Name + " swapped the banion " + Action.Item1
                    + " with the banion " + Action.Item2 + "]";
        }
        else
        {
            return "The turn hasn't already been done.";
        }
    }

    protected override void DoAction()
    {
        var chosenBanion = EntityOnTurn.SwapBanion();
        SetCurrentlyDeployedBanion(chosenBanion);
        _newBanion = chosenBanion?.Copy();
    }
}
