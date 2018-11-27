using System;

namespace GameSolvers.Exceptions
{
    public class InvalidBoardException : ApplicationException
    {
        public InvalidBoardException(string message) : base(message)
        {
        }
    }
}
