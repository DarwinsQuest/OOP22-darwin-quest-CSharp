using System.Collections.Immutable;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Element
{
    public class Electro : ImmutableElement
    {
        public Electro() : base("Electro", ImmutableHashSet.Create("Electro"), ImmutableHashSet.Create("Grass"))
        {

        }
    }
}
