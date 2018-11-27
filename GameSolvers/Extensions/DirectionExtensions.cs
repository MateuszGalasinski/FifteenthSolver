using Model;
using System;

namespace GameSolvers.Extensions
{
    public static class DirectionExtensions
    {
        public static Direction OppositeDirection(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Down:
                    return Direction.Top;
                case Direction.Top:
                    return Direction.Down;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Right;
                default:
                    throw new ArgumentException("Unknown direction");
            }
        }
    }
}
