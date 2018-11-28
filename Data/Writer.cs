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
                if (solution.IsSolved)
                {
                    writer.WriteLine(solution.Moves.Count);
                    foreach (var move in solution.Moves)
                    {
                        writer.Write($"{move.ToCharacterSign()} ");
                    }
                }
                else
                {
                    writer.WriteLine("-1");
                }   
            }

            //write additional info file
            using (StreamWriter writer = new StreamWriter(additionalInfoPath))
            {
                if (solution.IsSolved)
                {
                    writer.WriteLine(solution.Moves.Count);
                }
                else
                {
                    writer.WriteLine("-1");
                }
                writer.WriteLine(solution.VisitedStatesCounter);
                writer.WriteLine(solution.ProcessedStatesCounter);
                writer.WriteLine(solution.MaxRecursion);
                writer.WriteLine(solution.Timer.ElapsedMilliseconds);
            }
        }
    }
}
