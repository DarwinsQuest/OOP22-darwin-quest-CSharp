using System.Collections.ObjectModel;
using OOP22_darwin_quest_CSharp.EnricoMarchionni;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.Cipollone.Entity;

public interface IGameEntity : IGameObject
{
    event EventHandler<IBanion> SwapEvent;
    
    List<IBanion> GetInventory();

    bool AddToInventory(IBanion banion);

    bool AddToInventory(Collection<IBanion> banions);

    IBanion? UpdateInventory(IBanion oldBanion, IBanion newBanion);

    IBanion DeployBanion();

    // Move SelectMove(IBanion banion);

    IBanion? SwapBanion();

    // Decision GetDecision();

    bool IsOutOfBanions();
    
}
