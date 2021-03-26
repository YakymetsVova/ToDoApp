using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        public LinkedList<T> stack;

        public int Limit { get; private set; }
        public LimitedSizeStack(int limit)
        {
            stack = new LinkedList<T>();
            Limit = limit;
        }

        public void Push(T item)
        {
            if (Limit == 0) return;
            if (Count == Limit)
            {
                stack.RemoveFirst();
            }
            stack.AddLast(item);
        }

        public T Pop()
        {
            var elem = stack.Last.Value;
            stack.RemoveLast();            
            return elem;
        }

        public int Count
        {
            get
            {
                return stack.Count;
            }
        } 
    }
}
