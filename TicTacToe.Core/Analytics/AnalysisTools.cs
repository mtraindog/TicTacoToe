namespace TicTacToe.Core.Analytics
{
    public static class AnalysisTools
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currGameState"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static IEnumerable<AnalysisPosition> BuildStats(List<Move> currGameState, Player player)
        {
            var dataFile = File.ReadAllLines(Config.AppSettings.GameDataPath);
            var prevGames = dataFile.Where(gd => gd.StartsWith(string.Join("", currGameState.Select(gs => gs.Pos))));
            if (!prevGames.Any()) return null;

            // find free spaces
            var posStats = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }
                .Where(pos => !currGameState.Select(g => g.Pos).Contains(pos))
                .Select(pos => new AnalysisPosition { Pos = pos })
                .ToArray();

            foreach (var gameData in prevGames)
            {
                var gamePart = gameData.Split(',');
                var prevGame = gamePart[0];
                var prevResult = int.Parse(gamePart[1]);
                var prevGameNextPos = int.Parse(prevGame[currGameState.Count()].ToString());
                var pos = posStats.Where(x => x.Pos == prevGameNextPos)?.FirstOrDefault();

                if (pos != null)
                {
                    if (prevResult == player.PNum)
                    {
                        if (pos.MinMoves > prevGame.Length)
                            pos.MinMoves = prevGame.Length;
                        pos.Wins++;
                    }
                    else if (prevResult == 3)
                    {
                        pos.Ties++;
                        pos.Wins += 0.5;
                        pos.Losses += 0.5;

                    }
                    else pos.Losses++;
                }
            }

            return posStats;
        }
    }
}
