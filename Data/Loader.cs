﻿using Model;
using System;
using System.IO;

namespace Data
{
    public class Loader
    {
        public Board LoadState(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            string[] lines = File.ReadAllLines(filePath);
            string[] firstLineWords = lines[0].Split(' ');
            byte xLength = byte.Parse(firstLineWords[0]);
            byte yLength = byte.Parse(firstLineWords[1]);

            int[][] board = new int[yLength][];
            for (int i = 0; i < yLength; i++)
            {
                board[i] = new int[xLength];
            }

            for (int i = 0; i < yLength; i++)
            {
                string[] numbers = lines[1 + i].Split(' ');
                for (int j = 0; j < xLength; j++)
                {
                    board[i][j] = Int32.Parse(numbers[j]);
                }
            }
            return new Board(xLength, yLength, board);
        }
    }
}
