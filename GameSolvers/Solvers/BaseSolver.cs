using Model;
using System.Collections.Generic;

namespace GameSolvers.Solvers
{
    public abstract class BaseSolver : IGameSolver
    {
        protected List<Direction> SearchOrder = new List<Direction>() { Direction.Right, Direction.Down, Direction.Left, Direction.Top };
        protected HashSet<Board> CheckedBoards = new HashSet<Board>(new BoardValuesEqualityComparer());
        public Solution SolutionMetadata { get; set; } = new Solution();
        protected int MaxDepthSearch { get; set; } = 1;
        protected int CurrentDepthSearch { get; set; }
        public Board Solve(Board board)
        {
            SolutionMetadata.Timer.Start();
            CurrentDepthSearch = 0;
            Initialize(board, CurrentDepthSearch, out (Board board, int depth) current);

            while (!current.board.IsSolved())
            {
                AddChildren(current);

                if (HasRemainingChild())
                {
                    current = GetNextChild();
                    SolutionMetadata.ProcessedStatesCounter++;
                }
                else
                {
                    break;
                }

                CheckedBoards.Add(current.board);
            }

            SolutionMetadata.Timer.Stop();
            SolutionMetadata.MaxRecursion = CurrentDepthSearch;
            SolutionMetadata.Length = current.depth;
            SolutionMetadata.EndBoard = current.board;

            return current.board;
        }

        protected abstract bool HasRemainingChild();

        protected abstract void AddChildren((Board board, int depth) current);

        protected abstract (Board board, int depth) GetNextChild();

        protected void Initialize(Board board, int depthSearch, out (Board board, int depth) current)
        {
            current = (board, depthSearch);
            SolutionMetadata.VisitedStatesCounter++;
            SolutionMetadata.ProcessedStatesCounter++;
        }
    }
}
