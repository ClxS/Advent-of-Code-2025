using CommunityToolkit.HighPerformance;

namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 04, CodeType.Original)]
public class Day_04_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		string part1 = SolvePart1(input);
		string part2 = SolvePart2(input);

		return (part1, part2);
	}

	private static string SolvePart1(PuzzleInput input)
	{
		int accessable = 0;
		
		int rowHeight = input.Lines.Length;
		int rowWidth = input.Lines[0].Length;
		for (int y = 0; y < rowHeight; y++)
		{
			for (int x = 0; x < rowWidth; x++)
			{
				if (input.Lines[y][x] != '@')
				{
					continue;
				}

				int neighbours = 0;
				for (int yOffset = -1; yOffset <= 1; yOffset++)
				{
					for (int xOffset = -1; xOffset <= 1; xOffset++)
					{
						if (xOffset == 0 && yOffset == 0)
						{
							continue;
						}

						if (x + xOffset < 0 || x + xOffset >= rowWidth)
						{
							continue;
						}

						if (y + yOffset < 0 || y + yOffset >= rowHeight)
						{
							continue;
						}

						if (input.Lines[y + yOffset][x + xOffset] == '@')
						{
							neighbours++;
						}
					}
				}

				if (neighbours < 4)
				{
					accessable++;
				}
			}
		}
		
		return accessable.ToString();
	}

	private static unsafe void UnsafeStringMutate(string s, int i, char c)
	{
		fixed (char* p = s) p[i] = c;
	}

	private static string SolvePart2(PuzzleInput input)
	{
		int accessable = 0;

		List<(int X, int Y)> pendingRemovals = [];
		
		int rowHeight = input.Lines.Length;
		int rowWidth = input.Lines[0].Length;

		Span2D<byte> grid = new(input.Bytes, 0, rowHeight, rowWidth, 1);

		byte paperByte = 64;

		bool hadRemovals;
		do
		{
			hadRemovals = false;
			for (int y = 0; y < rowHeight; y++)
			{
				for (int x = 0; x < rowWidth; x++)
				{
					if (grid[y,x] != paperByte)
					{
						continue;
					}

					int neighbours = 0;
					for (int yOffset = -1; yOffset <= 1; yOffset++)
					{
						for (int xOffset = -1; xOffset <= 1; xOffset++)
						{
							if (xOffset == 0 && yOffset == 0)
							{
								continue;
							}

							if (x + xOffset < 0 || x + xOffset >= rowWidth)
							{
								continue;
							}

							if (y + yOffset < 0 || y + yOffset >= rowHeight)
							{
								continue;
							}

							if (grid[y + yOffset,x + xOffset] == paperByte)
							{
								neighbours++;
							}
						}
					}

					if (neighbours < 4)
					{
						pendingRemovals.Add((x, y));
						accessable++;
					}
				}
			}

			if (pendingRemovals.Count > 0)
			{
				hadRemovals = true;
				foreach ((int x, int y) in pendingRemovals)
				{
					grid[y,x] = 0;
				}
				
				pendingRemovals.Clear();
			}

		} while (hadRemovals);

		return accessable.ToString();
	}
}
