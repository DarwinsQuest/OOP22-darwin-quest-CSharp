using System.Collections.Immutable;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Element;

/*
 * This class doesn't exist in the java project because
 * it is serialized in its relative .json.
 * I added this class here mainly for test purposes.
 */
public class Electro : ImmutableElement
{
    public Electro() : base("Electro", ImmutableHashSet.Create("Electro"), ImmutableHashSet.Create("Grass"))
    {

    }
}
