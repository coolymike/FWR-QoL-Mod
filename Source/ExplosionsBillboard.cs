using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class ExplosionsBillboard : MonoBehaviour
{
	// Token: 0x0600004A RID: 74 RVA: 0x00008ED8 File Offset: 0x000070D8
	private void Awake()
	{
		if (this.AutoInitCamera)
		{
			this.Camera = Camera.main;
			this.Active = true;
		}
		this.t = base.transform;
		Vector3 localScale = this.t.parent.transform.localScale;
		localScale.z = localScale.x;
		this.t.parent.transform.localScale = localScale;
		this.camT = this.Camera.transform;
		Transform parent = this.t.parent;
		this.myContainer = new GameObject
		{
			name = "Billboard_" + this.t.gameObject.name
		};
		this.contT = this.myContainer.transform;
		this.contT.position = this.t.position;
		this.t.parent = this.myContainer.transform;
		this.contT.parent = parent;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00008FD8 File Offset: 0x000071D8
	private void Update()
	{
		if (this.Active)
		{
			this.contT.LookAt(this.contT.position + this.camT.rotation * Vector3.back, this.camT.rotation * Vector3.up);
		}
	}

	// Token: 0x04000064 RID: 100
	public Camera Camera;

	// Token: 0x04000065 RID: 101
	public bool Active = true;

	// Token: 0x04000066 RID: 102
	public bool AutoInitCamera = true;

	// Token: 0x04000067 RID: 103
	private GameObject myContainer;

	// Token: 0x04000068 RID: 104
	private Transform t;

	// Token: 0x04000069 RID: 105
	private Transform camT;

	// Token: 0x0400006A RID: 106
	private Transform contT;
}
