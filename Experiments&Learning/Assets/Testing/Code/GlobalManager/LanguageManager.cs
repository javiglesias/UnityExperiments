using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public static class LanguageManager
{
	private static SystemLanguage _currentLanguage = Application.systemLanguage;
	private static LanguageContent _currentLanguageContent = new LanguageContent();
	
	public static SystemLanguage GetCurrentLanguage()
	{
		return _currentLanguage;
	}

	public static string GetShortCurrentLanguage()
	{
		switch (_currentLanguage)
		{
		case SystemLanguage.Spanish:
			return "es";
		case SystemLanguage.French:
			return "fr";
		case SystemLanguage.German:
			return "de";
		case SystemLanguage.Portuguese:
			return "pt";
		case SystemLanguage.Italian:
			return "it";
		default:
			return "en";
		}
	}

	public static string GetLongCurrentLanguage()
	{
		switch (_currentLanguage)
		{
		case SystemLanguage.Spanish:
			return "Spanish";
		case SystemLanguage.French:
			return "French";
		case SystemLanguage.German:
			return "German";
		case SystemLanguage.Portuguese:
			return "Portuguese";
		case SystemLanguage.Italian:
			return "Italian";
		default:
			return "English";
		}
	}
	
	public static void LoadLanguageFile()
	{
		#if UNITY_ANDROID
		AndroidJavaClass localeClass = new AndroidJavaClass("java/util/Locale");
		AndroidJavaObject defaultLocale = localeClass.CallStatic<AndroidJavaObject>("getDefault");
		AndroidJavaObject usLocale = localeClass.GetStatic<AndroidJavaObject>("US");
		string systemLanguage = defaultLocale.Call<string>("getDisplayLanguage", usLocale);
		try
		{
			_currentLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), systemLanguage);
		}
		catch
		{
			_currentLanguage = SystemLanguage.English;
		}  
		#endif

		LoadLanguageFile(_currentLanguage);
	}
	
	public static void LoadLanguageFile(string language)
	{
		switch (language)
		{
		case "Spanish":
			LoadLanguageFile(SystemLanguage.Spanish);
			break;
		case "French":
			LoadLanguageFile(SystemLanguage.French);
			break;
		case "German":
			LoadLanguageFile(SystemLanguage.German);
			break;
		case "Portuguese":
			LoadLanguageFile(SystemLanguage.Portuguese);
			break;
		case "Italian":
			LoadLanguageFile(SystemLanguage.Italian);
			break;
		default:
			LoadLanguageFile(SystemLanguage.English);
			break;
		}
	}
	
	public static void LoadLanguageFile(SystemLanguage language)
	{
		_currentLanguage = language;
		_currentLanguageContent.ClearContent();
		
		switch (_currentLanguage)
		{
		case SystemLanguage.Spanish:
			LoadXmlFile("Language/Spanish");
			break;
		case SystemLanguage.French:
			LoadXmlFile("Language/French");
			break;
		case SystemLanguage.German:
			LoadXmlFile("Language/German");
			break;
		case SystemLanguage.Portuguese:
			LoadXmlFile("Language/Portuguese");
			break;
		case SystemLanguage.Italian:
			LoadXmlFile("Language/Italian");
			break;
		default:
			LoadXmlFile("Language/English");
			break;
		}
	}

	public static SystemLanguage GetNextLanguage(SystemLanguage language)
	{
		switch (language)
		{
		case SystemLanguage.Spanish:
			return SystemLanguage.English;
		case SystemLanguage.French:
			return SystemLanguage.German;
		case SystemLanguage.German:
			return SystemLanguage.Italian;
		case SystemLanguage.Portuguese:
			return SystemLanguage.Spanish;
		case SystemLanguage.Italian:
			return SystemLanguage.Portuguese;
		default:
			return SystemLanguage.French;
		}
	}
	
	public static String GetString(string key)
	{		
		return _currentLanguageContent.GetString(key);
	}
	
	private static void LoadXmlFile(String file)
	{
		TextAsset textAsset = (TextAsset) Resources.Load(file);  		
		XmlDocument xmlFile = new XmlDocument();
		xmlFile.LoadXml(GetTextWithoutBOM(textAsset));
	
		XmlNodeList stringList = xmlFile.GetElementsByTagName("string");
  
		foreach (XmlNode stringNode in stringList)
		{
			_currentLanguageContent.AddString(stringNode.Attributes["key"].Value, stringNode.InnerText);
		}
	}

	private static string GetTextWithoutBOM(TextAsset textAsset)
	{
        MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
		StreamReader streamReader = new StreamReader(memoryStream, true);
		string result = streamReader.ReadToEnd();
		streamReader.Close();
		memoryStream.Close();
		return result;
	}
}
