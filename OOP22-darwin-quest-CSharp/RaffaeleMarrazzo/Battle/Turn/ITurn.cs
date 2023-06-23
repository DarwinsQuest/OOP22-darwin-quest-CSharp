using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

public interface ITurn
{
    bool HasBeenDone { get; }

    IGameEntity EntityOnTurn { get; }

    IGameEntity OtherEntity { get; }

    IBanion? OnTurnCurrentlyDeployedBanion { get; }
    
    IBanion? OtherEntityCurrentlyDeployedBanion { get; }

    void PerformAction();

}
