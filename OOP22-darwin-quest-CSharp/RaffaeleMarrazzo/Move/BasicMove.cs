using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

namespace OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;
public class BasicMove : IDamageMove
{
    private const uint MIN_DAMAGE_INFLICTED = 1;
    public uint BaseDamage { get; }

    public uint DamageMultiplier { get; } = 2;

    public string Name { get; }

    public IElement Element { get; }

    public BasicMove(uint baseDamage, string name, IElement element)
    {
        if (baseDamage == 0)
        {
            throw new ArgumentException("baseDamage can't be zero.");
        }
        BaseDamage = baseDamage;
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("name can't be null or blank");
        }
        Name = name;
        if (element is null)
        {
            throw new ArgumentNullException(nameof(element));
        }
        Element = element;
    }

    public uint ComputeDamage(Banion playerBanion, Banion opponentBanion)
    {
        uint computedDamage;
        if (Element.IsWeaker(opponentBanion.Element))
        {
            computedDamage = BaseDamage / DamageMultiplier;
        } 
        else if (Element.IsStronger(opponentBanion.Element))
        {
            computedDamage = BaseDamage * DamageMultiplier;
        } 
        else
        {
            computedDamage = BaseDamage;
        }
        if (computedDamage <= 0)
        {
            computedDamage = MIN_DAMAGE_INFLICTED;
        }
        return computedDamage;
    }

    public void Perform(Banion playerBanion, Banion opponentBanion) => 
        opponentBanion.DecreaseHp(ComputeDamage(playerBanion, opponentBanion));

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj is null || obj.GetType() != this.GetType())
        {
            return false;
        }
        BasicMove move = (BasicMove)obj;
        return BaseDamage.Equals(move.BaseDamage) && Name.Equals(move.Name) && Element.Equals(move.Element);
    }

    public override int GetHashCode() => HashCode.Combine(BaseDamage, Name, Element);

    public override string ToString() => 
        "BasicMove[name = " + Name + ", element = " + Element.Name + ", base damage = " + BaseDamage;

}
