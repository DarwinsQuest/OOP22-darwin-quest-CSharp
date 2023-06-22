using System.Collections.Immutable;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Battle.Decision;
using OOP22_darwin_quest_CSharp.RaffaeleMarrazzo.Move;

namespace OOP22_darwin_quest_CSharp.Cipollone.Entity;

public abstract class AbstractGameEntity : IGameEntity
{
    private readonly List<IBanion> _inventory = new();

    protected AbstractGameEntity(string nickname)
    {
        if (string.IsNullOrWhiteSpace(nickname))
        {
            throw new ArgumentException("Entity nickname cannot be null or blank.");
        }
        Name = nickname;
    }

    public event EventHandler<IBanion>? SwapEvent;
    
    public string Name { get; }
    
    public IList<IBanion> GetInventory()
    {
        return _inventory.ToImmutableList();
    }

    public bool AddToInventory(IBanion banion)
    {
        if (_inventory.Contains(banion))
        {
            return false;
        }
        var previousSize = _inventory.Count;
        _inventory.Add(banion);
        return previousSize == _inventory.Count - 1;
    }

    public bool AddToInventory(IEnumerable<IBanion> banions)
    {
        var allowedBanions = banions.Where(b => !_inventory.Contains(b)).ToList();
        var previousSize = _inventory.Count;
        _inventory.AddRange(allowedBanions);
        return previousSize == _inventory.Count - allowedBanions.Count;
    }

    public IBanion? UpdateInventory(IBanion oldBanion, IBanion newBanion)
    {
        if (!_inventory.Contains(oldBanion) || _inventory.Contains(newBanion))
        {
            return null;
        }
        var index = _inventory.IndexOf(oldBanion);
        _inventory[index] = newBanion;
        return oldBanion;
    }

    public IBanion DeployBanion()
    {
        var banion = DecideDeployedBanion();
        SwapEvent?.Invoke(this, banion);
        return banion;
    }

    protected abstract IBanion DecideDeployedBanion();

    public abstract IMove SelectMove(IBanion banion);

    public IBanion? SwapBanion()
    {
        var banion = DecideSwappedBanion();
        if (banion is not null)
        {
            SwapEvent?.Invoke(this, banion);
        }
        return banion;
    }

    protected abstract IBanion? DecideSwappedBanion();

    public abstract IDecision GetDecision();

    public bool IsOutOfBanions()
    {
        return _inventory.All(banion => !banion.IsAlive);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Name == ((AbstractGameEntity) obj).Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
    
}
