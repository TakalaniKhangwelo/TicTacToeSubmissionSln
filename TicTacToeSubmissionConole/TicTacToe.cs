using System;
using TicTacToeRendererLib.Enums;
using TicTacToeRendererLib.Renderer;

namespace TicTacToeSubmissionConole
{
    // Main class for the Tic Tac Toe game
    public class TicTacToe
    {
        // Renderer for displaying the game board in the console
        private TicTacToeConsoleRenderer _boardRenderer;
        // Track the current player (either X or O)
        private PlayerEnum currentPlayer;
        // 3x3 game board represented as a nullable array of PlayerEnum
        private PlayerEnum?[,] board;

        // Constructor to initialize the game
        public TicTacToe()
        {
            _boardRenderer = new TicTacToeConsoleRenderer(10, 6); // Initialize the board renderer
            currentPlayer = PlayerEnum.X; // Player X starts the game
            board = new PlayerEnum?[3, 3]; // Create a 3x3 board initialized with null values
            _boardRenderer.Render(); // Render the initial empty board
        }

        // Method to run the main game loop
        public void Run()
        {
            bool gameOver = false; // Flag to track if the game is over

            while (!gameOver) // Loop until the game is over
            {
                // Display the current player's turn
                Console.SetCursorPosition(2, 19);
                Console.Write($"Player {currentPlayer}");

                // Prompt for row input
                Console.SetCursorPosition(2, 20);
                Console.Write("Please Enter Row (0-2): ");
                int row = GetValidInput(0, 2); // Get valid row input

                // Prompt for column input
                Console.SetCursorPosition(2, 22);
                Console.Write("Please Enter Column (0-2): ");
                int column = GetValidInput(0, 2); // Get valid column input

                // Check if the selected cell is empty
                if (board[row, column] == null)
                {
                    // Update the board with the current player's move
                    board[row, column] = currentPlayer;
                    _boardRenderer.AddMove(row, column, currentPlayer, true); // Render the move

                    // Check for a win or a draw
                    if (CheckWin(row, column))
                    {
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine($"Player {currentPlayer} wins!"); // Announce winner
                        gameOver = true; // Set game over flag
                    }
                    else if (IsBoardFull())
                    {
                        Console.SetCursorPosition(2, 24);
                        Console.WriteLine("It's a draw!"); // Announce draw
                        gameOver = true; // Set game over flag
                    }
                    else
                    {
                        // Switch players for the next turn
                        currentPlayer = (currentPlayer == PlayerEnum.X) ? PlayerEnum.O : PlayerEnum.X;
                    }
                }
                else
                {
                    // Prompt the user to try again if the cell is occupied
                    Console.SetCursorPosition(2, 24);
                    Console.WriteLine("That cell is already occupied. Try again.");
                }
            }
        }

        // Helper method to get valid user input
        private int GetValidInput(int min, int max)
        {
            int input;
            // Loop until valid input is provided
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                Console.SetCursorPosition(2, 24);
                Console.Write($"Invalid input. Please enter a number between {min} and {max}: ");
            }
            return input; // Return valid input
        }

        // Method to check if the current move results in a win
        private bool CheckWin(int row, int column)
        {
            // Check if any row contains the winning player's marks
            if (board[row, 0] == currentPlayer && board[row, 1] == currentPlayer && board[row, 2] == currentPlayer)
                return true;

            // Check if any column contains the winning player's marks
            if (board[0, column] == currentPlayer && board[1, column] == currentPlayer && board[2, column] == currentPlayer)
                return true;

            // Check both diagonals for a win
            if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
                (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
                return true;

            return false; // No win found
        }

        // Method to check if the board is full, indicating a draw
        private bool IsBoardFull()
        {
            // Iterate through each cell in the board
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // If any cell is null (empty), the board is not full
                    if (board[i, j] == null)
                        return false;
                }
            }
            return true; // All cells are filled
        }
    }
}

