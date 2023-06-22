using OOP22_darwin_quest_CSharp.EnricoMarchionni;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.Cipollone.Entity;

public interface IGameEntity : IGameObject
{
    event EventHandler<IBanion> SwapEvent;
    
    IList<IBanion> GetInventory();

    bool AddToInventory(IBanion banion);

    bool AddToInventory(IEnumerable<IBanion> banions);

    IBanion? UpdateInventory(IBanion oldBanion, IBanion newBanion);

    IBanion DeployBanion();

    IMove SelectMove(IBanion banion);

    IBanion? SwapBanion();

    IDecision GetDecision();

    bool IsOutOfBanions();
    
}
