using Data;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;

namespace DataTests
{
    public class LoaderTest
    {
        [Test]
        public void SimpleLoadTest()
        {
            string testFilePath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                @"Resources\testFile.txt");

            Loader loader = new Loader();
            GameData state = loader.LoadState(testFilePath);

            state.XLength.Should().Be(4);
            state.YLength.Should().Be(4);
            state.Board.Should().BeEquivalentTo(new int[][]
            {
                new int[] {0, 1, 2, 3 },
                new int[] {4, 5, 6, 7 },
                new int[] {8, 9, 10, 11 },
                new int[] {12, 13, 14, 15 },
            });
        }
    }
}
