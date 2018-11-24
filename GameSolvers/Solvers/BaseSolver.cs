using Model;
using System.Collections.Generic;

namespace GameSolvers.Solvers
{
    public abstract class BaseSolver : IGameSolver
    {
        protected List<Direction> SearchOrder = new List<Direction>() { Direction.Right, Direction.Down, Direction.Left, Direction.Top };
        protected HashSet<Board> CheckedBoards = new HashSet<Board>(new BoardValuesEqualityComparer());
        public Solution Solution { get; set; } = new Solution();
        protected int MaxDepthSearch { get; set; } = 1;
        protected int CurrentDepthSearch { get; set; }

        public Board Solve(Board board)
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

            Solution.ProcessedStatesCounter = CheckedBoards.Count;
            Solution.MaxRecursion = CurrentDepthSearch;
            Solution.Length = current.MovesHistory.Count;
            Solution.EndBoard = current;

            Solution.Timer.Stop();
            return current;
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
