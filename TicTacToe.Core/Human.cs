namespace TicTacToe.Core
{
    public class Human : Player
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerNum"></param>
        /// <param name="playerChar"></param>
        public Human(int playerNum, char playerChar)
            : base(playerNum, playerChar)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gateState"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        public override Move Move(List<Move> gateState, out IEnumerable<AnalysisPosition> stats)
        {
            stats = null;
            var move = new Move(this);
            Console.Write($"\n{PChar}'s turn: ");
            var input = Console.ReadLine();

            // attempt to parse user input as position data
            if (int.TryParse(input.Substring(0, 1), out var pos))
            {
                if (0 <= pos && pos < 9 && !gateState.Any(m => m.Pos == pos)) // check pos is vacant, and in range
                {
                    move.Pos = pos;
                    move.Over = true;
                }
            }

            return move;
        }
    }
}
