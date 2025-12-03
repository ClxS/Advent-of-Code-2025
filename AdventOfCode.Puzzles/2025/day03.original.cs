namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 03, CodeType.Original)]
public class Day_03_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		string part1 = SolvePart1(input);
		string part2 = SolvePart2(input);

		return ("part1", part2);
	}

	private static string SolvePart1(PuzzleInput input)
	{
		long sum = 0;
		foreach (string bank in input.Lines)
		{
			char minA = '\0';
			char minB = '\0';
			int aIdx = 0;

			ReadOnlySpan<char> chars = bank.AsSpan();
			for (int charIdx = 0; charIdx < chars.Length - 1; charIdx++)
			{
				char c = chars[charIdx];
				if (c > minA)
				{
					minA = c;
					aIdx = charIdx;
				}
			}
			
			for (int charIdx = aIdx + 1; charIdx < chars.Length; charIdx++)
			{
				char c = chars[charIdx];
				if (c > minB)
				{
					minB = c;
				}
			}
			
			int minAValue = minA - '0';
			int minBValue = minB - '0';

			sum += (minAValue * 10) + minBValue;
		}

		return sum.ToString();
	}

	private static string SolvePart2(PuzzleInput input)
	{
		long sum = 0;

		Span<char> numbers = stackalloc char[12];
		foreach (string bank in input.Lines)
		{
			int previousIdx = -1;

			ReadOnlySpan<char> chars = bank.AsSpan();
			for (int batteryIndex = 0; batteryIndex < 12; batteryIndex++)
			{
				numbers[batteryIndex] = '\0';
				for (int charIdx = previousIdx + 1; charIdx < chars.Length -11 + batteryIndex; charIdx++)
				{
					char c = chars[charIdx];
					if (c <= numbers[batteryIndex])
					{
						continue;
					}
					
					numbers[batteryIndex] = c;
					previousIdx = charIdx;
				}
			}

			long mult = 1;
			for (int numIdx = numbers.Length - 1; numIdx >= 0; numIdx--)
			{
				sum += mult * (numbers[numIdx] - '0');
				mult *= 10;
			}
		}

		return sum.ToString();
	}
}
