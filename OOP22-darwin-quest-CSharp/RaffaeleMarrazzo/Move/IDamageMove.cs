using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;
public interface IDamageMove : IMove
{
    uint BaseDamage { get; }

    uint DamageMultiplier { get; }

    uint ComputeDamage(Banion playerBanion, Banion opponentBanion);

}