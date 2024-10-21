using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ViruseWar
{
    public class ConcreteFieldMemento : IMementoField
    {
        private readonly Player[,]? m_field;
        public ConcreteFieldMemento(Player[,] matrix)
        {
            m_field = new Player[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                    m_field[i, j] = matrix[i, j];

        }
        public Player[,] GetMatrix()
        {
            if (m_field != null) return m_field;
            throw new NullReferenceException();
        }
    }
}