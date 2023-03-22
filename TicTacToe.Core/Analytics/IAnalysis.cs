namespace TicTacToe.Core.Analytics
{
    public interface IAnalysis
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stats"></param>
        /// <param name="currGameState"></param>
        /// <param name="player"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        bool TryGetPosition(IEnumerable<AnalysisPosition> stats, List<Move> currGameState, Player player, out int position);
    }
}
