namespace Minesweeper;

public class Minefield
{
    private readonly bool[,] layoutCoordinates = new bool[5, 5];
    private readonly bool[,] openCells = new bool[5, 5];
    private readonly int[,] adjacentBombCount = new int[5, 5];
    
    private int _numberOfBombs;
    private int _numberOfOpenCells;
    public bool GameSucceeded => _numberOfOpenCells == LayoutX * LayoutY - _numberOfBombs;
    
    public int LayoutX => layoutCoordinates.GetLength(0);
    public int LayoutY => layoutCoordinates.GetLength(1);

    public bool IsCellVisited(int x, int y) => openCells[x, y];
    public bool IsBomb(int x, int y) => layoutCoordinates[x, y];
    
    public bool IsOutOfBounds(int x, int y) =>
        x < 0 || x >= LayoutX || y < 0 || y >= LayoutY;
    
    public void SetBomb(int x, int y)
    {
        layoutCoordinates[x, y] = true;
        _numberOfBombs++;
    }
    
    public void OpenCell(int x, int y)
    {
        openCells[x, y] = true;
        _numberOfOpenCells++;
    }
    
    public void IncrementAdjacentBombCount(int x, int y)
    {
        adjacentBombCount[x, y] += 1;
    }

    public string GetDisplayValue(int x, int y)
    {
        var bombCount = adjacentBombCount[x, y];
        return bombCount > 0 ? bombCount.ToString() : IsCellVisited(x,y) ? " " : "?";
            
    }
}
