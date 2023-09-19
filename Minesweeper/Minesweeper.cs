using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Minesweeper;

public class Minesweeper
{
    //private static string playerGuess;
    private static int coordinate;
    private static int[] position = new int[2];
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
        field.SetMineFieldDisplay();
        bool gameOver = false;
        while (!gameOver)
        {
            
            DrawMineFieldState(field);
            string playerGuess;
            do
            {
                Console.WriteLine("Which position do you with to uncover?");
                playerGuess = Console.ReadLine();

            } while (!CheckInput(field, playerGuess));

            if (field.FoundBomb(position[0], position[1]))
            {
                field.UpdateMineFieldDisplay(position[0], position[1]);
                DrawMineFieldState(field);
                Console.WriteLine("You found a bomb! Game over");
                gameOver = true;
            }
            else if(field.CheckIfAllBombsUncovered())
            {                
                Console.WriteLine("All bombs uncovered... Victory!");
                gameOver = true;
            }
            else
            {                
                field.UpdateMineFieldDisplay(position[0], position[1]);
            }

        }       
    }

    public static void DrawMineFieldState(Minefield field)
    {
        int rowIndex = 4;
        Console.WriteLine("The new game state is: ");
        Console.WriteLine("  01234");
        while (rowIndex > -1)
        {
            Console.Write(rowIndex + "|");
            for (int i = 0; i < field.GetRowLength(); i++)
            {
                Console.Write(field.mineFieldState[i, rowIndex]);
            }
            rowIndex--;
            Console.Write("\n");

        }
    }

    public static bool CheckInput(Minefield field, string playerGuess)
    {
        if (playerGuess.Length != 2)
        {
            Console.WriteLine("Not a valid position input. Please input two numbers only! (Example: 40)");
            return false;            
        }
        else
        {
            int index = 0;
            foreach (char item in playerGuess)
            {
                //bool isNumberInput = int.TryParse(item.ToString(), out coordinate);
                if (!int.TryParse(item.ToString(), out coordinate) || coordinate > 4)
                {
                    Console.WriteLine("Not a valid position input. Please input coordinates between 0-4!");
                    return false;
                }
                position[index++] = coordinate;
            }

            if (field.IsTileUncovered(position[0], position[1]))
            {
                Console.WriteLine("Not a valid position input. Tile is already uncovered!");
                return false;
            }
        }
        
        
        return true;
    }
}
