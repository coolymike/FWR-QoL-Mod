using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class PlayerCameraController : MonoBehaviour
{
	// Token: 0x060000BC RID: 188 RVA: 0x00002B4A File Offset: 0x00000D4A
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		this.playerCamera = base.transform.root.GetComponent<PlayerSettings>().mainCamera;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00002B7D File Offset: 0x00000D7D
	private void Update()
	{
		this.SetFocusObject();
		if (this.focusObject != null && !PlayerSettings.disablePlayerControlls && this.playerCamera.target == CameraController.Target.Player)
		{
			this.UpdateLookRotation();
		}
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00002BAE File Offset: 0x00000DAE
	private void FixedUpdate()
	{
		if (this.focusObject != null)
		{
			this.ViewObstructionAnalysis();
			this.UpdateOriginPosition();
			this.UpdateCameraPosition();
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x0000A4BC File Offset: 0x000086BC
	private void SetFocusObject()
	{
		if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			this.focusObject = this.playerSettings.simulatedRagdoll.bodyElements.GetJoint(this.ragdollCore).transform;
			this.cameraAdjustment = this.cameraOffsetRagdollMode;
			this.followSpeed = this.followSpeedRagdoll;
			return;
		}
		if (this.usePlayerController)
		{
			this.focusObject = this.playerSettings.playerController.transform;
			this.cameraAdjustment = this.cameraOffsetPlayerMode;
		}
		else
		{
			this.focusObject = this.playerSettings.simulatedRagdoll.bodyElements.GetJoint(this.ragdollChest).transform;
			this.cameraAdjustment = this.cameraOffsetPlayerMode;
		}
		this.followSpeed = this.followSpeedController;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00002BD0 File Offset: 0x00000DD0
	private void UpdateOriginPosition()
	{
		this.cameraOriginTargetPosition = this.focusObject.position;
		this.cameraOriginTargetPosition.y = this.cameraOriginTargetPosition.y + this.cameraAdjustment.y;
		base.transform.position = this.cameraOriginTargetPosition;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000A584 File Offset: 0x00008784
	private void UpdateCameraPosition()
	{
		this.cameraTarget.localPosition = new Vector3(this.cameraAdjustment.x + this.cameraCollisionOffsetSmooth.x, this.cameraCollisionOffsetSmooth.y, this.cameraAdjustment.z + this.cameraCollisionOffsetSmooth.z);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000A5DC File Offset: 0x000087DC
	private void UpdateLookRotation()
	{
		this.lookRotation.x = this.lookRotation.x + InputManager.Look().y;
		this.lookRotation.y = this.lookRotation.y + InputManager.Look().x;
		this.lookRotation.x = Mathf.Clamp(this.lookRotation.x, this.rotationLimitX.x, this.rotationLimitX.y);
		this.lookRotationLerp.x = Mathf.Lerp(this.lookRotationLerp.x, this.lookRotation.x, this.lookRotationSmoothing);
		this.lookRotationLerp.y = Mathf.Lerp(this.lookRotationLerp.y, this.lookRotation.y, this.lookRotationSmoothing);
		base.transform.localEulerAngles = new Vector3(this.lookRotationLerp.x, this.lookRotationLerp.y, 0f);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00002C0E File Offset: 0x00000E0E
	public void ResetLookRotation(Vector3 localOffsetRotation)
	{
		this.lookRotation = new Vector2(localOffsetRotation.x, localOffsetRotation.y);
		this.lookRotationLerp = localOffsetRotation;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000A6D0 File Offset: 0x000088D0
	private void ViewObstructionAnalysis()
	{
		this.cameraRayDirectionCenter = (base.transform.TransformPoint(new Vector3(this.cameraAdjustment.x, 0f, this.cameraAdjustment.z)) - base.transform.position) / (base.transform.TransformPoint(new Vector3(this.cameraAdjustment.x, 0f, this.cameraAdjustment.z)) - base.transform.position).magnitude;
		this.cameraRayDirectionRight = (base.transform.TransformPoint(new Vector3(this.cameraAdjustment.x, 0f, this.cameraAdjustment.z) + Vector3.right * 3f) - base.transform.position) / (base.transform.TransformPoint(new Vector3(this.cameraAdjustment.x, 0f, this.cameraAdjustment.z) + Vector3.right * 3f) - base.transform.position).magnitude;
		Physics.Raycast(base.transform.position, this.cameraRayDirectionCenter, out this.hitCameraCenter, float.PositiveInfinity, this.cameraLayerMask);
		Physics.Raycast(base.transform.position, this.cameraRayDirectionRight, out this.hitCameraRight, float.PositiveInfinity, this.cameraLayerMask);
		if (this.hitCameraCenter.transform != null && Tag.CollidesWithCamera(this.hitCameraCenter.transform, true, true))
		{
			this.centerHitDistance = this.hitCameraCenter.distance * 0.5f;
			this.centerHitDistance = Mathf.Clamp(this.centerHitDistance, 0f, Mathf.Abs(this.cameraAdjustment.z));
		}
		else
		{
			this.centerHitDistance = Mathf.Abs(this.cameraAdjustment.z);
		}
		if (this.hitCameraRight.transform != null && Tag.CollidesWithCamera(this.hitCameraRight.transform, true, true))
		{
			this.rightHitDistance = this.hitCameraRight.distance * 0.6f;
			this.rightHitDistance = Mathf.Clamp(this.rightHitDistance, 0f, Mathf.Abs(this.cameraAdjustment.z));
		}
		else
		{
			this.rightHitDistance = Mathf.Abs(this.cameraAdjustment.z);
		}
		this.cameraCollisionOffset.z = this.centerHitDistance / Mathf.Abs(this.cameraAdjustment.z) - 1f;
		this.cameraCollisionOffset.z = this.cameraCollisionOffset.z * Mathf.Abs(this.cameraAdjustment.z);
		this.cameraCollisionOffset.z = this.cameraCollisionOffset.z * -1f;
		this.cameraCollisionOffset.x = (this.rightHitDistance / Mathf.Abs(this.cameraAdjustment.z) - 1f) * Mathf.Abs(this.cameraAdjustment.x);
		this.cameraCollisionOffsetSmooth = Vector3.Lerp(this.cameraCollisionOffsetSmooth, this.cameraCollisionOffset, 20f * Time.deltaTime);
	}

	// Token: 0x0400012A RID: 298
	private PlayerSettings playerSettings;

	// Token: 0x0400012B RID: 299
	private CameraController playerCamera;

	// Token: 0x0400012C RID: 300
	public Transform cameraTarget;

	// Token: 0x0400012D RID: 301
	public RagdollBodyElements.Joint ragdollChest = RagdollBodyElements.Joint.ChestHigh;

	// Token: 0x0400012E RID: 302
	public RagdollBodyElements.Joint ragdollCore;

	// Token: 0x0400012F RID: 303
	public bool usePlayerController;

	// Token: 0x04000130 RID: 304
	private Transform focusObject;

	// Token: 0x04000131 RID: 305
	public Vector2 rotationLimitX = new Vector2(-70f, 80f);

	// Token: 0x04000132 RID: 306
	public Vector3 cameraOffsetPlayerMode = new Vector3(1f, 1.8f, -2.6f);

	// Token: 0x04000133 RID: 307
	public Vector3 cameraOffsetRagdollMode = new Vector3(0f, 0f, -4f);

	// Token: 0x04000134 RID: 308
	private Vector3 cameraAdjustment;

	// Token: 0x04000135 RID: 309
	public float followSpeedController = 15f;

	// Token: 0x04000136 RID: 310
	public float followSpeedRagdoll = 50f;

	// Token: 0x04000137 RID: 311
	private float followSpeed;

	// Token: 0x04000138 RID: 312
	[HideInInspector]
	public Vector2 lookRotation;

	// Token: 0x04000139 RID: 313
	public float lookRotationSmoothing = 25f;

	// Token: 0x0400013A RID: 314
	private Vector3 cameraOriginTargetPosition;

	// Token: 0x0400013B RID: 315
	[HideInInspector]
	public Vector3 lookRotationLerp;

	// Token: 0x0400013C RID: 316
	private RaycastHit hitCameraRight;

	// Token: 0x0400013D RID: 317
	private RaycastHit hitCameraCenter;

	// Token: 0x0400013E RID: 318
	private float centerHitDistance;

	// Token: 0x0400013F RID: 319
	private float rightHitDistance;

	// Token: 0x04000140 RID: 320
	private Vector3 cameraCollisionOffset;

	// Token: 0x04000141 RID: 321
	private Vector3 cameraCollisionOffsetSmooth = Vector3.zero;

	// Token: 0x04000142 RID: 322
	private Vector3 cameraRayDirectionDown;

	// Token: 0x04000143 RID: 323
	private Vector3 cameraRayDirectionRight;

	// Token: 0x04000144 RID: 324
	private Vector3 cameraRayDirectionCenter;

	// Token: 0x04000145 RID: 325
	private LayerMask cameraLayerMask = -17925;
}
