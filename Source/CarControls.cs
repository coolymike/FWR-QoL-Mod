using System;
using System.Collections.Generic;
using NWH.WheelController3D;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class CarControls : MonoBehaviour
{
	// Token: 0x060001F0 RID: 496 RVA: 0x00003C57 File Offset: 0x00001E57
	private void Awake()
	{
		if (!this.ignoreBuildMode)
		{
			this.DisableVehicle(LevelManager.BuildModeOn);
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00003C6C File Offset: 0x00001E6C
	private void Update()
	{
		if (!this.ignoreBuildMode)
		{
			this.DisableVehicle(LevelManager.BuildModeOn);
		}
		if (this.vehicle.playerControlling != null)
		{
			this.PlayerDriver();
			return;
		}
		this.AIDriver();
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
	private void DisableVehicle(bool enabled)
	{
		Rigidbody[] componentsInChildren = this.vehicleRoot.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].isKinematic = enabled;
		}
		foreach (WheelController wheelController in this.wheelControllers)
		{
			wheelController.enabled = !enabled;
		}
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0000F940 File Offset: 0x0000DB40
	private void AIDriver()
	{
		if (this.NWHVehicleController != null)
		{
			this.NWHVehicleController.xAxis = this.steerDirection;
			this.NWHVehicleController.yAxis = this.forwardSpeed;
			this.NWHVehicleController.breakInput = (this.forwardSpeed < 0.05f && this.forwardSpeed > -0.05f);
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
	private void PlayerDriver()
	{
		if (this.NWHVehicleController != null)
		{
			if (InputManager.VehicleA() && !InputManager.VehicleB())
			{
				this.NWHVehicleController.yAxis = 0.75f;
			}
			else if (InputManager.VehicleB() && !InputManager.VehicleA())
			{
				this.NWHVehicleController.yAxis = -0.5f;
			}
			else
			{
				this.NWHVehicleController.yAxis = 0f;
			}
			this.NWHVehicleController.breakInput = (InputManager.Break() || (InputManager.VehicleA() && InputManager.VehicleB()));
			this.NWHVehicleController.xAxis = InputManager.Move(true).x;
		}
	}

	// Token: 0x040002C8 RID: 712
	public bool ignoreBuildMode;

	// Token: 0x040002C9 RID: 713
	public Vehicle vehicle;

	// Token: 0x040002CA RID: 714
	public Transform vehicleRoot;

	// Token: 0x040002CB RID: 715
	public NWHVehicleController NWHVehicleController;

	// Token: 0x040002CC RID: 716
	public List<WheelController> wheelControllers = new List<WheelController>();

	// Token: 0x040002CD RID: 717
	[Range(-1f, 1f)]
	public float forwardSpeed;

	// Token: 0x040002CE RID: 718
	[Range(-1f, 1f)]
	public float steerDirection;
}
