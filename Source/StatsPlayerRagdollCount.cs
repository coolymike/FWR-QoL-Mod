using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class StatsPlayerRagdollCount : MonoBehaviour
{
	// Token: 0x0600067F RID: 1663 RVA: 0x0001D268 File Offset: 0x0001B468
	private void Update()
	{
		if (PlayerSettings.instance.simulatedRagdoll.RagdollModeEnabled && !this.loggedRagdollCount)
		{
			this.playerRagdollCount++;
			this.loggedRagdollCount = true;
		}
		else if (!PlayerSettings.instance.simulatedRagdoll.RagdollModeEnabled)
		{
			this.loggedRagdollCount = false;
		}
		ScoreBoard.UpdateScoreBoard("Ragdoll Count", this.playerRagdollCount.ToString(), false);
	}

	// Token: 0x04000774 RID: 1908
	public int playerRagdollCount;

	// Token: 0x04000775 RID: 1909
	private bool loggedRagdollCount;
}
