using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class AudioGrabbing : MonoBehaviour
{
	// Token: 0x060001B1 RID: 433 RVA: 0x000039B7 File Offset: 0x00001BB7
	private void Start()
	{
		this.playerGrabbing.onGrabToggle += this.OnGrab;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x000039D0 File Offset: 0x00001BD0
	private void OnDestroy()
	{
		this.playerGrabbing.onGrabToggle -= this.OnGrab;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x000039E9 File Offset: 0x00001BE9
	private void OnGrab(bool grabbing)
	{
		if (!grabbing)
		{
			return;
		}
		this.audioSource.clip = this.grabbingSounds[UnityEngine.Random.Range(0, this.grabbingSounds.Length)];
		this.audioSource.Play();
	}

	// Token: 0x04000273 RID: 627
	public PlayerGrabbing playerGrabbing;

	// Token: 0x04000274 RID: 628
	public AudioClip[] grabbingSounds;

	// Token: 0x04000275 RID: 629
	public AudioSource audioSource;
}
