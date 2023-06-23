using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

public class DeployTurn : AbstractTurn, IDeployTurn
{
    private IBanion? _deployedBanion;
    public IBanion Action
    {
        get
        {
            if (HasBeenDone)
            {
                return _deployedBanion ?? throw new ArgumentNullException(nameof(_deployedBanion));
            }
            else
            {
                throw new InvalidOperationException("The action has already been done.");
            }
        }
    }

    public DeployTurn(IGameEntity entityOnTurn, IGameEntity otherEntity) : base(entityOnTurn, otherEntity)
    {
        _deployedBanion = null;
    }

    public DeployTurn(ITurn previousTurn) : base(previousTurn)
    {
        _deployedBanion = null;
    }

    public override string ToString()
    {
        if (HasBeenDone)
        {
            return "DeployTurn[ " + EntityOnTurn.Name + " deployed the Banion " + Action + "]";
        }
        else
        {
            return "The turn has not been done yet.";
        }
    }

    protected override void DoAction()
    {
        IBanion currentBanion = EntityOnTurn.DeployBanion();
        SetCurrentlyDeployedBanion(currentBanion);
        _deployedBanion = currentBanion.Copy();
    }

}
