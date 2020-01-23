using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class BlackHole : MonoBehaviour
{
	// Token: 0x060001BF RID: 447 RVA: 0x00003A70 File Offset: 0x00001C70
	private void FixedUpdate()
	{
		this.GiveDaSuck();
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
	private void GiveDaSuck()
	{
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		this.colliders = Physics.OverlapSphere(base.transform.position, this.suckRadius);
		this.i = 0;
		while (this.i < this.colliders.Length)
		{
			this.rigidbody = this.colliders[this.i].GetComponent<Rigidbody>();
			if (!(this.rigidbody == null))
			{
				this.rigidbody.AddExplosionForce(-this.suckStrength * ((this.rigidbody.mass < 10f) ? 1f : (this.rigidbody.mass * 0.1f)) * (Vector3.Distance(this.colliders[this.i].transform.position, base.transform.position) * 0.6f), base.transform.position, this.suckRadius, 0f, ForceMode.Force);
			}
			this.i++;
		}
	}

	// Token: 0x04000284 RID: 644
	public float suckRadius = 4.8f;

	// Token: 0x04000285 RID: 645
	public float suckStrength = 100f;

	// Token: 0x04000286 RID: 646
	private Collider[] colliders;

	// Token: 0x04000287 RID: 647
	private Rigidbody rigidbody;

	// Token: 0x04000288 RID: 648
	private int i;
}
