using Microsoft.Diagnostics.Tracing.Etlx;

namespace TraceReader
{
    internal sealed class TraceReader(string filePath)
    {
        private string _etlxPath = string.Empty;

        internal void ReadTrace()
        {
            if (filePath.EndsWith(".nettrace"))
            {
                _etlxPath = TraceLog.CreateFromEventPipeDataFile(filePath);
                Console.WriteLine($"Converted to: {_etlxPath}");
            }
            else
            {
                _etlxPath = filePath;
            }

            using (TraceLog traceLog = TraceLog.OpenOrConvert(_etlxPath, new TraceLogOptions { KeepAllEvents = true }))
            {
                var diagnosticSourceActivityEvents = traceLog.Events
                    .Where(e => e.ProviderName == "Microsoft-Diagnostics-DiagnosticSource")
                    .Where(e => e.EventName.Contains("Activity"))
                    .ToList();

                Console.WriteLine($"Activity Events Count: {diagnosticSourceActivityEvents.Count}");
                foreach (var traceEvent in diagnosticSourceActivityEvents)
                {
                    var m =traceEvent.GetDynamicMemberNames();

                    Console.WriteLine($"{traceEvent.EventName}, Time: {traceEvent.TimeStamp}, ProcessName:{traceEvent.ProcessName}");
                }
            }
        }
    }
}
