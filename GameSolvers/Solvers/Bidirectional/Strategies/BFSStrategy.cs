using GameSolvers.Solvers.Bidirectional.Strategies.Base;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers.Bidirectional.Strategies
{
    public class BFSStrategy : BaseStrategy, IStrategy
    {
        private readonly Queue<Board> _solutionsToSearch = new Queue<Board>();
        protected List<Direction> SearchOrder;

        public BFSStrategy(List<Direction> searchOrder)
        {
            SearchOrder = searchOrder ?? throw new ArgumentNullException(nameof(searchOrder));
        }

        public bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        public int RemainingCount => _solutionsToSearch.Count;

        public void AddChildren(Board current)
        {
            foreach (var direction in SearchOrder.Where(current.PossibleMoves.Contains))
            {
                _solutionsToSearch.Enqueue(current.GenerateChild(direction));
            }
        }

        public Board GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.Peek()))
            {
                _solutionsToSearch.Dequeue();
            }
            return _solutionsToSearch.Dequeue();;
        }
    }
}
