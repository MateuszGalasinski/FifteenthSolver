using Model;
using System.Collections.Generic;

namespace GameSolvers.Solvers.Bidirectional.Strategies.Base
{
    public interface IStrategy
    {
        HashSet<Board> CheckedBoards { get; }
        int RemainingCount { get; }

        void AddChildren(Board current);
        Board GetNextChild();
        bool HasRemainingChild();
    }
}