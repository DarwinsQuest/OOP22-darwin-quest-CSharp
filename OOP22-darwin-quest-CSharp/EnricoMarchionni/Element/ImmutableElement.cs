namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

public class ImmutableElement : IElement
{
    private readonly IReadOnlySet<string> _weaker;
    private readonly IReadOnlySet<string> _stronger;

    protected ImmutableElement(string name) : this(name, new HashSet<string>(), new HashSet<string>())
    {

    }

    public ImmutableElement(string name, IReadOnlySet<string> weaker, IReadOnlySet<string> stronger)
    {
        Name = name;
        _weaker = weaker;
        _stronger = stronger;
    }

    public string Name { get; protected set; }

    public bool IsStronger(IElement other)
    {
        return _weaker.Contains(other.Name);
    }

    public bool IsWeaker(IElement other)
    {
        return _stronger.Contains(other.Name);
    }

    public override bool Equals(object? obj)
    {
        return obj is ImmutableElement element && Name == element.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, _weaker, _stronger);
    }

    public override string? ToString()
    {
        return Name;
    }
}
