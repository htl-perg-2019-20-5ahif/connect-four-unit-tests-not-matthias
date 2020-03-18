using System;
using System.Collections.Generic;
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
        /// The current player (always modulo 2). Is either be 0 or 1.
        /// It'll be stored as 1 and 2 in the GameBoard.
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
                    GameBoard[column, row] = (byte)(Player + 1);
                    Player = (Player + 1) % 2;
                    return;
                }
            }

            throw new InvalidOperationException("Column is full");
        }

        /// <summary>
        /// Checks whether the game is over.
        /// </summary>
        /// <returns></returns>
        public bool IsGameOver()
        {
            var playerOneWon = IsTwoDimensionalWinner(1, true)  || IsTwoDimensionalWinner(1, false) || IsDiagonalWinner(1, true) || IsDiagonalWinner(1, false);
            var playerTwoWon = IsTwoDimensionalWinner(2, true)  || IsTwoDimensionalWinner(2, false) || IsDiagonalWinner(2, true) || IsDiagonalWinner(2, false);

            return IsBoardFull() || playerOneWon || playerTwoWon;
        }

        /// <summary>
        /// Checks the horizontal and vertical dimension if the player has won.
        /// </summary>
        /// <param name="player">The player number. Either 1 or 2.</param>
        /// <param name="horizontal">True = Horizontal, False = Vertical</param>
        /// <returns>True if the player has won</returns>
        public bool IsTwoDimensionalWinner(int player, bool horizontal)
        {
            // [Column, Row] Output: 
            // 1 2 0 0 0 0
            // 1 2 0 0 0 0
            // 1 2 0 0 0 0
            // 1 2 0 0 0 0
            // 0 0 0 0 0 0
            // 0 0 0 0 0 0
            // 0 0 0 0 0 0
            //
            // [Row, Column] Output: 
            // 1 1 1 1 0 0 0
            // 2 2 2 2 0 0 0
            // 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0
            // 0 0 0 0 0 0 0
            //
            // So we can use this, to caclulate the cound of both in one function.


            // Find the correct length
            // 
            int xLength = GameBoard.GetLength(0);
            int yLength = GameBoard.GetLength(1);

            if (horizontal)
            {
                xLength = GameBoard.GetLength(1);
                yLength = GameBoard.GetLength(0);
            }

            var count = 0;
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    // Get the correct field
                    // 
                    int field = horizontal ? GameBoard[y, x] : GameBoard[x, y];

                    // Determine the player
                    //
                    if (field == player)
                    {
                        count += 1;
                    }
                }

                // Check if a player has won.
                //
                if (count == 4)
                {
                    return true;
                }

                count = 0;
            }

            return false;
        }

        /// <summary>
        /// Checks if the player has won with 4 diagonal stones.
        /// </summary>
        /// <param name="player">The player number. Either 1 or 2.</param>
        /// <param name="rightToLeft">True = Right to Left, False = Left to Right</param>
        /// <returns>True if the player has won</returns>
        public bool IsDiagonalWinner(int player, bool rightToLeft)
        {
            // There's two diagonals: 
            // - Left -> Right
            // - Right -> Left
            //
            // We can calculate the delta of the x and y and compare it:
            // - Left -> Right: Column - Row = delta
            // - Right -> Left: Column + Row = delta

            // Map with the following data: 
            // [key]: delta
            // [value]: count of the fields that have the same delta
            var map = new Dictionary<int, int>();

            for (int column = 0; column < GameBoard.GetLength(0); column++)
            {
                for (int row = 0; row < GameBoard.GetLength(1); row++)
                {
                    var field = GameBoard[column, row];

                    int delta = rightToLeft ? column + row : column - row;

                    // Determine the player
                    //
                    if (field == player)
                    {
                        AddOrCreate(map, delta, 1);
                    }
                }
            }

            // Check whether a diagonal has been found.
            //
            return map.Any(pair => pair.Value == 4);
        }

        /// <summary>
        /// Checks, whether the board is full. 
        /// </summary>
        /// <returns>True if it's full, False if it's empty</returns>
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

        /// <summary>
        /// Adds the specified amount to the map item. If the item doesn't exit, it'll get created.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="amount"></param>
        public void AddOrCreate(Dictionary<int, int> map, int key, int amount)
        {
            if (!map.ContainsKey(key))
            {
                map.Add(key, amount);
            }
            else
            {
                map[key] += amount;
            }
        }

        /// <summary>
        /// Returns the current player, that can add a stone.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPlayer()
        {
            return Player + 1;
        }

        public void Print()
        {
            for (int column = 0; column < GameBoard.GetLength(0); column++)
            {
                for (int row = 0; row < GameBoard.GetLength(1); row++)
                {
                    var field = GameBoard[column, row];

                    Console.Write(field + " ");
                }

                Console.WriteLine();
            }
        }

        public void PrintReverse()
        {
            for (int row = 0; row < GameBoard.GetLength(1); row++)
            {
                for (int column = 0; column < GameBoard.GetLength(0); column++)
                {
                    var field = GameBoard[column, row];

                    Console.Write(field + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
