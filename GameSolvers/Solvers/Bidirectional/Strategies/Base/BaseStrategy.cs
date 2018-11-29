using Model;
using System.Collections.Generic;

namespace GameSolvers.Solvers.Bidirectional.Strategies.Base
{
    public abstract class BaseStrategy
    {
        public HashSet<Board> CheckedBoards { get; private set; } = new HashSet<Board>(new BoardValuesEqualityComparer());
    }
}
