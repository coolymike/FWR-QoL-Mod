using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003E RID: 62
[RequireComponent(typeof(RagdollSettings))]
[RequireComponent(typeof(RagdollBodyElements))]
public class BoneRetargeting : MonoBehaviour
{
	// Token: 0x06000121 RID: 289 RVA: 0x000030F3 File Offset: 0x000012F3
	private void Awake()
	{
		this.ragdollSettings = base.GetComponent<RagdollSettings>();
		if (this.animatedRagdoll != null)
		{
			this.animatedRagdollBodyElements = this.animatedRagdoll.GetComponent<RagdollBodyElements>();
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000BE0C File Offset: 0x0000A00C
	private void Start()
	{
		for (int i = 0; i < this.ragdollSettings.bodyElements.ragdollJoints.Length; i++)
		{
			if (!(this.ragdollSettings.bodyElements.ragdollJoints[i].GetComponent<ConfigurableJoint>() == null))
			{
				this.cJointsSimulated.Add(this.ragdollSettings.bodyElements.ragdollJoints[i].GetComponent<ConfigurableJoint>());
				this.rbJointsSimulated.Add(this.ragdollSettings.bodyElements.ragdollJoints[i].gameObject.GetComponent<Rigidbody>());
				if (this.animatedRagdoll != null)
				{
					this.cJointsAnimated.Add(this.animatedRagdollBodyElements.ragdollJoints[i]);
				}
			}
		}
		this.cJointsSimulatedCount = (float)this.cJointsSimulated.Count;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000BEDC File Offset: 0x0000A0DC
	private void FixedUpdate()
	{
		if (!this.skipRetargetOnce)
		{
			if (this.ragdollSettings.enableBoneRetargeting && !this.ragdollSettings.RagdollIsDismembered)
			{
				this.Retarget();
				this.setJointsToZero = false;
				return;
			}
			if (!this.setJointsToZero)
			{
				this.SetJointStrengthToZero();
				this.setJointsToZero = true;
				return;
			}
		}
		else
		{
			this.skipRetargetOnce = false;
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000BF38 File Offset: 0x0000A138
	private void Retarget()
	{
		if (this.animatedRagdoll != null)
		{
			int num = 0;
			while ((float)num < this.cJointsSimulatedCount)
			{
				this.sJointPosition = this.cJointsSimulated[num].transform.position;
				this.aJointPosition = this.cJointsAnimated[num].transform.position;
				this.cJointsSimulated[num].targetRotation = this.cJointsAnimated[num].transform.localRotation;
				if (!this.ragdollSettings.RagdollModeEnabled)
				{
					this.rbJointsSimulated[num].velocity = (this.aJointPosition - this.sJointPosition) * this.ragdollSettings.jointPositionStrength * Time.fixedDeltaTime;
					this.drive.positionSpring = this.ragdollSettings.jointRotationStrength * (LevelManager.SlowMotion ? 8f : 1f);
					this.drive.maximumForce = this.ragdollSettings.jointRotationStrength * (LevelManager.SlowMotion ? 8f : 1f);
					if (this.cJointsSimulated[num].angularXMotion != ConfigurableJointMotion.Free)
					{
						this.cJointsSimulated[num].angularXMotion = ConfigurableJointMotion.Free;
						this.cJointsSimulated[num].angularYMotion = ConfigurableJointMotion.Free;
						this.cJointsSimulated[num].angularZMotion = ConfigurableJointMotion.Free;
					}
				}
				else
				{
					this.drive.positionSpring = this.ragdollSettings.ragdollModeStrength;
					this.drive.maximumForce = this.ragdollSettings.ragdollModeStrength;
					if (this.cJointsSimulated[num].angularXMotion != ConfigurableJointMotion.Limited)
					{
						this.cJointsSimulated[num].angularXMotion = ConfigurableJointMotion.Limited;
						this.cJointsSimulated[num].angularYMotion = ConfigurableJointMotion.Limited;
						this.cJointsSimulated[num].angularZMotion = ConfigurableJointMotion.Limited;
					}
				}
				this.drive.positionDamper = this.positionDampaning;
				this.cJointsSimulated[num].slerpDrive = this.drive;
				num++;
			}
		}
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000C15C File Offset: 0x0000A35C
	private void SetJointStrengthToZero()
	{
		int num = 0;
		while ((float)num < this.cJointsSimulatedCount)
		{
			this.sJointPosition = this.cJointsSimulated[num].transform.position;
			this.drive.positionSpring = 0f;
			this.drive.maximumForce = 0f;
			this.cJointsSimulated[num].angularXMotion = ConfigurableJointMotion.Limited;
			this.cJointsSimulated[num].angularYMotion = ConfigurableJointMotion.Limited;
			this.cJointsSimulated[num].angularZMotion = ConfigurableJointMotion.Limited;
			this.drive.positionDamper = this.positionDampaning;
			this.cJointsSimulated[num].slerpDrive = this.drive;
			num++;
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000C21C File Offset: 0x0000A41C
	public void FixAllJointMotionLimits()
	{
		int num = 0;
		while ((float)num < this.cJointsSimulatedCount)
		{
			this.cJointsSimulated[num].xMotion = ConfigurableJointMotion.Locked;
			this.cJointsSimulated[num].yMotion = ConfigurableJointMotion.Locked;
			this.cJointsSimulated[num].zMotion = ConfigurableJointMotion.Locked;
			this.cJointsSimulated[num].angularXMotion = ConfigurableJointMotion.Limited;
			this.cJointsSimulated[num].angularYMotion = ConfigurableJointMotion.Limited;
			this.cJointsSimulated[num].angularZMotion = ConfigurableJointMotion.Limited;
			num++;
		}
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
	public void DisableLimbFromRagdoll(ConfigurableJoint joint)
	{
		if (this.ragdollSettings.ragdollInCooldown)
		{
			return;
		}
		this.ragdollSettings.RagdollIsDismembered = true;
		this.drive.positionSpring = 0f;
		this.drive.maximumForce = 0f;
		this.drive.positionDamper = this.positionDampaning;
		joint.slerpDrive = this.drive;
		joint.xMotion = ConfigurableJointMotion.Free;
		joint.yMotion = ConfigurableJointMotion.Free;
		joint.zMotion = ConfigurableJointMotion.Free;
		joint.angularXMotion = ConfigurableJointMotion.Free;
		joint.angularYMotion = ConfigurableJointMotion.Free;
		joint.angularZMotion = ConfigurableJointMotion.Free;
	}

	// Token: 0x040001A2 RID: 418
	[Header("Attributes")]
	private RagdollSettings ragdollSettings;

	// Token: 0x040001A3 RID: 419
	private readonly float positionDampaning = 5f;

	// Token: 0x040001A4 RID: 420
	[Header("Animated Ragdoll")]
	public GameObject animatedRagdoll;

	// Token: 0x040001A5 RID: 421
	private RagdollBodyElements animatedRagdollBodyElements;

	// Token: 0x040001A6 RID: 422
	private List<ConfigurableJoint> cJointsSimulated = new List<ConfigurableJoint>();

	// Token: 0x040001A7 RID: 423
	private float cJointsSimulatedCount;

	// Token: 0x040001A8 RID: 424
	private List<GameObject> cJointsAnimated = new List<GameObject>();

	// Token: 0x040001A9 RID: 425
	private List<Rigidbody> rbJointsSimulated = new List<Rigidbody>();

	// Token: 0x040001AA RID: 426
	private float distance;

	// Token: 0x040001AB RID: 427
	private Vector3 direction;

	// Token: 0x040001AC RID: 428
	private JointDrive drive;

	// Token: 0x040001AD RID: 429
	public bool skipRetargetOnce;

	// Token: 0x040001AE RID: 430
	private bool setJointsToZero;

	// Token: 0x040001AF RID: 431
	private SpringJoint springJoint;

	// Token: 0x040001B0 RID: 432
	private Vector3 sJointPosition;

	// Token: 0x040001B1 RID: 433
	private Vector3 aJointPosition;
}
