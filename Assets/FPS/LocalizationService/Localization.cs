using System.Data;
using System.Globalization;
using UnityEngine;

namespace FPS.LocalizationService
{
    //https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
    public static class Localization
    {
        private const string DefaultLanguage = "en";
        private static readonly DataTable DataTable = new();
        private static string _currentLanguage = DefaultLanguage;

        public static void Init()
        {
            var isLocFileHasSystemLanguage = false;
            _currentLanguage = SystemLanguage;
            Debug.Log("Language: " + _currentLanguage);

            string locFileContent = string.Empty;
            foreach (var textAsset in Resources.LoadAll<TextAsset>(""))
            {
                if (!textAsset.name.Contains("Localization"))
                    continue;

                locFileContent = textAsset.text.Replace("\r", "");
                break;
            }

            var lines = locFileContent.Split('\n');
            var headers = lines[0].Split(',');

            foreach (var header in headers)
            {
                if (header == _currentLanguage)
                    isLocFileHasSystemLanguage = true;
                DataTable.Columns.Add(header, typeof(string));
            }

            if (!isLocFileHasSystemLanguage)
            {
                Debug.LogWarning($"Haven't localization for {_currentLanguage}");
                _currentLanguage = DefaultLanguage;
            }

            for (int i = 0; i < lines.Length; i++)
            {
                var split = lines[i].Split(',');
                var row = DataTable.NewRow();
                for (int j = 0; j < headers.Length; j++)
                    row[j] = split[j];

                DataTable.Rows.Add(row);
            }
        }

        public static string Get(string key)
        {
            var rows = DataTable.Select($"key = '{key}'");
            if (rows.Length > 0)
            {
                var value = rows[0][_currentLanguage].ToString();
                return value;
            }

            Debug.LogError($"[{nameof(Localization)}:{nameof(Get)}] - localization item with key [{key}] doesn't exists.");
            return key;
        }

        // #if UNITY_ANDROID && !UNITY_EDITOR //Android WebGL
        //      private static string SystemLanguage
        //      {
        //          get
        //          {
        //              AndroidJavaClass localeClass = new AndroidJavaClass("java.util.Locale");
        //              AndroidJavaObject defaultLocale = localeClass.CallStatic<AndroidJavaObject>("getDefault");
        //              return defaultLocale.Call<string>("getLanguage");
        //          }
        //      }
    #if UNITY_WEBGL && !UNITY_EDITOR //Windows WebGL
        private static string SystemLanguage => GetSystemLanguageJS().Split('-')[0].ToLower();

        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern string GetSystemLanguageJS();
    #else //Windows or Android
        private static string SystemLanguage => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    #endif
    }
}