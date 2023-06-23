using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle;
public class BattleTile : IBattleTile
{

    private readonly IImmutableList<IGameEntity> _players;
    private readonly IList<ITurn> _battleTurns = new List<ITurn>();
    private IGameEntity? _winner;
    private bool _hasBeenDone;

    public IGameEntity Player => _players[0];

    public IGameEntity Opponent => _players[1];

    public IImmutableList<ITurn> BattleTurns
    {
        get
        {
            return _hasBeenDone ? _battleTurns.ToImmutableList() : ImmutableList.Create<ITurn>();
        }
    }

    public BattleTile(IGameEntity player, IGameEntity opponent)
    {
        if (player is not null && opponent is not null)
        {
            _players = ImmutableList.Create<IGameEntity>(player, opponent);
        }
        else
        {
            throw new ArgumentException("player or opponent cannot be null");
        }
    }

    public bool IsWinner(IGameEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        else
        {
            return _hasBeenDone && _winner is not null && this._winner.Equals(entity);
        }
    }

    public bool NewBattle()
    {
        _battleTurns.Clear();
        if (IsWinner(Player))
        {
            return false;
        }
        var firstTurn = new DeployTurn(Player, Opponent);
        firstTurn.PerformAction();
        _battleTurns.Add(firstTurn);
        var secondTurn = new DeployTurn(firstTurn);
        secondTurn.PerformAction();
        _battleTurns.Add(secondTurn);
        return true;
    }

    public bool NextTurn()
    {
        DoCurrentTurn();
        if (!NobodyIsOutOfBanions())
        {
            _hasBeenDone = true;
            SetWinner();
            return false;
        }
        return true;
    }

    public override int GetHashCode() => _players.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }
        return ReferenceEquals(this, obj) || ((BattleTile)obj).Opponent.Equals(this.Opponent);
    }

    public override string ToString() => "BattleTile [players= " + _players + "]";

    private void DoCurrentTurn()
    {
        ITurn currentTurn;
        ITurn previousTurn = _battleTurns[_battleTurns.Count - 1];
        if (previousTurn.OtherEntityCurrentlyDeployedBanion!.IsAlive)
        {
            currentTurn = GetEntityOnTurn(previousTurn).GetDecision().GetAssociatedTurn(previousTurn);
        }
        else
        {
            currentTurn = new SwapTurn(previousTurn);
        }
        _battleTurns.Add(currentTurn);
        currentTurn.PerformAction();
    }

    private bool NobodyIsOutOfBanions() => !_players.Where(player => player.IsOutOfBanions()).Any();

    private void SetWinner()
    {
        if (_hasBeenDone)
        {
            if (_players.Where(player => player.IsOutOfBanions()).Count() == 1 
                && _players.Where(player => player.IsOutOfBanions()).First().Equals(Player))
            {
                _winner = Opponent;
            }
            else
            {
                _winner = Player;
            }
        }
    }

    private static IGameEntity GetEntityOnTurn(ITurn previousTurn) => previousTurn.OtherEntity;

}
