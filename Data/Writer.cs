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
                writer.WriteLine(solution.Length);
                foreach (var move in solution.EndBoard.MovesHistory)
                {
                    writer.Write($"{move.ToCharacterSign()} ");
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
