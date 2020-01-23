using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class PlayerActiveRagdoll : MonoBehaviour
{
	// Token: 0x060000B8 RID: 184 RVA: 0x00002AF4 File Offset: 0x00000CF4
	private void Start()
	{
		this.ragdollSettings = base.GetComponent<PlayerSettings>().simulatedRagdoll;
		this.ragdollCore = this.ragdollSettings.bodyElements.GetJoint(RagdollBodyElements.Joint.Core).transform;
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00002B23 File Offset: 0x00000D23
	private void FixedUpdate()
	{
		if (GameData.cheats.activeRagdoll)
		{
			this.ActiveRagdoll();
		}
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0000A3E0 File Offset: 0x000085E0
	private void ActiveRagdoll()
	{
		if (Physics.Raycast(this.ragdollCore.position, Vector3.down, out this.hitInfo, float.PositiveInfinity, -1025) && this.hitInfo.distance <= this.maxGroundDistance)
		{
			this.groundDistance = this.hitInfo.distance;
		}
		else
		{
			this.groundDistance = this.maxGroundDistance;
		}
		if (this.groundDistance >= 8f && this.ragdollSettings.RagdollModeEnabled)
		{
			this.ragdollStrength = this.groundDistance * 20f;
		}
		else
		{
			this.ragdollStrength = 0f;
			this.ragdollSettings.ragdollModeStrength = 0f;
		}
		this.ragdollSettings.ragdollModeStrength = Mathf.Lerp(this.ragdollSettings.ragdollModeStrength, this.ragdollStrength, Time.deltaTime * 0.1f);
	}

	// Token: 0x04000124 RID: 292
	private RagdollSettings ragdollSettings;

	// Token: 0x04000125 RID: 293
	private Transform ragdollCore;

	// Token: 0x04000126 RID: 294
	private RaycastHit hitInfo;

	// Token: 0x04000127 RID: 295
	private float groundDistance;

	// Token: 0x04000128 RID: 296
	private float maxGroundDistance = 15f;

	// Token: 0x04000129 RID: 297
	private float ragdollStrength;
}
