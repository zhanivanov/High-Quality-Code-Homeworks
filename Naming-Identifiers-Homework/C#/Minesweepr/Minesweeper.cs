using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinesweeperGame
{
    public class Minesweeper
    {
        public class GameRanking
        {
            private string playerName;

            private int playerPoints;

            public string PlayerName
            {
                get
                {
                    return playerName;
                }

                set
                {
                    playerName = value;
                }
            }

            public int PlayerPoints
            {
                get
                {
                    return playerPoints;
                }

                set
                {
                    playerPoints = value;
                }
            }

            public GameRanking()
            {
            }

            public GameRanking(string playerName, int playerPoints)
            {
                this.playerName = playerName;
                this.playerPoints = playerPoints;
            }
        }

        private static void Main(string[] args)
        {
            string command = string.Empty;
            char[,] gameField = LoadGameField();
            char[,] gameBombs = LoadBombs();
            int counter = 0;
            bool isBoom = false;
            List<GameRanking> playersWithTheHighestScore = new List<GameRanking>(6);
            int currentRow = 0;
            int currentColumn = 0;
            bool isPaused = true;
            const int maks = 35;
            bool flag2 = false;

            do
            {
                if (isPaused)
                {
                    Console.WriteLine(
                        "Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki."
                        + " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");
                    PrintGameField(gameField);
                    isPaused = false;
                }

                Console.Write("Daj red i kolona : ");
                command = Console.ReadLine().Trim();
                if (command.Length >= 3)
                {
                    if (int.TryParse(command[0].ToString(), out currentRow) && int.TryParse(command[2].ToString(), out currentColumn)
                        && currentRow <= gameField.GetLength(0) && currentColumn <= gameField.GetLength(1))
                    {
                        command = "turn";
                    }
                }

                switch (command)
                {
                    case "top":
                        PrintRanking(playersWithTheHighestScore);
                        break;
                    case "restart":
                        gameField = LoadGameField();
                        gameBombs = LoadBombs();
                        PrintGameField(gameField);
                        isBoom = false;
                        isPaused = false;
                        break;
                    case "exit":
                        Console.WriteLine("4a0, 4a0, 4a0!");
                        break;
                    case "turn":
                        if (gameBombs[currentRow, currentColumn] != '*')
                        {
                            if (gameBombs[currentRow, currentColumn] == '-')
                            {
                                tisinahod(gameField, gameBombs, currentRow, currentColumn);
                                counter++;
                            }

                            if (maks == counter)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                PrintGameField(gameField);
                            }
                        }
                        else
                        {
                            isBoom = true;
                        }

                        break;
                    default:
                        Console.WriteLine("\nGreshka! nevalidna Komanda\n");
                        break;
                }

                if (isBoom)
                {
                    PrintGameField(gameBombs);
                    Console.Write("\nHrrrrrr! Umria gerojski s {0} to4ki. " + "Daj si niknejm: ", broya4);
                    string nickName = Console.ReadLine();
                    GameRanking topScorers = new GameRanking(nickName, counter);
                    if (playersWithTheHighestScore.Count < 5)
                    {
                        playersWithTheHighestScore.Add(topScorers);
                    }
                    else
                    {
                        for (int i = 0; i < playersWithTheHighestScore.Count; i++)
                        {
                            if (playersWithTheHighestScore[i].PlayerPoints < topScorers.PlayerPoints)
                            {
                                playersWithTheHighestScore.Insert(i, topScorers);
                                playersWithTheHighestScore.RemoveAt(playersWithTheHighestScore.Count - 1);
                                break;
                            }
                        }
                    }

                    playersWithTheHighestScore.Sort((GameRanking r1, GameRanking r2) => r2.PlayerName.CompareTo(r1.PlayerName));
                    playersWithTheHighestScore.Sort((GameRanking r1, GameRanking r2) => r2.PlayerPoints.CompareTo(r1.PlayerPoints));
                    PrintRanking(playersWithTheHighestScore);

                    gameField = LoadGameField();
                    gameBombs = LoadBombs();
                    counter = 0;
                    isBoom = false;
                    isPaused = true;
                }

                if (flag2)
                {
                    Console.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    PrintGameField(gameBombs);
                    Console.WriteLine("Daj si imeto, batka: ");
                    string imeee = Console.ReadLine();
                    GameRanking to4kii = new GameRanking(imeee, broya4);
                    playersWithTheHighestScore.Add(to4kii);
                    PrintRanking(playersWithTheHighestScore);
                    gameField = LoadGameField();
                    gameBombs = LoadBombs();
                    counter = 0;
                    flag2 = false;
                    isPaused = true;
                }
            }
            while (command != "exit");
            Console.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            Console.WriteLine("AREEEEEEeeeeeee.");
            Console.Read();
        }

        private static void PrintRanking(List<GameRanking> playerPoints)
        {
            Console.WriteLine("\nTo4KI:");
            if (playerPoints.Count > 0)
            {
                for (int i = 0; i < playerPoints.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} kutii", i + 1, playerPoints[i].PlayerName, playerPoints[i].PlayerPoints);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("prazna klasaciq!\n");
            }
        }

        private static void playerTurn(char[,] gameField, char[,] bombs, int currentRow, int currentColumn)
        {
            char bombsCount = CountMines(bombs, currentRow, currentColumn);
            bombs[currentRow, currentColumn] = bombsCount;
            gameField[currentRow, currentColumn] = bombsCount;
        }

        private static void PrintGameField(char[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < rows; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------\n");
        }

        private static char[,] LoadGameField()
        {
            int boardRows = 5;
            int boardColumns = 10;
            char[,] board = new char[boardRows, boardColumns];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }

        private static char[,] LoadBombs()
        {
            int rows = 5;
            int cols = 10;
            char[,] gameField = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    gameField[i, j] = '-';
                }
            }

            List<int> bombsInField = new List<int>();
            while (bombsInField.Count < 15)
            {
                Random randomValue = new Random();
                int randomPosition = randomValue.Next(50);
                if (!bombsInField.Contains(randomPosition))
                {
                    bombsInField.Add(randomPosition);
                }
            }

            foreach (int bomb in bombsInField)
            {
                int kol = bomb / cols;
                int red = bomb % cols;
                if (red == 0 && bomb != 0)
                {
                    cols--;
                    rows = cols;
                }
                else
                {
                    rows++;
                }

                gameField[cols, rows - 1] = '*';
            }

            return gameField;
        }

        private static void gameFIeldSize(char[,] gameField)
        {
            int col = gameField.GetLength(0);
            int row = gameField.GetLength(1);

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (gameField[i, j] != '*')
                    {
                        char count = CountMines(gameField, i, j);
                        gameField[i, j] = count;
                    }
                }
            }
        }

        private static char CountMines(char[,] gameField, int currentRow, int currentColumn)
        {
            int minesCount = 0;
            int rows = gameField.GetLength(0);
            int cols = gameField.GetLength(1);

            if (currentRow - 1 >= 0)
            {
                if (gameField[currentRow - 1, currentColumn] == '*')
                {
                    minesCount++;
                }
            }

            if (currentRow + 1 < rows)
            {
                if (gameField[currentRow + 1, currentColumn] == '*')
                {
                    minesCount++;
                }
            }

            if (currentColumn - 1 >= 0)
            {
                if (gameField[currentRow, currentColumn - 1] == '*')
                {
                    minesCount++;
                }
            }

            if (currentColumn + 1 < cols)
            {
                if (gameField[currentRow, currentColumn + 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((currentRow - 1 >= 0) && (currentColumn - 1 >= 0))
            {
                if (gameField[currentRow - 1, currentColumn - 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((currentRow - 1 >= 0) && (currentColumn + 1 < cols))
            {
                if (gameField[currentRow - 1, currentColumn + 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((currentRow + 1 < rows) && (currentColumn - 1 >= 0))
            {
                if (gameField[currentRow + 1, currentColumn - 1] == '*')
                {
                    minesCount++;
                }
            }

            if ((currentRow + 1 < rows) && (currentColumn + 1 < cols))
            {
                if (gameField[currentRow + 1, currentColumn + 1] == '*')
                {
                    minesCount++;
                }
            }

            return char.Parse(minesCount.ToString());
        }
    }
}