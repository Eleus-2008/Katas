using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public static class ChessBoard
    {
        public static Dictionary<int, int> Count(int[][] b)
        {
            var squares = new int[b.GetLength(0)][];
            for (var i = 0; i < squares.Length; i++)
            {
                squares[i] = new int[b.GetLength(0)];
            }

            for (var i = 1; i < b.GetLength(0); i++)
            {
                for (var j = 1; j < b.GetLength(0); j++)
                {
                    if (b[i][j] == 0)
                    {
                        continue;
                    }

                    var minNeighbour = GetMinNeighbourInSquare(squares, i, j);
                    if (minNeighbour >= 2)
                    {
                        squares[i][j] = minNeighbour + 1;
                        continue;
                    }

                    if (GetMinNeighbourInSquare(b, i, j) == 1)
                    {
                        squares[i][j] = 2;
                    }
                }
            }

            var result = new Dictionary<int, int>();
            foreach (var element in squares.SelectMany(arr => arr).Where(x => x != 0))
            {
                var size = element;
                while (size >= 2)
                {
                    if (result.ContainsKey(element))
                    {
                        result[size]++;
                    }
                    else
                    {
                        result.Add(size, 1);
                    }

                    size--;
                }
            }

            return result;

            int GetMinNeighbourInSquare(int[][] arr, int x, int y)
            {
                return Math.Min(Math.Min(arr[x - 1][y], arr[x][y - 1]), arr[x - 1][y - 1]);
            }
        }
    }
}