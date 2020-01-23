using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class TutorialPlatformJumpZone : MonoBehaviour
{
	// Token: 0x14000018 RID: 24
	// (add) Token: 0x060006C3 RID: 1731 RVA: 0x0001F578 File Offset: 0x0001D778
	// (remove) Token: 0x060006C4 RID: 1732 RVA: 0x0001F5AC File Offset: 0x0001D7AC
	public static event TutorialPlatformJumpZone.JumpedOnPlatformHandler JumpedOnPlatform;

	// Token: 0x060006C5 RID: 1733 RVA: 0x00007638 File Offset: 0x00005838
	private void OnTriggerEnter(Collider other)
	{
		if (Tag.Compare(other.transform, Tag.Tags.Player) && TutorialPlatformJumpZone.JumpedOnPlatform != null)
		{
			TutorialPlatformJumpZone.JumpedOnPlatform();
		}
	}

	// Token: 0x02000133 RID: 307
	// (Invoke) Token: 0x060006C8 RID: 1736
	public delegate void JumpedOnPlatformHandler();
}
