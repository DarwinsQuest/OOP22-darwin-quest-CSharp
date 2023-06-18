namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.World;

public interface IBoard
{
    uint FirstPos { get; }

    uint LastPos { get; }

    uint Levels { get; }

    uint Pos { get; }

    bool CanMove { get; }

    uint MaxStep { get; }

    uint? Move();
}
