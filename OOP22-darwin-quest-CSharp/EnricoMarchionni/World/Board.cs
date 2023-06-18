using OOP22_darwin_quest_CSharp.EnricoMarchionni.Difficulty;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.World;

public class Board : IBoard
{
    private readonly IPositiveIntSupplier _supplier;
    private uint _pos;

    public Board(uint levels, IPositiveIntSupplier supplier)
    {
        Levels = levels;
        _supplier = supplier;
    }

    public uint FirstPos { get; } = 1;

    public uint LastPos => FirstPos + Levels;

    public uint Levels { get; }

    public uint Pos => FirstPos + _pos;

    public bool CanMove => _pos < Levels;

    public uint MaxStep => _supplier.Max;

    public uint? Move()
    {
        var pos = _pos;
        _pos = Math.Min(_pos + _supplier.Next(), Levels);
        return _pos != pos ? _pos - pos : null;
    }
}
