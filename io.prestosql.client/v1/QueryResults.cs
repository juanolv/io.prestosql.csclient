using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class QueryResults
    {
        public String id{ get; set; }
        public Uri infoUri{ get; set; }
        public Uri partialCancelUri{ get; set; }
        public Uri nextUri{ get; set; }
        public List<Column> columns{ get; set; }
        public object[][] data{ get; set; }
        public StatementStats stats{ get; set; }
        public QueryError error{ get; set; }
        public List<PrestoWarning> warnings{ get; set; }
        public String updateType{ get; set; }
        public long updateCount{ get; set; }
    }
}
