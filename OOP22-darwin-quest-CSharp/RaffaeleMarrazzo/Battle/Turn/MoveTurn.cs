using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

public class MoveTurn : AbstractTurn, IMoveTurn
{
    private IMove? _actionDone;
    private readonly IBanion _activeBanion;
    private readonly IBanion _passiveBanion;
    private readonly IBanion _activeBanionCopy;
    private IBanion? _passiveBanionCopy;

    public Tuple<IMove, IBanion, IBanion> Action
    {
        get
        {
            if (HasBeenDone)
            {
                return new Tuple<IMove, IBanion, IBanion>(_actionDone!, _activeBanionCopy, _passiveBanionCopy!);
            }
            else
            {
                throw new InvalidOperationException("The action has not been done yet.");
            }
        }
    }

    public MoveTurn(ITurn previousTurn) : base (previousTurn)
    {
        if (previousTurn.OnTurnCurrentlyDeployedBanion is null || previousTurn.OtherEntityCurrentlyDeployedBanion is null)
        {
            throw new ArgumentException("All the entities must have a currently deployed banion.");
        }
        else
        {
            _actionDone = null;
            _activeBanion = previousTurn.OtherEntityCurrentlyDeployedBanion!;
            _passiveBanion = previousTurn.OnTurnCurrentlyDeployedBanion!;
            _activeBanionCopy = _activeBanion.Copy();
            _passiveBanionCopy = null;
        }
    }

    public override string ToString()
    {
        if (HasBeenDone)
        {
            return "MoveTurnImpl[ " + EntityOnTurn.Name + " performed the move " + Action.Item1
                    + " with the banion " + Action.Item2 + " against the banion " + Action.Item3 + "]";
        }
        else
        {
            return "The turn hasn't already been done.";
        }
    }

    protected override void DoAction()
    {
        _actionDone = EntityOnTurn.SelectMove(_activeBanion);
        _actionDone.Perform(_passiveBanion);
        _passiveBanionCopy = _passiveBanion.Copy();
    }
}
