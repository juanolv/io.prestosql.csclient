using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class StatementStats
    {
        public string state{ get; set; }
        public bool queued{ get; set; }
        public bool scheduled{ get; set; }
        public int nodes{ get; set; }
        public int totalSplits{ get; set; }
        public int queuedSplits{ get; set; }
        public int runningSplits{ get; set; }
        public int completedSplits{ get; set; }
        public long cpuTimeMillis{ get; set; }
        public long wallTimeMillis{ get; set; }
        public long queuedTimeMillis{ get; set; }
        public long elapsedTimeMillis{ get; set; }
        public long processedRows{ get; set; }
        public long processedBytes{ get; set; }
        public long peakMemoryBytes{ get; set; }
        public long spilledBytes{ get; set; }
        public StageStats rootStage{ get; set; }
    }
}
