using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.AI;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.Cipollone.Entity;

public class Opponent : AbstractGameEntity, IOpponent
{
    public IAI Ai { private get; set; }

    public Opponent(string nickname, IAI difficulty) : base(nickname)
    {
        Ai = difficulty;
    }

    protected override IBanion DecideDeployedBanion()
    {
        return Ai.DecideBanionDeployment(GetInventory());
    }

    public override IMove SelectMove(IBanion banion)
    {
        return Ai.DecideMoveSelection(banion.Moves.ToImmutableList());
    }

    protected override IBanion? DecideSwappedBanion()
    {
        return Ai.DecideBanionSwap(GetInventory());
    }

    public override IDecision GetDecision()
    {
        return Ai.GetDecision();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        var other = (Opponent) obj;
        return obj.GetType() == GetType() && base.Equals(other) && Ai.Equals(other.Ai);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Ai);
    }

    public override string ToString()
    {
        return base.ToString() + ": nickname='" + Name + '\''
            + ", inventory=" + GetInventory()
            + ", ai=" + Ai;
    }
    
}
