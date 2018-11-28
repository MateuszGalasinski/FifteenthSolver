using Model;
using System.Collections.Generic;

namespace BoardsGeneration
{
    public class Generator
    {
        private const int MaxRecursionDepth = 7;
        private readonly Board _solvedBoard = new Board(4, 4, new int[][]
        {
            new int[] {1, 2 , 3, 4},
            new int[] {5, 6 , 7, 8},
            new int[] {9, 10 , 11, 12},
            new int[] {13, 14 , 15, 0},
        });
        public HashSet<Board> GeneratedBoards { get; } = new HashSet<Board>(new BoardValuesEqualityComparer());

        public Generator()
        {
            GenerateChildren(_solvedBoard, 0);
        }

        private void GenerateChildren(Board board, int recursionDepth)
        {
            if (recursionDepth >= MaxRecursionDepth)
            {
                return;
            }
            foreach (var move in board.PossibleMoves)
            {
                Board child = board.GenerateChild(move);
                if (!GeneratedBoards.Contains(child))
                {
                    GeneratedBoards.Add(child);
                    GenerateChildren(child, recursionDepth + 1);
                }
            }
        }
    }
}
