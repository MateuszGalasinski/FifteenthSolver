using Data;
using GameSolvers.Solvers;
using Model;
using SolverConsoleApp.Models;
using System;
using System.Collections.Generic;

namespace SolverConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AppParameters parameters = ParseParameters(args);

            Loader loader = new Loader();
            Board board = loader.LoadState(parameters.StartFilePath);
            IGameSolver solver = GetSolver(parameters);
            solver.Solve(board);
            
            Writer writer = new Writer();
            writer.WriteSolution(parameters.SolutionFilePath, parameters.AdditionalFilePath, solver.Solution);
        }

        private static AppParameters ParseParameters(string[] args)
        {
            if (args.Length != 5)
            {
                throw new ArgumentException($"There should be five parameters, but found: {args.Length}");
            }

            AppParameters resultParameters = new AppParameters();
            int index = 0;

            StrategyType type;
            if (!Enum.TryParse(args[index].ToUpperInvariant(), out type))
            {
                throw new ArgumentException("Could not parse strategy type.");
            }
            index++;

            MetricType metric;
            if (type == StrategyType.ASTR)
            {
                if (!Enum.TryParse(args[index].ToLowerInvariant(), out metric))
                {
                    throw new ArgumentException("Could not parse metric type.");
                }   
            }
            else
            {
                resultParameters.SearchOrder = ToDirections(args[index].ToCharArray());
            }

            index++;

            resultParameters.StartFilePath = args[index];
            index++;
            resultParameters.SolutionFilePath = args[index];
            index++;
            resultParameters.AdditionalFilePath = args[index];

            return resultParameters;

            List<Direction> ToDirections(char[] characters)
            {
                List<Direction> directions = new List<Direction>();
                for (int i = 0; i < characters.Length; i++)
                {
                    switch (characters[i])
                    {
                        case 'R':
                            directions.Add(Direction.Right);
                            break;
                        case 'T':
                            directions.Add(Direction.Top);
                            break;
                        case 'L':
                            directions.Add(Direction.Left);
                            break;
                        case 'D':
                            directions.Add(Direction.Down);
                            break;
                    }
                }

                return directions;
            }
        }

        private static IGameSolver GetSolver(AppParameters parameters)
        {
            switch (parameters.StrategyType)
            {
                case StrategyType.DFS:
                    return new DFSSolver(parameters.SearchOrder, 30);
                case StrategyType.BFS:
                    return new BFSSolver();
                case StrategyType.ASTR:
                    if (parameters.MetricType == MetricType.Hamming)
                    {
                        return new HammingMetricSolver();
                    }
                    if (parameters.MetricType == MetricType.Manhattan)
                    {
                        return new ManhattanMetricSolver();
                    }
                    throw new ArgumentException("Invalid metric type");
                default:
                    throw new ArgumentException("Invalid strategy type");
            }
        }
    }
}
