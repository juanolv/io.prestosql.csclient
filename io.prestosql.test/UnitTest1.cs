using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using io.prestosql.client;

namespace io.prestosql.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FirstTest()
        {
            using (DbConnection Conn = new PrestoSqlDbConnection())
            {
                Conn.ConnectionString = "http://localhost:8080";
                Conn.Open();

                using (DbCommand Cmd = Conn.CreateCommand())
                {
                    Cmd.CommandText = "VALUES (true, TINYINT '1', SMALLINT '1', INTEGER '1', BIGINT '1', REAL '1.0', DOUBLE '1.0', DECIMAL '123.456', VARCHAR 'Hello World!', CHAR 's', VARBINARY '1111', DATE '2019-01-01', TIME '01:02:03.456', TIME '01:02:03.456 America/Los_Angeles', TIMESTAMP '2001-08-22 03:04:05.321', TIMESTAMP '2001-08-22 03:04:05.321 America/Los_Angeles', INTERVAL '3' MONTH, INTERVAL '2' DAY, ARRAY[1, 2, 3], MAP(ARRAY['foo', 'bar'], ARRAY[1, 2]), CAST(ROW(1, 2.0) AS ROW(x BIGINT, y DOUBLE)), IPADDRESS '10.0.0.1', IPADDRESS '2001:db8::1')";

                    using (DbDataReader Reader = Cmd.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            for (int i = 0; i < Reader.FieldCount; i++)
                            {
                                Type T = Reader.GetFieldType(i);
                                object Value = Reader.GetValue(i);
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void SingleResultTest()
        {
            using (DbConnection Conn = new PrestoSqlDbConnection())
            {
                Conn.ConnectionString = "http://localhost:8080";
                Conn.Open();

                using (DbCommand Cmd = Conn.CreateCommand())
                {
                    Cmd.CommandText = "SELECT 1";

                    int v = Convert.ToInt32(Cmd.ExecuteScalar());

                    if (v != 1)
                        Assert.Fail("Invalid return value");
                }
            }
        }


        [TestMethod]
        public void ErrorTest()
        {
            using (DbConnection Conn = new PrestoSqlDbConnection())
            {
                Conn.ConnectionString = "http://localhost:8080";
                Conn.Open();

                using (DbCommand Cmd = Conn.CreateCommand())
                {
                    Cmd.CommandText = @"ERROR HERE!";


                    try
                    {
                        using (DbDataReader Reader = Cmd.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                for (int i = 0; i < Reader.FieldCount; i++)
                                    Console.Write(Reader[i]);

                            }
                        }

                        Assert.Fail();
                    }
                    catch (PrestoSqlException ex)
                    {

                    }
                }
            }
        }


        [TestMethod]
        public void SecondTest()
        {
            using (DbConnection Conn = new PrestoSqlDbConnection())
            {
                Conn.ConnectionString = "http://localhost:8080";
                Conn.Open();

                using (DbCommand Cmd = Conn.CreateCommand())
                {
                    Cmd.CommandText = @"SELECT orderpriority, SUM(totalprice) AS totalprice
FROM tpch.sf1.orders AS O
INNER JOIN tpch.sf1.customer AS C ON O.custkey = C.custkey 
GROUP BY orderpriority ORDER BY orderpriority";

                    using (DbDataReader Reader = Cmd.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            for (int i = 0; i < Reader.FieldCount; i++)
                                Console.Write(Reader[i]);
                        }
                    }
                }
            }
        }

    }
}
