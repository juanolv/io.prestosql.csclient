using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public class Column
    {
        public string name { get; set; }
        public string type { get; set; }
        public ClientTypeSignature typeSignature { get; set; }


    }
}
