using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource sfxSource;
	public AudioSource MusicSource;

	[Range(0f, 1f)] public float SfxVolume = 1f;
	[Range(0f, 1f)] public float MusicVolume = 1f;

	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	public static SoundManager Instance = null;

	private const string PREF_SFX_VOL = "SfxVolume";
	private const string PREF_MUSIC_VOL = "MusicVolume";

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		// Load saved volumes (if any)
		if (PlayerPrefs.HasKey(PREF_SFX_VOL))
			SfxVolume = PlayerPrefs.GetFloat(PREF_SFX_VOL);
		if (PlayerPrefs.HasKey(PREF_MUSIC_VOL))
			MusicVolume = PlayerPrefs.GetFloat(PREF_MUSIC_VOL);

		ApplyVolumes();
	}

	// Apply the volume values to the audio sources
	public void ApplyVolumes()
	{
		if (sfxSource != null)
			sfxSource.volume = Mathf.Clamp01(SfxVolume);
		if (MusicSource != null)
			MusicSource.volume = Mathf.Clamp01(MusicVolume);
	}

	// Save current volume settings to PlayerPrefs
	public void SaveVolumes()
	{
		PlayerPrefs.SetFloat(PREF_SFX_VOL, Mathf.Clamp01(SfxVolume));
		PlayerPrefs.SetFloat(PREF_MUSIC_VOL, Mathf.Clamp01(MusicVolume));
		PlayerPrefs.Save();
	}

	// Set SFX volume (call from UI slider)
	public void SetSfxVolume(float volume)
	{
		SfxVolume = Mathf.Clamp01(volume);
		if (sfxSource != null) sfxSource.volume = SfxVolume;
		SaveVolumes();
	}

	// Set Music volume (call from UI slider)
	public void SetMusicVolume(float volume)
	{
		MusicVolume = Mathf.Clamp01(volume);
		if (MusicSource != null) MusicSource.volume = MusicVolume;
		SaveVolumes();
	}

	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip)
	{
		if (sfxSource == null || clip == null) return;
		sfxSource.volume = Mathf.Clamp01(SfxVolume);
		sfxSource.pitch = 1f;
		sfxSource.clip = clip;
		sfxSource.Play();
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip, bool loop = true)
	{
		if (MusicSource == null || clip == null) return;
		MusicSource.volume = Mathf.Clamp01(MusicVolume);
		MusicSource.clip = clip;
		MusicSource.loop = loop;
		MusicSource.Play();
	}

	// Play a random clip from an array, and randomize the pitch slightly.
	public void RandomSoundEffect(params AudioClip[] clips)
	{
		if (sfxSource == null || clips == null || clips.Length == 0) return;

		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

		sfxSource.volume = Mathf.Clamp01(SfxVolume);
		sfxSource.pitch = randomPitch;
		sfxSource.clip = clips[randomIndex];
		sfxSource.Play();
	}
}
