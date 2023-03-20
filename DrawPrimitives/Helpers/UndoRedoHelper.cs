using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives.Helpers
{
    public class UndoRedoHelper<T> where T : ICloneable
    {
        private Stack<T> undoStack, redoStack;

        public UndoRedoHelper()
        {
            redoStack = new Stack<T>();
            undoStack = new Stack<T>();
        }

        public void AddToUndoStack(T value)
        {
            undoStack.Push((T)value.Clone());
            if (redoStack.Count > 0)
                redoStack.Clear();
        }

        public bool CanUndo()
        {
            return undoStack.Count > 0;
        }

        public bool CanRedo()
        {
            return redoStack.Count > 0;
        }

        public T GetFromUndoStack()
        {
            redoStack.Push(undoStack.Peek());
            return undoStack.Pop();
        }

        public T GetFromRedoStack()
        {
            undoStack.Push(redoStack.Peek());
            return redoStack.Pop();
        }

        public void Clear()
        {
            redoStack.Clear();
            undoStack.Clear();
        }
    }
}
