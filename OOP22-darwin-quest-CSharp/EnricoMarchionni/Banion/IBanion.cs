using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Util;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

public interface IBanion : ICloneable<IBanion>, IGameObject, IElemental
{
    public const uint NUM_MOVES = 4;
    public const uint MIN_HP = 0;

    public event EventHandler<IBanion> EventBanionChanged;

    bool IsAlive { get; }

    IReadOnlySet<IMove> Moves { get; }

    bool ReplaceMove(IMove oldOne, IMove newOne);

    uint Hp { get; }

    uint MaxHp { get; set; }

    void IncreaseHp(uint amount);

    void DecreaseHp(uint amount);

    void SetHpToMax();
}
