namespace TicTacToe.Core.Analytics
{
    public class ShortestWinOrTie : IAnalysis
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stats"></param>
        /// <param name="currGameState"></param>
        /// <param name="player"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool TryGetPosition(IEnumerable<AnalysisPosition> stats, List<Move> currGameState, Player player, out int position)
        {
            if (stats == null)
            {
                position = -1;
                return false;
            }

            var shortestWin = stats.OrderBy(x => x.MinMoves).ThenByDescending(x => x.Score).First();

            if (shortestWin.Score >= 0.500)
            {
                position = shortestWin.Pos;
                return true;
            }
            else
            {
                var highestScore = stats.OrderByDescending(x => x.Score).First();

                if (highestScore.Score >= 0.500)
                {
                    position = highestScore.Pos;
                    return true;
                }
                else
                {
                    var mostTies = stats.OrderByDescending(x => x.Ties).First();

                    if (mostTies.Ties > mostTies.Wins && mostTies.Ties > mostTies.Losses)
                    {
                        position = mostTies.Pos;
                        return true;
                    }
                    else
                    {
                        position = -1;
                        return false;
                    }
                }
            }
        }
    }
}
