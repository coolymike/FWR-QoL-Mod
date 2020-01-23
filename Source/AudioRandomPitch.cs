using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class AudioRandomPitch : MonoBehaviour
{
	// Token: 0x060001A4 RID: 420 RVA: 0x0000383B File Offset: 0x00001A3B
	private void OnEnable()
	{
		this.audioSource.pitch = UnityEngine.Random.Range(this.randomPitchRange.x, this.randomPitchRange.y);
	}

	// Token: 0x04000269 RID: 617
	public AudioSource audioSource;

	// Token: 0x0400026A RID: 618
	public Vector2 randomPitchRange = new Vector2(0.8f, 1.2f);
}
