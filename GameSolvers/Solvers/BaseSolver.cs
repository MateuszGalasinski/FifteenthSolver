using Model;
using System.Collections.Generic;

namespace GameSolvers.Solvers
{
    public abstract class BaseSolver : IGameSolver
    {
        protected List<Direction> SearchOrder = new List<Direction>() { Direction.Right, Direction.Down, Direction.Left, Direction.Top };
        protected HashSet<Board> GeneratedBoards = new HashSet<Board>(new BoardValuesEqualityComparer());
        public Solution FoundSolutionMetadata { get; set; } = new Solution();
        protected int MaxDepthSearch { get; set; } = 1;
        protected int CurrentDepthSearch { get; set; }
        public Board Solve(Board board)
        {
            CurrentDepthSearch = 0;
            Initialize(board, CurrentDepthSearch, out (Board board, int depth) current);

            while (!current.board.IsSolved())
            {
                AddChildren(current);

                if (HasRemainingChild())
                {
                    current = GetNextChild();
                    FoundSolutionMetadata.ProcessedStatesCounter++;
                }
                else
                {
                    break;
                }
            }

            FoundSolutionMetadata.MaxRecursion = CurrentDepthSearch;
            FoundSolutionMetadata.Length = current.depth;
            FoundSolutionMetadata.EndBoard = current.board;

            return current.board;
        }

        protected abstract bool HasRemainingChild();

        protected abstract void AddChildren((Board board, int depth) current);

        protected abstract (Board board, int depth) GetNextChild();

        protected void Initialize(Board board, int depthSearch, out (Board board, int depth) current)
        {
            current = (board, depthSearch);
            GeneratedBoards.Add(current.board);
            FoundSolutionMetadata.VisitedStatesCounter++;
            FoundSolutionMetadata.ProcessedStatesCounter++;
        }
    }
}
