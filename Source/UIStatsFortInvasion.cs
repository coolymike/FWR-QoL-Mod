using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009B RID: 155
public class UIStatsFortInvasion : MonoBehaviour
{
	// Token: 0x06000319 RID: 793 RVA: 0x00014A20 File Offset: 0x00012C20
	private void Update()
	{
		this.invadersText.text = this.fortInvasionStats.totalUnacceptableRagdolls + " : Invaders";
		this.peacefulText.text = "Peaceful : " + this.fortInvasionStats.totalAcceptableRagdolls;
	}

	// Token: 0x0400047E RID: 1150
	public Text invadersText;

	// Token: 0x0400047F RID: 1151
	public Text peacefulText;

	// Token: 0x04000480 RID: 1152
	public StatsFortInvasion fortInvasionStats;
}
