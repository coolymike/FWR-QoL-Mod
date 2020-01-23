using System;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class MineTriggerDetection : MonoBehaviour
{
	// Token: 0x0600023A RID: 570 RVA: 0x00003FFC File Offset: 0x000021FC
	private void OnCollisionEnter(Collision collision)
	{
		this.mine.CollisionDetected(collision);
	}

	// Token: 0x04000338 RID: 824
	public Mine mine;
}
