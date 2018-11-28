using Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace BoardsGeneration
{
    public class Writer
    {
        public void SaveBoards(IEnumerable<Board> boards)
        {
            int counter = 1;
            Directory.CreateDirectory(Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                "Boards"));
            foreach (var board in boards)
            {
                string filePath = Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                    $@"Boards\{board.YLength}x{board.XLength}_{board.MovesHistory.Count}_{counter}.txt");

                WriteBoard(board, filePath);
                counter++;
            }
        }

        private void WriteBoard(Board board, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"{board.YLength} {board.XLength}");
                for (int i = 0; i < board.YLength; i++)
                {
                    for (int j = 0; j < board.XLength; j++)
                    {
                        writer.Write($"{board.Fields[i][j]} ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}