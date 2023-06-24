namespace OOP22_darwin_quest_CSharp.Cipollone.Evolution;

public interface IEvolvable
{
    bool Evolve(Predicate<EvolvableBanion> requirement);

    bool EvolveToLevel(uint level, Predicate<EvolvableBanion> requirement);
}
