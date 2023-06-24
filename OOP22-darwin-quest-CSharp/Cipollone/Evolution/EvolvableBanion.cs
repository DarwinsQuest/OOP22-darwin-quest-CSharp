using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.Cipollone.Evolution;

public class EvolvableBanion : Banion, IEvolvable
{
    private readonly IEvolution _evolution = new LinearEvolution();
    public record BanionStats(uint Level, uint Hp, uint MaxHp);
    public static uint MaxXp => 20;
    public uint Xp { get; private set; }

    public EvolvableBanion(IElement element, string name, uint hp, ISet<IMove> moves) : base(element, name, hp, moves)
    {
    }

    public void IncreaseXp(uint amount)
    {
        if (amount == 0)
        {
            throw new ArgumentException("Amount cannot be 0.");
        }
        uint increase = Xp + amount;
        if (increase <= MaxXp)
        {
            Xp = increase;
        }
        else
        {
            uint increaseRest = increase - MaxXp;
            Xp = increase - increaseRest;
            Evolve(_ => true);
            Xp = increaseRest;
        }
    }

    public bool Evolve(Predicate<EvolvableBanion> requirement) => _evolution.Evolve(this, requirement);

    public bool EvolveToLevel(uint level, Predicate<EvolvableBanion> requirement)
    {
        if (Level >= level)
        {
            throw new ArgumentException("Current level " + "(" + Level + ") is past or equal to: " + level);
        }
        var rollbackRecord = new BanionStats(Level, Hp, MaxHp);
        while (Level != level)
        {
            bool lastStatus = Evolve(requirement);
            if (!lastStatus)
            {
                RollbackStats(rollbackRecord);
                return false;
            }
        }
        return true;
    }

    private void RollbackStats(BanionStats record)
    {
        Level = record.Level;
        Hp = record.Hp;
        MaxHp = record.MaxHp;
    }
}
