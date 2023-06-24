namespace OOP22_darwin_quest_CSharp.Cipollone.Evolution;

public class LinearEvolution : IEvolution
{
    private const int MaxHp = 30;
    private const double HpMultiplier = 0.15;
    
    public bool Evolve(EvolvableBanion banion, Predicate<EvolvableBanion> requirement)
    {
        if (!requirement.Invoke(banion))
        {
            return false;
        }
        // Evolution will not prompt if the MaxHp ceiling is already reached.
        if (banion.Hp < MaxHp)
        {
            int increase = IncreaseByPercentage((int)banion.MaxHp, HpMultiplier);
            banion.MaxHp = (uint)Math.Min(increase, MaxHp);
        }
        banion.SetHpToMax();
        banion.IncreaseLevel();
        return true;
    }

    private int IncreaseByPercentage(int value, double percentage)
    {
        double increase = 1 + percentage;
        return (int)Math.Round(value * increase);
    }
}
