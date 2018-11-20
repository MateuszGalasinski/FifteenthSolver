using FluentAssertions;
using Model;
using NUnit.Framework;

namespace BoardTests
{
    public class BoardTests
    {
        [Test]
        public void IsSolver_Solved()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3}, 
                new int[] {4, 5, 6},
                new int[] {7, 8, 0},
            });

            board.IsSolved().Should().BeTrue();
        }

        [Test]
        public void IsSolver_NotSolved()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8},
            });

            board.IsSolved().Should().BeFalse();
        }


        [Test]
        public void BlankIndex_InCtor()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {6, 1, 2},
                new int[] {3, 4, 0},
                new int[] {6, 7, 8},
            });

            board.EmptyFieldIndex.Should().BeEquivalentTo((1, 2));
        }

        [Test]
        public void PossibleMoves_RightDownCorner()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {6, 1, 2},
                new int[] {3, 4, 5},
                new int[] {8, 7, 0},
            });

            board.PossibleMoves.Count.Should().Be(2);
            board.PossibleMoves.Should().Contain(Directions.Left);
            board.PossibleMoves.Should().Contain(Directions.Top);
        }

        [Test]
        public void PossibleMoves_LeftTopCorner()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {8, 7, 6},
            });

            board.PossibleMoves.Count.Should().Be(2);
            board.PossibleMoves.Should().Contain(Directions.Down);
            board.PossibleMoves.Should().Contain(Directions.Right);
        }
    }
}
