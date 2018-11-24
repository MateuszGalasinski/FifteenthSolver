using GameSolvers;
using Model;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class Writer
    {
        public void WriteSolution(string solutionFilePath, string additionalInfoPath, Solution solution)
        {
            Stack<string> moves = new Stack<string>();
            Board currentBoard = solution.EndBoard;
            while (currentBoard.LastMove != Direction.Unknown)
            {
                moves.Push(currentBoard.LastMove.ToCharacterSign());
                currentBoard = currentBoard.Parent;
            }

            //write solution file
            using (StreamWriter writer = new StreamWriter(solutionFilePath))
            {
                writer.WriteLine(solution.Length);

                while (moves.Count != 0)
                {
                    writer.Write($"{moves.Pop()} ");
                }
            }

            //write additional info file
            using (StreamWriter writer = new StreamWriter(additionalInfoPath))
            {
                writer.WriteLine(solution.Length);
                writer.WriteLine(solution.VisitedStatesCounter);
                writer.WriteLine(solution.ProcessedStatesCounter);
                writer.WriteLine(solution.MaxRecursion);
                writer.WriteLine(solution.Timer.ElapsedMilliseconds);
            }
        }
    }
}
