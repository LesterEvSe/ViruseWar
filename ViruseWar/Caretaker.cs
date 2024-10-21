using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViruseWar
{
    public class Caretaker()
    {
        private IMementoField? FirstPlMemento;
        private IMementoField? SecondPlMemento;

        private int NumOfBackups_FirstPl = 1;
        private int NumOfBackups_SecondPl = 1;
        public bool Backup(bool move_first)
        {
            //if (orig == null) return false;
            if (move_first && NumOfBackups_FirstPl > 0)
            {
                --NumOfBackups_FirstPl;
                FirstPlMemento = FieldLogic.Save();
                return true;
            }
            else if (!move_first && NumOfBackups_SecondPl > 0)
            {
                --NumOfBackups_SecondPl;
                SecondPlMemento = FieldLogic.Save();
                return true;
            }
            return false;
        }
        public bool RestoreField(bool move_first)
        {
            if (move_first && FirstPlMemento != null)
            {
                FieldLogic.Restore(FirstPlMemento);
                FirstPlMemento = null;
                return true;
            }
            else if (!move_first && SecondPlMemento != null)
            {
                FieldLogic.Restore(SecondPlMemento);
                SecondPlMemento = null;
                return true;
            }
            return false;
        }
    }
}