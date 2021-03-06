﻿using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text;

namespace io.prestosql.client
{
    internal class StandardTypes
    {
        static Dictionary<string, Type> m_TypeMapping = new Dictionary<string, Type>();
        static StandardTypes()
        {
            m_TypeMapping.Add(StandardTypes.BIGINT, typeof(Int64));
            m_TypeMapping.Add(StandardTypes.INTEGER, typeof(Int32));
            m_TypeMapping.Add(StandardTypes.SMALLINT, typeof(Int16));
            m_TypeMapping.Add(StandardTypes.TINYINT, typeof(Int16));
            m_TypeMapping.Add(StandardTypes.BOOLEAN, typeof(bool));
            m_TypeMapping.Add(StandardTypes.DATE, typeof(DateTime));
            m_TypeMapping.Add(StandardTypes.DECIMAL, typeof(decimal));
            m_TypeMapping.Add(StandardTypes.REAL, typeof(float));
            m_TypeMapping.Add(StandardTypes.DOUBLE, typeof(double));
            // TODO: Map HYPER_LOG_LOG
            // TODO: Map QDIGEST
            // TODO: Map P4_HYPER_LOG_LOG
            m_TypeMapping.Add(StandardTypes.INTERVAL_DAY_TO_SECOND, typeof(TimeSpan));
            m_TypeMapping.Add(StandardTypes.INTERVAL_YEAR_TO_MONTH, typeof(TimeSpan));
            m_TypeMapping.Add(StandardTypes.TIMESTAMP, typeof(DateTime));
            m_TypeMapping.Add(StandardTypes.TIMESTAMP_WITH_TIME_ZONE, typeof(DateTime));
            m_TypeMapping.Add(StandardTypes.TIME, typeof(DateTime));
            m_TypeMapping.Add(StandardTypes.TIME_WITH_TIME_ZONE, typeof(DateTime));
            m_TypeMapping.Add(StandardTypes.VARBINARY, typeof(byte[]));
            m_TypeMapping.Add(StandardTypes.VARCHAR, typeof(String));
            m_TypeMapping.Add(StandardTypes.CHAR, typeof(String));
            // TODO: Map ROW
            // TODO: Map ARRAY
            // TODO: Map MAP
            // TODO: Map JSON
            m_TypeMapping.Add(StandardTypes.IPADDRESS, typeof(IPAddress));
            // TODO: Map GEOMETRY
            // TODO: Map BING_TILE

        }

        public static string BIGINT = "bigint";
        public static string INTEGER = "integer";
        public static string SMALLINT = "smallint";
        public static string TINYINT = "tinyint";
        public static string BOOLEAN = "boolean";
        public static string DATE = "date";
        public static string DECIMAL = "decimal";
        public static string REAL = "real";
        public static string DOUBLE = "double";
        public static string HYPER_LOG_LOG = "HyperLogLog";
        public static string QDIGEST = "qdigest";
        public static string P4_HYPER_LOG_LOG = "P4HyperLogLog";
        public static string INTERVAL_DAY_TO_SECOND = "interval day to second";
        public static string INTERVAL_YEAR_TO_MONTH = "interval year to month";
        public static string TIMESTAMP = "timestamp";
        public static string TIMESTAMP_WITH_TIME_ZONE = "timestamp with time zone";
        public static string TIME = "time";
        public static string TIME_WITH_TIME_ZONE = "time with time zone";
        public static string VARBINARY = "varbinary";
        public static string VARCHAR = "varchar";
        public static string CHAR = "char";
        public static string ROW = "row";
        public static string ARRAY = "array";
        public static string MAP = "map";
        public static string JSON = "json";
        public static string IPADDRESS = "ipaddress";
        public static string GEOMETRY = "Geometry";
        public static string BING_TILE = "BingTile";

        internal static Type MapType(string typeName)
        {
            if (m_TypeMapping.ContainsKey(typeName))
                return m_TypeMapping[typeName];
            else
                return Type.Missing.GetType();
        }

        internal static object Convert(string typeName, object Obj)
        {
            if (typeName == StandardTypes.VARBINARY)
                return System.Convert.FromBase64String((string)Obj);
            else if (typeName == StandardTypes.TIME_WITH_TIME_ZONE)
                return Helper.ParseTimeWithTimeZone((string)Obj);
            else if (typeName == StandardTypes.TIMESTAMP_WITH_TIME_ZONE)
                return Helper.ParseTimeWithTimeZone((string)Obj);
            else if (typeName == StandardTypes.INTERVAL_YEAR_TO_MONTH)
                return Helper.ParseIntervalYearToMonth((string)Obj);
            else if (typeName == StandardTypes.INTERVAL_DAY_TO_SECOND)
                return Helper.ParseIntervalDayToSecond((string)Obj);
            else if (typeName == StandardTypes.ARRAY)
                return ((JArray)Obj).ToObject<object[]>();
            else if (typeName == StandardTypes.MAP)
                return Helper.ParseDictionary((JObject)Obj);
            else if (typeName == StandardTypes.ROW)
                return Obj; // TODO: Parse Row
            else if (typeName == StandardTypes.JSON)
                return ((JToken)Obj);
            else if (typeName == StandardTypes.IPADDRESS)
                return Helper.ParseIPAddress((string)Obj);
            else
                return System.Convert.ChangeType(Obj, MapType(typeName));
        }

    }
}
