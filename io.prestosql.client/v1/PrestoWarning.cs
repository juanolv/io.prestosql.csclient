using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class PrestoWarning
    {
        public WarningCode warningCode { get; set; }
        public  string message { get; set; }
    }
}
