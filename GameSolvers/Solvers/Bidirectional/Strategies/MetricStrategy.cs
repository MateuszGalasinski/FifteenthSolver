using GameSolvers.Solvers.Bidirectional.Strategies.Base;
using GameSolvers.Solvers.Metrics;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers.Bidirectional.Strategies
{
    public class MetricStrategy : BaseStrategy, IStrategy
    {
        private readonly IMetricCalculator _metricCalculator;
        private readonly SortedList<int, Board> _solutionsToSearch = new SortedList<int, Board>(new DuplicateKeyComparer<int>());

        public MetricStrategy(IMetricCalculator metricCalculator)
        {
            _metricCalculator = metricCalculator ?? throw new ArgumentNullException(nameof(metricCalculator));
        }

        public bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        public void AddChildren(Board current)
        {
            List<Direction> moves = current.PossibleMoves;
            ProcessedStatesCounter += moves.Count;

            foreach (var direction in moves)
            {
                Board newBoard = current.GenerateChild(direction);
                if (!CheckedBoards.Contains(newBoard))
                {
                    _solutionsToSearch.Add(_metricCalculator.CalculatePriority(newBoard), newBoard);
                }
            }
        }

        public Board GetNextChild()
        {
            while (CheckedBoards.Contains(_solutionsToSearch.First().Value))
            {
                _solutionsToSearch.RemoveAt(0);
            }

            var first = _solutionsToSearch.First();
            _solutionsToSearch.RemoveAt(0);
            return first.Value;
        }
    }
}
