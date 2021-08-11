using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class Game
{
    public enum Cell
    {
        Empty = 0,
        Apple,
        Snake,
        Head,
        Border
    }

    public string[] toString = new string[]
        {
            "  ",
            "**",
            "[]",
            "::",
            "{}"
        };

    private Snake snake;

    private Apple apple;

    private int height, width;

    private Cell[,] board;

    private (int x, int y)
        up = (0, -1),
        down = (0, 1),
        right = (1, 0),
        left = (-1, 0);

    public Game(int width, int height)
    {
        this.height = height;
        this.width = width;

        board = new Cell[height, width];

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                if (h == 0 || w == 0 ||
                    h == height - 1 || w == width - 1)
                    board[h, w] = Cell.Border;
                else
                    board[h, w] = Cell.Empty;
            }
        }

        this.snake = new Snake(new[] {
                (height/2, width/2),
                (height/2+1, width/2),
                (height/2+2, width/2)

            }, up);

        apple = new Apple(height, width);
    }

    public int renderBoard(bool additionalTexts)
    {
        if(additionalTexts)
            Console.WriteLine("Press 'P' to pause.");

        board[apple.location.y, apple.location.x] = Cell.Apple;

        //collision check on cell ahead, as well as checks if its game over
        var state = snake.takeStep(board, height, width);

        //Game over
        if (state == -1)
            return -1;

        //re-randomizes apple location
        else if (state == 1)
            apple.randomize();

        //repopulate the board with the snake with new xy positions
        for (int i = 0; i < snake.getBody().Count; i++)
        {
            var t = snake.getBody()[i];

            if (i == 0)
                board[t.y, t.x] = Cell.Head;
            else
                board[t.y, t.x] = Cell.Snake;
        }

        //button events
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo c = Console.ReadKey(true);
            switch (c.Key)
            {
                case ConsoleKey.W:
                    if (snake.direction != down)
                        snake.setDirection(up);
                    break;
                case ConsoleKey.S:
                    if (snake.direction != up)
                        snake.setDirection(down);
                    break;
                case ConsoleKey.D:
                    if (snake.direction != left)
                        snake.setDirection(right);
                    break;
                case ConsoleKey.A:
                    if (snake.direction != right)
                        snake.setDirection(left);
                    break;
                case ConsoleKey.Escape:
                    return -1;
                case ConsoleKey.P:
                    return 1;
                default:
                    break;
            }
        }

        //draw board
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if ((w == 0 && h == 0) || (w == 0 && h == height - 1) ||
                    (w == width - 1 && h == height - 1) || (w == width - 1 && h == 0))
                {
                    Console.Write("+");
                }
                else if (w == 0 || w == width - 1)
                {
                    Console.Write("--");
                }
                else if (h == 0 || h == height - 1)
                {
                    Console.Write("|");
                }
                else
                    Console.Write(toString[(int)board[w, h]]);

            }
            Console.WriteLine();
        }

        if (additionalTexts)
            Console.WriteLine("Direction: " + snake.direction.x + "," + snake.direction.y +
                "\nPoints: " + snake.getBody().Count);
                
        //clear the snake from board
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                if (board[h, w] != Cell.Border)
                    board[h, w] = Cell.Empty;
            }
        }

        return 0;
    }
}
