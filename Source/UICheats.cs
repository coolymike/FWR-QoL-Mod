using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A0 RID: 160
public class UICheats : MonoBehaviour
{
	// Token: 0x0600032B RID: 811 RVA: 0x00014DCC File Offset: 0x00012FCC
	private void Start()
	{
		this.activeRagdollToggle.isOn = GameData.cheats.activeRagdoll;
		this.slowMotionInPlayModeToggle.isOn = GameData.cheats.slowMotionInPlayMode;
		this.ignoreBuildCollisionCheckToggle.isOn = GameData.cheats.ignoreBuildCollisionCheck;
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00004C62 File Offset: 0x00002E62
	public void UpdateActiveRagdoll()
	{
		GameData.cheats.activeRagdoll = this.activeRagdollToggle.isOn;
		GameData.SaveCheats();
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00004C7E File Offset: 0x00002E7E
	public void UpdateSlowMotionInPlayMode()
	{
		GameData.cheats.slowMotionInPlayMode = this.slowMotionInPlayModeToggle.isOn;
		GameData.SaveCheats();
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00004C9A File Offset: 0x00002E9A
	public void UpdateIgnoreBuildCollisionCheck()
	{
		GameData.cheats.ignoreBuildCollisionCheck = this.ignoreBuildCollisionCheckToggle.isOn;
		GameData.SaveCheats();
	}

	// Token: 0x0400049A RID: 1178
	public Toggle activeRagdollToggle;

	// Token: 0x0400049B RID: 1179
	public Toggle slowMotionInPlayModeToggle;

	// Token: 0x0400049C RID: 1180
	public Toggle ignoreBuildCollisionCheckToggle;
}
