namespace AaDS.Games;

static class Simon
{
	static Random random = new();
	static TimeSpan buttonPress = TimeSpan.FromMilliseconds(500);
	static TimeSpan animationDelay = TimeSpan.FromMilliseconds(200);
	static int score = 0;
	static List<Direction> pattern = new();

	static string[] Renders =
	{
		// 0
		@"           ╔══════╗        " + '\n' +
		@"           ║      ║        " + '\n' +
		@"           ╚╗    ╔╝        " + '\n' +
		@"    ╔═══╗   ╚╗  ╔╝   ╔═══╗ " + '\n' +
		@"    ║   ╚═══╗╚══╝╔═══╝   ║ " + '\n' +
		@"    ║       ║    ║       ║ " + '\n' +
		@"    ║   ╔═══╝╔══╗╚═══╗   ║ " + '\n' +
		@"    ╚═══╝   ╔╝  ╚╗   ╚═══╝ " + '\n' +
		@"           ╔╝    ╚╗        " + '\n' +
		@"           ║      ║        " + '\n' +
		@"           ╚══════╝        ",
		// 1
		@"           ╔══════╗        " + '\n' +
		@"           ║██████║        " + '\n' +
		@"           ╚╗████╔╝        " + '\n' +
		@"    ╔═══╗   ╚╗██╔╝   ╔═══╗ " + '\n' +
		@"    ║   ╚═══╗╚══╝╔═══╝   ║ " + '\n' +
		@"    ║       ║    ║       ║ " + '\n' +
		@"    ║   ╔═══╝╔══╗╚═══╗   ║ " + '\n' +
		@"    ╚═══╝   ╔╝  ╚╗   ╚═══╝ " + '\n' +
		@"           ╔╝    ╚╗        " + '\n' +
		@"           ║      ║        " + '\n' +
		@"           ╚══════╝        ",
		// 2
		@"           ╔══════╗        " + '\n' +
		@"           ║      ║        " + '\n' +
		@"           ╚╗    ╔╝        " + '\n' +
		@"    ╔═══╗   ╚╗  ╔╝   ╔═══╗ " + '\n' +
		@"    ║   ╚═══╗╚══╝╔═══╝███║ " + '\n' +
		@"    ║       ║    ║███████║ " + '\n' +
		@"    ║   ╔═══╝╔══╗╚═══╗███║ " + '\n' +
		@"    ╚═══╝   ╔╝  ╚╗   ╚═══╝ " + '\n' +
		@"           ╔╝    ╚╗        " + '\n' +
		@"           ║      ║        " + '\n' +
		@"           ╚══════╝        ",
		// 3
		@"           ╔══════╗        " + '\n' +
		@"           ║      ║        " + '\n' +
		@"           ╚╗    ╔╝        " + '\n' +
		@"    ╔═══╗   ╚╗  ╔╝   ╔═══╗ " + '\n' +
		@"    ║   ╚═══╗╚══╝╔═══╝   ║ " + '\n' +
		@"    ║       ║    ║       ║ " + '\n' +
		@"    ║   ╔═══╝╔══╗╚═══╗   ║ " + '\n' +
		@"    ╚═══╝   ╔╝██╚╗   ╚═══╝ " + '\n' +
		@"           ╔╝████╚╗        " + '\n' +
		@"           ║██████║        " + '\n' +
		@"           ╚══════╝        ",
		// 4
		@"           ╔══════╗        " + '\n' +
		@"           ║      ║        " + '\n' +
		@"           ╚╗    ╔╝        " + '\n' +
		@"    ╔═══╗   ╚╗  ╔╝   ╔═══╗ " + '\n' +
		@"    ║███╚═══╗╚══╝╔═══╝   ║ " + '\n' +
		@"    ║███████║    ║       ║ " + '\n' +
		@"    ║███╔═══╝╔══╗╚═══╗   ║ " + '\n' +
		@"    ╚═══╝   ╔╝  ╚╗   ╚═══╝ " + '\n' +
		@"           ╔╝    ╚╗        " + '\n' +
		@"           ║      ║        " + '\n' +
		@"           ╚══════╝        ",
	};

	public static void Play()
	{
		Console.SetWindowSize(40, 20);
		bool endGame = false;
		Console.Clear();
		Console.CursorVisible = false;
		Clear();
		while (!endGame)
		{
			Thread.Sleep(buttonPress);
			pattern.Add((Direction)random.Next(1, 5));
			AnimateCurrentPattern();
			for (int i = 0; i < pattern.Count; i++)
			{
				GetInput(ref endGame, pattern[i]);
				score++;
				Clear();
				Render(Renders[(int)pattern[i]]);
				Thread.Sleep(buttonPress);
				Clear();
			}
		}

		Console.Clear();
		Console.Write("Game Over. Score: " + score + ".");
		Console.CursorVisible = true;
	}

	static void GetInput(ref bool endGame, Direction pattern)
	{
		bool waitInput = false;
		while (!waitInput)
		{
			waitInput = true;
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.UpArrow:
					if (pattern != Direction.Up)
						endGame = true;
					break;
				case ConsoleKey.RightArrow:
					if (pattern != Direction.Right)
						endGame = true;
					break;
				case ConsoleKey.DownArrow:
					if (pattern != Direction.Down)
						endGame = true;
					break;
				case ConsoleKey.LeftArrow:
					if (pattern != Direction.Left)
						endGame = true;
					break;
				case ConsoleKey.Escape:
					Console.Clear();
					Console.Write("Simon was closed.");
					endGame = true;
					return;
				default:
					waitInput = false;
					break;
			}
		}
	}

	static void Clear()
	{
		Console.SetCursorPosition(0, 0);
		Console.WriteLine("\n    Simon\n");
		Render(Renders[0]);
	}

	static void AnimateCurrentPattern()
	{
		for (int i = 0; i < pattern.Count; i++)
		{
			Clear();
			Render(Renders[(int)pattern[i]]);
			Thread.Sleep(buttonPress);
		}

		Clear();
	}

	static void Render(string @string)
	{
		int x = Console.CursorLeft;
		int startX = x;
		int y = Console.CursorTop;
		int startY = y;
		foreach (char c in @string)
			if (c is '\n')
				Console.SetCursorPosition(x, ++y);
			else
				Console.Write(c);
		Console.SetCursorPosition(startX, startY);
	}

	enum Direction
	{
		Up = 1,
		Right = 2,
		Down = 3,
		Left = 4,
	}
}