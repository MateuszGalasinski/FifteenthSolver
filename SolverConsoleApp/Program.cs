using Data;
using GameSolvers;
using GameSolvers.Solvers;
using GameSolvers.Solvers.Bidirectional;
using GameSolvers.Solvers.Bidirectional.Strategies;
using GameSolvers.Solvers.Bidirectional.Strategies.Base;
using GameSolvers.Solvers.Metrics;
using GameSolvers.Solvers.Unidirectional;
using GameSolvers.Solvers.Unidirectional.Base;
using Model;
using SolverConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolverConsoleApp
{
    class Program
    {
        private const int DFSMaxDepthSearch = 25;

        static void Main(string[] args)
        {
            AppParameters parameters = ParseParameters(args);
            Console.WriteLine($"Run with solution filepath: {parameters.SolutionFilePath}");
            Loader loader = new Loader();
            Board board = loader.LoadState(parameters.StartFilePath);
            IGameSolver solver = GetSolver(parameters);
            Solution solution = solver.Solve(board);
            
            Writer writer = new Writer();
            writer.WriteSolution(parameters.SolutionFilePath, parameters.AdditionalFilePath, solution);
        }

        private static AppParameters ParseParameters(string[] args)
        {
            if (args.Length != 5 && args.Length != 8)
            {
                throw new ArgumentException($"There should be 5 or 8 parameters, but found: {args.Length}.\n {args.ToList().Aggregate((p, q) => p + " " + q)} \n");
            }

            AppParameters resultParameters = new AppParameters();
            int index = 0;

            index = LookForStrategy(args, resultParameters, index);

            resultParameters.StartFilePath = args[index];
            index++;
            resultParameters.SolutionFilePath = args[index];
            index++;
            resultParameters.AdditionalFilePath = args[index];

            return resultParameters;
        }

        private static int LookForStrategy(string[] args, AppParameters resultParameters, int index)
        {
            if (!Enum.TryParse(args[index].ToUpperInvariant(), out StrategyType type))
            {
                throw new ArgumentException($"Could not parse strategy type.\n ---> {args[index]} \n");
            }
            resultParameters.StrategyTypes.Add(type);
            index++;

            index = LookForStrategyParameters(args, resultParameters, index, type);
            return index;
        }

        private static int LookForStrategyParameters(string[] args, AppParameters resultParameters, int index, StrategyType type)
        {
            if (type == StrategyType.ASTR) //look for metric
            {
                if (!Enum.TryParse(args[index].ToUpperInvariant(), out MetricType metric))
                {
                    throw new ArgumentException($"Could not parse metric type. \n ---> {args[index]} \n");
                }

                resultParameters.MetricTypes.Add(metric);
                index++;
            }
            else if (type == StrategyType.BD) // look for two strategies
            {
                index = LookForOneDirectionalStrategyParameters(args, resultParameters, index);
                index = LookForOneDirectionalStrategyParameters(args, resultParameters, index);
            }
            else // look for search order
            {
                resultParameters.SearchOrders.Add(ToDirections(args[index].ToCharArray()));
                index++;
            }

            return index;
        }

        private static int LookForOneDirectionalStrategyParameters(string[] args, AppParameters resultParameters, int index)
        {
            if (!Enum.TryParse(args[index].ToUpperInvariant(), out StrategyType strategy))
            {
                throw new ArgumentException($"Could not parse metric type. \n ---> {args[index]} \n");
            } 
            resultParameters.StrategyTypes.Add(strategy);
            index++;

            if (strategy == StrategyType.ASTR)
            {
                if (!Enum.TryParse(args[index].ToUpperInvariant(), out MetricType firstMetric))
                {
                    throw new ArgumentException($"Could not parse metric type. \n ---> {args[index]} \n");
                }
                resultParameters.MetricTypes.Add(firstMetric);
                index++;
            }
            else // look for search order
            {
                resultParameters.SearchOrders.Add(ToDirections(args[index].ToUpperInvariant().ToCharArray()));
                index++;
            }

            return index;
        }

        private static List<Direction> ToDirections(char[] characters)
        {
            List<Direction> directions = new List<Direction>();
            for (int i = 0; i < characters.Length; i++)
            {
                switch (characters[i])
                {
                    case 'R':
                        directions.Add(Direction.Right);
                        break;
                    case 'U':
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

        private static IGameSolver GetSolver(AppParameters parameters)
        {
            switch (parameters.StrategyTypes.First())
            {
                case StrategyType.DFS:
                    return new DFSSolver(parameters.SearchOrders.First(), 30);

                case StrategyType.BFS:
                    return new BFSSolver(parameters.SearchOrders.First());

                case StrategyType.ASTR:
                    if (parameters.MetricTypes.First() == MetricType.HAMM)
                    {
                        return new MetricSolver(new HammingMetricCalculator());
                    }
                    if (parameters.MetricTypes.First() == MetricType.MANH)
                    {
                        return new MetricSolver(new ManhattanMetricCalculator());
                    }
                    throw new ArgumentException("Invalid metric type");

                case StrategyType.BD:
                    IStrategy forwardStrategy = GetStrategy(parameters, 0);
                    IStrategy backwardStrategy = GetStrategy(parameters, 1);
                    return new BidirectionalBaseSolver(forwardStrategy, backwardStrategy);

                default:
                    throw new ArgumentException("Invalid strategy type");
            }
        }

        private static IStrategy GetStrategy(AppParameters parameters, int index)
        {
            if (parameters.StrategyTypes[index + 1] == StrategyType.DFS)
            {
                return new DFSStrategy(parameters.SearchOrders[index], DFSMaxDepthSearch);
            }
            if (parameters.StrategyTypes[index + 1] == StrategyType.BFS)
            {
                return new BFSStrategy(parameters.SearchOrders[index]);
            }
            if (parameters.StrategyTypes[index + 1] == StrategyType.ASTR)
            {
                if (parameters.MetricTypes[index] == MetricType.HAMM)
                {
                    return new MetricStrategy(new HammingMetricCalculator());
                }
                if (parameters.MetricTypes[index] == MetricType.MANH)
                {
                    return new MetricStrategy(new ManhattanMetricCalculator());
                }
            }
            throw new ArgumentException("Could not get parse strategy in GetStrategy");
        }
    }
}
