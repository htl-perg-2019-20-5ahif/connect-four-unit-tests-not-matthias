using ConnectFour.Logic;
using System;

namespace ConnectFour.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();

            // Set the current player that set the field. 
            // If the game ends, we know that this player has won.
            // 
            var currentPlayer = board.GetCurrentPlayer();
            
            // Start the game
            //
            do
            {
                try
                {
                    currentPlayer = board.GetCurrentPlayer();
                    Console.Write($"[+] Player {currentPlayer}: ");
                    var column = byte.Parse(Console.ReadLine());

                    board.AddStone(column);
                }
                catch (Exception)
                {
                    Console.WriteLine("[-] Invalid input. Please try again.");
                    continue;
                }

            } while (!board.IsGameOver());

            // Display a message for the winner
            //
            Console.WriteLine($"\n[ ] Player {currentPlayer} has won!");
        }
    }
}
