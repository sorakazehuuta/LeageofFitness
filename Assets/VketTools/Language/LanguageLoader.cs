using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using VketTools.Utilities;

namespace VketTools.Language
{
    public class LanguageLoader
    {
        private static Dictionary<SystemLanguage, string> languageGUIDs;
        private static SystemLanguage defaultLanguage = SystemLanguage.English;
        
        private const string LanguageSettingGuid = "9f51bf38fb8f0da4c969b76970101dda";

        private static Dictionary<SystemLanguage, string> LanguageFileGUIDs
        {
            get
            {
                if (languageGUIDs != null)
                {
                    return languageGUIDs;
                }

                languageGUIDs = new Dictionary<SystemLanguage, string>
                {
                    { SystemLanguage.English, "43a68a8d002a3d34e9c787c2dcbb9745" },
                    { SystemLanguage.Japanese, "2137bcb5081665440bfc0b49810ad3ea" }
                };

                return languageGUIDs;
            }
        }

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            var settings = FindLanguageSettingsInstance();

            if (settings == null)
            {
                EditorApplication.update += DelayedInitialize;
                return;
            }

            LanguageDictionary dictionary;
            if (settings.language == null)
            {
                string languageGUID;
                var currentLanguage = Application.systemLanguage;
                Debug.Log("Current system language = " + currentLanguage);
                if (LanguageFileGUIDs.TryGetValue(currentLanguage, out languageGUID))
                {
                    Debug.Log("Load LanguageFile which for " + currentLanguage);
                }
                else
                {
                    Debug.Log("LanguageFile which for current system language is not found. load default LanguageFile.");
                    languageGUID = LanguageFileGUIDs[defaultLanguage];
                }

                var defaultLanguagePath = AssetDatabase.GUIDToAssetPath(languageGUID);
                dictionary = AssetDatabase.LoadAssetAtPath<LanguageDictionary>(defaultLanguagePath);
            }
            else
            {
                Debug.Log("Load overrided LanguageFile = " + settings.language);
                dictionary = settings.language;
            }
            
            LocalizedMessage.SetDictionary(dictionary);
        }

        private static LanguageSettings FindLanguageSettingsInstance()
        {
            LanguageSettings asset;
            var assetPath = AssetDatabase.GUIDToAssetPath(LanguageSettingGuid);
            if (File.Exists(assetPath))
            {
                asset = AssetDatabase.LoadAssetAtPath<LanguageSettings>(assetPath);
            }
            else
            {
                asset = ScriptableObject.CreateInstance<LanguageSettings>();
                AssetDatabase.CreateAsset(asset, assetPath);
            }

            return asset;
        }

        private static void DelayedInitialize()
        {
            EditorApplication.update -= DelayedInitialize;
            Initialize();
        }
    }
}