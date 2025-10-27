using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Class to classify attributes of classes
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class MyAttribute : Attribute
    {
        /// <summary>
        /// the name of attribute
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ConnectionKey
        /// </summary>
        public ConnectionKeys ConnectionKey;

        /// <summary>
        /// the role of attribute
        /// </summary>
        public List<ActionType> Action { get; set; }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="name">the name of attribute</param>
        public MyAttribute(string name)
        {
            Name = name;
            Action = new List<ActionType>();
            ConnectionKey = ConnectionKeys.DevelopmentString;
        }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionKey"></param>
        public MyAttribute(string name, ConnectionKeys connectionKey)
        {
            Name = name;
            Action = new List<ActionType>();
            ConnectionKey = connectionKey;
        }

        /// <summary>
        /// Action constructor
        /// </summary>
        /// <param name="action"></param>
        public MyAttribute(ActionType action)
        {
            Action = new[] { action }.ToList();
            Name = null;
            ConnectionKey = ConnectionKeys.DevelopmentString;
        }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="action"></param>
        /// <param name="connectionKey"></param>
        public MyAttribute(ActionType action, ConnectionKeys connectionKey)
        {
            Action = new[] { action }.ToList();
            Name = null;
            ConnectionKey = connectionKey;
        }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="connectionKey"></param>
        public MyAttribute(ConnectionKeys connectionKey)
        {
            Action = new List<ActionType>();
            Name = null;
            ConnectionKey = connectionKey;
        }

        /// <summary>
        /// Action constructor
        /// </summary>
        /// <param name="actions"></param>
        public MyAttribute(params ActionType[] actions)
        {
            Action = actions.ToList();
            Name = null;
            ConnectionKey = ConnectionKeys.DevelopmentString;
        }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="name">the name of attribute</param>
        /// <param name="action">the role of attribute</param>
        public MyAttribute(string name, ActionType action)
        {
            Name = name;
            Action = new[] { action }.ToList();
            ConnectionKey = ConnectionKeys.DevelopmentString;
        }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="name">the name of attribute</param>
        /// <param name="actions">the role of attribute</param>
        public MyAttribute(string name, params ActionType[] actions)
        {
            Name = name;
            Action = actions.ToList();
            ConnectionKey = ConnectionKeys.DevelopmentString;
        }

        /// <summary>
        /// Action types
        /// </summary>
        public enum ActionType
        {
            /// <summary>
            /// Primary key
            /// </summary>
            Id,
            /// <summary>
            /// the attribute is a pk
            /// </summary>
            ForeignKey,
            /// <summary>
            /// Ignore from database operations
            /// </summary>
            DbIgnore
        }
    }

    /// <summary>
    /// Class to manipulate attributes of classes
    /// </summary>
    public static class _attr
    {
        /// <summary>
        /// All fields binding flags
        /// </summary>
        public static BindingFlags BindingFlags => BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;



        private static readonly ConcurrentDictionary<Type, FieldInfo> IdFieldCache = new();

        /// <summary>
        /// Get FieldInfo of primary key of an object
        /// </summary>
        /// <returns>FieldInfo</returns>
        public static FieldInfo GetId<T>() => typeof(T).GetId();

        /// <summary>
        /// Get FieldInfo of primary key of an object
        /// </summary>
        /// <returns>FieldInfo</returns>
        public static FieldInfo GetId(this Type type)
        {
            return IdFieldCache.GetOrAdd(type, t =>
            {
                var fields = t.GetAllFields();
                foreach (var field in fields)
                {
                    var name = field.GetName();
                    if (name.Equals("id", StringComparison.OrdinalIgnoreCase)) return field;
                    var attr = field.CustomAttribute();
                    if (attr != null && attr.Action.Contains(MyAttribute.ActionType.Id)) return field;
                }
                throw new Exception("No ID found");
            });

        }



        private static readonly ConcurrentDictionary<Type, IEnumerable<FieldInfo>> CachedFields = new();
        private static IEnumerable<FieldInfo> GetAllFields(this Type type) => CachedFields.GetOrAdd(type, t => t.GetAllFields_());

        /// <summary>
        /// Get all fields from type (private and public)
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>FieldInfo[]</returns>
        public static IEnumerable<FieldInfo> GetAllFields<T>() => GetAllFields(typeof(T));

        /// <summary>
        /// Get all fields from type (private and public)
        /// </summary>
        /// <param name="type"></param>
        /// <returns>FieldInfo[]</returns>
        private static IEnumerable<FieldInfo> GetAllFields_(this Type type)
        {
            if (CachedFields.TryGetValue(type, out var cachedFields)) return cachedFields;

            var baseType = type;
            var lst = new List<FieldInfo>();
            var fields = new List<FieldInfo>();
            do
            {
                fields.AddRange(baseType.GetFields(BindingFlags).ToList());
                baseType = baseType.BaseType;
            } while (baseType is not null && !baseType.IsInstanceOfType(typeof(object)));
            foreach (var f in fields.Where(f => !lst.Exists(tf => f.Name == tf.Name))) lst.Add(f);
            return lst;
        }



        private static readonly ConcurrentDictionary<FieldInfo, MyAttribute> AttributeCache = new();
        private static readonly ConcurrentDictionary<FieldInfo, string> FieldNameCache = new();

        /// <summary>
        /// Get CustomAttribute
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static MyAttribute CustomAttribute(this FieldInfo field) =>
            AttributeCache.GetOrAdd(field, f =>
            {
                var attr = f.GetCustomAttributes(typeof(MyAttribute), true)
                    .Cast<MyAttribute>()
                    .FirstOrDefault();

                if (attr != null) return attr;

                var name = f.Name.Replace("k__BackingField", "").Trim('<').Trim('>');
                if (name.StartsWith("_")) name = name.Trim('_');
                var prop = f.DeclaringType?.GetProperty(name, BindingFlags);
                if (prop == null) return null;

                return prop.GetCustomAttributes(typeof(MyAttribute), true)
                    .Cast<MyAttribute>()
                    .FirstOrDefault();
            });



        /// <summary>
        /// Get a field by name
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <param name="name">Field name</param>
        /// <returns>FieldInfo</returns>
        public static FieldInfo GetField<T>(string name) => GetField(typeof(T), name);

        /// <summary>
        /// Get a field by name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FieldInfo GetField(Type type, string name)
        {
            var baseType = type;
            do
            {
                var field = baseType.GetField(name, BindingFlags) ?? baseType.GetField($"<{name}>k__BackingField", BindingFlags);
                if (field != null) return field;
                baseType = baseType.BaseType;
            } while (baseType is not null && !baseType.IsInstanceOfType(typeof(object)));
            return null;
        }

        /// <summary>
        /// Get name af a field
        /// </summary>
        /// <param name="field">The field</param>
        /// <returns>the name</returns>
        public static string GetName(this FieldInfo field)
        {
            return FieldNameCache.GetOrAdd(field, f =>
            {
                var name = f.Name.Replace("k__BackingField", "").Trim('<').Trim('>');
                if (name.StartsWith("_")) name = name.Trim('_');
                var attr = f.CustomAttribute();
                if (attr != null)
                {
                    if (attr.Name.IsNotNull()) return attr.Name;
                    if (attr.Action.Contains(MyAttribute.ActionType.ForeignKey)) return name.ToLower() + "Id";
                }
                return name;
            });
        }

        /// <summary>
        /// Get attribute name of class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="withBrackets"></param>
        /// <returns></returns>
        public static string GetName<T>(bool withBrackets = true) => typeof(T).GetName(withBrackets);
        /// <summary>
        /// Get attribute name of class
        /// </summary>
        /// <param name="type"></param>
        /// <param name="withBrackets"></param>
        /// <returns></returns>
        public static string GetName(this Type type, bool withBrackets = true)
        {
            type = type.MainClass();
            var name = type?.Name;
            if (withBrackets) name = "[" + name + "]";
            var cAttrs = type.GetTypeInfo().GetCustomAttributes(true);
            if (cAttrs.Length <= 0) return name;
            var attr = (MyAttribute)cAttrs[0];
            if (attr.Name.IsNotNull()) name = attr.Name;
            return name;
        }

        private static readonly ConcurrentDictionary<Type, Func<object>> ConstructorCache = new();
        private static Func<object> CreateConstructor(Type type)
        {
            var ctor = Expression.New(type);
            var lambda = Expression.Lambda<Func<object>>(ctor);
            return lambda.Compile();
        }


        /// <summary>
        /// Instance an object and set pk value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pkVal">pk value</param>
        /// <returns></returns>
        public static object SetPkValue(this Type type, object pkVal)
        {
            var tObj = ConstructorCache.GetOrAdd(type, CreateConstructor)();
            type.GetId().SetValue(tObj, pkVal);
            return tObj;
        }

        /// <summary>
        /// Check if an object exists in Database
        /// </summary>
        /// <param name="obj">the object</param>
        /// <returns>boolean</returns>
        public static bool Exists<T>(this T obj)
        {
            if (obj == null) return false;
            var id = GetId<T>();
            var val = id.GetValue(obj);
            return val.IsNumeric() ? val.ToInt() > 0 : val.IsNotNull();
            //return _db.Exists(GetName<T>(), id.GetName() + " = '" + id.GetValue(obj) + "'", GetConnectionKey<T>());
        }
        /// <summary>
        /// Copy an object to another
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">the object to be copied</param>
        /// <returns>the type of object</returns>
        public static T Copy<T>(this T obj)
        {
            var type = obj.GetType();
            var newObj = (T)ConstructorCache.GetOrAdd(type, CreateConstructor)();
            var fields = GetAllFields<T>();
            foreach (var field in fields)
            {
                var tempField = type.GetField(field.Name, BindingFlags);
                if (tempField == null)
                {
                    var name = field.Name.Replace("k__BackingField", "").Trim('<').Trim('>');
                    if (name.StartsWith("_")) name = name.Trim('_');
                    var tempProperty = type.GetProperty(name, BindingFlags);
                    if (tempProperty != null) tempProperty.SetValue(newObj, tempProperty.GetValue(obj));
                }
                else tempField.SetValue(newObj, tempField.GetValue(obj));
            }
            return newObj;
        }

        /// <summary>
        /// Insert or update data of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="affectedFields"></param>
        /// <returns></returns>
        public static T Save<T>(this T obj, params string[] affectedFields)
        {
            var connectionKey = GetConnectionKey<T>();
            var isUpdate = Exists(obj);
            var id = GetId<T>();
            var idName = id.GetName();
            var fields = GetAllFields<T>();
            if (affectedFields.Length > 0)
            {
                fields = fields.Where(f =>
                {
                    var fieldName = f.GetName();
                    return idName == fieldName || affectedFields.Any(af => af.Is(fieldName));
                });
            }
            var tblName = GetName<T>();
            /*if (duplication)
            {
                if (Duplicated(obj, isUpdate, id, fields)) throw new Exception("Eléments déjà existant !!!");
            }*/
            var values = new List<object>();
            var names = new List<string>();
            var vn = new List<string>();

            using var con = new SqlConnection(connectionKey.ConnectionString());
            var cmd = new SqlCommand { Connection = con };
            con.Open();
            foreach (var field in fields)
            {
                var val = field.GetValue(obj);
                if (!isUpdate && val.IsNull()) continue;
                var fieldName = field.GetName();
                if (!SetCmdParamValue(cmd, field, fieldName, val, idName == fieldName)) continue;
                if (isUpdate) vn.Add($"[{fieldName}] = @{fieldName}");
                else
                {
                    names.Add($"[{fieldName}]");
                    values.Add($"@{fieldName}");
                }
            }
            var query = "";
            if (isUpdate) query += $"UPDATE {tblName} SET {string.Join(",", vn)} WHERE {idName} = @{idName}";
            else query = $"INSERT INTO {tblName} ({string.Join(",", names)}) OUTPUT inserted.{idName} VALUES ({string.Join(",", values)})";
            cmd.CommandText = query;
            var insertedId = cmd.ExecuteScalar();
            con.Close();
            if (isUpdate) return obj;
            if(id.FieldType.Name == "String") id.SetValue(obj, insertedId.ToString());
            else id.SetValue(obj, insertedId.ToInt());
            return obj;
            //return Get<T>(!isUpdate ? insertedId : id.GetValue(obj));
        }
        private static bool SetCmdParamValue(SqlCommand cmd, FieldInfo field, string fieldName, object val, bool isPk)
        {
            var attr = field.CustomAttribute();
            if (attr != null)
            {
                if (attr.Action.Contains(MyAttribute.ActionType.DbIgnore)) return false;
                if (attr.Action.Contains(MyAttribute.ActionType.ForeignKey))
                {
                    if (val.IsNotNull()) val = val.GetType().GetId().GetValue(val);
                }
            }

            if (field.FieldType.IsEnum) val = val.IsNull() ? 0 : (int)val;

            var dbType = _func.NetTypeToSqlDbType(field.FieldType);
            var parameter = cmd.Parameters.Add(fieldName, dbType);
            parameter.Value = val ?? DBNull.Value;

            parameter.Size = dbType switch
            {
                SqlDbType.VarChar when val is string s => Math.Max(1, s.Length),
                SqlDbType.VarBinary when val is byte[] b => b.Length,
                _ => parameter.Size
            };

            if (cmd.Parameters.IndexOf(fieldName) < 0) return false;
            return !isPk;
        }

        /// <summary>
        /// Update query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cond">condition</param>
        /// <param name="fields">field = 1 ...</param>
        /// <returns></returns>
        public static object Update<T>(string fields, string cond = null) => typeof(T).Update(fields, cond);
        
        /// <summary>
        /// Update query
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cond">condition</param>
        /// <param name="fields">status = 1 ...</param>
        /// <returns></returns>
        public static object Update(this Type type, string fields, string cond = null) => _db.Exec($"UPDATE {type.GetName()} SET {fields} {(cond.IsNotNull() ? $" WHERE {cond}" : "")}", type.GetConnectionKey());

        /// <summary>
        /// Delete from a table
        /// </summary>
        /// <param name="condition">condition</param>
        public static void Delete<T>(string condition = null) => typeof(T).Delete(condition);

        /// <summary>
        /// Delete from a table
        /// </summary>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        public static void Delete(this Type type, string condition = null)
        {
            try
            {
                _db.Exec($"DELETE FROM {type.GetName()} {(condition.IsNotNull() ? $"WHERE {condition}" : "")}", type.GetConnectionKey());
            }
            catch
            {
                throw new Exception("Vous ne pouvez pas supprimer cet élément !!!");
            }
        }



        /// <summary>
        /// Get information of a class from database
        /// </summary>
        /// <param name="value">value of pk</param>
        /// <param name="ignoreFk"></param>
        /// <param name="ignoreDeepFk"></param>
        /// <returns>Object</returns>
        public static T Get<T>(object value, bool ignoreFk = false, bool ignoreDeepFk = true) => (T)typeof(T).Get(value, ignoreFk, ignoreDeepFk);

        /// <summary>
        /// Get information of a class from database
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value">value of pk</param>
        /// <param name="ignoreFk"></param>
        /// <param name="ignoreDeepFk"></param>
        /// <returns>object</returns>
        public static object Get(this Type type, object value, bool ignoreFk = false, bool ignoreDeepFk = true)
        {
            var dt = _db.Where(type.GetId().GetName() + " = '" + value + "'", type.GetName(), "*", type.GetConnectionKey());
            return dt.Rows.Count <= 0 ? null : type.Get(dt.Rows[0], ignoreFk, ignoreDeepFk);
            //return dt.Rows.Count <= 0 ? ConstructorCache.GetOrAdd(type, CreateConstructor)() : type.Get(dt.Rows[0]);
        }

        /// <summary>
        /// Get object from dataRow
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="dr">dataRow</param>
        /// <param name="ignoreFk"></param>
        /// <param name="ignoreDeepFk"></param>
        /// <returns>object</returns>
        public static T Get<T>(DataRow dr, bool ignoreFk = false, bool ignoreDeepFk = true) => (T)typeof(T).Get(dr, ignoreFk, ignoreDeepFk);

        /// <summary>
        /// Get object from dataRow
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dr"></param>
        /// <param name="ignoreFk">ignore foreign object</param>
        /// <param name="ignoreDeepFk"></param>
        /// <returns></returns>
        public static object Get(this Type type, DataRow dr, bool ignoreFk = false, bool ignoreDeepFk = true)
        {
            var obj = ConstructorCache.GetOrAdd(type, CreateConstructor)();
            var fields = type.GetAllFields();
            foreach (var field in fields)
            {
                var attr = field.CustomAttribute();
                if (attr != null)
                {
                    if(attr.Action.Contains(MyAttribute.ActionType.DbIgnore)) continue;
                }
                object val;
                var name = field.GetName();
                if (attr != null && attr.Action.Contains(MyAttribute.ActionType.ForeignKey))
                {
                    if (!ignoreFk)
                    {
                        var res = dr.ValueOf(name);
                        val = res.IsNotNull() ? field.FieldType.Get(res, ignoreDeepFk) : null;
                    }
                    else val = null;
                }
                else if (field.FieldType.IsEnum) val = dr.IntegerValueOf(name);
                else
                {
                    switch (field.FieldType.Name)
                    {
                        case "Boolean":
                            val = dr.BooleanValueOf(name);
                            break;
                        case "Byte":
                        case "Int16":
                        case "Int32":
                        //case "Int64":
                            val = dr.IntegerValueOf(name);
                            break;
                        case "Int64":
                            val = dr.LongValueOf(name);
                            break;
                        case "Byte[]":
                            val = dr[name];
                            break;
                        case "Char[]":
                        case "String":
                            val = dr.StringValueOf(name);
                            break;
                        case "Single":
                        case "Double":
                            val = dr.DoubleValueOf(name);
                            break;
                        case "Decimal":
                            val = dr.DecimalValueOf(name);
                            break;
                        case "DateTime":
                            val = dr[name];
                            break;
                        case "Object":
                            val = dr.ValueOf(name);
                            break;
                        default:
                            val = null;
                            break;
                    }
                }
                if (val == null || val.IsNull()) continue;
                field.SetValue(obj, val);
            }
            return obj;
        }

        /// <summary>
        /// Get list of object by condition
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="condition">condition</param>
        /// <param name="ignoreFk"></param>
        /// <param name="ignoreDeepFk"></param>
        /// <returns>List of objects</returns>
        public static List<T> Get<T>(string condition, bool ignoreFk = false, bool ignoreDeepFk = true) => _db.Where<T>(condition).GetAll<T>(ignoreFk, ignoreDeepFk);
        
        /// <summary>
        /// Convert DataTable to list of objects
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="dt">DataTable</param>
        /// <param name="ignoreFk"></param>
        /// <param name="ignoreDeepFk"></param>
        /// <returns>List</returns>
        public static List<T> GetAll<T>(this DataTable dt, bool ignoreFk = false, bool ignoreDeepFk = true)
        {
            if (dt == null || dt.Rows.Count == 0) return new List<T>();

            // If ignoring FKs, use fast path
            if (ignoreFk)
                return (from DataRow dr in dt.Rows select Get<T>(dr, true, ignoreDeepFk)).ToList();

            // AGGRESSIVE OPTIMIZATION: Batch load all FKs to avoid N+1
            var type = typeof(T);
            var fields = type.GetAllFields().ToList();
            var fkFields = fields.Where(f =>
            {
                var attr = f.CustomAttribute();
                return attr != null && attr.Action.Contains(MyAttribute.ActionType.ForeignKey);
            }).ToList();

            // Pre-load all FKs in batches
            var fkCache = new Dictionary<(Type, object), object>();
            foreach (var fkField in fkFields)
            {
                var fkType = fkField.FieldType;
                var fkName = fkField.GetName();

                // Collect all FK IDs from the DataTable
                var fkIds = new HashSet<object>();
                foreach (DataRow dr in dt.Rows)
                {
                    var fkId = dr.ValueOf(fkName);
                    if (fkId.IsNotNull() && !(fkId is DBNull))
                        fkIds.Add(fkId);
                }

                if (fkIds.Count == 0) continue;

                // Batch load all FKs in ONE query
                var idField = fkType.GetId();
                var idFieldName = idField.GetName();
                var fkTableName = fkType.GetName();
                var connectionKey = fkType.GetConnectionKey();

                var idList = string.Join(",", fkIds.Select(id => $"'{id}'"));
                var query = $"SELECT * FROM {fkTableName} WHERE [{idFieldName}] IN ({idList})";
                var fkData = _db.Query(query, connectionKey);

                // Cache the loaded FKs
                foreach (DataRow fkRow in fkData.Rows)
                {
                    var fkObj = fkType.Get(fkRow, ignoreDeepFk, true);
                    var fkPkValue = idField.GetValue(fkObj);
                    fkCache[(fkType, fkPkValue)] = fkObj;
                }
            }

            // Now create objects and assign cached FKs
            var result = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                var obj = ConstructorCache.GetOrAdd(type, CreateConstructor)();
                foreach (var field in fields)
                {
                    var attr = field.CustomAttribute();
                    if (attr != null && attr.Action.Contains(MyAttribute.ActionType.DbIgnore)) continue;

                    object val;
                    var name = field.GetName();

                    if (attr != null && attr.Action.Contains(MyAttribute.ActionType.ForeignKey))
                    {
                        // Use cached FK object
                        var fkId = dr.ValueOf(name);
                        if (fkId.IsNotNull() && !(fkId is DBNull))
                        {
                            var key = (field.FieldType, fkId);
                            val = fkCache.ContainsKey(key) ? fkCache[key] : null;
                        }
                        else val = null;
                    }
                    else if (field.FieldType.IsEnum)
                    {
                        val = dr.IntegerValueOf(name);
                    }
                    else
                    {
                        switch (field.FieldType.Name)
                        {
                            case "Boolean":
                                val = dr.BooleanValueOf(name);
                                break;
                            case "Byte":
                            case "Int16":
                            case "Int32":
                                val = dr.IntegerValueOf(name);
                                break;
                            case "Int64":
                                val = dr.LongValueOf(name);
                                break;
                            case "Byte[]":
                                val = dr[name];
                                break;
                            case "Char[]":
                            case "String":
                                val = dr.StringValueOf(name);
                                break;
                            case "Single":
                            case "Double":
                                val = dr.DoubleValueOf(name);
                                break;
                            case "Decimal":
                                val = dr.DecimalValueOf(name);
                                break;
                            case "DateTime":
                                val = dr[name];
                                break;
                            case "Object":
                                val = dr.ValueOf(name);
                                break;
                            default:
                                val = null;
                                break;
                        }
                    }

                    if (val == null || val.IsNull()) continue;

                    field.SetValue(obj, val);
                }
                result.Add((T)obj);
            }

            return result;
        }

        /// <summary>
        /// Get connectionKey of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>ConnectionKeys</returns>
        public static ConnectionKeys GetConnectionKey<T>() => typeof(T).GetConnectionKey();
        /// <summary>
        /// Get connectionKey of a type
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>ConnectionKeys</returns>
        public static ConnectionKeys GetConnectionKey(this Type type)
        {
            type = type.MainClass();
            var tmp = type.GetTypeInfo().GetCustomAttributes();
            var attributes = tmp.ToList();
            return attributes.Any() ? ((MyAttribute)attributes.First()).ConnectionKey : ConnectionKeys.DevelopmentString;
        }
    }
}
