using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000093 RID: 147
public class UISettings : MonoBehaviour
{
	// Token: 0x060002F5 RID: 757 RVA: 0x00004937 File Offset: 0x00002B37
	private void Start()
	{
		this.FillData();
		this.menu.OnMenuActivated += this.OnThisMenu;
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00004956 File Offset: 0x00002B56
	private void OnDestroy()
	{
		this.menu.OnMenuActivated -= this.OnThisMenu;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0000496F File Offset: 0x00002B6F
	public void OnThisMenu(bool menuActivated)
	{
		if (menuActivated)
		{
			this.FillData();
			return;
		}
		this.ResetAudio();
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00004981 File Offset: 0x00002B81
	public void SaveChanges()
	{
		this.RecordData();
		this.ApplyData();
		GameData.SaveSettings();
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00004994 File Offset: 0x00002B94
	public void ResetSettings()
	{
		GameData.ResetSettings();
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0000499B File Offset: 0x00002B9B
	public void ResetEverything()
	{
		GameData.ResetEverything();
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00014000 File Offset: 0x00012200
	private void FillData()
	{
		this.musicVolume.value = (this.lastValueMusic = GameData.settings.MusicVolume);
		this.soundFXVolume.value = (this.lastValueSFX = GameData.settings.SoundFXVolume);
		this.voicesVolume.value = (this.lastValueVoices = GameData.settings.VoicesVolume);
		this.interfaceToggle.isOn = GameData.settings.DisplayHUD;
		this.cameraShakeToggle.isOn = GameData.settings.CameraShake;
		this.fullscreenToggle.isOn = Screen.fullScreen;
		this.vsyncToggle.isOn = (QualitySettings.vSyncCount > 0);
		this.resolutionDropdown.options.Clear();
		this.resolutions = Screen.resolutions;
		for (int i = 0; i < this.resolutions.Length; i++)
		{
			this.resolutionDropdown.options.Add(this.resolutions[i].ToString());
			if (this.resolutions[i].width == Screen.currentResolution.width)
			{
				this.resolutionDropdown.selectedOption = i;
			}
		}
		this.resolutionDropdown.UpdateOptions();
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00014144 File Offset: 0x00012344
	private void RecordData()
	{
		GameData.settings.MusicVolume = (this.lastValueMusic = this.musicVolume.value);
		GameData.settings.SoundFXVolume = (this.lastValueSFX = this.soundFXVolume.value);
		GameData.settings.VoicesVolume = (this.lastValueVoices = this.voicesVolume.value);
		GameData.settings.DisplayHUD = this.interfaceToggle.isOn;
		GameData.settings.CameraShake = this.cameraShakeToggle.isOn;
	}

	// Token: 0x060002FD RID: 765 RVA: 0x000049A2 File Offset: 0x00002BA2
	private void ResetAudio()
	{
		GameData.settings.MusicVolume = this.lastValueMusic;
		GameData.settings.SoundFXVolume = this.lastValueSFX;
		GameData.settings.VoicesVolume = this.lastValueVoices;
	}

	// Token: 0x060002FE RID: 766 RVA: 0x000141D8 File Offset: 0x000123D8
	public void PreviewAudioSettings()
	{
		GameData.settings.MusicVolume = this.musicVolume.value;
		GameData.settings.SoundFXVolume = this.soundFXVolume.value;
		GameData.settings.VoicesVolume = this.voicesVolume.value;
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00014224 File Offset: 0x00012424
	private void ApplyData()
	{
		QualitySettings.vSyncCount = (this.vsyncToggle.isOn ? 1 : 0);
		if (Application.isMobilePlatform)
		{
			return;
		}
		Screen.fullScreen = this.fullscreenToggle.isOn;
		Screen.SetResolution(Screen.resolutions[this.resolutionDropdown.selectedOption].width, Screen.resolutions[this.resolutionDropdown.selectedOption].height, this.fullscreenToggle.isOn);
	}

	// Token: 0x04000450 RID: 1104
	public UIMenu menu;

	// Token: 0x04000451 RID: 1105
	[Header("Audio")]
	public Slider musicVolume;

	// Token: 0x04000452 RID: 1106
	public Slider soundFXVolume;

	// Token: 0x04000453 RID: 1107
	public Slider voicesVolume;

	// Token: 0x04000454 RID: 1108
	private float lastValueMusic;

	// Token: 0x04000455 RID: 1109
	private float lastValueSFX;

	// Token: 0x04000456 RID: 1110
	private float lastValueVoices;

	// Token: 0x04000457 RID: 1111
	[Header("Visual")]
	public Toggle interfaceToggle;

	// Token: 0x04000458 RID: 1112
	public UIButtonOptions resolutionDropdown;

	// Token: 0x04000459 RID: 1113
	public Toggle vsyncToggle;

	// Token: 0x0400045A RID: 1114
	public UIButtonOptions shadowResolutionDropdown;

	// Token: 0x0400045B RID: 1115
	public Toggle displayHUDToggle;

	// Token: 0x0400045C RID: 1116
	public Toggle cameraShakeToggle;

	// Token: 0x0400045D RID: 1117
	public Toggle fullscreenToggle;

	// Token: 0x0400045E RID: 1118
	private Resolution[] resolutions;
}
