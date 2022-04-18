namespace ZToolbox
{
    public class Command
    {
        internal string Query { get; private set; }
        internal Dictionary<string, object> Parameters { get; private set; }
        internal bool IsStoredProcedure { get; private set; }

        public Command(string query, bool isStoredProcedure = false)
        {
            Query = query;
            Parameters = new Dictionary<string, object>();
            IsStoredProcedure = isStoredProcedure;
        }

        public void AddParameter(string parameterName, object? value)
        {
            Parameters.Add(parameterName, value ?? DBNull.Value);
        }

    }
}
