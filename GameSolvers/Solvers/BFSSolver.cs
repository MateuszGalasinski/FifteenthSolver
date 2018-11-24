using Model;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers
{
    public class BFSSolver : BaseSolver
    {
        private Queue<Board> _solutionsToSearch = new Queue<Board>();

        protected override bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        protected override void AddChildren(Board current)
        {
            foreach (var direction in SearchOrder.Where(current.PossibleMoves.Contains))
            {
                _solutionsToSearch.Enqueue(current.GenerateChild(direction));
            }
        }

        protected override Board GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.Peek()))
            {
                _solutionsToSearch.Dequeue();
            }
            return _solutionsToSearch.Dequeue();;
        }
    }
}
