namespace TicTacToe.Core
{
    public class Cpu<TAnalysis> : Player
        where TAnalysis : IAnalysis
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerNum"></param>
        /// <param name="playerChar"></param>
        public Cpu(int playerNum, char playerChar)
            : base(playerNum, playerChar)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currGameState"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Move RandomMove(List<Move> currGameState, Player player)
        {
            // find free spaces
            var free = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }
                .Where(pos => !currGameState.Select(g => g.Pos).Contains(pos))
                .ToArray();

            // select a random free space
            var seed = Guid.NewGuid().GetHashCode();
            var rPos = new System.Random(seed).Next(0, free.Count());
            return new Move(free[rPos], player);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        public override Move Move(List<Move> gameState, out IEnumerable<AnalysisPosition> stats)
        {
            stats = null;

            // First move, random
            if (!gameState.Any())
                return RandomMove(gameState, this);

            // instantiate the analysis 
            var analysis = (TAnalysis)Activator.CreateInstance(typeof(TAnalysis));

            if (analysis is RandomPosition)
                return RandomMove(gameState, this);

            stats = AnalysisTools.BuildStats(gameState, this);

            if (analysis.TryGetPosition(stats, gameState, this, out var pos))
                return new Move(pos, this);
            else
                return RandomMove(gameState, this);
        }
    }
}
