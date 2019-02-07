using System;
using System.IO;
using System.Net;
using System.Data.Common;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Data;

namespace io.prestosql.client
{
    using io.prestosql.client.v1;

    public class PrestoSqlDbConnection : DbConnection
    {
        public PrestoSqlDbConnection()
        { }

        public PrestoSqlDbConnection(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public override string ConnectionString { get; set; }

        public override string Database { get; }

        public override string DataSource { get; }

        public override string ServerVersion { get; }

        public override ConnectionState State { get; }

        public override void ChangeDatabase(string databaseName)
        {
        }

        public override void Close()
        {
        }

        public override void Open()
        {
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        protected override DbCommand CreateDbCommand()
        {
            return new PrestoSqlDbCommand() { Connection = this };
        }

        internal QueryResults ExecuteQuery(string Query)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("Content-Type", "application/json");
            Headers.Add("Accept", "application/json");
            Headers.Add(PrestoHeaders.PRESTO_USER, "juan");

            return Helper.HttpRequest<QueryResults>("POST", this.ConnectionString + "/v1/statement", Query, Headers);
        }

        internal QueryResults GetNextResult(Uri nextUri)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            Headers.Add("Accept", "application/json");

            return Helper.HttpRequest<QueryResults>("GET", nextUri.ToString(), "", Headers);
        }

    }
}
