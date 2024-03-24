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
            public char Symbole;

            public player(string name, char symbol)
            {
                Name = name;
                Symbole = symbol;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Symbole: {Symbole}";
            }
        }
        // Subclass of the abstract class:
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
                InitializeBoard();
            }

            // Method to initialize the board with '#' characters
            private void InitializeBoard()
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
            public void PrintBoard()
            {
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
        }

        static void Main(string[] args)
        {
            // Creating instances of the subclasses( base class player)
            humanPlayer player1 = new humanPlayer("Player X", 'X');
            humanPlayer player2 = new humanPlayer("Player O", 'O');

            Console.WriteLine(player1);
            Console.WriteLine(player2);


            // Create an instance of the Board class
            Board board = new Board();

            // Print out the initial board to the console
            board.PrintBoard();
        }
    }
}
