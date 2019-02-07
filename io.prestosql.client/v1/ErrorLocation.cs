using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class ErrorLocation
    {
        public int lineNumber { get; set; }
        public int columnNumber { get; set; }
    }
}
