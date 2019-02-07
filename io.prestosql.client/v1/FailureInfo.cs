using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class FailureInfo
    {
        public string type { get; set; }
        public string message { get; set; }
        public FailureInfo cause { get; set; }
        public List<FailureInfo> suppressed { get; set; }
        public List<String> stack { get; set; }
        public ErrorLocation errorLocation { get; set; }
    }
}
