using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

public abstract class AbstractTurn : ITurn
{
    private readonly Guid _id;
    private Tuple<IGameEntity, IBanion?> _entityOnTurn;
    private readonly Tuple<IGameEntity, IBanion?> _otherEntity;
    public bool HasBeenDone { get; private set; }

    public AbstractTurn(IGameEntity entityOnTurn, IGameEntity otherEntity)
    {
        _id = Guid.NewGuid();
        if (entityOnTurn is not null)
        {
            _entityOnTurn = new Tuple<IGameEntity, IBanion?>(entityOnTurn, null);
        }
        else
        {
            throw new ArgumentNullException(nameof(entityOnTurn));
        }
        if (otherEntity is not null)
        {
            _otherEntity = new Tuple<IGameEntity, IBanion?>(otherEntity, null);
        }
        else
        {
            throw new ArgumentNullException(nameof(otherEntity));
        }
    }

    public AbstractTurn(ITurn previousTurn)
    {
        if (previousTurn is null)
        {
            throw new ArgumentNullException(nameof(previousTurn));
        }
        if (!previousTurn.HasBeenDone)
        {
            throw new ArgumentException("previousTurn cannot be unperformed");
        }
        _id = Guid.NewGuid();
        _entityOnTurn = new Tuple<IGameEntity, IBanion?>(previousTurn.GetOtherEntity(), 
            previousTurn.OtherEntityCurrentlyDeployedBanion());
        _otherEntity = new Tuple<IGameEntity, IBanion?>(previousTurn.GetEntityOnTurn(), 
            previousTurn.OnTurnCurrentlyDeployedBanion());
    }

    public IGameEntity GetEntityOnTurn() => _entityOnTurn.Item1;

    public IGameEntity GetOtherEntity() => _otherEntity.Item1;

    public IBanion? OnTurnCurrentlyDeployedBanion() // can be a property?
    {
        if (HasBeenDone)
        {
            return _entityOnTurn.Item2;
        } 
        else
        {
            throw new InvalidOperationException("The turn must be done in order to call this method.");
        }
    }

    public IBanion? OtherEntityCurrentlyDeployedBanion() // can be a property?
    {
        if (HasBeenDone)
        {
            return _otherEntity.Item2;
        }
        else
        {
            throw new InvalidOperationException("The turn must be done in order to call this method.");
        }
    }

    public void PerformAction()
    {
        if (!HasBeenDone)
        {
            HasBeenDone = true;
            DoAction();
        } 
        else
        {
            throw new InvalidOperationException("The action has already been done.");
        }
    }

    protected abstract void DoAction();

    public override int GetHashCode() => HashCode.Combine(_id, _entityOnTurn, _otherEntity);

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != this.GetType())
        {
            return false;
        }
        return ReferenceEquals(this, obj) || ((AbstractTurn)obj)._id.Equals(this._id);
    }

    protected void SetCurrentlyDeployedBanion(IBanion? banion)
    {
        if (HasBeenDone)
        {
            _entityOnTurn = new Tuple<IGameEntity, IBanion?>(_entityOnTurn.Item1, banion);
        }
    }

}
