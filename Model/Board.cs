using System;
using System.Collections.Generic;

namespace Model
{
    public class Board
    {
        public int XLength { get; }
        public int YLength { get; }

        public Board(int xLength, int yLength)
        {
            XLength = xLength;
            YLength = yLength;
            Fields = new int[YLength][];
            for (int i = 0; i < YLength; i++)
            {
                Fields[i] = new int[XLength];
            }
        }

        public Board(int xLength, int yLength, int[][] board)
        {
            XLength = xLength;
            YLength = yLength;

            ValidateBoardSize(board);

            Fields = board ?? throw new ArgumentNullException(nameof(board));

            for (int i = 0; i < YLength; i++)
            {
                for (int j = 0; j < XLength; j++)
                {
                    if (board[i][j] == 0)
                    {
                        EmptyFieldIndex = (i, j);
                    }
                }
            }
        }


        public int[][] Fields { get; set; }

        public (int X, int Y) EmptyFieldIndex { get; private set; }

        public List<Directions> PossibleMoves
        {
            get
            {
                List<Directions> moves = new List<Directions>();
                if (EmptyFieldIndex.Y > 0)
                {
                    moves.Add(Directions.Top);
                }

                if (EmptyFieldIndex.Y < YLength - 1)
                {
                    moves.Add(Directions.Down);
                }

                if (EmptyFieldIndex.X > 0)
                {
                    moves.Add(Directions.Left);
                }

                if (EmptyFieldIndex.X < XLength - 1)
                {
                    moves.Add(Directions.Right);
                }

                return moves;
            }
        }

        public bool IsSolved()
        {
            for (int i = 0; i < YLength; i++)
            {
                for (int j = 0; j < XLength; j++)
                {
                    if (i == YLength - 1 && j == XLength - 1)
                    {
                        if (Fields[i][j] != 0)
                            return false;
                    }
                    else
                    {
                        if (Fields[i][j] != j + i * XLength + 1)
                            return false;
                    }

                }
            }
            return true;
        }

        private void ValidateBoardSize(int[][] board)
        {
            if (board.Length != YLength)
            {
                throw new ArgumentException("Invalid board Y length");
            }
            for (int i = 0; i < YLength; i++)
            {
                if (board[i].Length != XLength)
                {
                    throw new ArgumentException($"Invalid board X length at: {i}");
                }
            }
        }

    }
}
