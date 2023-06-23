namespace OOP22_darwin_quest_CSharp.Cipollone.Die;

public class Die : IIntSupplier
{
    private const int DefaultFaces = 6;
    private const int MinFaces = 4;
    private int _faces;
    private readonly Random _random = new();
    public int Faces
    {
        get => _faces;
        set
        {
            if (!IsDieLegal(value))
            {
                throw new ArgumentException("Not a platonic solid.");
            }
            _faces = value;
        }
    }

    public int Value => _random.Next(Faces) + 1;

    public Die()
    {
        _faces = DefaultFaces;
    }

    private bool IsDieLegal(int faces)
    {
        return faces >= MinFaces && faces % 2 == 0;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        Die other = (Die)obj;
        return obj.GetType() == GetType() && _faces == other._faces;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_faces, _random);
    }
    
}
