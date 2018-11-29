using FluentAssertions;
using Model;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BoardTests
{
    public class BoardValuesEqualityComparerTests
    {
        private BoardValuesEqualityComparer _comparer;

        [SetUp]
        public void Setup()
        {
            _comparer = new BoardValuesEqualityComparer();;
        }

        [Test]
        public void HashCodeShouldEqual()
        {
            Board first = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 0},
            });

            Board second = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 0},
            });

            _comparer.GetHashCode(first).Should().Be(_comparer.GetHashCode(second));
        }

        [Test]
        public void HashCodeNotEqual()
        {
            Board first = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 0},
            });

            Board second = new Board(3, 3, new int[][]
            {
                new int[] {0, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 1},
            });

            _comparer.GetHashCode(first).Should().NotBe(_comparer.GetHashCode(second));
        }

        [Test]
        public void HashCodeNotEqualMirror()
        {
            Board first = new Board(3, 3, new int[][]
            {
                new int[] {0, 8, 7},
                new int[] {6, 5, 4},
                new int[] {3, 2, 1},
            });

            Board second = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 0},
            });

            _comparer.GetHashCode(first).Should().NotBe(_comparer.GetHashCode(second));
        }

        [Test]
        public void HashCodeNotEqualSymmetric()
        {
            Board first = new Board(3, 3, new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 0},
            });

            Board second = new Board(3, 3, new int[][]
            {
                new int[] {0, 1, 2},
                new int[] {3, 4, 5},
                new int[] {6, 7, 8},
            });

            _comparer.GetHashCode(first).Should().NotBe(_comparer.GetHashCode(second));
        }

        [Test]
        public void HashCodeNotEqualRandom()
        {
            int howManyTries = 1_000_000;
            int size = 3;
            for (int i = 0; i < howManyTries; i++)
            {
                Board first = GenerateRandomBoard(size);
                Board second = GenerateRandomBoard(size);

                if (_comparer.Equals(first, second))
                {
                    _comparer.GetHashCode(first).Should().Be(_comparer.GetHashCode(second));
                }
                else
                {
                    _comparer.GetHashCode(first).Should().NotBe(_comparer.GetHashCode(second));
                }
            }
        }

        private Board GenerateRandomBoard(int size)
        {
            Board board = new Board(size, size);
            var values = new List<int>(Enumerable.Range(0, size * size));
            values.Shuffle();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board.Fields[i][j] = values.First();
                    values.RemoveAt(0);
                }
            }

            return board;
        }
    }
}
