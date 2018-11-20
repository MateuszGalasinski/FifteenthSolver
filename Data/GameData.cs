using System;

namespace Data
{
    public class GameData
    {
        public int XLength { get; }
        public int YLength { get; }

        public GameData(int xLength, int yLength)
        {
            XLength = xLength;
            YLength = yLength;
            Board = new int[YLength][];
            for (int i = 0; i < YLength; i++)
            {
                Board[i] = new int[XLength];
            }
        }

        public GameData(int xLength, int yLength, int[][] board)
        {
            XLength = xLength;
            YLength = yLength;
            Board = board ?? throw new ArgumentNullException(nameof(board));
        }

        public int[][] Board { get; set; }
    }
}
