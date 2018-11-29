using GameSolvers.Solvers.Unidirectional.Base;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers.Unidirectional
{
    public class BFSSolver : BaseSolver
    {
        private readonly Queue<Board> _solutionsToSearch = new Queue<Board>();
        protected List<Direction> SearchOrder;

        public BFSSolver(List<Direction> searchOrder)
        {
            SearchOrder = searchOrder ?? throw new ArgumentNullException(nameof(searchOrder));
        }

        protected override bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        protected override void AddChildren(Board current)
        {
            List<Direction> moves = current.PossibleMoves;
            Solution.ProcessedStatesCounter += moves.Count;

            foreach (var direction in SearchOrder.Where(moves.Contains))
            {
                Board child = current.GenerateChild(direction);
                if (!CheckedBoards.Contains(child))
                {
                    _solutionsToSearch.Enqueue(child);
                }
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
