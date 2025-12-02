namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 02, CodeType.Original)]
public class Day_02_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		string part1 = SolvePart1(input);
		string part2 = SolvePart2(input);

		return (part1, part2);
	}

	private static string SolvePart1(PuzzleInput input)
	{
		ulong invalidIds = 0;
		foreach (string idRange in input.Text.Split(','))
		{
			string[] parts = idRange.Split('-');
			ulong lowerRangeInclusive = ulong.Parse(parts[0]);
			ulong upperRangeInclusive = ulong.Parse(parts[1]);

			for (ulong i = lowerRangeInclusive; i <= upperRangeInclusive; i++)
			{
				string id = i.ToString();
				if (id.Length % 2 != 0)
				{
					continue;
				}

				ReadOnlySpan<char> firstHalf = id.AsSpan()[..(id.Length / 2)];
				ReadOnlySpan<char> secondHalf = id.AsSpan()[(id.Length / 2) ..];

				if (firstHalf.SequenceEqual(secondHalf))
				{
					invalidIds += i;
				}
			}

		}

		return invalidIds.ToString();
	}

	private static string SolvePart2(PuzzleInput input)
	{
		ulong invalidIds = 0;
		foreach (string idRange in input.Text.Split(','))
		{
			string[] parts = idRange.Split('-');
			ulong lowerRangeInclusive = ulong.Parse(parts[0]);
			ulong upperRangeInclusive = ulong.Parse(parts[1]);

			for (ulong i = lowerRangeInclusive; i <= upperRangeInclusive; i++)
			{
				string id = i.ToString();
				ReadOnlySpan<char> idSpan = id.AsSpan();
				
				int invalidSegments = 0;
				for (int factor = 1; factor <= id.Length / 2; factor++)
				{
					if (id.Length % factor != 0)
					{
						continue;
					}

					bool isRepeating = true;
					ReadOnlySpan<char> referenceChunk = idSpan[.. factor];
					for (int chunkIdx = 1; chunkIdx < id.Length / factor; chunkIdx++)
					{
						ReadOnlySpan<char> subChunk = idSpan[(chunkIdx * factor) .. ((chunkIdx + 1) * factor)];
						if (!referenceChunk.SequenceEqual(subChunk))
						{
							isRepeating = false;
							break;
						}
					}

					if (isRepeating)
					{
						invalidSegments++;
					}
				}

				if (invalidSegments >= 1)
				{
					invalidIds += i;
				}
			}

		}

		return invalidIds.ToString();
	}
}
