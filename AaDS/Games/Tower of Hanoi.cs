namespace AaDS.Games;

static class Tower_of_Hanoi
{
    static int disks;
    static DataStructures.Stack.Stack<int>[] stacks;
    static int moves = 0;
    static int? source;
    static State state = State.ChooseSource;

    public static void Play()
    {
        Console.SetWindowSize(50, 25);
        Console.CursorVisible = false;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n  Tower Of Hanoi\n");
            Console.WriteLine("  This is a puzzle game where you need to");
            Console.WriteLine("  move all the disks in the left stack to");
            Console.WriteLine("  the right stack. You can only move one");
            Console.WriteLine("  disk at a time from one stack to another");
            Console.WriteLine("  stack, and you may never place a disk on");
            Console.WriteLine("  top of a smaller disk on the same stack.\n");
            Console.WriteLine("  The more disks, the harder the puzzle.\n");
            Console.WriteLine("  Select the number of disks:");
            Console.WriteLine("  [3] 3 disks");
            Console.WriteLine("  [4] 4 disks");
            Console.WriteLine("  [5] 5 disks");
            Console.WriteLine("  [6] 6 disks");
            Console.WriteLine("  [7] 7 disks");
            Console.WriteLine("  [8] 8 disks");
            Console.WriteLine("  [9] 9 disks");
            Console.WriteLine("  [0] 10 disks");
            Console.WriteLine("  [escape] exit game");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D3: disks = 3; break;
                case ConsoleKey.D4: disks = 4; break;
                case ConsoleKey.D5: disks = 5; break;
                case ConsoleKey.D6: disks = 6; break;
                case ConsoleKey.D7: disks = 7; break;
                case ConsoleKey.D8: disks = 8; break;
                case ConsoleKey.D9: disks = 9; break;
                case ConsoleKey.D0: disks = 10; break;
                case ConsoleKey.Escape: return;
                default: continue;
            }
            break;
        }

        Console.Clear();
        stacks = new DataStructures.Stack.Stack<int>[] { new(), new(), new() };
        for (int i = disks; i > 0; i--)
            stacks[0].Push(i);
        while (stacks[2].Count != disks)
        {
            Render();
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Escape: return;
                case ConsoleKey.D1:
                    HandleStackButtonPress(0);
                    break;
                case ConsoleKey.D2:
                    HandleStackButtonPress(1);
                    break;
                case ConsoleKey.D3:
                    HandleStackButtonPress(2);
                    break;
            }
        }
        state = State.Win;
        Render();
        Console.ReadLine();
    }

    static void HandleStackButtonPress(int stack)
    {
        state = State.ChooseSource;
        if (source is null && stacks[stack].Count > 0)
            source = stack;
        else if (source is not null &&
                 (stacks[stack].Count is 0 || stacks[source.Value].Peek() < stacks[stack].Peek()))
        {
            stacks[stack].Push(stacks[source.Value].Pop());
            source = null;
            moves++;
        }
        else if (source == stack)
            source = null;
        else if (stacks[stack].Count is not 0)
            state = State.InvalidTarget;
    }

    static void Render()
    {
        Console.Clear();
        Console.WriteLine("\n  Tower Of Hanoi\n");
        Console.WriteLine($"  Minimum Moves: {(int)Math.Pow(2, disks) - 1}");
        Console.WriteLine($"\n  Moves: {moves}\n");
        for (int i = disks - 1; i > -1; i--)
        {
            foreach (var stack in stacks)
            {
                if (stack.Count > i)
                {
                    var temp = stack.ToList();
                    temp.Reverse();
                    RenderDisk(temp[i]);
                }
                else
                    RenderDisk(null);
            }

            Console.WriteLine();
        }
        string towerBase = new string('─', disks) + '┴' + new string('─', disks);
        Console.WriteLine($"  {towerBase}  {towerBase}  {towerBase}");
        Console.WriteLine($"  {RenderBelowBase(0)}  {RenderBelowBase(1)}  {RenderBelowBase(2)}\n");
        PrintState(state);
    }

    static void PrintState(State state)
    {
        switch (state)
        {
            case State.ChooseSource:
                Console.WriteLine("  [1], [2], or [3] select source stack");
                Console.Write("  [escape] exit game");
                break;
            case State.InvalidTarget:
                Console.WriteLine("  You may not place a disk on top of a");
                Console.WriteLine("  smaller disk on the same stack.");
                break;
            case State.Win:
                Console.WriteLine("  You solved the puzzle!");
                Console.Write("  [escape] exit game");
                break;
        }
    }

    static string RenderBelowBase(int stack) =>
        new string(stack == source ? '^' : ' ', disks - 1) + $"[{stack + 1}]" +
        new string(stack == source ? '^' : ' ', disks - 1);

    static void RenderDisk(int? disk)
    {
        Console.Write("  ");
        if (disk is null)
            Console.Write(new string(' ', disks) + '│' + new string(' ', disks));
        else
        {
            Console.Write(new string(' ', disks - disk.Value));
            Console.BackgroundColor = disk switch
            {
                1 => ConsoleColor.Red,
                2 => ConsoleColor.Green,
                3 => ConsoleColor.Blue,
                4 => ConsoleColor.Magenta,
                5 => ConsoleColor.Cyan,
                6 => ConsoleColor.DarkYellow,
                7 => ConsoleColor.White,
                8 => ConsoleColor.DarkGray,
                9 => ConsoleColor.DarkMagenta,
                10 => ConsoleColor.DarkBlue,
            };
            Console.Write(new string(' ', disk.Value) + ' ' + new string(' ', disk.Value));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(new string(' ', disks - disk.Value));
        }
    }

    enum State
    {
        ChooseSource,
        InvalidTarget,
        Win,
    }
}