using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;

namespace NuGetSearch
{
    public class AppSettings : INotifyPropertyChanged
    {
        // The isolated storage key names of our settings
        private const string IsIncludePreReleaseSettingKeyName = "IsIncludePreReleaseSetting";
        private const bool IsIncludePreReleaseSettingDefault = true;

        private readonly IsolatedStorageSettings _settings;

        public AppSettings()
        {
            try
            {
                // Get the settings for this application.
                _settings = IsolatedStorageSettings.ApplicationSettings;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while using IsolatedStorageSettings: " + e);
            }
        }

        public bool IsIncludePreReleaseSetting
        {
            get { return GetValueOrDefault(IsIncludePreReleaseSettingKeyName, IsIncludePreReleaseSettingDefault); }
            set
            {
                if (AddOrUpdateValue(IsIncludePreReleaseSettingKeyName, value))
                {
                    Save();
                    NotifyPropertyChanged("IsGroupByPinYinSetting");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        ///     Update a setting value for our application. If the setting does not
        ///     exist, then add the setting.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddOrUpdateValue(string key, Object value)
        {
            bool valueChanged = false;

            // If the key exists
            if (_settings.Contains(key))
            {
                // If the value has changed
                if (_settings[key] != value)
                {
                    // Store the new value
                    _settings[key] = value;
                    valueChanged = true;
                }
            }
                // Otherwise create the key.
            else
            {
                _settings.Add(key, value);
                valueChanged = true;
            }

            return valueChanged;
        }


        /// <summary>
        ///     Get the current value of the setting, or if it is not found, set the
        ///     setting to the default setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            // If the key exists, retrieve the value.
            if (_settings.Contains(key))
            {
                value = (T) _settings[key];
            }
                // Otherwise, use the default value.
            else
            {
                value = defaultValue;
            }

            return value;
        }


        /// <summary>
        ///     Save the settings.
        /// </summary>
        public void Save()
        {
            _settings.Save();
        }
    }
}