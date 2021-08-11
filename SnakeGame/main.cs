using System;
using System.Threading;

public class main
{
    static void Main(string[] args)
    {
        Game game = new Game(20, 20);

        paused(game, "Press Enter to Play");

        while (true)
        {
            Console.Clear();
            int state = game.renderBoard(true);

            if (state == -1)
                break;

            else if (state == 1)
                paused(game, "Game, Paused. Press Enter to continue.");

            Thread.Sleep(100);
        }

        game.renderBoard(false);
        Console.WriteLine("Game Over!");
    }

    static void paused(Game game, string message) 
    {
        game.renderBoard(false);
        Console.WriteLine(message);
        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo c = Console.ReadKey(true);
                if (c.Key == ConsoleKey.Enter)
                    break;
            }
        }
    }
}

