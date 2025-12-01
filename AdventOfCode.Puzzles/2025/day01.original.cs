namespace AdventOfCode.Puzzles._2025;

[Puzzle(2025, 01, CodeType.Original)]
public class Day_01_Original : IPuzzle
{
	public (string, string) Solve(PuzzleInput input)
	{
		string part1 = SolvePart1(input);
		string part2 = SolvePart2(input);

		return (part1, part2);
	}

	private static string SolvePart1(PuzzleInput input)
	{
		int value = 50;
		int zeroesHit = 0;
		foreach (string line in input.Lines)
		{
			bool isLeft = line[0] == 'L';
			int count = int.Parse(line[1..]);

			if (isLeft)
				value -= count;
			else
				value += count;

			if (value % 100 == 0)
			{
				zeroesHit++;
			}
		}

		return zeroesHit.ToString();
	}

	private static string SolvePart2(PuzzleInput input)
	{
		int value = 50;
		int zeroesHit = 0;
		foreach (string line in input.Lines)
		{
			int direction = int.Sign(line[0] - 'N');
			int count = int.Parse(line.AsSpan()[1..]);

			int loops = count / 100;
			count %= 100;

			int originalValue = value;
			value += count * direction;

			int valuesHitThisTime = loops;
			switch (value)
			{
				case 0:
					valuesHitThisTime++;
					break;
				case < 0 when originalValue > 0:
					valuesHitThisTime++;
					value = 100 + value;
					break;
				case < 0 when originalValue == 0:
					value = 100 + value;
					break;
				case >= 100:
					valuesHitThisTime++;
					value %= 100;
					break;
			}
			
			zeroesHit += valuesHitThisTime;
		}

		return zeroesHit.ToString();
	}
}
