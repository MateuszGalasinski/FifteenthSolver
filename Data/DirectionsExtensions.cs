using Model;
using System;

namespace Data
{
    public static class DirectionsExtensions
    {
        public static string ToCharacterSign(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return "R";
                case Direction.Down:
                    return "D";
                case Direction.Left:
                    return "L";
                case Direction.Top:
                    return "U";
                default:
                    throw new ArgumentException($"Cannot map direction: {direction} to any character");
            }
        }

        public static Direction ToDirection(this char directionCharacter)
        {
            switch (directionCharacter)
            {
                case 'R':
                    return Direction.Right;
                case 'D':
                    return Direction.Down;
                case 'L':
                    return Direction.Left;
                case 'U':
                    return Direction.Top;
                default:
                    throw new ArgumentException($"Cannot map character: {directionCharacter} to any direction");
            }
        }
    }
}
