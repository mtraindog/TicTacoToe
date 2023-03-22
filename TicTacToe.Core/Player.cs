namespace TicTacToe.Core
{
    public abstract class Player
    {
        readonly int _pNum;
        public int PNum => _pNum;
        readonly char _pChar;
        public char PChar => _pChar;
        public Player(int pNum, char pChar) { _pNum = pNum; _pChar = pChar; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        public abstract Move Move(List<Move> gameState, out IEnumerable<AnalysisPosition> stats);
    }
}
