namespace AaDS.Games;

static class HangMan
{
	static Random Random = new();

	static string[] Renders =
	{
		// 0
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"          ║   " + '\n' +
		@"          ║   " + '\n' +
		@"     ███  ║   " + '\n' +
		@"    ══════╩═══",
		// 1
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"          ║   " + '\n' +
		@"     ███  ║   " + '\n' +
		@"    ══════╩═══",
		// 2
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"     ███  ║   " + '\n' +
		@"    ══════╩═══",
		// 3
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      |\  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"     ███  ║   " + '\n' +
		@"    ══════╩═══",
		// 4
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"     ███  ║   " + '\n' +
		@"    ══════╩═══",
		// 5
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"       \  ║   " + '\n' +
		@"     ███  ║   " + '\n' +
		@"    ══════╩═══",
		// 6
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"     / \  ║   " + '\n' +
		@"     ███  ║   " + '\n' +
		@"    ══════╩═══",
	};

	static string[] DeathAnimation =
	{
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"     / \  ║   " + '\n' +
		@"     ███  ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"     / \  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o>  ║   " + '\n' +
		@"     /|   ║   " + '\n' +
		@"      >\  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"     / \  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"     <o   ║   " + '\n' +
		@"      |\  ║   " + '\n' +
		@"     /<   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"     / \  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o>  ║   " + '\n' +
		@"     /|   ║   " + '\n' +
		@"      >\  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o>  ║   " + '\n' +
		@"     /|   ║   " + '\n' +
		@"      >\  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"     / \  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"     <o   ║   " + '\n' +
		@"      |\  ║   " + '\n' +
		@"     /<   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"     <o   ║   " + '\n' +
		@"      |\  ║   " + '\n' +
		@"     /<   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"     <o   ║   " + '\n' +
		@"      |\  ║   " + '\n' +
		@"     /<   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"     /|\  ║   " + '\n' +
		@"     / \  ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      o   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      |   ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      /   ║   " + '\n' +
		@"      \   ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    |__   ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      .   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    \__   ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@"   ____   ║   " + '\n' +
		@"    ══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@"      .   ║   " + '\n' +
		@"    __    ║   " + '\n' +
		@"   /══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      .   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"    _ '   ║   " + '\n' +
		@"  _/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@"      _   ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@"      .   ║   " + '\n' +
		@"          ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      .   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@"      _   ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@"      .   ║   " + '\n' +
		@"          ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"      .   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@"      _   ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      .   ║   " + '\n' +
		@"          ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      '   ║   " + '\n' +
		@" __/══════╩═══",
		//
		@"      ╔═══╗   " + '\n' +
		@"      |   ║   " + '\n' +
		@"      O   ║   " + '\n' +
		@"          ║   " + '\n' +
		@"          ║   " + '\n' +
		@"      _   ║   " + '\n' +
		@" __/══════╩═══",
	};

	public static void Play()
	{
		if (!File.Exists("Words.txt"))
		{
			Console.WriteLine("Error: Missing \"Words.txt\" embedded resource.");
			Console.WriteLine("Press enter to continue...");
			Console.ReadLine();
			return;
		}

		string[] words = File.ReadAllLines("Words.txt");
		while (true)
		{
			PlayHangman(words);
			Console.WriteLine("Do you want to play again? (y/n): ");
			if (Console.ReadKey().KeyChar != 'y')
				break;
		}
	}

	static void PlayHangman(string[] words)
	{
		Console.CursorVisible = false;
		Console.Clear();
		Console.WriteLine("\n    Hangman\n");

		int incorrectGuesses = 0;
		string randomWord = words[new Random().Next(words.Length)].ToLower();
		char[] revealedChars = new string('_', randomWord.Length).ToCharArray();

		while (incorrectGuesses < Renders.Length && revealedChars.Contains('_'))
		{
			RenderGameState(revealedChars, incorrectGuesses);
			char guess = GetValidGuess();

			bool correctGuess = false;
			for (int i = 0; i < revealedChars.Length; i++)
				if (revealedChars[i] is '_' && randomWord[i] == guess)
				{
					revealedChars[i] = guess;
					correctGuess = true;
				}

			if (!correctGuess)
				incorrectGuesses++;
		}

		if (Renders.Length == incorrectGuesses)
		{
			AnimateDeath();
			Console.WriteLine("\n\n\n\n    Answer: " + randomWord);
			Console.WriteLine("    You lose...");
		}
		else
		{
			RenderGameState(revealedChars, incorrectGuesses);
			Console.WriteLine("\n    You win!");
		}
	}

	static char GetValidGuess()
	{
		while (true)
		{
			char guess = char.ToLower(Console.ReadKey().KeyChar);
			if (guess is >= 'a' and <= 'z')
				return guess;
		}
	}

	static void AnimateDeath()
	{
		foreach (var death in DeathAnimation)
		{
			Console.SetCursorPosition(4, 3);
			Render(death);
			Thread.Sleep(TimeSpan.FromMilliseconds(150));
		}
	}

	static void RenderGameState(char[] revealedChars, int incorrectGuesses)
	{
		if (incorrectGuesses == Renders.Length) return;
		Console.SetCursorPosition(4, 3);
		Console.CursorLeft = 4;
		Render(Renders[incorrectGuesses]);
		Console.Write("\n\n    Guess: ");
		foreach (char c in revealedChars)
			Console.Write(c + " ");
		Console.Write("\nYour letter is ");
	}

	static void Render(string @string)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c == '\n')
			{
				Console.WriteLine();
				Console.SetCursorPosition(x, ++y);
			}
			else
				Console.Write(c);
	}
}