using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ViruseWar
{
    // There is only one field in one window
    public class FieldLogic
    {
        private const int m_dimension = 10;
        private Player[,] m_field = new Player[m_dimension, m_dimension];
        private static FieldLogic? m_instance;
        private FieldLogic() { }
        public static FieldLogic GetObject()
        {
            if (m_instance == null)
            {
                m_instance = new FieldLogic();
                m_instance.m_field[0, m_dimension - 1] = Player.SECOND;
                m_instance.m_field[m_dimension - 1, 0] = Player.FIRST;
            }
            return m_instance;
        }

        public Player this[int r, int c]
        {
            get
            {
                if (r < 0 || r >= m_dimension)
                    throw new ArgumentOutOfRangeException(nameof(r), "Row is out of range.");
                if (c < 0 || c >= m_dimension)
                    throw new ArgumentOutOfRangeException(nameof(c), "Column is out of range.");
                if (m_instance == null)
                    throw new NullReferenceException();
                return m_instance.m_field[r, c];
            }
        }

        public static IMementoField Save()
        {
            if (m_instance == null) throw new NullReferenceException();
            return new ConcreteFieldMemento(m_instance.m_field);
        }
        public static void Restore(IMementoField memento)
        {
            if (memento is not ConcreteFieldMemento)
                throw new Exception("Unknown memento class");
            if (m_instance == null)
                throw new NullReferenceException();
            m_instance.m_field = memento.GetMatrix();
        }
        public static Player CheckCell(int row, int col, bool move_first)
        {
            if (m_instance == null)
                throw new NullReferenceException();
            Player curr_cell = m_instance.m_field[row, col];
            if (move_first && curr_cell == Player.FIRST || !move_first && curr_cell == Player.SECOND)
                return Player.EMPTY;
            if (curr_cell == Player.CAPTURED_FIRST || curr_cell == Player.CAPTURED_SECOND)
                return Player.EMPTY;

            Player CurrPlCol = move_first ? Player.FIRST : Player.SECOND;
            Player CapCol = move_first ? Player.CAPTURED_FIRST : Player.CAPTURED_SECOND;
            bool[,] m_check = new bool[m_dimension, m_dimension];

            bool CheckAlgorithm(int r, int c)
            {
                if (m_instance == null)
                    throw new NullReferenceException();
                m_check[r, c] = true;
                bool freeCell = false;
                int[,] variants = {
                    { r - 1, c - 1 },
                    { r - 1, c },
                    { r - 1, c + 1 },
                    { r, c - 1 },
                    { r, c + 1 },
                    { r + 1, c - 1 },
                    { r + 1, c },
                    { r + 1, c +1 }
                };

                for (int num = 0; num < variants.GetLength(0); ++num)
                {
                    if (variants[num, 0] < 0 || variants[num, 1] < 0 || variants[num, 0] >= m_dimension || variants[num, 1] >= m_dimension)
                        continue;
                    if (m_instance.m_field[variants[num, 0], variants[num, 1]] == CurrPlCol)
                        return true;

                    if (m_instance.m_field[variants[num, 0], variants[num, 1]] == CapCol && !m_check[variants[num, 0], variants[num, 1]])
                    {
                        freeCell = freeCell || CheckAlgorithm(variants[num, 0],
                        variants[num, 1]);
                    }
                }
                return freeCell;
            }

            if (!CheckAlgorithm(row, col)) return Player.EMPTY;
            return m_instance.m_field[row, col] = (curr_cell == Player.EMPTY) ? CurrPlCol : CapCol;
        }
    }
}