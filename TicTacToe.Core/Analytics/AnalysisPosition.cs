namespace TicTacToe.Core.Analytics
{
    public class AnalysisPosition
    {
        public int Pos { get; set; }
        public int MinMoves { get; set; } = 9;
        public double Wins { get; set; } = 0.0;
        public double Losses { get; set; } = 0.0;
        public double Ties { get; set; } = 0.0;
        public double GamesPlayed => Wins + Losses + Ties;
        public double Score => Wins / (Wins + Losses);
    }
}
