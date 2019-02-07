using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace io.prestosql.client
{
    using io.prestosql.client.v1;

    public class PrestoSqlDbDataReader : DbDataReader
    {
        internal PrestoSqlDbCommand DBCommand { get; set; }

        private int m_ColumnCount = 0;
        private List<Column> m_Columns;
        private Dictionary<string, int> m_ColumnIndex = new Dictionary<string, int>();

        private int m_RowCount = 0;
        private object[][] m_Rows = null;
        private int m_RowIndex = -1;
        private object[] m_Fields = null;

        internal PrestoSqlDbDataReader(PrestoSqlDbCommand Command)
        {
            this.DBCommand = Command;
        }


        public override bool Read()
        {
            if (m_RowIndex >= 0 && m_RowIndex < m_Rows.Length - 1)
            {
                m_RowIndex++;

                m_Fields = m_Rows[m_RowIndex];

                return true;
            }
            else
            {
                QueryResults Result = ((PrestoSqlDbCommand)this.DBCommand).GetNextResult();
                while (Result != null && Result.data == null)
                    Result = ((PrestoSqlDbCommand)this.DBCommand).GetNextResult();

                if (m_Columns == null)
                {
                    m_Columns = Result.columns;
                    m_ColumnCount = m_Columns.Count;
                    int i = 0;
                    foreach (Column col in m_Columns)
                        m_ColumnIndex.Add(col.name, i++);
                }

                if (Result != null)
                {
                    m_Rows = Result.data;
                    m_RowCount += m_Rows?.Length ?? 0;
                    m_RowIndex = 0;
                    m_Fields = m_Rows[m_RowIndex];

                    return true;
                }
                else
                    return false;
            }
        }

        public override bool NextResult()
        {
            return false;
        }

        public override object this[int ordinal]
        {
            get { return m_Fields[ordinal]; }
        }

        public override object this[string name]
        {
            get
            {
                if (m_ColumnIndex.ContainsKey(name))
                    return m_Fields[m_ColumnIndex[name]];
                else
                    return null;
            }
        }

        public override int Depth { get { return 0; } }
        public override int FieldCount { get { return m_ColumnCount; } }
        public override bool HasRows { get { return m_RowCount > 0; } }
        public override bool IsClosed { get { return false; } }
        public override int RecordsAffected { get { return m_RowCount; } }

        public override string GetName(int ordinal)
        {
            if (ordinal >= 0 && ordinal < m_ColumnCount)
                return m_Columns[ordinal].name;
            else
                return null;
        }
        public override Type GetFieldType(int ordinal)
        {
            if (ordinal >= 0 && ordinal < m_ColumnCount)
                return StandardTypes.MapType(m_Columns[ordinal].type);
            else
                return null;
        }

        public override int GetOrdinal(string name)
        {
            if (m_ColumnIndex.ContainsKey(name))
                return m_ColumnIndex[name];
            else
                return -1;
        }

        public override bool GetBoolean(int ordinal)
        {
            return Convert.ToBoolean(m_Fields[ordinal]);
        }

        public override byte GetByte(int ordinal)
        {
            return Convert.ToByte(m_Fields[ordinal]);
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int ordinal)
        {
            return Convert.ToChar(m_Fields[ordinal]);
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return Convert.ToDateTime(m_Fields[ordinal]);
        }

        public override decimal GetDecimal(int ordinal)
        {
            return Convert.ToDecimal(m_Fields[ordinal]);
        }

        public override double GetDouble(int ordinal)
        {
            return Convert.ToDouble(m_Fields[ordinal]);
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override float GetFloat(int ordinal)
        {
            return Convert.ToSingle(m_Fields[ordinal]);
        }

        public override Guid GetGuid(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override short GetInt16(int ordinal)
        {
            return Convert.ToInt16(m_Fields[ordinal]);
        }

        public override int GetInt32(int ordinal)
        {
            return Convert.ToInt32(m_Fields[ordinal]);
        }

        public override long GetInt64(int ordinal)
        {
            return Convert.ToInt64(m_Fields[ordinal]);
        }

        public override string GetString(int ordinal)
        {
            return Convert.ToString(m_Fields[ordinal]);
        }

        public override object GetValue(int ordinal)
        {
            return m_Fields[ordinal];
        }

        public override int GetValues(object[] values)
        {
            values = m_Fields;

            return m_Fields.Length;
        }

        public override bool IsDBNull(int ordinal)
        {
            return false;
        }
    }
}
