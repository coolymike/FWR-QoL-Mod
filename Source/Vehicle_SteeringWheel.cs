using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class Vehicle_SteeringWheel : MonoBehaviour
{
	// Token: 0x060001F7 RID: 503 RVA: 0x00003CB4 File Offset: 0x00001EB4
	private void Start()
	{
		this.vehicle = base.transform.root.GetComponent<Vehicle>();
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00003CCC File Offset: 0x00001ECC
	private void Update()
	{
		if (this.vehicle != null && this.vehicle.playerControlling != null)
		{
			this.Steer();
		}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00003CF5 File Offset: 0x00001EF5
	private void Steer()
	{
		base.transform.localEulerAngles = new Vector3(0f, InputManager.Move(true).x * this.steerAngle, 0f);
	}

	// Token: 0x040002CF RID: 719
	public Vehicle vehicle;

	// Token: 0x040002D0 RID: 720
	public float steerAngle = 80f;
}
