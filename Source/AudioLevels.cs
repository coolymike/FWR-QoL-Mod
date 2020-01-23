using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Token: 0x02000052 RID: 82
public class AudioLevels : MonoBehaviour
{
	// Token: 0x0600019D RID: 413 RVA: 0x000037B9 File Offset: 0x000019B9
	private void Awake()
	{
		SceneManager.sceneLoaded += this.OnSceneLoaded;
		this.GamePaused(false);
	}

	// Token: 0x0600019E RID: 414 RVA: 0x000037D3 File Offset: 0x000019D3
	private void Start()
	{
		GameData.OnVolumeChanged += this.SetAudioLevels;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x000037E6 File Offset: 0x000019E6
	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= this.OnSceneLoaded;
		GameData.OnVolumeChanged -= this.SetAudioLevels;
		LevelManager.OnGamePauseToggle -= this.GamePaused;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000381B File Offset: 0x00001A1B
	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (LevelManager.instance != null)
		{
			LevelManager.OnGamePauseToggle += this.GamePaused;
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000E570 File Offset: 0x0000C770
	private void GamePaused(bool paused)
	{
		if (paused)
		{
			this.mixerGroup.audioMixer.SetFloat("Volume-Voices", -80f);
			this.mixerGroup.audioMixer.SetFloat("Volume-SFX", -80f);
			return;
		}
		this.SetAudioLevels();
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
	private void SetAudioLevels()
	{
		this.mixerGroup.audioMixer.SetFloat("Volume-Music", (GameData.settings.MusicVolume < -39f) ? -80f : GameData.settings.MusicVolume);
		this.mixerGroup.audioMixer.SetFloat("Volume-Voices", (GameData.settings.VoicesVolume < -39f) ? -80f : GameData.settings.VoicesVolume);
		this.mixerGroup.audioMixer.SetFloat("Volume-SFX", (GameData.settings.SoundFXVolume < -39f) ? -80f : GameData.settings.SoundFXVolume);
		this.mixerGroup.audioMixer.SetFloat("Volume-UI", (GameData.settings.SoundFXVolume - 10f < -49f) ? -80f : (GameData.settings.SoundFXVolume - 10f));
	}

	// Token: 0x04000268 RID: 616
	public AudioMixerGroup mixerGroup;
}
