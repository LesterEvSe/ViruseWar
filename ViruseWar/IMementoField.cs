using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ViruseWar
{
    public interface IMementoField
    {
        Player[,] GetMatrix();
        string SerializeMatrix();
    }
}