using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client.v1
{
    public static class PrestoHeaders
    {
        public static string PRESTO_USER = "X-Presto-User";
        public static string PRESTO_SOURCE = "X-Presto-Source";
        public static string PRESTO_CATALOG = "X-Presto-Catalog";
        public static string PRESTO_SCHEMA = "X-Presto-Schema";
        public static string PRESTO_PATH = "X-Presto-Path";
        public static string PRESTO_TIME_ZONE = "X-Presto-Time-Zone";
        public static string PRESTO_LANGUAGE = "X-Presto-Language";
        public static string PRESTO_TRACE_TOKEN = "X-Presto-Trace-Token";
        public static string PRESTO_SESSION = "X-Presto-Session";
        public static string PRESTO_SET_CATALOG = "X-Presto-Set-Catalog";
        public static string PRESTO_SET_SCHEMA = "X-Presto-Set-Schema";
        public static string PRESTO_SET_PATH = "X-Presto-Set-Path";
        public static string PRESTO_SET_SESSION = "X-Presto-Set-Session";
        public static string PRESTO_CLEAR_SESSION = "X-Presto-Clear-Session";
        public static string PRESTO_PREPARED_STATEMENT = "X-Presto-Prepared-Statement";
        public static string PRESTO_ADDED_PREPARE = "X-Presto-Added-Prepare";
        public static string PRESTO_DEALLOCATED_PREPARE = "X-Presto-Deallocated-Prepare";
        public static string PRESTO_TRANSACTION_ID = "X-Presto-Transaction-Id";
        public static string PRESTO_STARTED_TRANSACTION_ID = "X-Presto-Started-Transaction-Id";
        public static string PRESTO_CLEAR_TRANSACTION_ID = "X-Presto-Clear-Transaction-Id";
        public static string PRESTO_CLIENT_INFO = "X-Presto-Client-Info";
        public static string PRESTO_CLIENT_TAGS = "X-Presto-Client-Tags";
        public static string PRESTO_CLIENT_CAPABILITIES = "X-Presto-Client-Capabilities";
        public static string PRESTO_RESOURCE_ESTIMATE = "X-Presto-Resource-Estimate";

        public static string PRESTO_CURRENT_STATE = "X-Presto-Current-State";
        public static string PRESTO_MAX_WAIT = "X-Presto-Max-Wait";
        public static string PRESTO_MAX_SIZE = "X-Presto-Max-Size";
        public static string PRESTO_TASK_INSTANCE_ID = "X-Presto-Task-Instance-Id";
        public static string PRESTO_PAGE_TOKEN = "X-Presto-Page-Sequence-Id";
        public static string PRESTO_PAGE_NEXT_TOKEN = "X-Presto-Page-End-Sequence-Id";
        public static string PRESTO_BUFFER_COMPLETE = "X-Presto-Buffer-Complete";
    }
}
