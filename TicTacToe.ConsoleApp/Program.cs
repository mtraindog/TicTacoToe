namespace TicTacToe
{
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (Config.AppSettings.RunSimulations)
            {
                Console.WriteLine("Running simulations...");
                RunSimulation(100, () => new Game(PlayerType.CpuRandom, PlayerType.CpuRandom, Options.Simulation));
                RunSimulation(10, () => new Game(PlayerType.CpuRandom, PlayerType.CpuShortestWinOrTie, Options.Simulation));
                RunSimulation(10, () => new Game(PlayerType.CpuShortestWinOrTie, PlayerType.CpuRandom, Options.Simulation));
                RunSimulation(10, () => new Game(PlayerType.CpuShortestWinOrTie, PlayerType.CpuShortestWinOrTie, Options.Simulation));
            }

            // Demo learning cpu vs. learning cpu
            for (var i = 0; i < 2; i++)
                new Game(PlayerType.CpuShortestWinOrTie, PlayerType.CpuShortestWinOrTie, Options.Demo).Start();

            // Single player game humn vs. learning cpu
            new Game(PlayerType.Human, PlayerType.CpuShortestWinOrTie, Options.HumanPlayer).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numGames"></param>
        /// <param name="makeNewGame"></param>
        static void RunSimulation(int numGames, Func<Game> makeNewGame)
        {
            for (var i = 0; i < numGames; i++)
                makeNewGame().Start();
        }
    }
}