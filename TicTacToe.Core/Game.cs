namespace TicTacToe.Core
{
    public class Game
    {
        const char EMPTY_CHAR = ' ';
        readonly Options _opt;
        readonly Player _p1, _p2;
        readonly List<Move> _gameState = new List<Move>();
        public Player CurrentPlayer => _gameState.Count() % 2 == 0 ? _p1 : _p2;
        public bool Win { get; private set; } = false;
        public bool Tie { get; private set; } = false;
        public bool Over => Win || Tie;
        public string Serialize => $"{string.Join("", _gameState.Select(m => m.Pos))},{(Over ? Win ? _gameState.Last().PNum : 3 : 4)}\n";

        /// <summary>
        /// New Game 
        /// </summary>
        /// <param name="playerOneType"></param>
        /// <param name="playerTwoType"></param>
        /// <param name="options"></param>
        public Game(PlayerType playerOneType, PlayerType playerTwoType, Options options)
        {
            _opt = options;
            var p1Char = !_opt.HasFlag(Options.OGoesFirst) ? 'X' : 'O';
            var p2Char = !_opt.HasFlag(Options.OGoesFirst) ? 'O' : 'X';
            _p1 = NewPlayer(playerOneType, 1, p1Char);
            _p2 = NewPlayer(playerTwoType, 2, p2Char);
        }

        /// <summary>
        /// Make a New Player based on specified Player Type
        /// </summary>
        /// <param name="playerType"></param>
        /// <param name="playerNum"></param>
        /// <param name="playerChar"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Player NewPlayer(PlayerType playerType, int playerNum, char playerChar)
        {
            return playerType switch
            {
                PlayerType.CpuRandom => new Cpu<RandomPosition>(playerNum, playerChar),
                PlayerType.CpuShortestWinOrTie => new Cpu<ShortestWinOrTie>(playerNum, playerChar),
                PlayerType.Human => new Human(playerNum, playerChar),
                _ => throw new NotImplementedException()
            };
        }

        /// <summary>
        /// Start the game loop
        /// </summary>
        public void Start()
        {
            DisplayStart();

            while (!Over)
            {
                Move turn;

                do
                {
                    if (_gameState.Any())
                        Console.WriteLine($"\n{string.Join(", ", _gameState.Select(mv => $"{mv.PChar}:{mv.Pos}"))}");
                    turn = CurrentPlayer.Move(_gameState, out var stats);

                    if (_opt.HasFlag(Options.Display) && !(CurrentPlayer is Human))
                        Thread.Sleep(Config.AppSettings.CpuMoveSleepMs);

                } while (!turn.Over);

                _gameState.Add(turn);
                Display();
                CheckGameOver();
            }

            Display();
            SaveGameData();
        }

        /// <summary>
        /// Display instructions for human player
        /// </summary>
        public void DisplayStart()
        {
            if (!_opt.HasFlag(Options.HumanPlayer)) return;
            Console.Clear();

            Console.WriteLine(" 0 | 1 | 2\n--- --- ---\n 3 | 4 | 5\n--- --- ---\n 6 | 7 | 8");
            Console.WriteLine("\nHow to play: Enter the number corresponding to the space you wish to occupy.");
        }

        /// <summary>
        /// Display game state to the console
        /// </summary>
        public void Display()
        {
            if (!_opt.HasFlag(Options.Display)) return; Console.Clear();
            var g = GameStateCharArray();

            // Draw game board
            for (var i = 0; i < 9; i += 3)
            {
                Console.WriteLine($" {g[i]} | {g[i + 1]} | {g[i + 2]}");
                if (i < 5) Console.WriteLine("--- --- ---");
            }

            if (Over) // Display game summary
            {
                Console.WriteLine($"\n{string.Join(", ", _gameState.Select(m => $"{m.PChar}:{m.Pos}"))}");
                Console.WriteLine($"\n{(Win ? _gameState.Last().PChar + " wins!" : "Tie")}");

                if (!_opt.HasFlag(Options.HumanPlayer))
                    Thread.Sleep(Config.AppSettings.GameEndSleepMs);
            }
        }

        /// <summary>
        /// Create char array of current game state
        /// </summary>
        /// <returns></returns>
        char[] GameStateCharArray()
        {
            var g = new string(EMPTY_CHAR, 9).ToArray();
            foreach (var mv in _gameState) g[mv.Pos] = mv.PChar;
            return g;
        }

        /// <summary>
        /// Check game over conditions (win or tie)
        /// </summary>
        public void CheckGameOver()
        {
            var g = GameStateCharArray();
            var w = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 0, 3, 6, 1, 4, 7, 2, 5, 8, 0, 4, 8, 2, 4, 6 };

            for (var i = 0; i < 22; i += 3)
            {
                var check = new[] { g[w[i]], g[w[i + 1]], g[w[i + 2]] };
                Win = !check.Any(x => x == EMPTY_CHAR) && check.All(x => x == check[0]);
                if (Win) return;
            }

            Tie = !Win && !g.Any(x => x == EMPTY_CHAR);
        }

        /// <summary>
        /// Save serialized game to file for analysis
        /// </summary>
        void SaveGameData()
        {
            // Save game data (complete games only)
            var serializedGame = Serialize;

            if (!serializedGame.EndsWith(",4"))
                File.AppendAllText(Config.AppSettings.GameDataPath, serializedGame);

        }
    }
}
