using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest;

[TestClass]
public class Tests
{
    [TestMethod]
    public void GetDisplayName_ShouldReturnBlankSpace_WhenCellIsOpenAndDoesntHaveAdjacentBombCount()
    {
        // Arrange
        var minefield = new Minefield();
        minefield.SetBomb(0,0);
        // Act
        minefield.OpenCell(4,4);
        
        // Assert
        var displayName = minefield.GetDisplayValue(4, 4);
        Assert.AreEqual(" ", displayName);
    }
    
    [TestMethod]
    public void GetDisplayName_ShouldReturnBombCount_WhenCellIsOpenAndHasAdjacentBombCount()
    {
        // Arrange
        var minefield = new Minefield();
        minefield.SetBomb(4,2);
        
        // Act
        Minesweeper.Minesweeper.OpenCell(minefield,4,0);
        
        // Assert
        var displayName = minefield.GetDisplayValue(4, 1);
        Assert.AreEqual("1", displayName);
    }
    
    [TestMethod]
    public void ValidateAndParseUserInput_ShouldReturnTrue_WhenInputIsValid()
    {
        // Arrange
       
        // Act
        var isValid = Minesweeper.Minesweeper.ValidateAndParseUserInput(" ", "4 0", out var x, out var y);
        
        // Assert
        Assert.IsTrue(isValid);
        Assert.AreEqual(4, x);
        Assert.AreEqual(0, y);
    }
    
    [TestMethod]
    public void ValidateAndParseUserInput_ShouldReturnFalse_WhenInputIsInValid()
    {
        // Arrange
       
        // Act
        var isValid = Minesweeper.Minesweeper.ValidateAndParseUserInput(" ", "abc", out var x, out var y);
        
        // Assert
        Assert.IsFalse(isValid);
        Assert.AreEqual(0, x);
        Assert.AreEqual(0, y);
    }
    
    [TestMethod]
    public void IsOutOfBounds_ShouldReturnFalse_WhenCoordinatesAreInBounds()
    {
        // Arrange
        var minefield = new Minefield();
        
        
        // Act
        var isValid = minefield.IsOutOfBounds(2, 2);
        
        // Assert
        Assert.IsFalse(isValid);
    }
    
    [TestMethod]
    public void IsOutOfBounds_ShouldReturnTrue_WhenCoordinatesAreOutOfBounds()
    {
        // Arrange
        var minefield = new Minefield();
        
        // Act
        var isValid = minefield.IsOutOfBounds(10, 20);
        
        // Assert
        Assert.IsTrue(isValid);
    }
}
