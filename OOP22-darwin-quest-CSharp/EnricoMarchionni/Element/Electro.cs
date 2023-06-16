using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Element
{
    public class Electro : ImmutableElement
    {
        public Electro() : base("Electro", ImmutableHashSet.Create("Electro"), ImmutableHashSet.Create("Grass"))
        {

        }
    }
}
