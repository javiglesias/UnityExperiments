using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public string _language;    // App language
    public bool _howToPlay; // How to play shown or not
    public bool _sound;     // Sound on/off
    public bool _music;     // Music on/off
    public bool _difficult; // Difficult on/off

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _language = PlayerPrefs.GetString("Language", "");
        _howToPlay = (PlayerPrefs.GetInt("HowToPlay", 0) == 1);
        _sound = (PlayerPrefs.GetInt("Sound", 1) == 1);
        _music = (PlayerPrefs.GetInt("Music", 1) == 1);
        _difficult = (PlayerPrefs.GetInt("Difficult", 1) == 1);
    }

    public void Save()
    {
        PlayerPrefs.SetString("Language", _language);

        if (_howToPlay) PlayerPrefs.SetInt("HowToPlay", 1);
        else PlayerPrefs.SetInt("HowToPlay", 0);

        if (_sound) PlayerPrefs.SetInt("Sound", 1);
        else PlayerPrefs.SetInt("Sound", 0);

        if (_music) PlayerPrefs.SetInt("Music", 1);
        else PlayerPrefs.SetInt("Music", 0);

        if (_difficult) PlayerPrefs.SetInt("Difficult", 1);
        else PlayerPrefs.SetInt("Difficult", 0);


        PlayerPrefs.Save();
    }
}
