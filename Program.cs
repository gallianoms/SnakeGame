class Program
{
	const char SnakeChar = 'S';
	const char InvalidChar = 'x';
	const char EmptyChar = ' ';

	static void Main(string[] args)
	{
		SnakeGame();

		Console.ReadKey();
	}

	static void SnakeGame()
	{
		Console.Write("Enter the board size: ");
		int boardSize = ValidateBoardSize();

		char?[,] board = InitializeBoard(boardSize);
		PrintBoard(board);

		int snakeRow = boardSize / 2;
		int snakeColumn = boardSize / 2;

		while (true)
		{
			var key = Console.ReadKey(true).Key;
			(snakeRow, snakeColumn) = MoveSnake(key, board, snakeRow, snakeColumn);
			PrintBoard(board);

			if (AllAdjacentPositionsOccupied(board, snakeRow, snakeColumn))
			{
				Console.WriteLine("Game over!");
				break;
			}
		}
	}

	static int ValidateBoardSize()
	{
		int boardSize;

		while (!int.TryParse(Console.ReadLine(), out boardSize) || boardSize <= 0)
		{
			Console.WriteLine("Invalid boardSize. Please enter a positive integer.");
		}

		return boardSize;
	}

	static char?[,] InitializeBoard(int boardSize)
	{
		char?[,] board = new char?[boardSize, boardSize];

		for (int row = 0; row < boardSize; row++)
		{
			for (int column = 0; column < boardSize; column++)
			{
				board[row, column] = EmptyChar;
			}
		}

		int middleSquare = boardSize / 2;
		board[middleSquare, middleSquare] = SnakeChar;

		return board;
	}

	static void PrintBoard(char?[,] board)
	{
		int boardSize = board.GetLength(0);

		Console.Clear();

		for (int row = 0; row < boardSize; row++)
		{
			for (int column = 0; column < boardSize; column++)
			{
				char? cell = board[row, column];
				Console.Write(cell.HasValue ? cell.Value.ToString() : " ");
				Console.Write(" | ");
			}
			Console.WriteLine();
			Console.WriteLine(new string('-', boardSize * 4 - 1));
		}
	}

	static (int, int) MoveSnake(ConsoleKey? key, char?[,] board, int currentRow, int currentColumn)
	{
		int boardSize = board.GetLength(0);
		int newRow = currentRow;
		int newColumn = currentColumn;

		switch (key)
		{
			case ConsoleKey.LeftArrow:
				newColumn--;
				break;
			case ConsoleKey.RightArrow:
				newColumn++;
				break;
			case ConsoleKey.UpArrow:
				newRow--;
				break;
			case ConsoleKey.DownArrow:
				newRow++;
				break;
			default:
				break;
		}

		// Check if the snake is out of board limits
		if (newRow < 0 || newRow >= boardSize || newColumn < 0 || newColumn >= boardSize)
			return (currentRow, currentColumn);

		// Check if new position is occupied
		if (board[newRow, newColumn] == InvalidChar)
			return (currentRow, currentColumn);

		// Move the snake to the new position and invalidate the old position
		board[currentRow, currentColumn] = InvalidChar;
		board[newRow, newColumn] = SnakeChar;

		return (newRow, newColumn);
	}

	static bool AllAdjacentPositionsOccupied(char?[,] board, int snakeRow, int snakeColumn)
	{
		int boardSize = board.GetLength(0);

		// Check if the top adjacent position is occupied
		if (snakeRow - 1 >= 0 && board[snakeRow - 1, snakeColumn] == EmptyChar)
			return false;

		// Check if the bottom adjacent position is occupied
		if (snakeRow + 1 < boardSize && board[snakeRow + 1, snakeColumn] == EmptyChar)
			return false;

		// Check if the left adjacent position is occupied
		if (snakeColumn - 1 >= 0 && board[snakeRow, snakeColumn - 1] == EmptyChar)
			return false;

		// Check if the right adjacent position is occupied
		if (snakeColumn + 1 < boardSize && board[snakeRow, snakeColumn + 1] == EmptyChar)
			return false;

		return true;
	}
}
