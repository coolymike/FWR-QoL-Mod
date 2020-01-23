using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000045 RID: 69
[Serializable]
public class RagdollSettings : MonoBehaviour
{
	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000142 RID: 322 RVA: 0x0000325D File Offset: 0x0000145D
	// (set) Token: 0x06000143 RID: 323 RVA: 0x00003265 File Offset: 0x00001465
	public CollisionDetectionMode CollisionDetectionMode
	{
		get
		{
			return this.collisionDetectionMode;
		}
		set
		{
			this.collisionDetectionMode = value;
			this.UpdateCollisionDetectionMode();
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000144 RID: 324 RVA: 0x00003274 File Offset: 0x00001474
	// (set) Token: 0x06000145 RID: 325 RVA: 0x0000327C File Offset: 0x0000147C
	public bool RagdollModeEnabled
	{
		get
		{
			return this.ragdollModeEnabled;
		}
		set
		{
			if (this.ragdollModeEnabled == value)
			{
				return;
			}
			this.ragdollModeEnabled = value;
			if (this.OnRagdollToggle != null)
			{
				this.OnRagdollToggle(value);
			}
			this.RagdollResetIndicator(value);
		}
	}

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000146 RID: 326 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
	// (remove) Token: 0x06000147 RID: 327 RVA: 0x0000D618 File Offset: 0x0000B818
	public event RagdollSettings.RagdollModeHandler OnRagdollToggle;

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000148 RID: 328 RVA: 0x000032AA File Offset: 0x000014AA
	// (set) Token: 0x06000149 RID: 329 RVA: 0x000032B2 File Offset: 0x000014B2
	public bool RagdollIsDismembered
	{
		get
		{
			return this.ragdollIsDismembered;
		}
		set
		{
			if (this.ragdollIsDismembered == value)
			{
				return;
			}
			this.ragdollIsDismembered = value;
			if (!this.RagdollModeEnabled && value)
			{
				this.RagdollModeEnabled = true;
			}
		}
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000D650 File Offset: 0x0000B850
	private void Awake()
	{
		this.boneRetargeting = base.GetComponent<BoneRetargeting>();
		for (int i = 0; i < this.bodyElements.ragdollJoints.Length; i++)
		{
			this.jointStartPositions.Add(this.bodyElements.ragdollJoints[i], this.bodyElements.ragdollJoints[i].transform.localPosition);
		}
		LevelManager.OnBuildModeToggle += this.ResetOnBuildMode;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000032D9 File Offset: 0x000014D9
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.ResetOnBuildMode;
	}

	// Token: 0x0600014C RID: 332 RVA: 0x000032EC File Offset: 0x000014EC
	private void Start()
	{
		this.rigidbodyChildren = base.GetComponentsInChildren<Rigidbody>();
		this.boxColliderChildren = this.bodyElements.ragdollJoints[0].GetComponentsInChildren<BoxCollider>();
		this.jointStartPositionStrength = this.jointPositionStrength;
		this.jointStartRotationStrength = this.jointRotationStrength;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000332A File Offset: 0x0000152A
	private void RagdollResetIndicator(bool ragdollMode)
	{
		if (!ragdollMode && this.enableCooldown)
		{
			if (this.ragdollInCooldown)
			{
				base.CancelInvoke("DisableRagdollResetIndicator");
			}
			this.ragdollInCooldown = true;
			base.Invoke("DisableRagdollResetIndicator", this.ragdollResetCooldown);
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00003362 File Offset: 0x00001562
	private void DisableRagdollResetIndicator()
	{
		this.ragdollInCooldown = false;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000336B File Offset: 0x0000156B
	private void ResetOnBuildMode(bool buildModeIsOn)
	{
		if (!buildModeIsOn)
		{
			return;
		}
		if (this.resetRagdollPositionOnBuildMode)
		{
			this.ResetRagdoll();
		}
		if (this.resetRagdollModeOnBuildMode)
		{
			this.RagdollModeEnabled = false;
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000338E File Offset: 0x0000158E
	private void FixedUpdate()
	{
		this.UpdateCollisionDetectionMode();
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00003396 File Offset: 0x00001596
	private void EnableRagdollOnDismemberment()
	{
		if (!this.RagdollModeEnabled && this.RagdollIsDismembered)
		{
			this.RagdollModeEnabled = true;
		}
	}

	// Token: 0x06000152 RID: 338 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
	public void UpdateCollisionDetectionMode()
	{
		for (int i = 0; i < this.rigidbodyChildren.Length; i++)
		{
			this.rigidbodyChildren[i].collisionDetectionMode = this.collisionDetectionMode;
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
	public void ResetRagdoll()
	{
		this.ResetJointVelocity();
		for (int i = 0; i < this.bodyElements.ragdollJoints.Length; i++)
		{
			this.bodyElements.ragdollJoints[i].transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			if (this.jointStartPositions.ContainsKey(this.bodyElements.ragdollJoints[i]))
			{
				this.bodyElements.ragdollJoints[i].transform.localPosition = this.jointStartPositions[this.bodyElements.ragdollJoints[i]];
			}
		}
		this.RagdollIsDismembered = false;
		if (this.boneRetargeting != null)
		{
			this.boneRetargeting.FixAllJointMotionLimits();
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000D7BC File Offset: 0x0000B9BC
	public void ResetJointVelocity()
	{
		if (this.boneRetargeting != null)
		{
			this.boneRetargeting.skipRetargetOnce = true;
		}
		for (int i = 0; i < this.rigidbodyChildren.Length; i++)
		{
			this.rigidbodyChildren[i].velocity = new Vector3(0f, 0f, 0f);
			this.rigidbodyChildren[i].angularVelocity = new Vector3(0f, 0f, 0f);
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000D838 File Offset: 0x0000BA38
	public static void DismemberJoint(Transform theObject)
	{
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		ConfigurableJoint component = theObject.GetComponent<ConfigurableJoint>();
		if (component != null)
		{
			BoneRetargeting componentInParent = theObject.GetComponentInParent<BoneRetargeting>();
			if (componentInParent != null)
			{
				componentInParent.DisableLimbFromRagdoll(component);
			}
		}
	}

	// Token: 0x040001FE RID: 510
	public RagdollBodyElements bodyElements;

	// Token: 0x040001FF RID: 511
	private BoneRetargeting boneRetargeting;

	// Token: 0x04000200 RID: 512
	[Space]
	[Header("Physics Settings")]
	public bool physicsEnabled = true;

	// Token: 0x04000201 RID: 513
	private CollisionDetectionMode collisionDetectionMode;

	// Token: 0x04000202 RID: 514
	[Space]
	public bool resetRagdollPositionOnBuildMode = true;

	// Token: 0x04000203 RID: 515
	public bool resetRagdollModeOnBuildMode = true;

	// Token: 0x04000204 RID: 516
	public bool ragdollModeEnabled;

	// Token: 0x04000206 RID: 518
	public bool enableBoneRetargeting = true;

	// Token: 0x04000207 RID: 519
	private bool ragdollIsDismembered;

	// Token: 0x04000208 RID: 520
	[Space]
	public bool enableCooldown;

	// Token: 0x04000209 RID: 521
	public bool ragdollInCooldown;

	// Token: 0x0400020A RID: 522
	private float ragdollResetCooldown = 1.5f;

	// Token: 0x0400020B RID: 523
	[HideInInspector]
	public float jointStartPositionStrength;

	// Token: 0x0400020C RID: 524
	[HideInInspector]
	public float jointStartRotationStrength;

	// Token: 0x0400020D RID: 525
	[Space]
	public float jointPositionStrength = 1000f;

	// Token: 0x0400020E RID: 526
	public float jointRotationStrength = 600f;

	// Token: 0x0400020F RID: 527
	public float ragdollModeStrength;

	// Token: 0x04000210 RID: 528
	[HideInInspector]
	public Rigidbody[] rigidbodyChildren;

	// Token: 0x04000211 RID: 529
	private BoxCollider[] boxColliderChildren;

	// Token: 0x04000212 RID: 530
	public Dictionary<GameObject, Vector3> jointStartPositions = new Dictionary<GameObject, Vector3>();

	// Token: 0x02000046 RID: 70
	// (Invoke) Token: 0x06000158 RID: 344
	public delegate void RagdollModeHandler(bool RagdollIsActive);
}
