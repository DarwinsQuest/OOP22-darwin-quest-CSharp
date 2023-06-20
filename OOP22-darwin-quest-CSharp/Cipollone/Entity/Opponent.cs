using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.Cipollone.Entity;

public class Opponent : AbstractGameEntity, IOpponent
{
    public Opponent(string nickname) : base(nickname)
    {
        // todo: add AI here.
    }

    protected override IBanion DecideDeployedBanion()
    {
        throw new NotImplementedException();
    }

    protected override IBanion? DecideSwappedBanion()
    {
        throw new NotImplementedException();
    }

    // todo: add Equals and HashCode after AI is added.

    public override string ToString()
    {
        // todo: add AI ToString.
        return base.ToString() + ": nickname='" + Name + '\''
            + ", inventory=" + GetInventory()
            + ", ai=";
    }
    
}
