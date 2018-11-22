using Model;

namespace GameSolvers
{
    public class Solution
    {
        public Board EndBoard { get; set; }
        public int Length { get; set; }
        public int ProcessedStatesCounter { get; set; }
        public int VisitedStatesCounter { get; set; }
        public int MaxRecursion { get; set; }
    }
}
