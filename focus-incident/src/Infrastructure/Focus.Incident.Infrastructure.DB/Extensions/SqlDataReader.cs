using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Focus.Incident.Infrastructure.DB.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static string SafeGetString(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }

        public static DateTime? SafeGetDate(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            return null;
        }

        public static float? SafeGetDouble(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return Convert.ToSingle(reader.GetDouble(colIndex));
            }
            else
            {
                return null;
            }
        }
    }
}
