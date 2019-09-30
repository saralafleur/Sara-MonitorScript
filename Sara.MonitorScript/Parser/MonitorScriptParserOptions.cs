namespace Sara.MonitorScript.Parser
{
    public sealed class MonitorScriptParserOptions
    {
        public static readonly MonitorScriptParserOptions Default = new MonitorScriptParserOptions();
        public static readonly MonitorScriptParserOptions OptionalSemicolons = new MonitorScriptParserOptions { EnforceSemicolons = false };

        public bool AllowRootStatements { get; set; }

        public bool EnforceSemicolons { get; set; }

        public MonitorScriptParserOptions()
        {
            EnforceSemicolons = true;
            AllowRootStatements = true;
        }
    }
}
