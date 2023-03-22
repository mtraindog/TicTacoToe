namespace TicTacToe.Core
{
    public class Move
    {
        readonly Player _p;
        public Player Player => _p;
        public int PNum => _p.PNum;
        public char PChar => _p.PChar;
        public int Pos { get; set; }
        public bool Over { get; set; } = false;
        public Move(Player p) { _p = p; }
        public Move(int pos, Player p) { Pos = pos; _p = p; Over = true; }
    }
}
