using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

public interface ITurn
{
    bool HasBeenDone { get; }

    IGameEntity GetEntityOnTurn();

    IGameEntity GetOtherEntity();

    IBanion? OnTurnCurrentlyDeployedBanion();

    IBanion? OtherEntityCurrentlyDeployedBanion();

    void PerformAction();

}
