using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerController playerController;
    [Header("Camera")]
    public CameraController cameraController;
    [Header("Music")]
    public AudioClip backgroundMusic;

    public void Awake()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
		        if (Game.Initialize("test"))
                    LoadingFinished();
        #else
                Game.Initialize("test");
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        Game._soundManager.playBackgroundMusic(backgroundMusic, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
