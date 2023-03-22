namespace TicTacToe.Core
{
    public static class Config
    {
        static readonly IConfigurationRoot _config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .Build();

        public static AppSettings AppSettings => _config.GetSection("AppSettings").Get<AppSettings>()!;
    }

    public class AppSettings
    {
        public string GameDataPath { get; set; }
        public int GameEndSleepMs { get; set; }
        public int CpuMoveSleepMs { get; set; }
        public bool RunSimulations { get; set; }
    }
}
