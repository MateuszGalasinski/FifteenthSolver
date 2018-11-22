using FluentAssertions;
using Model;
using NUnit.Framework;

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
    }
}
