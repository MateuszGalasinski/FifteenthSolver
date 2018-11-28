using System;
using System.Collections.Generic;

namespace GameSolvers
{
    internal class DuplicateKeyComparer<TKey> : IComparer<TKey> where TKey : struct, IComparable
    {

        public int Compare(TKey x, TKey y)
        {
            int result = x.CompareTo(y);

            if (result == 0)
                return 1;
            else
                return result;
        }
    }
}
