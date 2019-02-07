using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class ClientTypeSignatureParameter
    {
        public ParameterKind kind { get; set; }
        public object value { get; set; }
    }
}
