using Model;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameSolvers
{
    public class Solution
    {
        public bool IsSolved { get; set; }
        public List<Direction> Moves { get; set; }
        public int ProcessedStatesCounter { get; set; }
        public int VisitedStatesCounter { get; set; }
        public int MaxRecursion { get; set; }
        public Stopwatch Timer { get; set; } = new Stopwatch();
    }
}
