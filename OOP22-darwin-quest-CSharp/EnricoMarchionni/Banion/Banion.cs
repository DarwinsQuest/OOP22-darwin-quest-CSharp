using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

public class Banion : IBanion
{
    private readonly Guid id;
    private readonly ISet<IMove> _moves;
    private uint _maxHp;

    public event EventHandler<IBanion>? EventBanionChanged;

    private Banion(Banion banion)
    {
        id = Guid.NewGuid();
        Element = banion.Element;
        _moves = new HashSet<IMove>(banion._moves);
        Name = banion.Name;
        Hp = banion.Hp;
        _maxHp = banion.MaxHp;
    }

    public Banion(IElement element, string name, uint hp, ISet<IMove> moves)
    {
        id = Guid.NewGuid();
        Element = element ?? throw new ArgumentNullException(nameof(element));
        if (moves.Count != IBanion.NUM_MOVES)
        {
            throw new ArgumentException($"The {nameof(moves)} have to be exactly {IBanion.NUM_MOVES}");
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
        _maxHp = Hp;
    }

    private bool IsMoveAcceptable(IMove move)
    {
        return move.Element.Equals(Element) || move.Element.GetType().Equals(typeof(Neutral));
    }

    public string Name { get; }

    public IElement Element { get; }

    public bool IsAlive => Hp > IBanion.MIN_HP;

    public IReadOnlySet<IMove> Moves => ImmutableHashSet.CreateRange(_moves);

    public uint Level { get; protected set; } = 1;
    
    public void IncreaseLevel() => Level += 1;

    public uint Hp { get; protected set; }

    public uint MaxHp
    {
        get => _maxHp;
        set
        {
            if (value != MaxHp)
            {
                if (value <= IBanion.MIN_HP)
                {
                    throw new ArgumentException($"{nameof(Banion)} {nameof(MaxHp)} can't be set to {IBanion.MIN_HP} or lower value", nameof(MaxHp));
                }
                _maxHp = value;
                if (Hp > MaxHp)
                {
                    Hp = MaxHp;
                }
                EventBanionChanged?.Invoke(this, this);
            }
        }
    }

    public void IncreaseHp(uint amount)
    {
        if (amount == 0)
        {
            throw new ArgumentException("Amount to increase Hp cannot be zero", nameof(amount));
        }
        uint newHp;
        try
        {
            newHp = checked(Hp + amount);
        }
        catch (OverflowException)
        {
            newHp = MaxHp;
        }
        Hp = Math.Min(newHp, MaxHp);
        EventBanionChanged?.Invoke(this, this);
    }

    public void DecreaseHp(uint amount)
    {
        if (amount == 0)
        {
            throw new ArgumentException("Amount to decrease Hp cannot be zero", nameof(amount));
        }
        var newHp = amount >= Hp ? IBanion.MIN_HP : Hp - amount;
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

    public override bool Equals(object? obj)
    {
        return obj is Banion banion && id.Equals(banion.id);
    }

    public override int GetHashCode() => id.GetHashCode();
}
