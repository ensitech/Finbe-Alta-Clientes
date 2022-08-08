using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AltaCliente
{
    public class Tuple<T1, T2>
    {
        public T1 First { get; private set; }

        public T2 Second { get; private set; }

        internal Tuple(T1 first, T2 second)
        {
            this.First = first;
            this.Second = second;
        }
    }

}
