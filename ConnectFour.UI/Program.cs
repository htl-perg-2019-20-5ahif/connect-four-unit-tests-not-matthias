using ConnectFour.Logic;
using System;

namespace ConnectFour.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();

            // Start the game
            //
            do
            {
                try
                {
                    Console.Write($"[+] Player {board.GetCurrentPlayer()}: ");
                    var column = byte.Parse(Console.ReadLine());

                    board.AddStone(column);
                }
                catch (Exception)
                {
                    Console.WriteLine("[-] Invalid input. Please try again.");
                    continue;
                }

            } while (board.GetGameState() == GameState.NotEnded);

            // Parse the game state
            //
            switch (board.GetGameState())
            {
                case GameState.Draw:
                    Console.WriteLine($"\n[ ] Nobody has won!");
                    break;

                case GameState.PlayerOne:
                    Console.WriteLine($"\n[ ] Player 1 has won!");
                    break;

                case GameState.PlayerTwo:
                    Console.WriteLine($"\n[ ] Player 2 has won!");
                    break;
            }
        }
    }
}
