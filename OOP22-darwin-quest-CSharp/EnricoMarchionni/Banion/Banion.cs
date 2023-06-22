using System;
using System.Collections.Immutable;
using System.Xml.Linq;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

public class Banion : IBanion
{
    private const uint NUM_MOVES = 4;

    private readonly Guid id;
    private readonly ISet<IMove> _moves;
    public event EventHandler<IBanion>? EventBanionChanged;

    private Banion(Banion banion)
    {
        id = Guid.NewGuid();
        Element = banion.Element;
        _moves = new HashSet<IMove>(banion._moves);
        Name = banion.Name;
        Hp = banion.Hp;
        MaxHp = banion.MaxHp;
    }

    public Banion(IElement element, string name, uint hp, ISet<IMove> moves)
    {
        id = Guid.NewGuid();
        Element = element ?? throw new ArgumentNullException(nameof(element));
        if (moves.Count != NUM_MOVES)
        {
            throw new ArgumentException($"The {nameof(moves)} have to be exactly {NUM_MOVES}");
        }
        if (moves.Any(m => !IsMoveAcceptable(m)))
        {
            throw new ArgumentException($"The {nameof(moves)} have to be all acceptable");
        }
        _moves = new HashSet<IMove>(moves);
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"{nameof(name)} can't be null or white spaces only");
        }
        Name = name;
        if (hp <= IBanion.MIN_HP)
        {
            throw new ArgumentException($"{nameof(hp)} must be greater that {IBanion.MIN_HP}");
        }
        Hp = hp;
        MaxHp = Hp;
    }

    private bool IsMoveAcceptable(IMove move)
    {
        return move.Element.Equals(Element) || move.Element.GetType().Equals(typeof(Neutral));
    }

    public string Name { get; }

    public IElement Element { get; }

    public bool IsAlive => Hp > IBanion.MIN_HP;

    public IReadOnlySet<IMove> Moves => ImmutableHashSet.CreateRange(_moves);

    public uint Level { get; private set; } = 1;

    public uint Hp { get; private set; }

    public uint MaxHp { get; private set; }

    public void IncreaseHp(uint amount)
    {
        var newHp = Hp + amount;
        if (newHp <= Hp)
        {
            throw new ArgumentException("Amount to increase Hp cannot be negative or zero", nameof(amount));
        }
        Hp = Math.Min(newHp, MaxHp);
        EventBanionChanged?.Invoke(this, this);
    }

    public void DecreaseHp(uint amount)
    {
        var newHp = Hp - amount;
        if (newHp >= Hp)
        {
            throw new ArgumentException("Amount to decrease Hp cannot be negative or zero", nameof(amount));
        }
        Hp = Math.Max(newHp, IBanion.MIN_HP);
        EventBanionChanged?.Invoke(this, this);
    }

    public void SetHpToMax()
    {
        if (Hp != MaxHp)
        {
            Hp = MaxHp;
            EventBanionChanged?.Invoke(this, this);
        }
    }

    public bool ReplaceMove(IMove oldOne, IMove newOne)
    {
        var result = IsMoveAcceptable(newOne) && _moves.Remove(oldOne) && _moves.Add(newOne);
        if (result)
        {
            EventBanionChanged?.Invoke(this, this);
        }
        return result;
    }

    public IBanion Copy()
    {
        return new Banion(this);
    }

    public static bool operator ==(Banion? left, Banion? right) => left is Banion banion && banion.Equals(right);

    public static bool operator !=(Banion? left, Banion? right) => !(left == right);

    public bool Equals(IBanion? other)
    {
        return other is Banion banion && id.Equals(banion.id);
    }

    public override bool Equals(object? obj)
    {
        return obj is Banion banion && id.Equals(banion.id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, Name, Element, IsAlive, Hp, MaxHp, Level);
    }
}
