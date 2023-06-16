using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP22_darwin_quest_CSharp.EnricoMarchionni.Difficulty
{
    public interface IPositiveIntSupplier
    {
        uint Max { get; }

        uint Next();
    }
}
