using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class PlaySoundOnEnable : MonoBehaviour
{
	// Token: 0x0600019B RID: 411 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
	private void OnEnable()
	{
		if (this.audioSource != null && this.audioClips.Length != 0)
		{
			this.audioSource.clip = this.audioClips[UnityEngine.Random.Range(0, this.audioClips.Length)];
			this.audioSource.pitch = UnityEngine.Random.Range(this.randomPitchRange.x, this.randomPitchRange.y);
			this.audioSource.Play();
		}
	}

	// Token: 0x04000265 RID: 613
	public AudioSource audioSource;

	// Token: 0x04000266 RID: 614
	public Vector2 randomPitchRange = new Vector2(0.8f, 1.2f);

	// Token: 0x04000267 RID: 615
	public AudioClip[] audioClips;
}
