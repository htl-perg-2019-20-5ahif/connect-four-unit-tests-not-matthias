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
        public void CheckIfBoradIsEmpty()
        {
            var b = new Board();

            Assert.False(b.IsBoardFull());
        }

    }
}
