using System.Collections.ObjectModel;
using OOP22_darwin_quest_CSharp.EnricoMarchionni.Banion;

namespace OOP22_darwin_quest_CSharp.Cipollone.Entity;

public abstract class AbstractGameEntity : IGameEntity
{
    private static readonly List<IBanion> Inventory = new();

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
    
    public List<IBanion> GetInventory()
    {
        return new List<IBanion>(Inventory);
    }

    public bool AddToInventory(IBanion banion)
    {
        if (Inventory.Contains(banion))
        {
            return false;
        }
        var previousSize = Inventory.Count;
        Inventory.Add(banion);
        return previousSize == Inventory.Count - 1;
    }

    public bool AddToInventory(Collection<IBanion> banions)
    {
        var allowedBanions = banions.Where(b => !Inventory.Contains(b)).ToList();
        var previousSize = Inventory.Count;
        Inventory.AddRange(banions);
        return previousSize == Inventory.Count - allowedBanions.Count;
    }

    public IBanion? UpdateInventory(IBanion oldBanion, IBanion newBanion)
    {
        if (!Inventory.Contains(oldBanion) || Inventory.Contains(newBanion))
        {
            return null;
        }
        var index = Inventory.IndexOf(oldBanion);
        Inventory.Insert(index, newBanion);
        return oldBanion;
    }

    public IBanion DeployBanion()
    {
        var banion = DecideDeployedBanion();
        SwapEvent?.Invoke(this, banion);
        return banion;
    }

    protected abstract IBanion DecideDeployedBanion();

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

    public bool IsOutOfBanions()
    {
        return Inventory.All(banion => !banion.IsAlive);
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
