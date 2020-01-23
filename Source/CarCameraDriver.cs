using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class CarCameraDriver : MonoBehaviour
{
	// Token: 0x060001EC RID: 492 RVA: 0x00003C27 File Offset: 0x00001E27
	private void Start()
	{
		this.vehicle = base.transform.root.GetComponent<Vehicle>();
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00003C3F File Offset: 0x00001E3F
	private void Update()
	{
		base.transform.position = this.cameraTarget.position;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000F874 File Offset: 0x0000DA74
	private void FixedUpdate()
	{
		if (this.vehicle.playerControlling != null)
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.cameraTarget.rotation, Time.deltaTime * 10f);
		}
	}

	// Token: 0x040002C6 RID: 710
	public Transform cameraTarget;

	// Token: 0x040002C7 RID: 711
	public Vehicle vehicle;
}
