using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Connect_Four_OOP.Program;

namespace Connect_Four_OOP
{
        //Interface to create the board. It allows changing the numbers of columns and rows easily.
        public interface IBoardInitializer
        {
            void InitializeBoard(int rows, int columns);
            void PrintBoard();
        }

        class PatternGenerator //business logic
        {
            IBoardInitializer p;
            public PatternGenerator(IBoardInitializer obj)
            {
                p = obj;
            }
            public void InitializeBoard(int rows, int columns)
            {
                p.InitializeBoard(rows, columns);
            }
            public void PrintBoard()
            {
                p.PrintBoard();
            }
        }

        //create player abstract class:
        public abstract class Player
        {
            public string Name;
            public char Symbol;

            public Player(string name, char symbol)
            {
                Name = name;
                Symbol = symbol;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Symbol: {Symbol}";
            }
        }

    // The HumanPlayer class Inherits from the Player abstract class to defines the behavior to players, such us their name and symbol.
    public class humanPlayer : Player
        {
            public humanPlayer(string name, char symbol) : base(name, symbol)
            {

            }

            public override string ToString()
            {
                return base.ToString(); // use the inf set on the base class.
            }
        }

    // Controller Class: it handles the inicial menu where users can select options to start the game or exit the program
    public class Menu
    {
        //Indicator if human players were selected.If the player already selected 1 when the game resets, it won't show the initial menu.
        private static bool humanPlayersSelected = false;

        public static void InitialMenu()
        {


            while (true)
            {
                if (!humanPlayersSelected) // Check if human players have not been selected yet
                {
                    Console.WriteLine("Object-Oriented Programming Project 'Connect Four'!\n");
                    Console.WriteLine("Please, Enter your choice:\n");
                    Console.WriteLine("Enter 1 for playing Human VS Human game");
                    Console.WriteLine("Enter 2 for Exit\n");

                    int sel = GetSelection();

                    if (sel == 2)
                    {
                        Console.WriteLine("\nExiting game. Thank you for playing!");
                        break;
                    }
                    else if (sel == 1)
                    {
                        humanPlayersSelected = true; // Set the indicator to true when human players are selected
                        HumanVsHuman(); // Call the method to start the human players game
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input. Please enter either the number  1 or 2.\n");
                    }
                }
                else
                {
                    // If human players are already selected, directly show the board
                    HumanVsHuman();
                }
            }

        }

        // Prompt the user to select an option (1-2) and return the selected option.
        private static int GetSelection()
        {
            while (true)
            {
                Console.Write("Select an option (1-2): ");
                string select = Console.ReadLine();

                if (int.TryParse(select, out int sel))
                {
                    return sel;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a number.\n");
                }
            }
        }

        private static void HumanVsHuman()
        {
            // Create two human players with symbols 'X' and 'O' respectively.
            HumanPlayer player1 = new HumanPlayer("Player X", 'X');
            HumanPlayer player2 = new HumanPlayer("Player O", 'O');

            // Initialize a new board with 6 rows and 7 columns.
            BoardInitializer boardInitializer = new BoardInitializer();
            boardInitializer.InitializeBoard(6, 7);
            boardInitializer.PrintBoard();

            // Initialize a new game with the created players and board.
            GameLogic game = new GameLogic(player1, player2, boardInitializer);

            // Start the game.
            game.StartGame();
        }
    }

    public class BoardInitializer : IBoardInitializer
        {
            public char[,] board;
            private int rows;
            private int columns;

