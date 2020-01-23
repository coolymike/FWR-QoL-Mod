using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class RagdollPositionToController : MonoBehaviour
{
	// Token: 0x0600013D RID: 317 RVA: 0x000031F1 File Offset: 0x000013F1
	private void Start()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		this.animatedRagdoll = base.gameObject;
		this.characterController = this.controller.GetComponent<CharacterController>();
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00003226 File Offset: 0x00001426
	private void FixedUpdate()
	{
		this.animatedRagdoll.transform.position = this.controller.transform.position;
		if (this.controllerIsAgent)
		{
			this.UpdateRotationForAgent();
			return;
		}
		this.UpdateRotationForController();
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
	private void UpdateRotationForAgent()
	{
		this.animatedRagdoll.transform.eulerAngles = new Vector3(0f, this.controller.transform.rotation.eulerAngles.y, 0f);
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000D434 File Offset: 0x0000B634
	private void UpdateRotationForController()
	{
		this.playerControllerVelocity = new Vector3(this.characterController.velocity.x, 0f, this.characterController.velocity.z);
		if ((double)this.playerControllerVelocity.magnitude > 0.1)
		{
			this.lastSavedPlayerVelocity = this.playerControllerVelocity;
		}
		if (this.lastSavedPlayerVelocity == Vector3.zero)
		{
			this.lastSavedPlayerVelocity = base.gameObject.transform.forward;
		}
		this.animatedRagdoll.transform.eulerAngles = new Vector3(this.animatedRagdoll.transform.eulerAngles.x, Mathf.LerpAngle(this.animatedRagdoll.transform.eulerAngles.y, Quaternion.LookRotation(this.lastSavedPlayerVelocity).eulerAngles.y, 6f * Time.deltaTime), this.animatedRagdoll.transform.eulerAngles.z);
		this.animatedRagdoll.transform.localEulerAngles = new Vector3(this.animatedRagdoll.transform.localEulerAngles.x, this.animatedRagdoll.transform.localEulerAngles.y, Mathf.LerpAngle(this.animatedRagdoll.transform.localEulerAngles.z, Mathf.Clamp(this.animatedRagdoll.transform.InverseTransformDirection(this.playerControllerVelocity).x * -3.5f * this.playerSettings.playerController.velocityXZ, -7f, 7f), 12f * Time.deltaTime));
	}

	// Token: 0x040001F4 RID: 500
	public GameObject controller;

	// Token: 0x040001F5 RID: 501
	private PlayerSettings playerSettings;

	// Token: 0x040001F6 RID: 502
	private CharacterController characterController;

	// Token: 0x040001F7 RID: 503
	private GameObject animatedRagdoll;

	// Token: 0x040001F8 RID: 504
	public bool controllerIsAgent;

	// Token: 0x040001F9 RID: 505
	private Vector3 playerControllerVelocity;

	// Token: 0x040001FA RID: 506
	private Vector3 lastSavedPlayerVelocity;

	// Token: 0x040001FB RID: 507
	private Quaternion direction;

	// Token: 0x040001FC RID: 508
	private Vector3 moveDirection;

	// Token: 0x040001FD RID: 509
	private Vector3 relativePos;
}
