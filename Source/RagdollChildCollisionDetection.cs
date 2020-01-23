using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class RagdollChildCollisionDetection : MonoBehaviour
{
	// Token: 0x0600009A RID: 154 RVA: 0x000029B4 File Offset: 0x00000BB4
	private void OnCollisionEnter(Collision collided)
	{
		this.ragdollCollisionDetection.CollisionDetected(collided.transform, collided.relativeVelocity);
	}

	// Token: 0x0600009B RID: 155 RVA: 0x000029CD File Offset: 0x00000BCD
	private void OnTriggerEnter(Collider collided)
	{
		this.ragdollCollisionDetection.CollisionDetected(collided.transform, new Vector3(0f, 0f, 0f));
	}

	// Token: 0x04000111 RID: 273
	public RagdollCollisionDetection ragdollCollisionDetection;
}
