using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class ClientTypeSignature
    {
        public string rawType;
        public  List<ClientTypeSignatureParameter> arguments;
    }
}
