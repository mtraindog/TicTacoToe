namespace TicTacToe.Core
{
    [Flags]
    public enum Options
    {
        Simulation  = 0b0000, // Run simulation with no display
        Display     = 0b0001, // Run demo or human player game w/display
        Demo        = 0b0001,
        HumanPlayer = 0b0011, // At least one player is human
        OGoesFirst  = 0b0100, // for human vs. human, allows prev winner to go first
    }
}
