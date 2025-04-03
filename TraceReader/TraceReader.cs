
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Etlx;

namespace TraceReader
{
    internal sealed class TraceReader(string filePath)
    {
        private string _etlxPath = string.Empty;

        public void ReadTrace()
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
                var diagnosticSourceEvents = traceLog.Events
                    .Where(e => e.ProviderName == "Microsoft-Diagnostics-DiagnosticSource")
                    .ToList();

                var activityEvents = diagnosticSourceEvents
                    .Where(e => e.EventName.Contains("Activity"))
                    .ToList();

                Console.WriteLine($"Activity Events Count: {activityEvents.Count}");
                foreach (var traceEvent in activityEvents)
                {
                    Console.WriteLine($"{traceEvent.EventName}, Time: {traceEvent.TimeStamp}, ProcessName:{GetProcessName(traceLog, traceEvent)}");
                }
            }
        }

        private string GetProcessName(TraceLog traceLog, TraceEvent traceEvent)
        {
            var process = traceLog.Processes.FirstOrDefault(p => p.ProcessID == traceEvent.ProcessID);
            if (process != null)
            {
                return process.Name;
            }

            return traceEvent.ProcessName;
        }
    }
}
