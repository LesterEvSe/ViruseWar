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
        public static FieldLogic Instance = new();
        private FieldLogic()
        {
            m_field[0, m_dimension - 1] = Player.SECOND;
            m_field[m_dimension - 1, 0] = Player.FIRST;
        }

        public Player this[int r, int c]
        {
            get
            {
                if (r < 0 || r >= m_dimension)
                    throw new ArgumentOutOfRangeException(nameof(r), "Row is out of range.");
                if (c < 0 || c >= m_dimension)
                    throw new ArgumentOutOfRangeException(nameof(c), "Column is out of range.");
                return Instance.m_field[r, c];
            }
        }

        public static IMementoField Save()
        {
            if (Instance == null) throw new NullReferenceException();
            return new ConcreteFieldMemento(Instance.m_field);
        }
        public static void Restore(IMementoField memento)
        {
            if (memento is not ConcreteFieldMemento)
                throw new Exception("Unknown memento class");
            Instance.m_field = memento.GetMatrix();
        }

        public static void Restore(string field)
        {
            string[] values = field.Split(',');
            int index = 0;

            for (int i = 0; i < m_dimension; ++i)
            {
                for (int j = 0; j < m_dimension; ++j)
                {
                    Instance.m_field[i, j] = (Player)Enum.Parse(typeof(Player), values[index++]);
                }
            }
        }

        public static string Serialize()
        {
            List<string> serializedData = [];
            for (int i = 0; i < m_dimension; ++i)
            {
                for (int j = 0; j < m_dimension; ++j)
                {
                    serializedData.Add(Instance.m_field[i, j].ToString());
                }
            }
            return string.Join(",", serializedData);
        }

        public static Player CheckCell(int row, int col, bool move_first)
        {
            if (Instance == null)
                throw new NullReferenceException();
            Player curr_cell = Instance.m_field[row, col];
            if (move_first && curr_cell == Player.FIRST || !move_first && curr_cell == Player.SECOND)
                return Player.EMPTY;
            if (curr_cell == Player.CAPTURED_FIRST || curr_cell == Player.CAPTURED_SECOND)
                return Player.EMPTY;

            Player CurrPlCol = move_first ? Player.FIRST : Player.SECOND;
            Player CapCol = move_first ? Player.CAPTURED_FIRST : Player.CAPTURED_SECOND;
            bool[,] m_check = new bool[m_dimension, m_dimension];

            bool CheckAlgorithm(int r, int c)
            {
                if (Instance == null)
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
                    { r + 1, c + 1 }
                };

                for (int num = 0; num < variants.GetLength(0); ++num)
                {
                    if (variants[num, 0] < 0 || variants[num, 1] < 0 || variants[num, 0] >= m_dimension || variants[num, 1] >= m_dimension)
                        continue;
                    if (Instance.m_field[variants[num, 0], variants[num, 1]] == CurrPlCol)
                        return true;

                    if (Instance.m_field[variants[num, 0], variants[num, 1]] == CapCol && !m_check[variants[num, 0], variants[num, 1]])
                    {
                        freeCell = freeCell || CheckAlgorithm(variants[num, 0],
                        variants[num, 1]);
                    }
                }
                return freeCell;
            }

            if (!CheckAlgorithm(row, col)) return Player.EMPTY;
            return Instance.m_field[row, col] = (curr_cell == Player.EMPTY) ? CurrPlCol : CapCol;
        }
    }
}