            public void InitializeBoard(int rows, int columns)
            {
                this.rows = rows;
                this.columns = columns;
                board = new char[rows, columns];
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < columns; col++)
                    {
                        board[row, col] = '#';
                    }
                }
            }

            public void PrintBoard()
            {
                Console.Clear();

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

                Console.Write("  ");
                for (int col = 0; col < columns; col++)
                {
                    Console.Write((col + 1) + "  ");
                }
                Console.WriteLine(" ");
            }
        }

        //Creating Model class: implements intermediate steps and holds information about the game.
        //methods: Validate Win, Validate Draw, Print messages.
        public class GameLogic
        {
            public char[,] board;
            public Player player1;
            public Player player2;
            public Player currentPlayerTurn;
            public bool gameOver = false;
            public BoardInitializer boardInitializer;

            public bool GameOver { get { return gameOver; } }

            public GameLogic(Player player1, Player player2, BoardInitializer boardInitializer)
            {
                this.player1 = player1;
                this.player2 = player2;
                currentPlayerTurn = player1;
                this.boardInitializer = boardInitializer;
                this.board = boardInitializer.board;
            }

            public void StartGame()
            {
                do
                {
                    // Print the board and current player's turn
                    boardInitializer.PrintBoard();
                    Console.WriteLine($"It's {currentPlayerTurn.Name}'s turn ({currentPlayerTurn.Symbol}):");

                    // Prompt the player to enter a column number
                    int column = GetColumnNumber();

                    // Handle player input and game logic
                    HandlePlayerMove(column);

                    // Check if the game is over
                    if (GameOver)
                    {
                        // Prompt the player to restart or exit the game
                        PromptRestartOrExit();
                    }
                } while (!GameOver);
            }

            // Method to get the column number from the player
            private int GetColumnNumber()
            {
                while (true)
                {
                    Console.Write("Please enter a column number between 1 and 7: ");
                    if (int.TryParse(Console.ReadLine(), out int column))
                    {
                        if (column < 1 || column > boardInitializer.board.GetLength(1))
                        {
                            Console.WriteLine("Incorrect column number. Please enter a number between 1 and 7.");
                        }
                        else
                        {
                            return column;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect input. Please provide a number between 1 and 7.");
                    }
                }
            }

            // Method to handle player move and game logic
            private void HandlePlayerMove(int column)
            {
                if (!gameOver)
                {
                    int row = GetAvailableRow(column - 1);
                    board[row, column - 1] = currentPlayerTurn.Symbol;

                    // Check if the game is over after the player's move
                    if (validationWin(row, column - 1) || ValidationDraw())
                    {
                        gameOver = true;
                    }

                    // Switch to the next player's turn
                    currentPlayerTurn = (currentPlayerTurn == player1) ? player2 : player1;
                }
            }

            // Method to get the first available row in the selected column
            private int GetAvailableRow(int column)
            {
                int row = boardInitializer.board.GetLength(0) - 1;
                while (row >= 0 && board[row, column] != '#')
                {
                    row--;
                }
                return row;
            }

            // Method to prompt the player to restart or exit the game
            private void PromptRestartOrExit()
            {
                string input;
                do
                {
                    Console.Write("Restart? Yes(1)  No(0): ");
                    input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            RestartGame();
                            break;
                        case "0":
                            Console.WriteLine("\nExiting game. Thank you for playing!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please enter either '1' to restart or '0' to continue.");
                            break;
                    }
                } while (input != "1" && input != "0");
            }

            // Method to restart the game
            private void RestartGame()
            {
                gameOver = true;
                boardInitializer.InitializeBoard(boardInitializer.board.GetLength(0), boardInitializer.board.GetLength(1));
                currentPlayerTurn = player1;
            }


            //Method to validate if there is a win
            public bool validationWin(int row, int col)
            {
                char symbol = board[row, col];
                int rows = boardInitializer.board.GetLength(0);
                int columns = boardInitializer.board.GetLength(1);

                // Check for horizontal win
                for (int c = 0; c <= columns - 4; c++)
                {
                    if (board[row, c] == symbol &&
                        board[row, c + 1] == symbol &&
                        board[row, c + 2] == symbol &&
                        board[row, c + 3] == symbol)
                    {
                        boardInitializer.PrintBoard(); //If there is a win print 4 symbols to the board before showing the message.
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
                        boardInitializer.PrintBoard();
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
                            boardInitializer.PrintBoard();
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
                            boardInitializer.PrintBoard();
                            Console.WriteLine($"It is a Connect 4. {currentPlayerTurn.Name} wins!");
                            return true;
                        }
                    }
                }

                return false;
            }

            //Method to validate is there is a win
            public bool ValidationDraw()
            {
                foreach (var cell in boardInitializer.board)
                {
                    if (cell == '#')
                        return false;
                }
                boardInitializer.PrintBoard();
                Console.WriteLine($"The board is full. There is a Draw");
                return true;
            }
        }
    

    class Program
    { 
    static void Main(string[] args)
        {
            // Instance of (base class Player)
            humanPlayer player1 = new humanPlayer("Player X", 'X');
            humanPlayer player2 = new humanPlayer("Player O", 'O');

            Console.WriteLine(player1);
            Console.WriteLine(player2);


            // Initialize a new board with 6 rows and 7 columns.
            BoardInitializer boardInitializer = new BoardInitializer();
            boardInitializer.InitializeBoard(6, 7);
            boardInitializer.PrintBoard();

        }
    }

}