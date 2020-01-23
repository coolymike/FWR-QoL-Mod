using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class PlayerControlRagdoll : MonoBehaviour
{
	// Token: 0x060000F2 RID: 242 RVA: 0x00002DCC File Offset: 0x00000FCC
	private void Start()
	{
		this.playerSettings = base.GetComponent<PlayerSettings>();
		this.rigidbodyJoint = this.playerSettings.simulatedRagdoll.bodyElements.GetJoint(this.ragdollTargetJoint).GetComponent<Rigidbody>();
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000B4D0 File Offset: 0x000096D0
	private void FixedUpdate()
	{
		if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled && !LevelManager.StatusIsWinOrLose(true) && !this.playerSettings.simulatedRagdoll.RagdollIsDismembered)
		{
			this.forceDirection = new Vector3(InputManager.Move(true).x, InputManager.Move(true).y, InputManager.Move(true).y / 2f);
			this.forceDirection = this.playerSettings.mainCamera.transform.TransformDirection(this.forceDirection);
			this.rigidbodyJoint.AddForce(new Vector3(this.forceDirection.x, 0f, this.forceDirection.z) * (float)this.forceStrength, ForceMode.Acceleration);
		}
	}

	// Token: 0x0400016A RID: 362
	private PlayerSettings playerSettings;

	// Token: 0x0400016B RID: 363
	public RagdollBodyElements.Joint ragdollTargetJoint = RagdollBodyElements.Joint.ChestHigh;

	// Token: 0x0400016C RID: 364
	public int forceStrength = 100;

	// Token: 0x0400016D RID: 365
	private Rigidbody rigidbodyJoint;

	// Token: 0x0400016E RID: 366
	private Vector3 forceDirection;
}
