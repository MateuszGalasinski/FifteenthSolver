using System;
using System.Collections.Generic;

namespace Model
{
    public class BoardValuesEqualityComparer : IEqualityComparer<Board>
    {
        public bool Equals(Board x, Board y)
        {
            if (x.YLength == y.YLength && x.XLength == y.XLength)
            {
                for (int i = 0; i < x.YLength; i++)
                {
                    for (int j = 0; j < x.XLength; j++)
                    {
                        if (x.Fields[i][j] != y.Fields[i][j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Board obj)
        {
            unchecked
            {
                //int hash = 17;
                //for (int i = 0; i < obj.YLength; i++)
                //{
                //    for (int j = 0; j < obj.XLength; j++)
                //    {
                //        hash = hash * 31 + obj.Fields[i][j].GetHashCode();
                //    }
                //}
                //return hash;

                int p = 16777619;
                long hash = 2166136261l;

                for (int i = 0; i < obj.YLength; i++)
                {
                    for (int j = 0; j < obj.XLength; j++)
                    {
                        hash = (hash ^ obj.Fields[i][j] ^ (i * j)) * p;
                    }
                }

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;

                return (int)hash % Int32.MaxValue;
            }
        }
    }
}
