using System;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

public class Banion : IBanion
{
    private readonly Guid id;
    //private readonly ISet<IMove> _moves;
    public event EventHandler<IBanion>? BanionChanged;

    public Banion(IElement element, string name, uint hp/*, ISet<IMove> moves*/)
    {
        id = Guid.NewGuid();
        Element = element ?? throw new ArgumentNullException(nameof(element));
        //_moves = new HashSet<>(); => WAITING FOR Raffaele to push IMove
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

    public string Name { get; }

    public IElement Element { get; }

    public bool IsAlive => Hp > IBanion.MIN_HP;

    //public ISet<IMove> Moves => throw new NotImplementedException();

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
        BanionChanged?.Invoke(this, this);
    }

    public void DecreaseHp(uint amount)
    {
        var newHp = Hp - amount;
        if (newHp >= Hp)
        {
            throw new ArgumentException("Amount to decrease Hp cannot be negative or zero", nameof(amount));
        }
        Hp = Math.Max(newHp, IBanion.MIN_HP);
        BanionChanged?.Invoke(this, this);
    }

    public void SetHpToMax()
    {
        if (Hp != MaxHp)
        {
            Hp = MaxHp;
            BanionChanged?.Invoke(this, this);
        }
    }

    //public bool ReplaceMove(IMove oldOne, IMove newOne)
    //{
    //    throw new NotImplementedException();
    //}

    public static bool operator ==(Banion? left, Banion? right) => left is Banion banion && banion.Equals(right);

    public static bool operator !=(Banion? left, Banion? right) => !(left == right);

    public override bool Equals(object? obj)
    {
        return obj is Banion banion && id.Equals(banion.id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, Name, Element, IsAlive, Hp, MaxHp, Level);
    }
}
