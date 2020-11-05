using System.Collections.Generic;
using System.Globalization;
using System.Collections;
using System.Drawing;
using System;

public class Solution {
public int ShortestPath(int[][] grid, int k)
{
	int[,] directions = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
	int m = grid.Length;
	int n = grid[0].Length;

	bool[,,] visited = new bool[m, n, k + 1];
	Queue<(int, int, int)> queue = new Queue<(int, int, int)>();
	queue.Enqueue((0, 0, k));
	visited[0, 0, k] = true;
	int shortestPath = 0;

	while (queue.Count > 0)
	{
		int count = queue.Count;

		for (int j = 0; j < count; j++)
		{
			(int x, int y, int obstacles) = queue.Dequeue();

			if (x == m - 1 && y == n - 1)
				return shortestPath;

			for (int i = 0; i < directions.GetLength(0); i++)
			{
				int newx = x + directions[i, 0];
				int newy = y + directions[i, 1];

				if (newx >= 0 && newy >= 0 && newx < m && newy < n && !visited[newx, newy, obstacles])
				{
					if (grid[newx][newy] == 0)
					{
						visited[newx, newy, obstacles] = true;
						queue.Enqueue((newx, newy, obstacles));
					}

					else if (grid[newx][newy] == 1 && obstacles > 0 && !visited[newx, newy, obstacles - 1])
					{                                
						visited[newx, newy, obstacles - 1] = true;
						queue.Enqueue((newx, newy, obstacles - 1));
					}
				}
			}
		}
		shortestPath++;
	}

	return -1;
}
}