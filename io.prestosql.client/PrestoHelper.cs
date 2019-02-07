using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client
{
    public class PrestoHelper
    {
        public static Type MapType(string Type)
        {
            return Type.GetType();
        }
    }
}
