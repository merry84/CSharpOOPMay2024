using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
        =>this.Count == 0;
        public void AddRange(Stack<string> strings)
        {
            foreach (var item in strings)
            {
                this.Push(item);
            }
        }
    }
}
