using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int R = int.Parse(inputs[0]); // number of rows.
        int C = int.Parse(inputs[1]); // number of columns.
        int A = int.Parse(inputs[2]); // number of rounds between the time the alarm countdown is activated and the time the alarm goes off.
        var game = new Game();
        // game loop
        while (true)
        {
            game.Map.Clear();
            inputs = Console.ReadLine().Split(' ');
            int KR = int.Parse(inputs[0]); // row where Kirk is located.
            int KC = int.Parse(inputs[1]); // column where Kirk is located.
            for (int i = 0; i < R; i++)
            {
                var row = Console.ReadLine();
                game.Map.Add(row);
                if (row.Contains('T'))
                {
                    game.Location.Y = i;
                    game.Location.X = row.IndexOf('T');
                }
            }

            game.Map.ForEach(r => { Console.Error.WriteLine(r); });

            var direction = "RIGHT";
            if(game.CanMove(direction))
            {
                game.Move(direction);
            }
            else if (game.CanMove("DOWN"))
            {
                direction = "DOWN";
                game.Move(direction);
            }
            else if(game.CanMove("LEFT"))
            {
                direction = "LEFT";
                game.Move(direction);
            }
            else if (game.CanMove("UP"))
            {
                direction = "UP";
                game.Move(direction);
            }
            else
            {
                direction = "WAIT";
            }

            Console.WriteLine(direction);
        }
    }
}
class Game
{
    public List<string> Map { get; set; } = new List<string>();
    public Location Location { get; set; } = new Location();

    public bool CanMove(Location location)
    {
        return Map[location.Y][location.X].Equals('.') || Map[location.Y][location.X].Equals('C');
    }

    public bool CanMove(string direction)
    {
        var location = Location;
        switch (direction)
        {
            case "RIGHT":
                location.X++;
                break;
            case "LEFT":
                location.X--;
                break;
            case "UP":
                location.Y++;
                break;
            case "DOWN":
                location.Y--;
                break;
            default:
                break;
        }
        return CanMove(location);
    }

    public void Move(Location location)
    {
        var somestring = Map[Location.Y];
        var ch = somestring.ToCharArray();
        ch[Location.X] = '.';
        Map[Location.Y] = new string(ch);

        Location = location;

        somestring = Map[Location.Y];
        ch = somestring.ToCharArray();
        ch[Location.X] = 'T';
        Map[Location.Y] = new string(ch);
    }

    public void Move(string direction)
    {
        var location = Location;

        var somestring = Map[Location.Y];
        var ch = somestring.ToCharArray();
        ch[Location.X] = '.';
        Map[Location.Y] = new string(ch);

        switch (direction)
        {
            case "RIGHT":
                location.X++;
                break;
            case "LEFT":
                location.X--;
                break;
            case "UP":
                location.Y++;
                break;
            case "DOWN":
                location.Y--;
                break;
            default:
                break;
        }

        somestring = Map[location.Y];
        ch = somestring.ToCharArray();
        ch[location.X] = 'T';
        Map[location.Y] = new string(ch);
    }
}
class Location
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;

    public override string ToString()
    {
        return "X :" + X + " Y :" + Y;
    }
}