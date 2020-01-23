using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class CanonAim : MonoBehaviour
{
	// Token: 0x060001C2 RID: 450 RVA: 0x0000EAEC File Offset: 0x0000CCEC
	private void Start()
	{
		this.playerSettings = PlayerSettings.instance;
		this.playerTargetJoint = this.playerSettings.simulatedRagdoll.bodyElements.GetJoint(RagdollBodyElements.Joint.ChestHigh).transform;
		this.vehicle = base.GetComponent<Vehicle>();
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00003A96 File Offset: 0x00001C96
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00003AA9 File Offset: 0x00001CA9
	private void FixedUpdate()
	{
		this.AimLogic();
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x0000EB44 File Offset: 0x0000CD44
	private void AimLogic()
	{
		if (!LevelManager.BuildModeOn)
		{
			if (this.vehicle != null && this.vehicle.playerControlling != null)
			{
				this.PlayerLook();
			}
			else if (this.targetPlayer)
			{
				this.FollowTargetObject(this.playerTargetJoint);
			}
			else if (this.targetRagdolls)
			{
				this.FollowTargetObject(this.LockOnRagdoll());
			}
			else
			{
				this.FollowTargetObject(this.targetObject);
			}
			this.audioSource.volume = Vector3.Distance(this.canonBarrel.eulerAngles, this.lastEularRotation) * 0.3f;
			this.lastEularRotation = this.canonBarrel.eulerAngles;
			return;
		}
		this.audioSource.volume = 0f;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00003AB1 File Offset: 0x00001CB1
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (buildModeOn && this.resetAimOnBuildMode)
		{
			this.ResetLook();
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x0000EC08 File Offset: 0x0000CE08
	private void PlayerLook()
	{
		this.look.x = this.look.x + InputManager.Move(true).x * this.playerAimSpeed;
		this.look.y = this.look.y + InputManager.Move(true).y * this.playerAimSpeed * -0.8f;
		this.look.y = Mathf.Clamp(this.look.y, -90f, 40f);
		this.canonLookRotation = Quaternion.Slerp(this.canonRotator.localRotation, Quaternion.Euler(0f, this.look.x, 0f), this.smoothRotation * Time.deltaTime);
		this.barrelLookRotation = Quaternion.Slerp(this.canonBarrel.localRotation, Quaternion.Euler(this.look.y, 0f, 0f), this.smoothRotation * Time.deltaTime);
		this.canonRotator.localRotation = this.canonLookRotation;
		this.canonBarrel.localRotation = this.barrelLookRotation;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x0000ED1C File Offset: 0x0000CF1C
	private void FollowTargetObject(Transform target)
	{
		if (target == null)
		{
			return;
		}
		this.canonLookRotation = Quaternion.LookRotation(target.position - this.canonRotator.position);
		this.canonLookRotation = Quaternion.RotateTowards(this.canonRotator.rotation, this.canonLookRotation, this.aiSmoothRotation * Time.deltaTime);
		this.canonRotator.eulerAngles = new Vector3(0f, this.canonLookRotation.eulerAngles.y, 0f);
		this.barrelLookRotation = Quaternion.LookRotation(this.canonBarrel.parent.InverseTransformPoint(target.position) - this.canonBarrel.localPosition);
		this.barrelLookRotation = Quaternion.Slerp(this.canonBarrel.rotation, this.barrelLookRotation, this.smoothRotation * Time.deltaTime);
		this.canonBarrel.localEulerAngles = new Vector3(this.barrelLookRotation.eulerAngles.x, 0f, 0f);
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000EE2C File Offset: 0x0000D02C
	private Transform LockOnRagdoll()
	{
		if (SmartRagdollController.activeRagdollsInScene.Count == 0)
		{
			return null;
		}
		if (this.closestRagdoll == null)
		{
			this.AssignClosestRagdoll();
			if (this.closestRagdoll != null)
			{
				return this.closestRagdoll.simulatedRagdollCore;
			}
			return null;
		}
		else if (this.closestRagdoll.simulatedRagdoll.RagdollModeEnabled || !this.closestRagdoll.isActiveAndEnabled)
		{
			this.AssignClosestRagdoll();
			if (this.closestRagdoll != null)
			{
				return this.closestRagdoll.simulatedRagdollCore;
			}
			return null;
		}
		else
		{
			if (Vector3.Distance(this.closestRagdoll.simulatedRagdollCore.position, this.canonBarrel.position) <= 24f)
			{
				return this.closestRagdoll.simulatedRagdollCore;
			}
			this.AssignClosestRagdoll();
			if (this.closestRagdoll != null)
			{
				return this.closestRagdoll.simulatedRagdollCore;
			}
			return null;
		}
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00003AC4 File Offset: 0x00001CC4
	private void AssignClosestRagdoll()
	{
		this.closestRagdoll = RagdollDatabase.ClosestRagdoll(this.canonBarrel.position);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00003ADC File Offset: 0x00001CDC
	private void ResetLook()
	{
		this.canonRotator.localEulerAngles = Vector3.zero;
		this.canonBarrel.localEulerAngles = Vector3.zero;
	}

	// Token: 0x04000289 RID: 649
	private PlayerSettings playerSettings;

	// Token: 0x0400028A RID: 650
	private Transform playerTargetJoint;

	// Token: 0x0400028B RID: 651
	private Vehicle vehicle;

	// Token: 0x0400028C RID: 652
	private readonly float playerAimSpeed = 1.2f;

	// Token: 0x0400028D RID: 653
	public bool resetAimOnBuildMode = true;

	// Token: 0x0400028E RID: 654
	public bool targetPlayer;

	// Token: 0x0400028F RID: 655
	public bool targetRagdolls;

	// Token: 0x04000290 RID: 656
	public Transform targetObject;

	// Token: 0x04000291 RID: 657
	public Transform canonRotator;

	// Token: 0x04000292 RID: 658
	public Transform canonBarrel;

	// Token: 0x04000293 RID: 659
	public float aiSmoothRotation = 150f;

	// Token: 0x04000294 RID: 660
	private readonly float smoothRotation = 10f;

	// Token: 0x04000295 RID: 661
	private Quaternion canonLookRotation;

	// Token: 0x04000296 RID: 662
	private Quaternion barrelLookRotation;

	// Token: 0x04000297 RID: 663
	[Header("Audio")]
	public AudioSource audioSource;

	// Token: 0x04000298 RID: 664
	private Vector3 lastEularRotation;

	// Token: 0x04000299 RID: 665
	private Vector2 look;

	// Token: 0x0400029A RID: 666
	private SmartRagdollController closestRagdoll;
}
