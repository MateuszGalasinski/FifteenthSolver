using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{
    public class DFTSolver : BaseSolver
    {
        private Stack<(Board board, int depth)> _solutionsToSearch = new Stack<(Board, int depth)>();

        public DFTSolver(List<Direction> searchOrder, int maxDepthSearch)
        {
            SearchOrder = searchOrder;
            SearchOrder.Reverse(); // adding to stack should go in reversed order

            MaxDepthSearch = maxDepthSearch;
        }

        protected override void AddChildren((Board board, int depth) current)
        {
            if (CurrentDepthSearch < MaxDepthSearch)
            {
                CurrentDepthSearch++;
                FoundSolutionMetadata.MaxRecursion = Math.Max(FoundSolutionMetadata.MaxRecursion, CurrentDepthSearch);

                foreach (var direction in current.board.PossibleMoves.OrderBy(d => SearchOrder.IndexOf(d))) //use possible moves in order given in _searchOrder
                {
                    Board newBoard = current.board.GenerateChild(direction);
                    if (!GeneratedBoards.Contains(newBoard))
                    {
                        GeneratedBoards.Add(newBoard);
                        _solutionsToSearch.Push((newBoard, CurrentDepthSearch));
                    }
                }
            }
        }

        protected override bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        protected override (Board board, int depth) GetNextChild()
        {
            return _solutionsToSearch.Pop();
        }
    }
}
