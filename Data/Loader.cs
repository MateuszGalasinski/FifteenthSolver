using System;
using System.IO;

namespace Data
{
    public class Loader
    {
        public GameData LoadState(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            Console.WriteLine(filePath);
            string[] lines = File.ReadAllLines(filePath);
            string[] firstLineWords = lines[0].Split(' ');
            byte xLength = byte.Parse(firstLineWords[0]);
            byte yLength = byte.Parse(firstLineWords[1]);

            int[][] board = new int[yLength][];
            for (int i = 0; i < yLength; i++)
            {
                board[i] = new int[xLength];
            }

            for (int i = 0; i < yLength; i++)
            {
                string[] numbers = lines[1 + i].Split(' ');
                for (int j = 0; j < xLength; j++)
                {
                    board[i][j] = Int32.Parse(numbers[i]);
                }
            }
            return new GameData(xLength, yLength, board);
        }
    }
}
