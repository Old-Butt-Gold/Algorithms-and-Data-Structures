namespace AaDS.Games;

static class Brain
{
	public static void Play()
	{
		(char Letter, string CodeWord)[] array = new[]
		{
			('A', "Alpha"), ('B', "Bravo"), ('C', "Charlie"), ('D', "Delta"),
			('E', "Echo"), ('F', "Foxtrot"), ('G', "Golf"), ('H', "Hotel"),
			('I', "India"), ('J', "Juliett"), ('K', "Kilo"), ('L', "Lima"),
			('M', "Mike"), ('N', "November"), ('O', "Oscar"), ('P', "Papa"),
			('Q', "Quebec"), ('R', "Romeo"), ('S', "Sierra"), ('T', "Tango"),
			('U', "Uniform"), ('V', "Victor"), ('W', "Whiskey"), ('X', "X-ray"),
			('Y', "Yankee"), ('Z', "Zulu"),
		};

		bool returnToMainMenu = false;

		while (true)
		{
			Console.Clear();
			Info();
			WaitStartOfTheGame();

			while (!returnToMainMenu)
			{
				int index = Random.Shared.Next(array.Length);
				Console.Clear();
				Console.WriteLine("\n  What is the NATO phonetic alphabet code word for...\n");
				Console.Write($"  {array[index].Letter}? ");
				string input = Console.ReadLine()!;
				Console.WriteLine();
				Console.WriteLine(input.Trim().ToUpper() == array[index].CodeWord.ToUpper()
					? "  Correct! :)"
					: $"  Incorrect. :( {array[index].Letter} -> {array[index].CodeWord}");
				Console.Write("  Press [enter] to continue or [escape] to exit...");
				WaitStartOfTheGame();
			}
		}

		void WaitStartOfTheGame()
		{
			while (true)
			{
				ConsoleKey key = Console.ReadKey(true).Key;
				if (key is ConsoleKey.Enter)
				{
					returnToMainMenu = false;
					break;
				}

				if (key is ConsoleKey.Escape)
				{
					returnToMainMenu = true;
					Console.Clear();
					Console.WriteLine("Flash Cards was closed.");
					return;
				}
			}
		}

		void Info()
		{
			Console.WriteLine("\n   Flash Cards\n");
			Console.WriteLine("  In this game you will be doing flash card exercises");
			Console.WriteLine("  to help you memorize the NATO phonetic alphabet. The");
			Console.WriteLine("  NATO phonetic alphabet is commonly used during radio");
			Console.WriteLine("  communication in aviation. Each flash card will have");
			Console.WriteLine("  a letter from the English alphabet and you need to");
			Console.WriteLine("  provide the corresponding code word for that letter.\n");
			Console.WriteLine("  |  NATO phonetic alphabet code words");
			Console.WriteLine("  |");
			Console.WriteLine("  |  A -> Alpha      B -> Bravo       C -> Charlie    D -> Delta");
			Console.WriteLine("  |  E -> Echo       F -> Foxtrot     G -> Golf       H -> Hotel");
			Console.WriteLine("  |  I -> India      J -> Juliett     K -> Kilo       L -> Lima");
			Console.WriteLine("  |  M -> Mike       N -> November    O -> Oscar      P -> Papa");
			Console.WriteLine("  |  Q -> Quebec     R -> Romeo       S -> Sierra     T -> Tango");
			Console.WriteLine("  |  U -> Uniform    V -> Victor      W -> Whiskey    X -> X-ray");
			Console.WriteLine("  |  Y -> Yankee     Z -> Zulu\n");
			Console.Write("  Press [enter] to continue or [escape] to quit...");
		}
	}
}