using System;
using UnityEngine;

namespace UGF.Database.Runtime.Prefs
{
    public class PrefsDatabase : Database<string, string>
    {
        protected override void OnAdd(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            if (PlayerPrefs.HasKey(key)) throw new ArgumentException($"Value already exists with the specified key: '{key}'.");

            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        protected override bool OnRemove(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.Save();

                return true;
            }

            return false;
        }

        protected override bool OnTrySet(string key, string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));
            if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));

            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();

            return true;
        }

        protected override bool OnTryGet(string key, out string value)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentException("Value cannot be null or empty.", nameof(key));

            value = PlayerPrefs.GetString(key);

            return !string.IsNullOrEmpty(value);
        }
    }
}
