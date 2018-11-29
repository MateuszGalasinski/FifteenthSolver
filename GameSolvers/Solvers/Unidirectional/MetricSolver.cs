using GameSolvers.Solvers.Metrics;
using GameSolvers.Solvers.Unidirectional.Base;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSolvers.Solvers.Unidirectional
{
    public class MetricSolver : BaseSolver
    {
        private readonly IMetricCalculator _metricCalculator;
        private readonly SortedList<int, Board> _solutionsToSearch = new SortedList<int, Board>(new DuplicateKeyComparer<int>());

        public MetricSolver(IMetricCalculator metricCalculator)
        {
            _metricCalculator = metricCalculator ?? throw new ArgumentNullException(nameof(metricCalculator));
        }

        protected override bool HasRemainingChild()
        {
            return _solutionsToSearch.Count != 0;
        }

        protected override void AddChildren(Board current)
        {
            List<Direction> moves = current.PossibleMoves;
            Solution.ProcessedStatesCounter += moves.Count;

            foreach (var direction in moves)
            {
                Board newBoard = current.GenerateChild(direction);
                if (!CheckedBoards.Contains(newBoard))
                {
                    _solutionsToSearch.Add(_metricCalculator.CalculatePriority(newBoard), newBoard);
                }
            }
        }

        protected override Board GetNextChild()
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
