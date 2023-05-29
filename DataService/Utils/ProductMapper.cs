using System.Data;
using System.Reflection;

namespace DataService.Utils;

public static class ProductMapper
{
    #region Mapper Table
    public static List<T>? ToList<T>(
        this DataTable table,
        Dictionary<string, string>? fields = null
    ) where T : new()
    {
        List<T> result = new();
        //result.AddRange(collection: from DataRow row in table.Rows
        //                            let item = CreateItemFromRow<T>(row, properties)
        //                            select item);

        foreach (var row in table.Rows)
        {
            var item = ToModel<T>((DataRow)row, fields);
            result.Add(item);
        }
        return result;
    }
    #endregion
    #region Mapper Row
    public static T ToModel<T>(
        this DataRow row,
        Dictionary<string, string>? fields = null
    ) where T : new()
    {
        IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        T item = new();

        if (fields != null)
            foreach (PropertyInfo property in properties)
                foreach (string modelField in fields.Keys)
                {
                    string dbField = fields[modelField];
                    if (!row.Table.Columns.Contains(dbField))
                        continue;

                    else if (property.Name == modelField)
                    {
                        if (row[dbField] == DBNull.Value)
                            property.SetValue(item, null, null);
                        else if (row.Table.Columns[dbField]!.DataType == typeof(decimal))
                            property.SetValue(item, double.Parse($"{row[dbField]}"), null);
                        else
                            property.SetValue(item, row[dbField], null);
                    }

                }

        return item;
    }
    #endregion
}
