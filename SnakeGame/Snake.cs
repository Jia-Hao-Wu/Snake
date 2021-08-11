using System;
using System.Collections.Generic;

public class Snake
{
    private List<(int x, int y)> body;

    public (int x, int y) direction;

    public Snake((int x, int y)[] body, (int x, int y) direction)
    {
        this.body = new List<(int x, int y)>(body);

        this.direction = direction;
    }

    public int takeStep(Game.Cell[,] board, int height, int width)
    {
        int resultState = 0;

        var tmp = body[0];
        (int x, int y) tmp1;

        for (int i = 0; i < body.Count; i++)
        {
            //the snake head steps forward
            if (i == 0)
            {
                body[i] = addition(body[i], direction);

                //collisions
                var newPosition = body[i];

                //Check if there are more than 1 same coordinates which is game over
                if (body.FindAll(x => newPosition.x == x.x && newPosition.y == x.y).Count > 1)
                    return -1;

                var boardCollision = board[newPosition.y, newPosition.x];

                if (boardCollision == Game.Cell.Border)
                {
                    //check which border the snake has collided on

                    //left
                    if (newPosition.x == 0)
                        body[i] = (width - 2, newPosition.y);

                    //right
                    else if (newPosition.x == width - 1)
                        body[i] = (0, newPosition.y);

                    //top
                    else if (newPosition.y == 0)
                        body[i] = (newPosition.x, height - 2);

                    //bottom
                    else if (newPosition.y == height - 1)
                        body[i] = (newPosition.x, 0);
                }
                else if (boardCollision == Game.Cell.Apple)
                {
                    body.Add(body[body.Count - 1]);
                    resultState = 1;
                }
            }

            //the body follows the previous head steps
            else
            {
                tmp1 = body[i];
                body[i] = tmp;
                tmp = tmp1;
            }
        }
        return resultState;
    }

    public void setDirection((int x, int y) direction)
    {

        this.direction = direction;
    }

    public List<(int x, int y)> getBody()
    {
        return body;
    }

    (int x, int y) multiply((int x, int y) i, (int x, int y) j)
    {
        return (i.x * j.y, i.y * j.y);
    }

    (int x, int y) addition((int x, int y) i, (int x, int y) j)
    {
        return (i.x + j.x, i.y + j.y);
    }
}

