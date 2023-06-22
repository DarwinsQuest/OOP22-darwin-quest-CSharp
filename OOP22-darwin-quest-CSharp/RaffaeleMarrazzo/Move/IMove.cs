using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;
public interface IMove : EnricoMarchionni.IGameObject, EnricoMarchionni.Element.IElemental
{
    void Perform(IBanion playerBanion, IBanion opponentBanion);

}
