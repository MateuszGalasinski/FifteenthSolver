using GameSolvers;
using System.IO;

namespace Data
{
    public class Writer
    {
        public void WriteSolution(string solutionFilePath, string additionalInfoPath, Solution solution)
        {

            //write solution file
            using (StreamWriter writer = new StreamWriter(solutionFilePath))
            {
                writer.WriteLine(solution.Moves.Count);
                foreach (var move in solution.Moves)
                {
                    writer.Write($"{move.ToCharacterSign()} ");
                }
            }

            //write additional info file
            using (StreamWriter writer = new StreamWriter(additionalInfoPath))
            {
                writer.WriteLine(solution.Moves.Count);
                writer.WriteLine(solution.VisitedStatesCounter);
                writer.WriteLine(solution.ProcessedStatesCounter);
                writer.WriteLine(solution.MaxRecursion);
                writer.WriteLine(solution.Timer.ElapsedMilliseconds);
            }
        }
    }
}
