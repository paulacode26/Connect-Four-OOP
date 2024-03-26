using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Connect_Four_OOP.Program;

namespace Connect_Four_OOP
{
    internal class Program
    {
        //create player abstract class:
        public abstract class player
        {
            public string Name;
            public char Symbol;

            public player(string name, char symbol)
            {
                Name = name;
                Symbol = symbol;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Symbol: {Symbol}";
            }
        }
        // Subclass of the abstract class for human player:
        public class humanPlayer : player
        {
            public humanPlayer(string name, char symbol) : base(name, symbol)
            {

            }

            public override string ToString()
            {
                return base.ToString(); // use the inf set on the base class.
            }
        }
        //Creating Model class: implements intermediate steps and holds information about the game.
        //methods:  Create the board, Validate Win, Validate Draw, Print messages.

        // Model class representing the board:


        // creating Model class implements intermediate steps and holds information about the game
        // method: initialize and create the board (#) the board, validation Win, validation Draw, Print messages
        public class Board
        {
            // Define the dimensions of the board
            private int rows = 6;
            private int columns = 7;

            // Create the 2D array to represent the board
            private char[,] board;

            // Constructor to initialize the board
            public Board()
            {
                board = new char[rows, columns];
                initializeBoard();
            }

            // Method to initialize the board with '#' characters
            public void initializeBoard()
            {
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        board[row, col] = '#';
                    }
                }
            }

            // Method to print the board
            public void printBoard()
            {
                Console.Clear(); //Clear the console screen to remove any previous output create by white loop. and provide a refreshed interface for the next turn.
                                 // The player interacts only with the initial board, updating registers instead of creating new ones.

                Console.WriteLine("Connect 4 Game Development Project:");
                Console.WriteLine("                                  ");
                for (int row = 0; row < rows; row++)
                {
                    Console.Write("| ");
                    for (int col = 0; col < columns; col++)
                    {
                        Console.Write(board[row, col] + "  ");
                    }
                    Console.WriteLine("|");
                }
                Console.WriteLine("| -------------------  |");
                Console.WriteLine("  1  2  3  4  5  6  7   "); // Print column numbers below the board
            }

            //Method to validate if there is a win
            public bool validationWin(int row, int col)
            {
                char symbol = board[row, col];

                // Check for horizontal win
                for (int c = 0; c <= columns - 4; c++)
                {
                    if (board[row, c] == symbol &&
                        board[row, c + 1] == symbol &&
                        board[row, c + 2] == symbol &&
                        board[row, c + 3] == symbol)
                    {
                        printBoard(); //If there is a win print 4 symbols to the board before showing the message.
                        Console.WriteLine($"It is a Connect 4. {currentPlayerTurn.Name} wins!");
                        return true;
                    }
                }

                // Check for vertical win
                for (int r = 0; r <= rows - 4; r++)
                {
                    if (board[r, col] == symbol &&
                        board[r + 1, col] == symbol &&
                        board[r + 2, col] == symbol &&
                        board[r + 3, col] == symbol)
                    {
                        printBoard(); //If there is a win print 4 symbols to the board before showing the message.
                        Console.WriteLine($"It is a Connect 4. {currentPlayerTurn.Name} wins!");
                        return true;
                    }
                }

                // Check for diagonal win (top-left to bottom-right)
                for (int r = 0; r <= rows - 4; r++)
                {
                    for (int c = 0; c <= columns - 4; c++)
                    {
                        if (board[r, c] == symbol &&
                            board[r + 1, c + 1] == symbol &&
                            board[r + 2, c + 2] == symbol &&
                            board[r + 3, c + 3] == symbol)
                        {
                            printBoard(); //If there is a win print 4 symbols to the board before showing the message.
                            Console.WriteLine($"It is a Connect 4. {currentPlayerTurn.Name} wins!");
                            return true;
                        }
                    }
                }

                // Check for diagonal win (bottom-right to top-left)
                for (int r = 3; r < rows; r++)
                {
                    for (int c = 0; c <= columns - 4; c++)
                    {
                        if (board[r, c] == symbol &&
                            board[r - 1, c + 1] == symbol &&
                            board[r - 2, c + 2] == symbol &&
                            board[r - 3, c + 3] == symbol)
                        {
                            printBoard(); //If there is a win print 4 symbols to the board before showing the message.
                            Console.WriteLine($"It is a Connect 4. {currentPlayerTurn.Name} wins!");
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        static void Main(string[] args)
        {
            // Instance of (base class Player)
            humanPlayer player1 = new humanPlayer("Player X", 'X');
            humanPlayer player2 = new humanPlayer("Player O", 'O');

            Console.WriteLine(player1);
            Console.WriteLine(player2);


            // Instance of board class
            Board board = new Board();

            // Iniciar el juego
            board.printBoard();


        }
    }

}