using ConnectFour.Logic;
using System;
using Xunit;

namespace ConnectFour.Tests
{
    public class BoardTests
    {
        [Fact]
        public void AddWithInvalidColumnIndex()
        {
            var b = new Board();

            Assert.Throws<ArgumentOutOfRangeException>(() => b.AddStone(7));
        }

        [Fact]
        public void PlayerChangesWhenAddingStone()
        {
            var b = new Board();

            var oldPlayer = b.Player;
            b.AddStone(0);

            // Verify that player has changed
            Assert.NotEqual(oldPlayer, b.Player);
        }

        [Fact]
        public void AddingTooManyStonesToARow()
        {
            var b = new Board();

            for (var i = 0; i < 6; i++)
            {
                b.AddStone(0);
            }

            var oldPlayer = b.Player;
            Assert.Throws<InvalidOperationException>(() => b.AddStone(0));
            Assert.Equal(oldPlayer, b.Player);
        }

        [Fact]
        public void CheckIfGameIsNotOver()
        {
            var b = new Board();

            Assert.False(b.IsGameOver());
        }

        [Fact]
        public void CheckIfBoardIsFull()
        {
            var b = new Board();
            for (byte column = 0; column < 7; column++)
            {
                for (byte row = 0; row < 6; row++)
                {
                    b.AddStone(column);
                }
            }

            Assert.True(b.IsBoardFull());
        }

        [Fact]
        public void CheckIfBoardIsEmpty()
        {
            var b = new Board();

            Assert.False(b.IsBoardFull());
        }

        [Fact]
        public void CheckIfPlayerOneWonHorizontal()
        {
            var b = new Board();

            // Column 1
            b.AddStone(0); // Player 1
            b.AddStone(0); // Player 2

            // Column 2
            b.AddStone(1); // Player 1
            b.AddStone(1); // Player 2

            // Column 3
            b.AddStone(2); // Player 1
            b.AddStone(2); // Player 2

            // Column 4
            b.AddStone(3); // Player 1
            b.AddStone(3); // Player 2

            Assert.True(b.IsTwoDimensionalWinner(1, true));
        }

        [Fact]
        public void CheckIfPlayerOneWonVertical()
        {
            var b = new Board();

            // Column 1
            b.AddStone(0); // Player 1
            b.AddStone(1); // Player 2

            // Column 2
            b.AddStone(0); // Player 1
            b.AddStone(2); // Player 2

            // Column 3
            b.AddStone(0); // Player 1
            b.AddStone(3); // Player 2

            // Column 4
            b.AddStone(0); // Player 1
            b.AddStone(4); // Player 2

            Assert.True(b.IsTwoDimensionalWinner(1, false));
        }

        [Fact]
        public void CheckIfPlayerOneWonLeftToRightDiagonal()
        {
            var b = new Board();

            //       11
            //     7 10
            //   3 6 9
            // 1 2 4 5 8
            // 
            // Odd Numbers: Player One
            // Even Numbers: Player Two
            //
            //       1
            //     1 2
            //   1 2 1
            // 1 2 2 1 2

            // Column 1
            b.AddStone(0); // Player 1

            // Column 2
            b.AddStone(1);  // Player 2
            b.AddStone(1);  // Player 1

            // Column 3
            b.AddStone(2);  // Player 2
            b.AddStone(3);  // Player 1
            b.AddStone(2);  // Player 2
            b.AddStone(2);  // Player 1

            // Column 4
            b.AddStone(4);  // Player 2
            b.AddStone(3);  // Player 1
            b.AddStone(3);  // Player 2
            b.AddStone(3);  // Player 1

            Assert.True(b.IsDiagonalWinner(1, false));
            Assert.False(b.IsDiagonalWinner(1, true));
        }

        [Fact]
        public void CheckIfGetCurrentPlayerChanges()
        {
            var b = new Board();

            var playerOne = b.GetCurrentPlayer();
            Assert.Equal(playerOne, b.GetCurrentPlayer());

            b.AddStone(0);
            var playerTwo = b.GetCurrentPlayer();
            Assert.Equal(playerTwo, b.GetCurrentPlayer());

            Assert.NotEqual(playerOne, playerTwo);
        }

        // TODO: Check for RightToLeft and player two
    }
}
