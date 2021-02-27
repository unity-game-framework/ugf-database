using System;
using System.Threading.Tasks;

namespace UGF.Database.Runtime
{
    public static class DatabaseExtensions
    {
        public static T Get<T>(this IDatabase database, object key)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            if (key == null) throw new ArgumentNullException(nameof(key));

            return (T)database.Get(key);
        }

        public static bool TryGet<T>(this IDatabase database, object key, out T value)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (database.TryGet(key, out object result))
            {
                value = (T)result;
                return true;
            }

            value = default;
            return false;
        }

        public static async Task<T> GetAsync<T>(this IDatabase database, object key)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            if (key == null) throw new ArgumentNullException(nameof(key));

            object value = await database.GetAsync(key);

            return (T)value;
        }

        public static async Task<DatabaseGetAsyncResult<T>> TryGetAsync<T>(this IDatabase database, object key)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            if (key == null) throw new ArgumentNullException(nameof(key));

            DatabaseGetAsyncResult result = await database.TryGetAsync(key);

            return result.HasValue
                ? new DatabaseGetAsyncResult<T>((T)result.Value)
                : new DatabaseGetAsyncResult<T>();
        }
    }
}
