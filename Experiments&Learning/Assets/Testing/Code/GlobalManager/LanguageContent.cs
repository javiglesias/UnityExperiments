using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LanguageContent
{
	private List<string> _keyList = new List<string>();
	private List<string> _textList = new List<string>();
	
	public bool AddString(string key, string text)
	{
		key = key.ToUpper();
		if (_keyList.Contains(key))
		{
			Debug.LogWarning("[i18N ADD STRING] Duplicated key: " + key);
			return false;
		}
		
		_keyList.Add(key);
		_textList.Add(text);
		return true;			
	}
	
	public bool RemoveString(string key)
	{
		key = key.ToUpper();
		int index = _keyList.IndexOf(key);
		if (index < 0)
		{
			Debug.LogWarning("[i18N REMOVE STRING] Key doesn't exists: " + key);
			return false;
		}
		_keyList.RemoveAt(index);
		_textList.RemoveAt(index);
		return true;
	}
	
	public bool ClearContent()
	{
		_keyList.Clear();
		_textList.Clear();
		return true;
	}
	
	public String GetString(string key)
	{
		key = key.ToUpper();
		int index = _keyList.IndexOf(key);
		if (index < 0)
		{
			Debug.LogWarning("[i18N GET STRING] Key doesn't exists: " + key);
			return key;
		}
		
		return _textList[index];
	}
}