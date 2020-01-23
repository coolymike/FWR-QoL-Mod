using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x0200004F RID: 79
public class AudioSlowMotionEffects : MonoBehaviour
{
	// Token: 0x06000193 RID: 403 RVA: 0x0000378C File Offset: 0x0000198C
	private void Update()
	{
		this.SlowMotionAudioEffects();
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
	private void SlowMotionAudioEffects()
	{
		if (this.playerSettings == null)
		{
			return;
		}
		if (LevelManager.SlowMotion && this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			if (!this.slowMotionReverbZone.activeInHierarchy)
			{
				this.slowMotionReverbZone.SetActive(true);
			}
			this.audioMixer.SetFloat("SFX_Lowpass_Cutoff", 700f);
			this.audioMixer.SetFloat("Voices_Lowpass_Cutoff", 2000f);
			return;
		}
		if (this.slowMotionReverbZone.activeInHierarchy)
		{
			this.slowMotionReverbZone.SetActive(false);
		}
		this.audioMixer.SetFloat("SFX_Lowpass_Cutoff", 22000f);
		this.audioMixer.SetFloat("Voices_Lowpass_Cutoff", 22000f);
	}

	// Token: 0x0400025C RID: 604
	public AudioMixer audioMixer;

	// Token: 0x0400025D RID: 605
	public PlayerSettings playerSettings;

	// Token: 0x0400025E RID: 606
	public GameObject slowMotionReverbZone;
}
