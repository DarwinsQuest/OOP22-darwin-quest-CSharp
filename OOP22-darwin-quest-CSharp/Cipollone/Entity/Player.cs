using System.Text.RegularExpressions;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.Cipollone.Entity;

public class Player : AbstractGameEntity, IPlayer
{
    private const string NoInputMsg = "This version will not include player input.";
    
    public Player(string nickname) : base(nickname)
    {
        if (!IsNameValid(nickname))
        {
            throw new ArgumentException("Invalid nickname format.");
        }
    }

    protected override IBanion DecideDeployedBanion()
    {
        throw new NotImplementedException(NoInputMsg);
    }

    public override IMove SelectMove(IBanion banion)
    {
        throw new NotImplementedException(NoInputMsg);
    }

    protected override IBanion? DecideSwappedBanion()
    {
        throw new NotImplementedException(NoInputMsg);
    }

    public override IDecision GetDecision()
    {
        throw new NotImplementedException(NoInputMsg);
    }

    public static bool IsNameValid(string nickname)
    {
        const string pattern = "^[a-zA-Z](?:[a-zA-Z0-9_]*[a-zA-Z0-9])?$";
        return Regex.IsMatch(nickname, pattern);
    }

    public override string ToString()
    {
        return base.ToString() + ": nickname='" + Name + '\''
               + ", inventory=" + GetInventory();
    }
}
