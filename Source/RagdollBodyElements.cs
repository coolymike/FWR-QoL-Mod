using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class RagdollBodyElements : MonoBehaviour
{
	// Token: 0x06000134 RID: 308 RVA: 0x0000C79C File Offset: 0x0000A99C
	public GameObject GetJoint(RagdollBodyElements.Joint jointName)
	{
		switch (jointName)
		{
		case RagdollBodyElements.Joint.Core:
			return this.ragdollJoints[0];
		case RagdollBodyElements.Joint.ChestLow:
			return this.ragdollJoints[1];
		case RagdollBodyElements.Joint.ChestHigh:
			return this.ragdollJoints[2];
		case RagdollBodyElements.Joint.Head:
			return this.ragdollJoints[3];
		case RagdollBodyElements.Joint.LeftArm:
			return this.ragdollJoints[4];
		case RagdollBodyElements.Joint.LeftElbow:
			return this.ragdollJoints[5];
		case RagdollBodyElements.Joint.RightArm:
			return this.ragdollJoints[6];
		case RagdollBodyElements.Joint.RightElbow:
			return this.ragdollJoints[7];
		case RagdollBodyElements.Joint.LeftLeg:
			return this.ragdollJoints[8];
		case RagdollBodyElements.Joint.LeftKnee:
			return this.ragdollJoints[9];
		case RagdollBodyElements.Joint.LeftFoot:
			return this.ragdollJoints[10];
		case RagdollBodyElements.Joint.RightLeg:
			return this.ragdollJoints[11];
		case RagdollBodyElements.Joint.RightKnee:
			return this.ragdollJoints[12];
		case RagdollBodyElements.Joint.RightFoot:
			return this.ragdollJoints[13];
		default:
			Debug.LogError("You haven't selected a valid joint, silly willy!");
			return null;
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000C87C File Offset: 0x0000AA7C
	public Vector3 AverageJointPosition()
	{
		this.averageJointPosition = new Vector3(0f, 0f, 0f);
		for (int i = 0; i < this.ragdollJoints.Length; i++)
		{
			this.averageJointPosition += this.ragdollJoints[i].transform.position;
		}
		return this.averageJointPosition / (float)this.ragdollJoints.Length;
	}

	// Token: 0x040001CB RID: 459
	public GameObject ragdollMesh;

	// Token: 0x040001CC RID: 460
	public GameObject[] ragdollJoints = new GameObject[0];

	// Token: 0x040001CD RID: 461
	private Vector3 averageJointPosition;

	// Token: 0x02000042 RID: 66
	public enum Joint
	{
		// Token: 0x040001CF RID: 463
		Core,
		// Token: 0x040001D0 RID: 464
		ChestLow,
		// Token: 0x040001D1 RID: 465
		ChestHigh,
		// Token: 0x040001D2 RID: 466
		Head,
		// Token: 0x040001D3 RID: 467
		LeftArm,
		// Token: 0x040001D4 RID: 468
		LeftElbow,
		// Token: 0x040001D5 RID: 469
		RightArm,
		// Token: 0x040001D6 RID: 470
		RightElbow,
		// Token: 0x040001D7 RID: 471
		LeftLeg,
		// Token: 0x040001D8 RID: 472
		LeftKnee,
		// Token: 0x040001D9 RID: 473
		LeftFoot,
		// Token: 0x040001DA RID: 474
		RightLeg,
		// Token: 0x040001DB RID: 475
		RightKnee,
		// Token: 0x040001DC RID: 476
		RightFoot
	}
}
