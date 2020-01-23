using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class PlayerCollisionPoints : MonoBehaviour
{
	// Token: 0x060000C6 RID: 198 RVA: 0x00002C2E File Offset: 0x00000E2E
	private void Awake()
	{
		this.playerSettings = base.GetComponent<PlayerSettings>();
		this.ragdollCollisionDetection = this.playerSettings.simulatedRagdoll.GetComponent<RagdollCollisionDetection>();
		this.ragdollCollisionDetection.OnCollision += this.Collided;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00002C69 File Offset: 0x00000E69
	private void Start()
	{
		this.ragdollCoreRigidbody = this.playerSettings.simulatedRagdoll.bodyElements.GetJoint(RagdollBodyElements.Joint.Core).GetComponent<Rigidbody>();
		base.InvokeRepeating("AddFallingCP", 1f, 0.1f);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00002CA1 File Offset: 0x00000EA1
	private void Update()
	{
		if ((LevelManager.BuildModeOn || !this.playerSettings.simulatedRagdoll.RagdollModeEnabled) && this.collidedObjects.Count > 0)
		{
			this.collidedObjects.Clear();
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00002CD5 File Offset: 0x00000ED5
	private void OnDestroy()
	{
		this.ragdollCollisionDetection.OnCollision -= this.Collided;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00002CEE File Offset: 0x00000EEE
	private void FixedUpdate()
	{
		this.FallingCP();
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000AAC0 File Offset: 0x00008CC0
	private void FallingCP()
	{
		if (!this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			this.acceptFallingCollisionPoints = false;
			return;
		}
		if (this.ragdollCoreRigidbody.velocity.y < -20f)
		{
			this.fallTime -= Time.deltaTime;
		}
		else
		{
			this.fallTime = 1f;
		}
		this.acceptFallingCollisionPoints = (this.fallTime < 0f);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00002CF6 File Offset: 0x00000EF6
	private void AddFallingCP()
	{
		if (this.acceptFallingCollisionPoints)
		{
			GameData.collisionPoints++;
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000AB30 File Offset: 0x00008D30
	private void Collided(Transform colliderTransform, Vector3 impactVelocity)
	{
		if (LevelManager.StatusIsWinOrLose(true))
		{
			return;
		}
		if (!this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			return;
		}
		int instanceID = colliderTransform.GetInstanceID();
		if (this.collidedObjects.Contains(instanceID))
		{
			return;
		}
		this.collidedObjects.Add(instanceID);
		GameData.stats.XP++;
		GameData.stats.collisions++;
	}

	// Token: 0x04000146 RID: 326
	private PlayerSettings playerSettings;

	// Token: 0x04000147 RID: 327
	private RagdollCollisionDetection ragdollCollisionDetection;

	// Token: 0x04000148 RID: 328
	private Rigidbody ragdollCoreRigidbody;

	// Token: 0x04000149 RID: 329
	private float fallTime;

	// Token: 0x0400014A RID: 330
	private int fallTimeFrameOffset;

	// Token: 0x0400014B RID: 331
	private List<int> collidedObjects = new List<int>();

	// Token: 0x0400014C RID: 332
	private bool acceptFallingCollisionPoints;
}
