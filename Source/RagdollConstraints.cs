using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
[RequireComponent(typeof(RagdollBodyElements))]
public class RagdollConstraints : MonoBehaviour
{
	// Token: 0x06000137 RID: 311 RVA: 0x000031D1 File Offset: 0x000013D1
	private void Awake()
	{
		this.ragdollSettings = base.GetComponent<RagdollSettings>();
		this.AddRigidbodies();
		this.AddCollider();
		this.AddConstraints();
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
	private void AddRigidbodies()
	{
		for (int i = 0; i < this.bodyElements.ragdollJoints.Length; i++)
		{
			if (this.enableFeet || (this.bodyElements.GetJoint(RagdollBodyElements.Joint.RightFoot) != this.bodyElements.ragdollJoints[i] && this.bodyElements.GetJoint(RagdollBodyElements.Joint.LeftFoot) != this.bodyElements.ragdollJoints[i]))
			{
				this.jointRB = this.bodyElements.ragdollJoints[i].AddComponent<Rigidbody>();
				this.jointRB.interpolation = RigidbodyInterpolation.None;
				this.jointRB.drag = this.rDrag;
				if (this.ragdollSettings != null)
				{
					this.jointRB.collisionDetectionMode = this.ragdollSettings.CollisionDetectionMode;
				}
				else
				{
					this.jointRB.collisionDetectionMode = CollisionDetectionMode.Discrete;
				}
				if (this.bodyElements.GetJoint(RagdollBodyElements.Joint.Head) == this.bodyElements.ragdollJoints[i])
				{
					this.jointRB.angularDrag = this.rAngularDrag * 6f;
				}
				else
				{
					this.jointRB.angularDrag = this.rAngularDrag;
				}
			}
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000CA10 File Offset: 0x0000AC10
	private void AddCollider()
	{
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.Core).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, 0f, 0f);
		this.jointBC.size = new Vector3(0.3f, 0.1f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.ChestLow).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, 0.175f, 0f);
		this.jointBC.size = new Vector3(0.3f, 0.3f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.ChestHigh).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, 0.175f, 0f);
		this.jointBC.size = new Vector3(0.3f, 0.3f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.Head).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, 0.175f, 0f);
		this.jointBC.size = new Vector3(0.3f, 0.3f, 0.3f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.LeftArm).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, -0.15f, 0f);
		this.jointBC.size = new Vector3(0.1f, 0.4f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.LeftElbow).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, -0.225f, 0f);
		this.jointBC.size = new Vector3(0.1f, 0.4f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.RightArm).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, -0.15f, 0f);
		this.jointBC.size = new Vector3(0.1f, 0.4f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.RightElbow).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, -0.225f, 0f);
		this.jointBC.size = new Vector3(0.1f, 0.4f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.LeftLeg).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, -0.275f, 0f);
		this.jointBC.size = new Vector3(0.1f, 0.5f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.LeftKnee).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, -0.275f, 0f);
		this.jointBC.size = new Vector3(0.1f, 0.5f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.LeftFoot).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, 0f, 0.05f);
		this.jointBC.size = new Vector3(0.1f, 0.1f, 0.2f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.RightLeg).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, -0.275f, 0f);
		this.jointBC.size = new Vector3(0.1f, 0.5f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.RightKnee).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, -0.275f, 0f);
		this.jointBC.size = new Vector3(0.1f, 0.5f, 0.1f);
		this.jointBC.material = this.physicsMaterial;
		this.jointBC = this.bodyElements.GetJoint(RagdollBodyElements.Joint.RightFoot).AddComponent<BoxCollider>();
		this.jointBC.center = new Vector3(0f, 0f, 0.05f);
		this.jointBC.size = new Vector3(0.1f, 0.1f, 0.2f);
		this.jointBC.material = this.physicsMaterial;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
	private void SetJointConstraint(RagdollBodyElements.Joint jointName, RagdollBodyElements.Joint connectedJoint, bool enableCollision, Vector4 limits)
	{
		this.jointCJ = this.bodyElements.GetJoint(jointName).AddComponent<ConfigurableJoint>();
		this.jointCJ.connectedBody = this.bodyElements.GetJoint(connectedJoint).GetComponent<Rigidbody>();
		this.jointCJ.enableCollision = enableCollision;
		this.jointLimit.limit = limits.x;
		this.jointCJ.lowAngularXLimit = this.jointLimit;
		this.jointLimit.limit = limits.y;
		this.jointCJ.highAngularXLimit = this.jointLimit;
		this.jointLimit.limit = limits.z;
		this.jointCJ.angularYLimit = this.jointLimit;
		this.jointLimit.limit = limits.w;
		this.jointCJ.angularZLimit = this.jointLimit;
		this.jointLimitSpring.spring = this.linearSpring;
		this.jointLimitSpring.damper = this.linearDamp;
		this.jointCJ.linearLimitSpring = this.jointLimitSpring;
		this.jointLimitSpring.spring = this.angularSpring;
		this.jointLimitSpring.damper = this.angularDamp;
		this.jointCJ.angularXLimitSpring = this.jointLimitSpring;
		this.jointCJ.angularYZLimitSpring = this.jointLimitSpring;
		this.jointCJ.rotationDriveMode = RotationDriveMode.Slerp;
		this.jointCJ.projectionMode = JointProjectionMode.PositionAndRotation;
		this.jointCJ.projectionDistance = 0.01f;
		this.jointCJ.projectionAngle = 0.01f;
		this.jointCJ.autoConfigureConnectedAnchor = true;
		this.jointCJ.swapBodies = true;
		this.jointCJ.enablePreprocessing = false;
		this.jointCJ.xMotion = ConfigurableJointMotion.Locked;
		this.jointCJ.yMotion = ConfigurableJointMotion.Locked;
		this.jointCJ.zMotion = ConfigurableJointMotion.Locked;
		this.jointCJ.angularXMotion = ConfigurableJointMotion.Limited;
		this.jointCJ.angularYMotion = ConfigurableJointMotion.Limited;
		this.jointCJ.angularZMotion = ConfigurableJointMotion.Limited;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000D1AC File Offset: 0x0000B3AC
	private void AddConstraints()
	{
		this.SetJointConstraint(RagdollBodyElements.Joint.ChestLow, RagdollBodyElements.Joint.Core, false, this.limitXXYZChestLow);
		this.SetJointConstraint(RagdollBodyElements.Joint.ChestHigh, RagdollBodyElements.Joint.ChestLow, false, this.limitXXYZChestHigh);
		this.SetJointConstraint(RagdollBodyElements.Joint.Head, RagdollBodyElements.Joint.ChestHigh, false, this.limitXXYZHead);
		this.SetJointConstraint(RagdollBodyElements.Joint.LeftArm, RagdollBodyElements.Joint.ChestHigh, true, this.limitXXYZArm);
		this.SetJointConstraint(RagdollBodyElements.Joint.LeftElbow, RagdollBodyElements.Joint.LeftArm, false, this.limitXXYZElbow);
		this.SetJointConstraint(RagdollBodyElements.Joint.RightArm, RagdollBodyElements.Joint.ChestHigh, true, this.limitXXYZArm);
		this.SetJointConstraint(RagdollBodyElements.Joint.RightElbow, RagdollBodyElements.Joint.RightArm, false, this.limitXXYZElbow);
		this.SetJointConstraint(RagdollBodyElements.Joint.LeftLeg, RagdollBodyElements.Joint.Core, false, this.limitXXYZLeg);
		this.SetJointConstraint(RagdollBodyElements.Joint.LeftKnee, RagdollBodyElements.Joint.LeftLeg, false, this.limitXXYZKnee);
		if (this.enableFeet)
		{
			this.SetJointConstraint(RagdollBodyElements.Joint.LeftFoot, RagdollBodyElements.Joint.LeftKnee, false, this.limitXXYZFoot);
		}
		this.SetJointConstraint(RagdollBodyElements.Joint.RightLeg, RagdollBodyElements.Joint.Core, false, this.limitXXYZLeg);
		this.SetJointConstraint(RagdollBodyElements.Joint.RightKnee, RagdollBodyElements.Joint.RightLeg, false, this.limitXXYZKnee);
		if (this.enableFeet)
		{
			this.SetJointConstraint(RagdollBodyElements.Joint.RightFoot, RagdollBodyElements.Joint.RightKnee, false, this.limitXXYZFoot);
		}
	}

	// Token: 0x040001DD RID: 477
	public RagdollBodyElements bodyElements;

	// Token: 0x040001DE RID: 478
	private RagdollSettings ragdollSettings;

	// Token: 0x040001DF RID: 479
	[Header("Settings")]
	[Space]
	public bool enableFeet = true;

	// Token: 0x040001E0 RID: 480
	[Space]
	[Header("Rigidbody Settings")]
	public float rDrag = 0.04f;

	// Token: 0x040001E1 RID: 481
	public float rAngularDrag = 0.04f;

	// Token: 0x040001E2 RID: 482
	public PhysicMaterial physicsMaterial;

	// Token: 0x040001E3 RID: 483
	[Space]
	[Header("Joint Settings")]
	public float linearSpring = 300f;

	// Token: 0x040001E4 RID: 484
	public float linearDamp = 15f;

	// Token: 0x040001E5 RID: 485
	public float angularSpring = 300f;

	// Token: 0x040001E6 RID: 486
	public float angularDamp = 15f;

	// Token: 0x040001E7 RID: 487
	[Header("Joint Limits")]
	[Space]
	public Vector4 limitXXYZChestLow = new Vector4(-10f, 10f, 0f, 10f);

	// Token: 0x040001E8 RID: 488
	public Vector4 limitXXYZChestHigh = new Vector4(-20f, 20f, 0f, 10f);

	// Token: 0x040001E9 RID: 489
	public Vector4 limitXXYZHead = new Vector4(-20f, 20f, 30f, 10f);

	// Token: 0x040001EA RID: 490
	[Space]
	public Vector4 limitXXYZArm = new Vector4(-170f, 50f, 20f, 70f);

	// Token: 0x040001EB RID: 491
	public Vector4 limitXXYZElbow = new Vector4(-130f, 0f, 0f, 0f);

	// Token: 0x040001EC RID: 492
	[Space]
	public Vector4 limitXXYZLeg = new Vector4(-90f, 30f, 25f, 25f);

	// Token: 0x040001ED RID: 493
	public Vector4 limitXXYZKnee = new Vector4(0f, 135f, 0f, 0f);

	// Token: 0x040001EE RID: 494
	public Vector4 limitXXYZFoot = new Vector4(-20f, 30f, 0f, 0f);

	// Token: 0x040001EF RID: 495
	private Rigidbody jointRB;

	// Token: 0x040001F0 RID: 496
	private BoxCollider jointBC;

	// Token: 0x040001F1 RID: 497
	private ConfigurableJoint jointCJ;

	// Token: 0x040001F2 RID: 498
	private SoftJointLimitSpring jointLimitSpring;

	// Token: 0x040001F3 RID: 499
	private SoftJointLimit jointLimit;
}
