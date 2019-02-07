using System;
using System.Data.Common;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace io.prestosql.client
{
    using io.prestosql.client.v1;

    public class PrestoSqlDbCommand : DbCommand
    {
        public override string CommandText { get; set; }
        public override int CommandTimeout { get; set; }
        public override CommandType CommandType { get; set; }
        public override bool DesignTimeVisible { get; set; }
        public override UpdateRowSource UpdatedRowSource { get; set; }
        protected override DbConnection DbConnection { get; set; }
        protected override DbParameterCollection DbParameterCollection { get; }
        protected override DbTransaction DbTransaction { get; set; }

        internal QueryResults Result { get; set; }

        public override void Cancel()
        {
        }


        public override void Prepare()
        {
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        internal QueryResults GetNextResult()
        {
            if (this.Result != null && this.Result.nextUri != null)
            {
                this.Result = ((PrestoSqlDbConnection)this.DbConnection).GetNextResult(this.Result.nextUri);

                if (this.Result.error != null)
                    throw PrestoSqlException.Create(this.Result.error);

                return this.Result;
            }
            else
                return null;
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            this.Result = ((PrestoSqlDbConnection)this.DbConnection).ExecuteQuery(this.CommandText);

            return new PrestoSqlDbDataReader(this);
        }

        public override int ExecuteNonQuery()
        {
            int Rows = 0;

            using (DbDataReader Reader = ExecuteDbDataReader(CommandBehavior.Default))
            {
                while (Reader.Read())
                    Rows++;
                return Rows;
            }
        }

        public override object ExecuteScalar()
        {
            using (DbDataReader Reader = ExecuteDbDataReader(CommandBehavior.Default))
            {
                if (Reader.Read())
                    return Reader[0];
                else
                    return null;
            }
        }
    }
}
