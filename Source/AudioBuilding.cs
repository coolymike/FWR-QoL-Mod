using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class AudioBuilding : MonoBehaviour
{
	// Token: 0x060001A6 RID: 422 RVA: 0x00003880 File Offset: 0x00001A80
	private void Start()
	{
		this.playerBuildSystem.OnPlace += this.OnPlace;
		this.playerBuildSystem.OnRemove += this.OnRemove;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x000038B0 File Offset: 0x00001AB0
	private void OnDestroy()
	{
		this.playerBuildSystem.OnPlace -= this.OnPlace;
		this.playerBuildSystem.OnRemove -= this.OnRemove;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x000038E0 File Offset: 0x00001AE0
	private void OnPlace()
	{
		if (!this.canPlayPlace)
		{
			return;
		}
		this.audioSource.clip = this.placedClip;
		this.audioSource.Play();
		this.canPlayPlace = false;
		base.Invoke("CanPlayPlace", 0.05f);
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000391E File Offset: 0x00001B1E
	private void CanPlayPlace()
	{
		this.canPlayPlace = true;
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00003927 File Offset: 0x00001B27
	private void OnRemove()
	{
		if (!this.canPlayRemove)
		{
			return;
		}
		this.audioSource.clip = this.removedClip;
		this.audioSource.Play();
		this.canPlayRemove = false;
		base.Invoke("CanPlayRemove", 0.05f);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00003965 File Offset: 0x00001B65
	private void CanPlayRemove()
	{
		this.canPlayRemove = true;
	}

	// Token: 0x0400026B RID: 619
	public PlayerBuildSystem playerBuildSystem;

	// Token: 0x0400026C RID: 620
	public AudioClip placedClip;

	// Token: 0x0400026D RID: 621
	public AudioClip removedClip;

	// Token: 0x0400026E RID: 622
	public AudioSource audioSource;

	// Token: 0x0400026F RID: 623
	private bool canPlayPlace = true;

	// Token: 0x04000270 RID: 624
	private bool canPlayRemove = true;
}
