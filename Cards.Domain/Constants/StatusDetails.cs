namespace Cards.Domain.Constants
{
    public static class StatusDetails
    {
        public const string ToDo = "ToDo";
        public const string InProgress = "In Progress";
        public const string Done = "Done";

        public static Dictionary<string, string> StatusNameToIdMappings { get; } = new Dictionary<string, string>
        {
            { ToDo, "Todo343d-f8ec-4197-b0b2-f3365f71f2e2" },
            { InProgress, "InProgress643-4e2e-bba7-8ebebb32d606" },
            { Done, "Done83ea-b4c1-4107-a66b-da86fcecf73f" }
        };
    }
}