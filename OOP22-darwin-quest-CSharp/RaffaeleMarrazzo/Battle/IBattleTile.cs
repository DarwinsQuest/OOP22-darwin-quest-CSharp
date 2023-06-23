using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.Cipollone.Entity;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Turn;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle;
public interface IBattleTile
{
    IGameEntity Player { get; }
    
    IGameEntity Opponent { get; }

    IImmutableList<ITurn> BattleTurns { get; }

    bool NewBattle();

    bool NextTurn();

    bool IsWinner(IGameEntity entity);


}
