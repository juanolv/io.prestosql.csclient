using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class StageStats
    {
        public string stageId { get; set; }
        public string state { get; set; }
        public bool done{ get; set; }
        public int nodes{ get; set; }
        public int totalSplits{ get; set; }
        public int queuedSplits{ get; set; }
        public int runningSplits{ get; set; }
        public int completedSplits{ get; set; }
        public long cpuTimeMillis{ get; set; }
        public long wallTimeMillis{ get; set; }
        public long processedRows{ get; set; }
        public long processedBytes{ get; set; }
        public List<StageStats> subStages{ get; set; }
    }
}
