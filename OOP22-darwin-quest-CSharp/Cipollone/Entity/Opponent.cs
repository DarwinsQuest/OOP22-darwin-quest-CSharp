using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.AI;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.Cipollone.Entity;

public class Opponent : AbstractGameEntity, IOpponent
{
    private readonly IAI _ai;
    
    public Opponent(string nickname, IAI difficulty) : base(nickname)
    {
        _ai = difficulty;
    }

    protected override IBanion DecideDeployedBanion()
    {
        return _ai.DecideBanionDeployment(GetInventory());
    }

    public override IMove SelectMove(IBanion banion)
    {
        return _ai.DecideMoveSelection(banion.Moves.ToImmutableList());
    }

    protected override IBanion? DecideSwappedBanion()
    {
        return _ai.DecideBanionSwap(GetInventory());
    }

    public override IDecision GetDecision()
    {
        return _ai.GetDecision();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        var other = (Opponent) obj;
        return obj.GetType() == GetType() && base.Equals(other) && _ai.Equals(other._ai);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _ai);
    }

    public override string ToString()
    {
        return base.ToString() + ": nickname='" + Name + '\''
            + ", inventory=" + GetInventory()
            + ", ai=" + _ai;
    }
    
}
