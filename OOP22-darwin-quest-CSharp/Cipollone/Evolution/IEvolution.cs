using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.Cipollone.Evolution;

public interface IEvolution
{
    bool Evolve(EvolvableBanion banion, Predicate<EvolvableBanion> requirement);
}
