
using System.Collections.Immutable;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Element
{
    public class Air : ImmutableElement
    {
        public Air() : base("Air", ImmutableHashSet.Create("Water", "Rock"), ImmutableHashSet.Create("Grass", "Air"))
        {

        }
    }
}
