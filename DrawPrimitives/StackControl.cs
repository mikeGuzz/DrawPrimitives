using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawPrimitives
{
    public class StackControl<T>
    {
        private Stack<T> stack1, stack2;

        public StackControl()
        {
            stack2 = new Stack<T>();
            stack1 = new Stack<T>();
        }

        public void PushState(T newState)
        {
            stack1.Push(newState);
            if(stack2.Count > 0)
                stack2.Clear();
        }

        public bool CanPopState()
        {
            return stack1.Count > 0;
        }

        public bool CanPullState()
        {
            return stack2.Count > 0;
        }

        public T PopState()
        {
            stack2.Push(stack1.Peek());
            return stack1.Pop();
        }

        public T PullState()
        {
            stack1.Push(stack2.Peek());
            return stack2.Pop();
        }
    }
}
