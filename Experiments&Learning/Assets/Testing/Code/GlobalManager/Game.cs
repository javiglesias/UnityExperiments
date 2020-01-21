using UnityEngine;

public static class Game
{
	private static bool _initialized = false;

    public static SaveManager _saveManager;
    public static SoundManager _soundManager;

	public static bool Initialize(string hashWord)
	{
		if (_initialized)
			return false;
		
		_initialized = true;

		#if UNITY_ANDROID || UNITY_IOS
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;
		#elif UNITY_WEBPLAYER
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;
		#endif

		Screen.sleepTimeout = SleepTimeout.SystemSetting;

        _saveManager = (new GameObject("SaveManager")).AddComponent<SaveManager>();
        _soundManager = (new GameObject("SoundManager")).AddComponent<SoundManager>();

        if (_saveManager._language.Equals(""))
            LanguageManager.LoadLanguageFile();
        else
            LanguageManager.LoadLanguageFile(_saveManager._language);

        return true;
	}
}

