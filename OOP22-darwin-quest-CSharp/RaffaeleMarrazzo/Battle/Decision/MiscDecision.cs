using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;

public abstract class MiscDecision : IDecision
{

    private readonly string _name;

    protected MiscDecision(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("name can't be null or blank");
        }
        _name = name;
    }

    public override int GetHashCode() => HashCode.Combine(_name);

    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }
        MiscDecision decision = (MiscDecision)obj;
        return ReferenceEquals(this, obj) || _name.Equals(decision._name);
    }

    public override string ToString() => "Decision: " + _name;

    public abstract ITurn GetAssociatedTurn(ITurn previousTurn);
}
