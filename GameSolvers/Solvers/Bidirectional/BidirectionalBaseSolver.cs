using GameSolvers.Extensions;
using Model;
using System;
using System.Collections.Generic;

namespace GameSolvers.Solvers
{
    public class BidirectionalBaseSolver
    {
        private readonly ManhattanMetricStrategy _forwardSolverStrategy;
        private readonly ManhattanMetricStrategy _backwardSolverStrategy;

        public BidirectionalBaseSolver(ManhattanMetricStrategy forwardSolverStrategy, ManhattanMetricStrategy backwardSolverStrategy)
        {
            _forwardSolverStrategy = forwardSolverStrategy ?? throw new ArgumentNullException(nameof(forwardSolverStrategy));
            _backwardSolverStrategy = backwardSolverStrategy ?? throw new ArgumentNullException(nameof(backwardSolverStrategy));
        }

        public Solution Solution { get; set; } = new Solution();

        public List<Direction> Solve(Board forwardBoard)
        {
            Board backwardBoard = GenerateSolvedBoard(forwardBoard.YLength, forwardBoard.XLength);
            _forwardSolverStrategy.CheckedBoards.Add(forwardBoard);
            _backwardSolverStrategy.CheckedBoards.Add(backwardBoard);
            while (true)
            {
                //move forward tree
                _forwardSolverStrategy.CheckedBoards.Add(forwardBoard);
                _forwardSolverStrategy.AddChildren(forwardBoard);
                forwardBoard = _forwardSolverStrategy.GetNextChild();

                //move backward tree
                _backwardSolverStrategy.CheckedBoards.Add(backwardBoard);
                _backwardSolverStrategy.AddChildren(backwardBoard);
                backwardBoard = _backwardSolverStrategy.GetNextChild();

                //check for solution
                if (_forwardSolverStrategy.CheckedBoards.Contains(backwardBoard)) // it is solved
                {
                    //concatenate two boards histories
                    _forwardSolverStrategy.CheckedBoards.TryGetValue(backwardBoard, out Board forwardSolved);
                    List<Direction> solutionMoves = new List<Direction>(forwardSolved.MovesHistory);
                    backwardBoard.MovesHistory.Reverse();
                    foreach (var direction in backwardBoard.MovesHistory)
                    {
                        solutionMoves.Add(direction.OppositeDirection());
                    }
                    return solutionMoves;
                }
            }
        }

        private Board GenerateSolvedBoard(int yLength, int xLength)
        {
            Board board = new Board(xLength, yLength);
            for (int i = 0; i < yLength; i++)
            {
                for (int j = 0; j < xLength; j++)
                {
                    board.Fields[i][j] = 1 + j + i * yLength;
                }
            }

            board.Fields[yLength - 1][xLength - 1] = 0;
            board.EmptyIndex = (yLength-1, xLength-1);

            return board;
        }
    }
}
