using System;
using System.Collections.Generic;
using System.Text;

namespace io.prestosql.client
{
    using io.prestosql.client.v1;

    public class PrestoSqlException : Exception
    {
        public QueryError QueryError { get; internal set; }
        internal PrestoSqlException(QueryError error) : base(error.message)
        {
            this.QueryError = error;
        }

        internal static PrestoSqlException Create(QueryError error)
        {
            switch(error.errorType)
            {
                case "USER_ERROR": return new PrestoSqlUserException(error);
                case "INTERNAL_ERROR": return new PrestoSqlInternalException(error);
                case "INSUFFICIENT_RESOURCES": return new PrestoSqlInsufficientResourceException(error);
                case "EXTERNAL": return new PrestoSqlExternalException(error);
                default:
                    return new PrestoSqlException(error);
            }
        }

        public string GetMessage() { return this.QueryError.message; }
        public string GetSqlState() { return this.QueryError.sqlState; }
        public int GetErrorCode() { return this.QueryError.errorCode; }
        public string GetErrorName() { return this.QueryError.errorName; }
        public string GetErrorType() { return this.QueryError.errorType; }
        public ErrorLocation GetErrorLocation() { return this.QueryError.errorLocation; }
        public FailureInfo GetFailureInfo() { return this.QueryError.failureInfo; }
    }


    public class PrestoSqlUserException : PrestoSqlException
    {
        internal PrestoSqlUserException(QueryError error): base(error) { }
    }

    public class PrestoSqlInternalException : PrestoSqlException
    {
        internal PrestoSqlInternalException(QueryError error) : base(error) { }
    }

    public class PrestoSqlInsufficientResourceException : PrestoSqlException
    {
        internal PrestoSqlInsufficientResourceException(QueryError error) : base(error) { }
    }

    public class PrestoSqlExternalException : PrestoSqlException
    {
        internal PrestoSqlExternalException(QueryError error) : base(error) { }
    }
}
