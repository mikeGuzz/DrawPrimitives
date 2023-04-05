using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Helpers
{
    public class UndoRedoHelper<T> where T : ICloneable
    {
        private Stack<T> undo = new Stack<T>();
        private Stack<T> redo = new Stack<T>();

        public int Limit { get; set; } = 1000;

        public UndoRedoHelper() { }

        public void AddItem(T value)
        {
            if (redo.Count != 0)
                redo.Clear();
            if(undo.Count >= Limit)
                undo.Clear();
            undo.Push(value);
        }

        public bool CanUndo()
        {
            return undo.Any();
        }

        public bool CanRedo()
        {
            return redo.Any();
        }

        public T Undo(T value)
        {
            if (redo.Count >= Limit)
                redo.Clear();
            redo.Push(value);
            return undo.Pop();
        }

        public T Redo(T value)
        {
            if (undo.Count >= Limit)
                undo.Clear();
            undo.Push(value);
            return redo.Pop();
        }

        public void Clear()
        {
            undo.Clear();
            redo.Clear();
        }
    }
}
