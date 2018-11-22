using Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GameSolvers
{
    public class BFTSolver
    {
        private List<Directions> _searchOrder = new List<Directions>(){Directions.Right, Directions.Down, Directions.Left, Directions.Top};
        private Queue<Board> _solutionsToSearch = new Queue<Board>();
        private HashSet<Board> _generatedBoards = new HashSet<Board>(new BoardValuesEqualityComparer());
        public Board StartBoard { get; private set; }

        public Board Solve(Board board)
        {
            StartBoard = board.Clone();
            Board current = board;
            _generatedBoards.Add(current);
            while (!current.IsSolved())
            {
                foreach (var direction in current.PossibleMoves.OrderBy(d => _searchOrder.IndexOf(d))) //use possible moves in order given in _searchOrder
                {
                    Board newBoard = current.GenerateChild(direction);
                    if (!_generatedBoards.Contains(newBoard))
                    {
                        _generatedBoards.Add(newBoard);
                        _solutionsToSearch.Enqueue(newBoard);
                    }
                }

                current = _solutionsToSearch.Dequeue();
#if DEBUG
                Debug.WriteLine($"current Y, X: {current.EmptyIndex.Y} {current.EmptyIndex.X}");
                for (int i = 0; i < current.YLength; i++)
                {
                    string line = "";
                    for (int j = 0; j < current.XLength; j++)
                    {
                        line += $"{current.Fields[i][j]}";
                    }
                    Debug.WriteLine(line);
                }
            }
#endif
            return current;
        }
    }
}
