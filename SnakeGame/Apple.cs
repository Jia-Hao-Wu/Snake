using System;
using System.Collections.Generic;

public class Apple
{
    public (int x, int y) location;
    public (int height, int width) size;

    private Random r = new Random();

    public Apple(int height, int width) 
    {
        this.size = (height, width);

        randomize();
    }

    public void randomize() 
    {
        this.location = (r.Next(1, size.width-1), r.Next(1, size.height-1));
    }
}

