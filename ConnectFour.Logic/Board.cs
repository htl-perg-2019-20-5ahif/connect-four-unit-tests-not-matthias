using System;
using System.Data;
using System.Linq;

namespace ConnectFour.Logic
{
    public class Board
    {
        /// <summary>
        /// [Column, Row]
        /// [0..6, 0..5]
        /// </summary>
        private readonly byte[,] GameBoard = new byte[7, 6];

        /// <summary>
        /// The current player (always modulo 2).
        /// </summary>
        internal int Player = 0;

        /// <summary>
        /// Adds a stone to the specified column. 
        /// </summary>
        /// <param name="column">The column where the stone should be added.</param>
        public void AddStone(byte column)
        {
            if (column > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }

            for (int row = 0; row < 6; row++)
            {
                var cell = GameBoard[column, row];

                if (cell == 0)
                {
                    Player = (Player + 1) % 2;
                    GameBoard[column, row] = (byte)(Player + 1);
                    return;
                }
            }

            throw new InvalidOperationException("Column is full");
        }

        public bool HasGameEnded()
        {
            if (IsBoardFull())
            {
                return true;
            }

            // TODO: Check if player won


            return default;
        }

        public int GetHorizontalWinner()
        {
            var columnLength = GameBoard.GetLength(0);
            var rowLength = GameBoard.GetLength(1);

            for (byte row = 0; row < rowLength; row++)
            {
                //byte lastNumber
                for (byte column = 0; column < columnLength; column++)
                {

                }
            }

            return default;
        }

        public int GetVerticalWinner()
        {
            return default;
        }

        public int GetDiagonalWinner()
        {
            // Column - Row = Delta
            // 
            // 
                
            return default;
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                var maxRow = GameBoard.GetLength(1) - 1;
                if (GameBoard[i, maxRow] == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
