using GameSolvers.Extensions;
using GameSolvers.Solvers.Base;
using GameSolvers.Solvers.Bidirectional.Strategies.Base;
using Model;
using System;
using System.Collections.Generic;

namespace GameSolvers.Solvers.Bidirectional
{
    public class BidirectionalBaseSolver : IGameSolver
    {
        private readonly IStrategy _forwardSolverStrategy;
        private readonly IStrategy _backwardSolverStrategy;
        private bool _isForwardTreeExpandable = true;
        private bool _isBackwardTreeExpandable = true;

        public BidirectionalBaseSolver(IStrategy forwardSolverStrategy, IStrategy backwardSolverStrategy)
        {
            _forwardSolverStrategy = forwardSolverStrategy ?? throw new ArgumentNullException(nameof(forwardSolverStrategy));
            _backwardSolverStrategy = backwardSolverStrategy ?? throw new ArgumentNullException(nameof(backwardSolverStrategy));
        }

        public Solution Solution { get; set; } = new Solution();

        public Solution Solve(Board forwardBoard)
        {
            Solution.Timer.Start();
            if (forwardBoard.IsSolved())
            {
                Solution.Moves = new List<Direction>();
                Solution.ProcessedStatesCounter = 1;
                Solution.VisitedStatesCounter = 1;
                Solution.Timer.Stop();
                return Solution;
            }
            Board backwardBoard = GenerateSolvedBoard(forwardBoard.YLength, forwardBoard.XLength);
            while (true)
            {
                if (_isForwardTreeExpandable)   //move forward tree
                {
                    _forwardSolverStrategy.CheckedBoards.Add(forwardBoard);
                    _forwardSolverStrategy.AddChildren(forwardBoard);
                    if (_forwardSolverStrategy.HasRemainingChild())
                    {
                        forwardBoard = _forwardSolverStrategy.GetNextChild();
                    }
                    else
                    {
                        _isForwardTreeExpandable = false;
                        if (!_isBackwardTreeExpandable)
                        {
                            Solution.IsSolved = false;
                            break;
                        }
                    }
                }

                if (_isBackwardTreeExpandable) // move backward treee
                {
                    _backwardSolverStrategy.CheckedBoards.Add(backwardBoard);
                    _backwardSolverStrategy.AddChildren(backwardBoard);

                    if (_backwardSolverStrategy.HasRemainingChild())
                    {
                        backwardBoard = _backwardSolverStrategy.GetNextChild();
                    }
                    else
                    {
                        _isBackwardTreeExpandable = false;
                        if (!_isForwardTreeExpandable)
                        {
                            Solution.IsSolved = false;
                            break;
                        }
                    }
                }

                Solution.MaxRecursion = Math.Max(
                    Solution.MaxRecursion,
                    Math.Max(forwardBoard.MovesHistory.Count, backwardBoard.MovesHistory.Count));

                //check for solution
                if (_forwardSolverStrategy.CheckedBoards.TryGetValue(backwardBoard, out Board forwardSolved)) // backward met any forward
                {
                    GenerateSolution(forwardSolved, backwardBoard);
                    break;
                }
                if (_backwardSolverStrategy.CheckedBoards.TryGetValue(forwardBoard, out Board backwardSolved)) // forward met any backward
                {
                    GenerateSolution(forwardBoard, backwardSolved);
                    break;
                }
            }

            SaveMetadata();
            return Solution;
        }

        private void GenerateSolution(Board forwardBoard, Board backwardSolved)
        {
            List<Direction> solutionMoves = new List<Direction>(forwardBoard.MovesHistory);
            backwardSolved.MovesHistory.Reverse();
            foreach (var direction in backwardSolved.MovesHistory)
            {
                solutionMoves.Add(direction.OppositeDirection());
            }

            Solution.Moves = solutionMoves;
            Solution.IsSolved = true;
        }

        private void SaveMetadata()
        {
            Solution.ProcessedStatesCounter = _forwardSolverStrategy.CheckedBoards.Count + _backwardSolverStrategy.CheckedBoards.Count;
            Solution.VisitedStatesCounter = Solution.ProcessedStatesCounter +
                                            _forwardSolverStrategy.RemainingCount +
                                            _backwardSolverStrategy.RemainingCount;
            Solution.Timer.Stop();
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
