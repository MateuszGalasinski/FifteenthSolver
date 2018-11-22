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

            board.EmptyIndex.Should().BeEquivalentTo((2, 1));
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


        [Test]
        public void PossibleMoves_MiddleTopRow()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {2, 0, 3},
                new int[] {4, 5, 6},
                new int[] {8, 7, 1},
            });

            board.PossibleMoves.Count.Should().Be(3);
            board.PossibleMoves.Should().Contain(Directions.Left);
            board.PossibleMoves.Should().Contain(Directions.Down);
            board.PossibleMoves.Should().Contain(Directions.Right);
        }

        [Test]
        public void GenerateChild_Right()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 0, 6},
                new int[] {7, 8, 5},
            });

            Board child = board.GenerateChild(Directions.Right);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {1, 2, 3},
                    new int[] {4, 6, 0},
                    new int[] {7, 8, 5},
                },
                board));
        }

        [Test]
        public void GenerateChild_Left()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 0, 6},
                new int[] {7, 8, 5},
            });

            Board child = board.GenerateChild(Directions.Left);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {1, 2, 3},
                    new int[] {0, 4, 6},
                    new int[] {7, 8, 5},
                },
                board));
        }

        [Test]
        public void GenerateChild_Top()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 0, 6},
                new int[] {7, 8, 5},
            });

            Board child = board.GenerateChild(Directions.Top);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {1, 0, 3},
                    new int[] {4, 2, 6},
                    new int[] {7, 8, 5},
                },
                board));
        }

        [Test]
        public void GenerateChild_Down()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 0, 6},
                new int[] {7, 8, 5},
            });

            Board child = board.GenerateChild(Directions.Down);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {1, 2, 3},
                    new int[] {4, 8, 6},
                    new int[] {7, 0, 5},
                },
                board));
        }

        [Test]
        public void GenerateChild_Down_FromTopRightCorner()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {2, 3, 0},
                new int[] {4, 5, 6},
                new int[] {7, 8, 1},
            });

            Board child = board.GenerateChild(Directions.Down);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {2, 3, 6},
                    new int[] {4, 5, 0},
                    new int[] {7, 8, 1},
                },
                board));
        }

        [Test]
        public void GenerateChild_Left_FromTopRightCorner()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {2, 3, 0},
                new int[] {4, 5, 6},
                new int[] {7, 8, 1},
            });

            Board child = board.GenerateChild(Directions.Left);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {2, 0, 3},
                    new int[] {4, 5, 6},
                    new int[] {7, 8, 1},
                },
                board));
        }

        [Test]
        public void GenerateChild_Left_FromRightSide()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {2, 3, 6},
                new int[] {4, 5, 0},
                new int[] {7, 8, 1},
            });

            Board child = board.GenerateChild(Directions.Left);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {2, 3, 6},
                    new int[] {4, 0, 5},
                    new int[] {7, 8, 1},
                },
                board));
        }


        [Test]
        public void GenerateChild_Down_FromRightSide()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {2, 3, 6},
                new int[] {4, 5, 0},
                new int[] {7, 8, 1},
            });

            Board child = board.GenerateChild(Directions.Down);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {2, 3, 6},
                    new int[] {4, 5, 1},
                    new int[] {7, 8, 0},
                },
                board));
        }


        [Test]
        public void GenerateChild_Top_FromRightSide()
        {
            Board board = new Board(3, 3, new int[][]
            {
                new int[] {2, 3, 6},
                new int[] {4, 5, 0},
                new int[] {7, 8, 1},
            });

            Board child = board.GenerateChild(Directions.Top);

            child.Should().NotBeNull();
            child.Should().BeEquivalentTo(new Board(3,
                3,
                new int[][]
                {
                    new int[] {2, 3, 0},
                    new int[] {4, 5, 6},
                    new int[] {7, 8, 1},
                },
                board));
        }
    }
}
