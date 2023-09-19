using System.Reflection.Metadata;

namespace Minesweeper;

class Minefield
{
    private bool[,] _bombLocations = new bool[5, 5];
    public string[,] mineFieldState = new string[5, 5];
    private string[,] solutionState = new string[5, 5]; 
    private static (int x, int y)[] coords = new (int, int)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

    public void SetBomb(int x, int y)
    {
        _bombLocations[x, y] = true;
    }

    public void SetMineFieldDisplay()
    {
        for (int i = 0; i < mineFieldState.GetLength(0); i++) 
        {
            for(int j = 0; j < mineFieldState.GetLength(1); j++)
            {
                mineFieldState[i, j] = "?";

                if (_bombLocations[i, j] == true)
                {
                    solutionState[i, j] = "X";
                }
                else
                {
                    int numberOfNeighbourBombs = 0;
                    foreach (var item in coords)
                    {
                        int _x = i - item.x;
                        int _y = j - item.y;
                        if (_x < mineFieldState.GetLength(0) && _y < mineFieldState.GetLength(1) && _x >= 0 && _y >= 0) //If not then there is no square to connect
                        {
                            if (_bombLocations[_x, _y] == true)
                            {
                                numberOfNeighbourBombs++;
                            }
                        }
                    }

                    if(numberOfNeighbourBombs > 0)
                    {
                        solutionState[i, j] = numberOfNeighbourBombs.ToString();
                    }
                    else
                    {
                        solutionState[i, j] = " ";
                    }
                }
            }
        }
    }


    public bool FoundBomb(int x, int y)
    {
        if (_bombLocations[x, y] == true)
        {
            return true;
        }
        return false;
    }

    public void UpdateMineFieldDisplay(int x, int y) //Use BFS search to traverse the field
    {

        mineFieldState[x, y] = solutionState[x, y];
        if (solutionState[x, y] == " ")
        {
            foreach (var item in coords)
            {
                int _x = x - item.x;
                int _y = y - item.y;
                if (_x < mineFieldState.GetLength(0) && _y < mineFieldState.GetLength(1) && _x >= 0 && _y >= 0)
                {
                    if (mineFieldState[_x, _y] == "?")
                    {
                        UpdateMineFieldDisplay(_x, _y);
                    }
                    
                }
            }
        }                
    }

    public bool CheckIfAllBombsUncovered()
    {
        for (int i = 0; i < mineFieldState.GetLength(0); i++)
        {
            for (int j = 0; j < mineFieldState.GetLength(1); j++)
            {
                if(mineFieldState[i, j] == "?" && _bombLocations[i, j] == false)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int GetRowLength()
    {
        return mineFieldState.GetLength(0);
    }

    public bool IsTileUncovered(int x, int y)
    {
        if (mineFieldState[x, y] == "?")
        {
            return false;
        }
        return true;
    }
}
