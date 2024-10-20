using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace VWClassLibrary
{
    // There is only one field in one window
    public class FieldLogic
    {
        private const int m_dimension = 10;
        private Player[,] m_field = new Player[m_dimension, m_dimension];
        private static FieldLogic m_instance = null;
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
                if (r < 0 || c < 0 || r >= m_dimension || c >= m_dimension)
                    throw new ArgumentOutOfRangeException();
                return m_instance.m_field[r, c];
            }
        }

        public IMementoField Save() { return new ConcreteFieldMemento(m_instance.m_field); }
        public void Restore(IMementoField memento)
        {
            if (!(memento is ConcreteFieldMemento))
                throw new Exception("Unknown memento class");
            m_instance.m_field = memento.GetMatrix();
        }
        public Player CheckCell(int row, int col, bool move_first)
        {
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
            if (CheckAlgorithm(row, col))
            {
                if (curr_cell == Player.EMPTY)
                {
                    m_instance.m_field[row, col] = CurrPlCol;
                    return m_instance.m_field[row, col];
                }
                m_instance.m_field[row, col] = CapCol;
                return m_instance.m_field[row, col];
            }
            return Player.EMPTY;
        }
    }
}