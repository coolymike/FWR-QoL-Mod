using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class VehicleController : MonoBehaviour
{
	// Token: 0x1700008F RID: 143
	// (get) Token: 0x060006CC RID: 1740 RVA: 0x00007659 File Offset: 0x00005859
	// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
	public bool Mounted
	{
		get
		{
			return this.mounted;
		}
		set
		{
			if (this.mounted == value)
			{
				return;
			}
			if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
			{
				this.mounted = false;
			}
			else
			{
				this.mounted = value;
			}
			if (this.FocusVehicle != null)
			{
				if (value)
				{
					this.FocusVehicle.playerControlling = this.playerSettings;
				}
				else
				{
					this.FocusVehicle.playerControlling = null;
				}
			}
			VehicleController.OnBoolHandler onMountedVehicle = this.OnMountedVehicle;
			if (onMountedVehicle == null)
			{
				return;
			}
			onMountedVehicle(value);
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060006CE RID: 1742 RVA: 0x00007661 File Offset: 0x00005861
	// (set) Token: 0x060006CF RID: 1743 RVA: 0x00007669 File Offset: 0x00005869
	public Vehicle FocusVehicle
	{
		get
		{
			return this.focusVehicle;
		}
		set
		{
			if (this.focusVehicle == value)
			{
				return;
			}
			this.focusVehicle = value;
			VehicleController.OnBoolHandler onFocusVehicle = this.OnFocusVehicle;
			if (onFocusVehicle == null)
			{
				return;
			}
			onFocusVehicle(value != null);
		}
	}

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x060006D0 RID: 1744 RVA: 0x0001F65C File Offset: 0x0001D85C
	// (remove) Token: 0x060006D1 RID: 1745 RVA: 0x0001F694 File Offset: 0x0001D894
	public event VehicleController.OnBoolHandler OnFocusVehicle;

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x060006D2 RID: 1746 RVA: 0x0001F6CC File Offset: 0x0001D8CC
	// (remove) Token: 0x060006D3 RID: 1747 RVA: 0x0001F704 File Offset: 0x0001D904
	public event VehicleController.OnBoolHandler OnMountedVehicle;

	// Token: 0x060006D4 RID: 1748 RVA: 0x00007698 File Offset: 0x00005898
	private void Awake()
	{
		this.playerSettings = base.GetComponent<PlayerSettings>();
		this.playerSettings.simulatedRagdoll.OnRagdollToggle += this.OnRagdollToggle;
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x000076D3 File Offset: 0x000058D3
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
		this.playerSettings.simulatedRagdoll.OnRagdollToggle -= this.OnRagdollToggle;
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00007702 File Offset: 0x00005902
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (buildModeOn)
		{
			this.DismountVehicle();
		}
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00007702 File Offset: 0x00005902
	private void OnRagdollToggle(bool ragdollModeEnabled)
	{
		if (ragdollModeEnabled)
		{
			this.DismountVehicle();
		}
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x0000770D File Offset: 0x0000590D
	private void Update()
	{
		if (InputManager.MountVehicle() && !this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			if (!this.Mounted && !LevelManager.BuildModeOn)
			{
				this.MountVehicle();
				return;
			}
			if (this.Mounted)
			{
				this.DismountVehicle();
			}
		}
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x0000774C File Offset: 0x0000594C
	private void FixedUpdate()
	{
		this.DetectVehicle();
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0001F73C File Offset: 0x0001D93C
	private void DetectVehicle()
	{
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			return;
		}
		if (this.Mounted)
		{
			return;
		}
		if (!Physics.Raycast(this.playerSettings.mainCamera.transform.position, this.playerSettings.mainCamera.transform.TransformDirection(Vector3.forward), out this.hit, 6f))
		{
			this.FocusVehicle = null;
			return;
		}
		this.FocusVehicle = this.hit.transform.root.GetComponentInChildren<Vehicle>();
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00007754 File Offset: 0x00005954
	private void MountVehicle()
	{
		if (this.FocusVehicle != null)
		{
			this.Mounted = true;
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0000776B File Offset: 0x0000596B
	private void DismountVehicle()
	{
		this.Mounted = false;
	}

	// Token: 0x040007BE RID: 1982
	private PlayerSettings playerSettings;

	// Token: 0x040007BF RID: 1983
	private bool mounted;

	// Token: 0x040007C0 RID: 1984
	public Vehicle focusVehicle;

	// Token: 0x040007C3 RID: 1987
	private RaycastHit hit;

	// Token: 0x040007C4 RID: 1988
	private Vehicle vehicle;

	// Token: 0x02000136 RID: 310
	// (Invoke) Token: 0x060006DF RID: 1759
	public delegate void OnBoolHandler(bool value);
}
