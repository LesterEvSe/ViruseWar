using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace VWClassLibrary
{
    public class Caretaker
    {
        private IMementoField FirstPlMemento = null;
        private IMementoField SecondPlMemento = null;
        private FieldLogic orig = null;
        public Caretaker(FieldLogic obj) { orig = obj; }

        private int NumOfBackups_FirstPl = 1;
        private int NumOfBackups_SecondPl = 1;
        public bool Backup(bool move_first)
        {
            if (move_first && NumOfBackups_FirstPl > 0)
            {
                --NumOfBackups_FirstPl;
                FirstPlMemento = orig.Save();
                return true;
            }
            else if (!move_first && NumOfBackups_SecondPl > 0)
            {
                --NumOfBackups_SecondPl;
                SecondPlMemento = orig.Save();
                return true;
            }
            return false;
        }
        public bool RestoreField(bool move_first)
        {
            if (move_first && FirstPlMemento != null)
            {
                orig.Restore(FirstPlMemento);
                FirstPlMemento = null;
                return true;
            }
            else if (!move_first && SecondPlMemento != null)
            {
                orig.Restore(SecondPlMemento);
                SecondPlMemento = null;
                return true;
            }
            return false;
        }
    }
}