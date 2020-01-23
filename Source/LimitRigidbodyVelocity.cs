using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class LimitRigidbodyVelocity : MonoBehaviour
{
	// Token: 0x06000459 RID: 1113 RVA: 0x0000595B File Offset: 0x00003B5B
	private void Start()
	{
		this.maxVelocity = Mathf.Abs(Physics.gravity.y) * 3f;
		if (this.includeRigidbodyChildren)
		{
			this.rigidbodies = base.GetComponentsInChildren<Rigidbody>();
			return;
		}
		this.rigidbodies[0] = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00018520 File Offset: 0x00016720
	private void FixedUpdate()
	{
		for (int i = 0; i < this.rigidbodies.Length; i++)
		{
			this.speed = this.rigidbodies[i].velocity.magnitude;
			if (this.speed > this.maxVelocity)
			{
				this.rigidbodies[i].AddForce(-(this.rigidbodies[i].velocity.normalized * (this.speed - this.maxVelocity)));
			}
		}
	}

	// Token: 0x040005C1 RID: 1473
	public bool includeRigidbodyChildren = true;

	// Token: 0x040005C2 RID: 1474
	public Rigidbody[] rigidbodies;

	// Token: 0x040005C3 RID: 1475
	private float maxVelocity;

	// Token: 0x040005C4 RID: 1476
	private float speed;
}
