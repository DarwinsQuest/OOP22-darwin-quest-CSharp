namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Element
{
    public class ImmutableElement : IElement
    {
        private readonly ISet<string> _weaker;
        private readonly ISet<string> _stronger;

        protected ImmutableElement(string name) : this(name, new HashSet<string>(), new HashSet<string>())
        {

        }

        public ImmutableElement(string name, ISet<string> weaker, ISet<string> stronger)
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
    }
}
