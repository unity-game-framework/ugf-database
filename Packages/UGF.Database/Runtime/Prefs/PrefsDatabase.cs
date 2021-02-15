using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UGF.Database.Runtime.Prefs
{
    public class PrefsDatabase : Database<string, string>
    {
        protected override bool OnTrySet(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();

            return true;
        }

        protected override Task<bool> OnTrySetAsync(string key, string value)
        {
            bool result = OnTrySet(key, value);

            return Task.FromResult(result);
        }

        protected override bool OnTryGet(string key, out string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            value = PlayerPrefs.GetString(key);

            return !string.IsNullOrEmpty(value);
        }

        protected override Task<DatabaseGetAsyncResult<string>> OnTryGetAsync(string key)
        {
            return OnTryGet(key, out string value) ? Task.FromResult(new DatabaseGetAsyncResult<string>(value)) : Task.FromResult(new DatabaseGetAsyncResult<string>());
        }
    }
}
