using Microsoft.Data.SqlClient;

namespace Inmobiliaria.Persistence.Extensions;


public static class DataReaderExtensions
{
    public static T GetValue<T>(this SqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        if (reader.IsDBNull(ordinal)) return default!;
        return (T)reader.GetValue(ordinal);
    }

    public static T? GetNullableValue<T>(this SqlDataReader reader, string columnName) where T : struct
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? null : (T?)reader.GetValue(ordinal);
    }

    public static string GetStringOrEmpty(this SqlDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
    }

    public static Guid GetGuidSafe(this SqlDataReader reader, string columnName) => reader.GetValue<Guid>(columnName);
    public static int GetInt32Safe(this SqlDataReader reader, string columnName) => reader.GetValue<int>(columnName);
    public static decimal GetDecimalSafe(this SqlDataReader reader, string columnName) => reader.GetValue<decimal>(columnName);
    public static bool GetBooleanSafe(this SqlDataReader reader, string columnName) => reader.GetValue<bool>(columnName);
    public static DateTime GetDateTimeSafe(this SqlDataReader reader, string columnName) => reader.GetValue<DateTime>(columnName);

    // Extensión segura para columnas opcionales como TotalCount
    public static bool HasColumn(this SqlDataReader reader, string columnName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
            if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }

    public static T? GetNullableValueSafe<T>(this SqlDataReader reader, string columnName) where T : struct
    {
        return reader.HasColumn(columnName) && !reader.IsDBNull(reader.GetOrdinal(columnName))
            ? (T?)reader.GetValue(reader.GetOrdinal(columnName))
            : null;
    }
}
