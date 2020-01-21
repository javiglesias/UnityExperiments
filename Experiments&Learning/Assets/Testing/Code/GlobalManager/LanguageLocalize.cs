using System;
using UnityEngine;
using UnityEngine.UI;

public class LanguageLocalize : MonoBehaviour
{
	private Text textLabel;
	private string textTag;

	private void Start()
	{
		textLabel = GetComponent<Text>();
		textTag = textLabel.text;
		UpdateText();
	}

	public void UpdateText()
	{
		textLabel.text = LanguageManager.GetString(textTag);
	}
}