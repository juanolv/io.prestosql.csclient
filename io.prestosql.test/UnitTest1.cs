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
                    Cmd.CommandText = "SELECT 1";

                    using (DbDataReader Reader = Cmd.ExecuteReader())
                    {
                        if (Reader.Read())
                        {
                            int v = Reader.GetInt32(0);

                            if (v != 1)
                                Assert.Fail("Invalid return value");
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
