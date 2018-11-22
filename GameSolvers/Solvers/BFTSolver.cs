using Model;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{
    public class BFTSolver : BaseSolver
    {
        private Queue<(Board board, int depth)> _solutionsToSearch = new Queue<(Board board, int depth)>();

        protected override bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        protected override void AddChildren((Board board, int depth) current)
        {
            foreach (var direction in current.board.PossibleMoves.OrderBy(d => SearchOrder.IndexOf(d))) //use possible moves in order given in _searchOrder
            {
                Board newBoard = current.board.GenerateChild(direction);
                if (!GeneratedBoards.Contains(newBoard))
                {
                    GeneratedBoards.Add(newBoard);
                    _solutionsToSearch.Enqueue((newBoard, CurrentDepthSearch));
                }
            }
        }

        protected override (Board board, int depth) GetNextChild()
        {
            return _solutionsToSearch.Dequeue();;
        }
    }
}
