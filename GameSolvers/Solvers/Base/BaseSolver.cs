using Model;
using System.Collections.Generic;

namespace GameSolvers.Solvers.Base
{
    public abstract class BaseSolver : IGameSolver
    {
        protected HashSet<Board> CheckedBoards = new HashSet<Board>(new BoardValuesEqualityComparer());
        public Solution Solution { get; set; } = new Solution();
        protected int MaxDepthSearch { get; set; } = 1;
        protected int CurrentDepthSearch { get; set; }

        public Solution Solve(Board board)
        {
            Solution.Timer.Start();
            Initialize(board, out Board current);

            while (!current.IsSolved())
            {
                CheckedBoards.Add(current);
                AddChildren(current);

                if (HasRemainingChild())
                {
                    current = GetNextChild();
                }
                else
                {
                    break;
                }
            }

            Solution.IsSolved = current.IsSolved();
            Solution.ProcessedStatesCounter = CheckedBoards.Count;
            Solution.MaxRecursion = CurrentDepthSearch;
            Solution.Moves = current.MovesHistory;

            Solution.Timer.Stop();
            return Solution;
        }

        protected abstract bool HasRemainingChild();

        protected abstract void AddChildren(Board current);

        protected abstract Board GetNextChild();

        protected void Initialize(Board board, out Board current)
        {
            current = board;
            Solution.VisitedStatesCounter++;
        }
    }
}
