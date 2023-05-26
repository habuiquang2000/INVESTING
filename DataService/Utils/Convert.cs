//using System.Data;
//using System.Reflection;

//namespace DataService.Services
//{
//    #region Extensions
//    public static class Extensions
//    {
//        public static List<T>? ToList<T>(this DataTable? table) where T : new()
//        {
//            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();
//            List<T>? result = new();
//            //result.AddRange(collection: from DataRow row in table.Rows
//            //                            let item = CreateItemFromRow<T>(row, properties)
//            //                            select item);

//            foreach (var row in table.Rows)
//            {
//                var item = CreateItemFromRow<T>((DataRow)row, properties);
//                result.Add(item);
//            }
//            return result;
//        }

//        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
//        {
//            T item = new();
//            foreach (PropertyInfo property in properties)
//            {
//                if (property.PropertyType == typeof(DayOfWeek))
//                {
//                    DayOfWeek? day = (DayOfWeek)Enum.Parse(
//                        typeof(DayOfWeek),
//                        row[property.Name].ToString()
//                        );
//                    property.SetValue(item, day, null);
//                }
//                else
//                {
//                    if (!row.Table.Columns.Contains(property.Name))
//                        continue;

//                    if (row[property.Name] == DBNull.Value)
//                        property.SetValue(item, null, null);
//                    else
//                    {
//                        if (Nullable.GetUnderlyingType(property.PropertyType) != null)
//                        {
//                            //nullable
//                            object? convertedValue = null;
//                            try
//                            {
//                                convertedValue = Convert.ChangeType(
//                                    row[property.Name],
//                                    conversionType: property.PropertyType
//                                    );
//                            }
//                            catch (Exception error)
//                            {
//                                throw new Exception(error.Message);
//                            }
//                            property.SetValue(item, convertedValue, null);
//                        }
//                        else
//                            property.SetValue(item, row[property.Name], null);
//                    }
//                }
//            }
//            return item;
//        }
//    }
//    #endregion
//}
