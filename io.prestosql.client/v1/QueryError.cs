using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class QueryError
    {
        public string message { get; set; }
        public string sqlState { get; set; }
        public int errorCode { get; set; }
        public string errorName { get; set; }
        public string errorType { get; set; }
        public ErrorLocation errorLocation { get; set; }
        public FailureInfo failureInfo { get; set; }
    }
}
