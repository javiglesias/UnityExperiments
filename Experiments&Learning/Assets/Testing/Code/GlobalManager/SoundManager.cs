using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	[Tooltip( "Anytime you play a sound and set the scaledVolume it is multiplied by this value" )]
	public float _soundEffectVolume = 1f;
	public int _initialCapacity = 10;
	public int _maxCapacity = 15;
	public bool _clearAllAudioClipsOnLevelLoad = true;
	[NonSerialized]
	public SMSound _backgroundSound;

	private Stack<SMSound> _availableSounds;

	private List<SMSound> _playingSounds;
	
	private float _musicEffectVolume = 0.8f;
	public float MusicEffectVolume
	{
		get {
			return _musicEffectVolume;
		}
		set {
			_musicEffectVolume = value;
			if (_backgroundSound != null)
				_backgroundSound.SetVolume(_musicEffectVolume);
		}
	}

	#region MonoBehaviour

	private void Awake()
	{
		DontDestroyOnLoad(this);

		gameObject.AddComponent<AudioListener>();

		// Create the _soundList to speed up sound playing in game
		_availableSounds = new Stack<SMSound>( _maxCapacity );
		_playingSounds = new List<SMSound>();

		for( int i = 0; i < _initialCapacity; i++ )
			_availableSounds.Push( new SMSound( this ) );

		if (!Game._saveManager._sound)
			_soundEffectVolume = 0f;
		
		if (!Game._saveManager._music)
			MusicEffectVolume = 0f;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
		if(_clearAllAudioClipsOnLevelLoad )
		{
			for( var i = _playingSounds.Count - 1; i >= 0; i-- )
			{
				var s = _playingSounds[i];
				s.audioSource.clip = null;

				_availableSounds.Push( s );
				_playingSounds.RemoveAt( i );
			}
		}
	}
	
	private void Update()
	{
		for(var i = _playingSounds.Count - 1; i >= 0; i--)
		{
			var sound = _playingSounds[i];
			if( sound._playingLoopingAudio )
				continue;

			sound._elapsedTime += Time.deltaTime;
			if( sound._elapsedTime > sound.audioSource.clip.length )
				sound.Stop();
		}
	}

	#endregion

	/// <summary>
	/// fetches the next available sound and adds the sound to the _playingSounds List
	/// </summary>
	/// <returns>The available sound.</returns>
	private SMSound nextAvailableSound()
	{
		SMSound sound = null;

		if( _availableSounds.Count > 0 )
			sound = _availableSounds.Pop();

		// if we didnt find an available found, bail out
		if( sound == null )
			sound = new SMSound( this );
		_playingSounds.Add( sound );

		return sound;
	}

	/// <summary>
	/// starts up the background music and optionally loops it. You can access the SKSound via the backgroundSound field.
	/// </summary>
	/// <param name="audioClip">Audio clip.</param>
	/// <param name="loop">If set to <c>true</c> loop.</param>
	public SMSound playBackgroundMusic( AudioClip audioClip, float volume, bool loop = true )
	{
		if( _backgroundSound == null )
			_backgroundSound = new SMSound( this );

		_backgroundSound.PlayAudioClip( audioClip, volume * _musicEffectVolume, 1f );
		_backgroundSound.SetLoop( loop );

		return _backgroundSound;
	}

	public void PauseBackgroundMusic()
	{
		if (_backgroundSound != null)
			_backgroundSound.Pause();
	}

	public void ResumeBackgroundMusic()
	{
		if (_backgroundSound != null)
			_backgroundSound.Resume();
	}

	public void StopBackgroundMusic()
	{
		if (_backgroundSound != null)
			_backgroundSound.Stop();
	}


	/// <summary>
	/// fetches any AudioSource it can find and uses the standard PlayOneShot to play. Use this if you don't require any
	/// extra control over a clip and don't care about when it completes. It avoids the call to StartCoroutine.
	/// </summary>
	/// <param name="audioClip">Audio clip.</param>
	/// <param name="volumeScale">Volume scale.</param>
	public void playOneShot( AudioClip audioClip, float volumeScale = 1f )
	{
		// find an audio source. any will work
		AudioSource source = null;

		if( _availableSounds.Count > 0 )
			source = _availableSounds.Peek().audioSource;
		else
			source = _playingSounds[0].audioSource;

		source.PlayOneShot( audioClip, volumeScale * _soundEffectVolume );
	}


	/// <summary>
	/// plays the AudioClip with the default volume (soundEffectVolume)
	/// </summary>
	/// <returns>The sound.</returns>
	/// <param name="audioClip">Audio clip.</param>
	public SMSound playSound( AudioClip audioClip )
	{
		return playSound( audioClip, 1f );
	}


	/// <summary>
	/// plays the AudioClip with the specified volume
	/// </summary>
	/// <returns>The sound.</returns>
	/// <param name="audioClip">Audio clip.</param>
	/// <param name="volume">Volume.</param>
	public SMSound playSound( AudioClip audioClip, float volume )
	{
		return playSound( audioClip, volume, 1f );
	}
	
	
	/// <summary>
	/// plays the AudioClip with the specified pitch
	/// </summary>
	/// <returns>The sound.</returns>
	/// <param name="audioClip">Audio clip.</param>
	/// <param name="pitch">Pitch.</param>
	public SMSound playPitchedSound( AudioClip audioClip, float pitch )
	{
		return playSound( audioClip, 1f, pitch );
	}
		
	/// <summary>
	/// plays the AudioClip with the specified volumeScale and pitch
	/// </summary>
	/// <returns>The sound.</returns>
	/// <param name="audioClip">Audio clip.</param>
	/// <param name="volume">Volume.</param>
	public SMSound playSound( AudioClip audioClip, float volumeScale, float pitch )
	{
		// Find the first SKSound not being used. if they are all in use, create a new one
		SMSound sound = nextAvailableSound();
		sound.PlayAudioClip( audioClip, volumeScale * _soundEffectVolume, pitch );

		return sound;
	}
		
	/// <summary>
	/// loops the AudioClip. Do note that you are responsible for calling either stop or fadeOutAndStop on the SKSound
	/// or it will not be recycled
	/// </summary>
	/// <returns>The sound looped.</returns>
	/// <param name="audioClip">Audio clip.</param>
	public SMSound playSoundLooped( AudioClip audioClip )
	{
		// find the first SKSound not being used. if they are all in use, create a new one
		SMSound sound = nextAvailableSound();
		sound.PlayAudioClip( audioClip, _soundEffectVolume, 1f );
		sound.SetLoop( true );

		return sound;
	}
		
	/// <summary>
	/// used internally to recycle SKSounds and their AudioSources
	/// </summary>
	/// <param name="sound">Sound.</param>
	public void recycleSound( SMSound sound )
	{
		var index = 0;
		while( index < _playingSounds.Count )
		{
			if( _playingSounds[index] == sound )
				break;
			index++;
		}

		if (index < _playingSounds.Count)
		{
			_playingSounds.RemoveAt (index);

			// if we are already over capacity dont recycle this sound but destroy it instead
			if (_availableSounds.Count + _playingSounds.Count >= _maxCapacity)
				Destroy (sound.audioSource);
			else
				_availableSounds.Push (sound);
		}
	}


	#region SKSound inner class

	public class SMSound
	{
		private SoundManager _manager;
		public AudioSource audioSource;
		internal Action _completionHandler;
		internal bool _playingLoopingAudio;
		internal float _elapsedTime;


		public SMSound( SoundManager manager )
		{
			_manager = manager;
			audioSource = _manager.gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
		}


		/// <summary>
		/// fades out the audio over duration. this will short circuit and stop immediately if the elapsedTime exceeds the clip.length
		/// </summary>
		/// <returns>The out.</returns>
		/// <param name="duration">Duration.</param>
		/// <param name="onComplete">On complete.</param>
		private IEnumerator FadeOut( float duration, Action onComplete )
		{
			var startingVolume = audioSource.volume;

			// fade out the volume
			while( audioSource.volume > 0.0f && _elapsedTime < audioSource.clip.length )
			{
				audioSource.volume -= Time.deltaTime * startingVolume / duration;
				yield return null;
			}

			Stop();

			// all done fading out
			if( onComplete != null )
				onComplete();
		}

		public void SetVolume(float volume)
		{
			audioSource.volume = volume * _manager._soundEffectVolume;
		}

		public void SetPitch(float pitch)
		{
			audioSource.pitch = pitch;
		}

		/// <summary>
		/// sets whether the SKSound should loop. If true, you are responsible for calling stop on the SKSound to recycle it!
		/// </summary>
		/// <returns>The SKSound.</returns>
		/// <param name="shouldLoop">If set to <c>true</c> should loop.</param>
		public SMSound SetLoop( bool shouldLoop )
		{
			_playingLoopingAudio = true;
			audioSource.loop = shouldLoop;

			return this;
		}


		/// <summary>
		/// sets an Action that will be called when the clip finishes playing
		/// </summary>
		/// <returns>The SKSound.</returns>
		/// <param name="handler">Handler.</param>
		public SMSound SetCompletionHandler( Action handler )
		{
			_completionHandler = handler;

			return this;
		}


		/// <summary>
		/// stops the audio clip, fires the completionHandler if necessary and recycles the SKSound
		/// </summary>
		public void Stop()
		{
			audioSource.Stop();

			if( _completionHandler != null )
			{
				_completionHandler();
				_completionHandler = null;
			}

			_manager.recycleSound( this );
		}

		public void Pause()
		{
			if (audioSource.isPlaying)
				audioSource.Pause();
		}
		
		public void Resume()
		{
			if (!audioSource.isPlaying)
				audioSource.Play();
		}

		/// <summary>
		/// fades out the audio clip over time. Note that if the clip finishes before the fade completes it will short circuit
		/// the fade and stop playing
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="handler">Handler.</param>
		public void FadeOutAndStop( float duration, Action handler = null )
		{
			_manager.StartCoroutine
			(
				FadeOut( duration, () =>
			    {
					if( handler != null )
						handler();
				})
			);
		}

		internal void PlayAudioClip( AudioClip audioClip, float volume, float pitch )
		{
			_playingLoopingAudio = false;
			_elapsedTime = 0;

			// setup the GameObject and AudioSource and start playing
			audioSource.clip = audioClip;
			audioSource.volume = volume;
			audioSource.pitch = pitch;

			// reset some defaults in case the AudioSource was changed
			audioSource.loop = false;
			audioSource.panStereo = 0;
			audioSource.mute = false;

			audioSource.Play();
		}

	}

	#endregion

}
