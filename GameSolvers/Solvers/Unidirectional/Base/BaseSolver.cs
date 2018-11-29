using Model;
using System;
using System.Collections.Generic;

namespace GameSolvers.Solvers.Unidirectional.Base
{
    public abstract class BaseSolver : IGameSolver
    {
        protected HashSet<Board> CheckedBoards = new HashSet<Board>(new BoardValuesEqualityComparer());
        public Solution Solution { get; set; } = new Solution();
        protected int MaxDepthSearch { get; set; } = 1;

        public Solution Solve(Board board)
        {
            Solution.Timer.Start();
            Board current = board;
            Solution.MaxRecursion = Math.Max(Solution.MaxRecursion, current.MovesHistory.Count);

            while (!current.IsSolved())
            {
                CheckedBoards.Add(current);
                AddChildren(current);
                if (HasRemainingChild())
                {
                    current = GetNextChild();
                    Solution.MaxRecursion = Math.Max(Solution.MaxRecursion, current.MovesHistory.Count);
                }
                else
                {
                    break;
                }
            }
            //include final board impact
            CheckedBoards.Add(current);

            Solution.IsSolved = current.IsSolved();
            Solution.VisitedStatesCounter = CheckedBoards.Count;
            Solution.Moves = current.MovesHistory;

            Solution.Timer.Stop();
            return Solution;
        }

        protected abstract bool HasRemainingChild();

        protected abstract void AddChildren(Board current);

        protected abstract Board GetNextChild();
    }
}
