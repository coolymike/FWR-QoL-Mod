using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000055 RID: 85
public class AudioButton : MonoBehaviour
{
	// Token: 0x060001AD RID: 429 RVA: 0x00003984 File Offset: 0x00001B84
	private void Start()
	{
		this.button.onClick.AddListener(delegate
		{
			this.PlaySound();
		});
	}

	// Token: 0x060001AE RID: 430 RVA: 0x000039A2 File Offset: 0x00001BA2
	private void PlaySound()
	{
		this.audioSource.Play();
	}

	// Token: 0x04000271 RID: 625
	public Button button;

	// Token: 0x04000272 RID: 626
	public AudioSource audioSource;
}
