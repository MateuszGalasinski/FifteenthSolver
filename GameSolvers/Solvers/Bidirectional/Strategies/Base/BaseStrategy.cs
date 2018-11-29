using Model;
using System.Collections.Generic;

namespace GameSolvers.Solvers.Bidirectional.Strategies.Base
{
    public abstract class BaseStrategy
    {
        public HashSet<Board> CheckedBoards { get; } = new HashSet<Board>(new BoardValuesEqualityComparer());
        public int ProcessedStatesCounter { get; protected set; } = 0;
    }
}
