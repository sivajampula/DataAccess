using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EarlFileWatcher.DataAccess
{
    public class DataAccessHelper
    {
        public static List<T> Convert<T>(DataTable dataTable) where T : class
        {
            if(dataTable !=null)
            {
                return dataTable.AsEnumerable().Select(row =>
                {
                    return ConvertNoEmptyCheck<T>(row); ;
                }).ToList();
            }
            return null;
        }

        public static T Convert<T>(DataRow dataRow) where T : class
        {
            string currentPropertyName = string.Empty;
            try
            {
                if(dataRow != null)
                {
                    var properties = typeof(T).GetProperties();

                    var objt = Activator.CreateInstance<T>();

                    foreach (var prop in properties)
                    {
                        currentPropertyName = prop.Name;
                        if (dataRow.Table.Columns.Contains(prop.Name))
                        {
                            if(dataRow[prop.Name] !=null && !string.IsNullOrEmpty(dataRow[prop.Name].ToString()))
                            {
                                prop.SetValue(objt, dataRow[prop.Name]);
                            }
                        }
                    }
                    return objt;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw new Exception("Failure while assigning property " + currentPropertyName, ex);
            }
        }

        public static T ConvertNoEmptyCheck<T>(DataRow dataRow) where T : class
        {
            string currentPropertyName = string.Empty;
            try
            {
                if (dataRow != null)
                {
                    var properties = typeof(T).GetProperties();

                    var objt = Activator.CreateInstance<T>();

                    foreach (var prop in properties)
                    {
                        currentPropertyName = prop.Name;
                        if (dataRow.Table.Columns.Contains(prop.Name))
                        {
                            if (dataRow[prop.Name] != null )
                            {
                                prop.SetValue(objt, dataRow[prop.Name]);
                            }
                        }
                    }
                    return objt;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Failure while assigning property " + currentPropertyName, ex);
            }
        }
    }
}
