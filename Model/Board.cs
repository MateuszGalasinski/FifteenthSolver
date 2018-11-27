using System;
using System.Collections.Generic;

namespace Model
{
    public class Board
    {
        public List<Direction> MovesHistory { get; set; } = new List<Direction>();
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

            EmptyIndex = (0, 0);
        }

        public Board(int xLength, int yLength, int[][] board, Board parent, List<Direction> history) : this(xLength, yLength, board)
        {
            MovesHistory = history;
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
                        EmptyIndex = (j, i);
                    }
                }
            }
        }

        public int[][] Fields { get; set; }

        public (int X, int Y) EmptyIndex { get; set; }

        public List<Direction> PossibleMoves
        {
            get
            {
                List<Direction> moves = new List<Direction>();
                if (EmptyIndex.Y > 0)
                {
                    moves.Add(Direction.Top);
                }

                if (EmptyIndex.Y < YLength - 1)
                {
                    moves.Add(Direction.Down);
                }

                if (EmptyIndex.X > 0)
                {
                    moves.Add(Direction.Left);
                }

                if (EmptyIndex.X < XLength - 1)
                {
                    moves.Add(Direction.Right);
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

        /// <summary>
        /// Generates new, cloned board with parent set to this object.
        /// </summary>
        /// <param name="moveDirection">Direction of move that generate child board</param>
        /// <returns></returns>
        public Board GenerateChild(Direction moveDirection)
        {
            Board child = Clone();
            child.MoveEmpty(moveDirection);
            return child;
        }

        public void MoveEmpty(Direction direction)
        {
            (int x, int y) newIndex;
            switch (direction)
            {
                case Direction.Left:
                    newIndex = (EmptyIndex.X - 1, EmptyIndex.Y);
                    break;
                case Direction.Top:
                    newIndex = (EmptyIndex.X, EmptyIndex.Y - 1);
                    break;
                case Direction.Right:
                    newIndex = (EmptyIndex.X + 1, EmptyIndex.Y);
                    break;
                case Direction.Down:
                    newIndex = (EmptyIndex.X, EmptyIndex.Y + 1);
                    break;
                default:
                    throw new NotSupportedException($"Not implemented move direction: {direction}");
            }
            SwapFields(EmptyIndex, newIndex);
            EmptyIndex = newIndex;
            MovesHistory.Add(direction);
        }

        public Board Clone()
        {
            Board clone = new Board(XLength, YLength);
            for (int i = 0; i < YLength; i++)
            {
                for (int j = 0; j < XLength; j++)
                {
                    clone.Fields[i][j] = Fields[i][j];
                }
            }

            clone.EmptyIndex = EmptyIndex;
            clone.MovesHistory.AddRange(MovesHistory);
            return clone;
        }

        private void SwapFields((int x, int y) first, (int x, int y) second)
        {
            int temp = Fields[first.y][first.x];
            Fields[first.y][first.x] = Fields[second.y][second.x];
            Fields[second.y][second.x] = temp;
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
