namespace Minesweeper;

public class Cell
{
    public bool IsOpen { get; set; }
    public bool IsBomb { get; set; }
    public int AdjacentBombCount { get; set; }
}
public class Minefield
{
    private readonly Cell[,] cells = new Cell[5, 5];
    
    private int _numberOfBombs;
    private int _numberOfOpenCells;
    public bool GameSucceeded => _numberOfOpenCells == LayoutX * LayoutY - _numberOfBombs;
    
    public int LayoutX => cells.GetLength(0);
    public int LayoutY => cells.GetLength(1);

    public Minefield()
    {
        for (var x = 0; x < LayoutX; x++)
        {
            for (var y = 0; y < LayoutY; y++)
            {
                cells[x, y] = new Cell();
            }
        }
    }

    public bool IsCellVisited(int x, int y) => cells[x, y].IsOpen;
    public bool IsBomb(int x, int y) => cells[x, y].IsBomb;
    
    public bool IsOutOfBounds(int x, int y) =>
        x < 0 || x >= LayoutX || y < 0 || y >= LayoutY;
    
    public void SetBomb(int x, int y)
    {
        cells[x, y].IsBomb = true;
        _numberOfBombs++;
    }
    
    public void OpenCell(int x, int y)
    {
        cells[x, y].IsOpen = true;
        _numberOfOpenCells++;
    }
    
    public void IncrementAdjacentBombCount(int x, int y)
    {
        cells[x, y].AdjacentBombCount += 1;
    }

    public string GetDisplayValue(int x, int y)
    {
        var bombCount = cells[x, y].AdjacentBombCount;
        return bombCount > 0 ? bombCount.ToString() : IsCellVisited(x,y) ? " " : "?";
            
    }
}
