using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class GameGoal : MonoBehaviour
{
	// Token: 0x060003EC RID: 1004 RVA: 0x00017240 File Offset: 0x00015440
	private void Update()
	{
		this.BeamPower();
		if (this.shortBeam)
		{
			this.beam.localPosition = new Vector3(0f, 1f, 0f);
			this.beam.localScale = new Vector3(4f, 1f, 4f);
			return;
		}
		this.beam.localPosition = new Vector3(0f, 500f, 0f);
		this.beam.localScale = new Vector3(4f, 500f, 4f);
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x000172D8 File Offset: 0x000154D8
	private void OnTriggerEnter(Collider other)
	{
		if (Tag.Compare(other.transform, Tag.Tags.Player) && LevelManager.instance.winState == LevelManager.WinState.None && (this.allowRagdollModeForWin || (!this.allowRagdollModeForWin && !PlayerSettings.instance.simulatedRagdoll.RagdollModeEnabled)))
		{
			LevelManager.instance.winState = LevelManager.WinState.Win;
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0001732C File Offset: 0x0001552C
	private void BeamPower()
	{
		if (LevelManager.instance.winState != LevelManager.WinState.None)
		{
			this.beamPower = Mathf.Lerp(this.beamPower, 0f, Time.fixedDeltaTime * 5f);
			return;
		}
		this.beamPower = Mathf.Lerp(this.beamPower, 1f, Time.fixedDeltaTime * 5f);
	}

	// Token: 0x0400054C RID: 1356
	public bool allowRagdollModeForWin;

	// Token: 0x0400054D RID: 1357
	public Transform beam;

	// Token: 0x0400054E RID: 1358
	public bool shortBeam;

	// Token: 0x0400054F RID: 1359
	private float beamPower = 1f;
}
