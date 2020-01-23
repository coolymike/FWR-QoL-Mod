using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class PlayAnimation : MonoBehaviour
{
	// Token: 0x060003E2 RID: 994 RVA: 0x00005436 File Offset: 0x00003636
	private void OnEnable()
	{
		base.StartCoroutine(this.Play(UnityEngine.Random.Range(this.waitTimeRange.x, this.waitTimeRange.y)));
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00005460 File Offset: 0x00003660
	private void OnDisable()
	{
		if (this.animationSystem.isPlaying)
		{
			this.animationSystem.Stop();
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0000547A File Offset: 0x0000367A
	private IEnumerator Play(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		this.animationSystem.Play();
		yield break;
	}

	// Token: 0x04000546 RID: 1350
	public Animation animationSystem;

	// Token: 0x04000547 RID: 1351
	public Vector2 waitTimeRange;
}
