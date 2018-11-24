using Model;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{
    public class BFSSolver : BaseSolver
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
                _solutionsToSearch.Enqueue((newBoard, CurrentDepthSearch));
            }
        }

        protected override (Board board, int depth) GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.Peek().board))
            {
                _solutionsToSearch.Dequeue();
            }
            return _solutionsToSearch.Dequeue();;
        }
    }
}
