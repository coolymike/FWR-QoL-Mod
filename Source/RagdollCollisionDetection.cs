using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class RagdollCollisionDetection : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x0600009D RID: 157 RVA: 0x00009FD4 File Offset: 0x000081D4
	// (remove) Token: 0x0600009E RID: 158 RVA: 0x0000A00C File Offset: 0x0000820C
	public event RagdollCollisionDetection.RagdollCollisionHandler OnCollision;

	// Token: 0x0600009F RID: 159 RVA: 0x000029F4 File Offset: 0x00000BF4
	private void Awake()
	{
		if (!this.calculateCollision)
		{
			base.enabled = false;
			return;
		}
		this.bodyElements = base.GetComponent<RagdollBodyElements>();
		this.thisRootTransformID = base.transform.root.GetInstanceID();
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00002A28 File Offset: 0x00000C28
	private void Start()
	{
		this.AddJointCollisionScripts();
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00002A30 File Offset: 0x00000C30
	public void CollisionDetected(Transform collidedObject, Vector3 impactVelocity)
	{
		if (collidedObject.root.GetInstanceID() == this.thisRootTransformID)
		{
			return;
		}
		if (this.OnCollision != null)
		{
			this.OnCollision(collidedObject, impactVelocity);
		}
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x0000A044 File Offset: 0x00008244
	private void AddJointCollisionScripts()
	{
		for (int i = 0; i < this.bodyElements.ragdollJoints.Length; i++)
		{
			this.jointRigidbody = this.bodyElements.ragdollJoints[i].GetComponent<Rigidbody>();
			if (!(this.jointRigidbody == null))
			{
				this.collisionDetection = this.jointRigidbody.gameObject.AddComponent<RagdollChildCollisionDetection>();
				this.collisionDetection.ragdollCollisionDetection = this;
				this.collisionDetection.enabled = true;
			}
		}
	}

	// Token: 0x04000112 RID: 274
	private RagdollBodyElements bodyElements;

	// Token: 0x04000113 RID: 275
	public bool calculateCollision = true;

	// Token: 0x04000114 RID: 276
	private int thisRootTransformID;

	// Token: 0x04000116 RID: 278
	private Rigidbody jointRigidbody;

	// Token: 0x04000117 RID: 279
	private RagdollChildCollisionDetection collisionDetection;

	// Token: 0x0200002D RID: 45
	// (Invoke) Token: 0x060000A5 RID: 165
	public delegate void RagdollCollisionHandler(Transform collidedObject, Vector3 impactVelocity);
}
