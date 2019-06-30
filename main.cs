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
            }

            if(game.Location == null)
            {
                game.Location = game.GetStartingPosition();
            }

            var direction = "";
            // chercher le C
            if (!game.ControlRoomFinded())
            {
                direction = game.GetDirectionToSearchControlRoomLocation();
            }
            // aller au C
            else if(!game.PassedInControlRoomLocation())
            {
                direction = game.GetDirectionToControlRoomLocation();
            }
            // aller au T
            else
            {
                direction = game.GetDirectionToStartingPosition();
            }
            if(!string.IsNullOrEmpty(direction))
            {
                game.Move(direction);
                game.Map.ForEach(r => { Console.Error.WriteLine(r); });
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
    public Location Location { get; set; } = null;
    public bool ControlRoomPassed { get; set; } = false;

    public bool ControlRoomFinded()
    {
        return GetClassRoomLocation() != null;
    }
    public bool PassedInControlRoomLocation()
    {
        if(IsControlRoom(Location))
        {
            ControlRoomPassed = true;
        }

        return ControlRoomPassed;
    }
    public string GetDirectionToSearchControlRoomLocation()
    {
        var direction = "";
        if (CanMove("RIGHT"))
        {
            direction = "RIGHT";
        }
        else if (CanMove("DOWN"))
        {
            direction = "DOWN";
        }
        else if (CanMove("LEFT"))
        {
            direction = "LEFT";
        }
        else if (CanMove("UP"))
        {
            direction = "UP";
        }

        return direction;
    }
    public string GetDirectionToControlRoomLocation()
    {
        if(GetClassRoomLocation().X < Location.X && CanMove("LEFT"))
        {
            return "LEFT";
        }
        if (GetClassRoomLocation().X > Location.X && CanMove("RIGHT"))
        {
            return "RIGHT";
        }
        if (GetClassRoomLocation().Y < Location.Y && CanMove("DOWN"))
        {
            return "DOWN";
        }
        if (GetClassRoomLocation().Y > Location.Y && CanMove("UP"))
        {
            return "UP";
        }

        return GetDirectionToSearchControlRoomLocation();
    }
    public string GetDirectionToStartingPosition()
    {
        return GetDirectionToSearchControlRoomLocation();
    }

    public Location GetClassRoomLocation()
    {
        for (int i = 0; i < Map.Count(); i++)
        {
            var row = Map[i];
            if (row.Contains('C'))
            {
                return new Location
                {
                    Y = i,
                    X = row.IndexOf('C')
                };
            }
        }
        return null;
    }

    public Location GetStartingPosition()
    {
        for (int i = 0; i < Map.Count(); i++)
        {
            var row = Map[i];
            if (row.Contains('T'))
            {
                return new Location
                {
                    Y = i,
                    X = row.IndexOf('T')
                };
            }
        }
        return null;
    }

    public bool IsHallowSpace(Location location)
    {
        return Map[location.Y][location.X].Equals('.');
    }

    public bool IsControlRoom(Location location)
    {
        return Map[location.Y][location.X].Equals('C');
    }

    public bool IsUnknowCell(Location location)
    {
        return Map[location.Y][location.X].Equals('?');
    }

    public bool CanMove(Location location)
    {
        return IsHallowSpace(location) || IsControlRoom(location);
    }

    public bool CanMove(string direction)
    {
        var location = new Location
        {
            X = Location.X,
            Y = Location.Y
        };
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

    public void Move(string direction)
    {
        switch (direction)
        {
            case "RIGHT":
                Location.X++;
                break;
            case "LEFT":
                Location.X--;
                break;
            case "UP":
                Location.Y++;
                break;
            case "DOWN":
                Location.Y--;
                break;
            default:
                break;
        }
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