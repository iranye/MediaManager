namespace MediaManager.API.Helpers
{
    public static class GetOptions
    {
        public static string GetOption(IConfiguration configuration, string setting, string? section = null)
        {
            var option = Environment.GetEnvironmentVariable($"{setting}");
            if (String.IsNullOrWhiteSpace(option))
            {
                option = String.IsNullOrWhiteSpace(section) ? configuration[setting] : configuration[$"{section}:{setting}"];
            }

            return option;
        }
    }
}
