
namespace AaDS.Games;

static class RPS
{
    static Move _playerMove;
    
    static void Play()
    {
        Random random = new Random();
        int wins = 0;
        int draws = 0;
        int losses = 0;
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Rock, Paper, Scissors\n");
            Console.Write("Choose [r]ock, [p]aper, [s]cissors, or [e]xit:");
            GetPlayerMove();
            if (_playerMove == Move.Exit)
                break;
            
            Move computerMove = (Move)random.Next(3);
            Console.WriteLine($"The computer chose {computerMove}.");
            switch (playerMove: _playerMove, computerMove)
            {
                case (Move.Rock, Move.Paper) or
                    (Move.Paper, Move.Scissors) or
                    (Move.Scissors, Move.Rock):
                    Console.WriteLine("You lose.");
                    losses++;
                    break;
                case (Move.Rock, Move.Scissors) or
                    (Move.Paper, Move.Rock) or
                    (Move.Scissors, Move.Paper):
                    Console.WriteLine("You win.");
                    wins++;
                    break;
                default:
                    Console.WriteLine("This game was a draw.");
                    draws++;
                    break;
            }
            Console.WriteLine($"Score: {wins} wins, {losses} losses, {draws} draws");
            Console.WriteLine("Press Enter To Continue...");
            Console.ReadLine();
        }

        static void GetPlayerMove()
        {
            bool isExit;
            do
            {
                isExit = false;
                switch (Console.ReadLine()!.ToLower())
                {
                    case "rock" or "r":
                        _playerMove = Move.Rock;
                        break;
                    case "paper" or "p":
                        _playerMove = Move.Paper;
                        break;
                    case "scissors" or "s":
                        _playerMove = Move.Scissors;
                        break;
                    case "exit" or "e":
                        _playerMove = Move.Exit;
                        return;
                    default:
                        Console.WriteLine("Invalid Input. Try Again...");
                        isExit = true;
                        break;
                }
            } while (isExit);
        }
    }

    enum Move
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2,
        Exit = 3,
    }
}