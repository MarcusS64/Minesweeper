using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTest;

[TestClass]
public class Tests
{
    [TestMethod]
    public void TestMethodOutOfBounds()
    {
        var field = new Minefield();

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.SetMineFieldDisplay();
        string playerGuess = "05";
        Assert.IsFalse(Minesweeper.Minesweeper.CheckInput(field, playerGuess));
    }

    [TestMethod]
    public void TestMethodNotANumber()
    {
        var field = new Minefield();

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.SetMineFieldDisplay();
        string playerGuess = "0a";
        Assert.IsFalse(Minesweeper.Minesweeper.CheckInput(field, playerGuess));
    }

    [TestMethod]
    public void TestMethodSamePositionGuess()
    {
        var field = new Minefield();

        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        field.SetMineFieldDisplay();
        field.UpdateMineFieldDisplay(4, 0);
        string playerGuess = "40";
        Assert.IsFalse(Minesweeper.Minesweeper.CheckInput(field, playerGuess));
    }

}
