namespace Minesweeper;

public class Minesweeper
{
    private static readonly int[] dx = {-1, 0, 1, 1, 1, 0, -1, -1};
    private static readonly int[] dy = {-1, -1, -1, 0, 1, 1, 1, 0};
    
    static void Main()
    {
        var field = new Minefield();

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        //the mine field should look like this now:
        //  01234
        //4|1X1
        //3|11111
        //2|2211X
        //1|XX111
        //0|X31

        // Game code...
        
        Console.WriteLine("-------");
        PrintGame(field);
        Console.WriteLine("-------");

        do
        {
            Console.WriteLine("Enter coordinates in the form of 'x y'");
            var userInput = Console.ReadLine();
            
            const string splitString = " ";
            if (!ValidateAndParseUserInput(splitString, userInput, out var x, out var y))
            {
                Console.WriteLine("Please enter coordinates in the form of 'x y'");
                continue;
            }

            if (field.IsOutOfBounds(x, y))
            {
                Console.WriteLine("Please enter coordinates between 0 and 4");
                continue;
            }

            if (field.IsCellVisited(x, y))
            {
                Console.WriteLine("Coordinates already visited, please choose other coordinates");
                continue;
            }
            
            if (field.IsBomb(x, y))
            {
                Console.WriteLine("Boooooom! You stepped on a mine!");
                break;
            }

            OpenCell(field, x, y);
              
            Console.WriteLine("-------");
            PrintGame(field);
            Console.WriteLine("-------");

            if (!field.GameSucceeded) 
                continue;
            
            Console.WriteLine("Congratulations! You won the game!");
            break;


        } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
    }

    public static void OpenCell(Minefield field, int x, int y)
    {
        field.OpenCell(x,y);
        
        if(HasAdjacentBombCount(field, x, y))
            return;

        for (var i = 0; i < dx.Length; i++)
        {
            var adjacentX = x + dx[i];
            var adjacentY = y + dy[i];
            
            if(field.IsOutOfBounds(adjacentX, adjacentY) || field.IsCellVisited(adjacentX, adjacentY))
                continue;
            
            OpenCell(field, adjacentX, adjacentY);
        }
    }

    private static bool HasAdjacentBombCount(Minefield field, int x, int y)
    {
        var count = 0;
        for (int i = 0; i < dx.Length; i++)
        {
            var adjacentX = x + dx[i];
            var adjacentY = y + dy[i];

            if (field.IsOutOfBounds(adjacentX, adjacentY))
                continue;

            if (field.IsBomb(adjacentX, adjacentY))
            {
                field.IncrementAdjacentBombCount(x, y);
                count++;
            }
        }

        return count > 0;
    }
    

    public static void PrintGame(Minefield field)
    {
        // Reverse loop to print the field in the correct orientation and add an additional line ( >=0 ) for the x-axis to print the field coordinates
        for (var y = field.LayoutY; y >= 0; y--)
        {
            // Prints the field coordinates
            Console.Write(y == field.LayoutY ? "  " : $"{y}|");
            
            // Prints the field
            for (var x = 0; x < field.LayoutX; x++)
            {
                if (y == field.LayoutY)
                {
                    Console.Write(x);
                    continue;
                }
                
                Console.Write(field.GetDisplayValue(x,y));
            }
            
            Console.WriteLine();
        }
    }

    public static bool ValidateAndParseUserInput(string splitString, string? userInput, out int x, out int y)
    {
        x = 0;
        y = 0;
        
        if (string.IsNullOrWhiteSpace(userInput))
            return false;
        
        var coordinates = userInput.Split(splitString, StringSplitOptions.RemoveEmptyEntries);          
        
        return int.TryParse(coordinates[0], out x) && int.TryParse(coordinates[1], out y);
    }
}