using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000BC RID: 188
public class UITouchScreen : MonoBehaviour
{
	// Token: 0x060003B8 RID: 952 RVA: 0x00016254 File Offset: 0x00014454
	private void Start()
	{
		this.playerSettings = PlayerSettings.instance;
		if (this.playerSettings != null)
		{
			this.playerSettings.simulatedRagdoll.OnRagdollToggle += this.OnBoolUpdate;
			this.playerSettings.playerController.OnFlyToggle += this.OnBoolUpdate;
			this.playerSettings.vehicleController.OnFocusVehicle += this.OnBoolUpdate;
			this.playerSettings.vehicleController.OnMountedVehicle += this.OnBoolUpdate;
		}
		LevelManager.OnBuildModeToggle += this.OnBoolUpdate;
		this.UpdateElements();
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00016304 File Offset: 0x00014504
	private void OnDestroy()
	{
		if (this.playerSettings != null)
		{
			this.playerSettings.simulatedRagdoll.OnRagdollToggle -= this.OnBoolUpdate;
			this.playerSettings.playerController.OnFlyToggle -= this.OnBoolUpdate;
			this.playerSettings.vehicleController.OnFocusVehicle -= this.OnBoolUpdate;
			this.playerSettings.vehicleController.OnMountedVehicle -= this.OnBoolUpdate;
		}
		LevelManager.OnBuildModeToggle -= this.OnBoolUpdate;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000052DE File Offset: 0x000034DE
	private void OnBoolUpdate(bool status)
	{
		this.UpdateElements();
	}

	// Token: 0x060003BB RID: 955 RVA: 0x000163A0 File Offset: 0x000145A0
	private void UpdateElements()
	{
		base.gameObject.SetActive(Application.isMobilePlatform);
		if (this.playerSettings == null)
		{
			return;
		}
		this.jump.SetActive(!this.playerSettings.vehicleController.Mounted);
		if (!this.jump.activeInHierarchy)
		{
			UnityEvent unityEvent = this.onJumpHide;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
		}
		this.shift.SetActive(this.playerSettings.playerController.FlyMode);
		if (!this.shift.activeInHierarchy)
		{
			UnityEvent unityEvent2 = this.onShiftHide;
			if (unityEvent2 != null)
			{
				unityEvent2.Invoke();
			}
		}
		this.interact.SetActive(this.playerSettings.vehicleController.Mounted || this.playerSettings.vehicleController.FocusVehicle != null);
		this.ragdoll.SetActive(!this.playerSettings.vehicleController.Mounted && !LevelManager.BuildModeOn);
		this.setSpawn.SetActive(!this.playerSettings.vehicleController.Mounted && this.playerSettings.playerTeleport.allowSetSpawn && !this.playerSettings.simulatedRagdoll.RagdollModeEnabled);
		this.toggleBuildMode.SetActive(WorldData.instance != null);
		this.toggleBuildItemMenu.SetActive(WorldData.instance != null && LevelManager.BuildModeOn);
		this.place.SetActive(LevelManager.BuildModeOn);
		if (!this.place.activeInHierarchy)
		{
			UnityEvent unityEvent3 = this.onPlaceHide;
			if (unityEvent3 != null)
			{
				unityEvent3.Invoke();
			}
		}
		this.remove.SetActive(LevelManager.BuildModeOn);
		if (!this.remove.activeInHierarchy)
		{
			UnityEvent unityEvent4 = this.onRemoveHide;
			if (unityEvent4 != null)
			{
				unityEvent4.Invoke();
			}
		}
		this.rotateLeft.SetActive(LevelManager.BuildModeOn);
		this.rotateRight.SetActive(LevelManager.BuildModeOn);
		this.vehicleA.SetActive(this.playerSettings.vehicleController.Mounted);
		if (!this.vehicleA.activeInHierarchy)
		{
			UnityEvent unityEvent5 = this.onVehicleAHide;
			if (unityEvent5 != null)
			{
				unityEvent5.Invoke();
			}
		}
		this.vehicleB.SetActive(this.playerSettings.vehicleController.Mounted);
		if (!this.vehicleB.activeInHierarchy)
		{
			UnityEvent unityEvent6 = this.onVehicleBHide;
			if (unityEvent6 != null)
			{
				unityEvent6.Invoke();
			}
		}
		this.toggleSlowMotion.SetActive(LevelManager.instance != null && LevelManager.instance.allowSlowMotion);
	}

	// Token: 0x0400050B RID: 1291
	private PlayerSettings playerSettings;

	// Token: 0x0400050C RID: 1292
	[Header("Locomotion")]
	public GameObject jump;

	// Token: 0x0400050D RID: 1293
	public UnityEvent onJumpHide;

	// Token: 0x0400050E RID: 1294
	public GameObject shift;

	// Token: 0x0400050F RID: 1295
	public UnityEvent onShiftHide;

	// Token: 0x04000510 RID: 1296
	public GameObject interact;

	// Token: 0x04000511 RID: 1297
	public GameObject ragdoll;

	// Token: 0x04000512 RID: 1298
	public GameObject setSpawn;

	// Token: 0x04000513 RID: 1299
	[Header("Build Mode")]
	public GameObject toggleBuildMode;

	// Token: 0x04000514 RID: 1300
	public GameObject toggleBuildItemMenu;

	// Token: 0x04000515 RID: 1301
	public GameObject place;

	// Token: 0x04000516 RID: 1302
	public UnityEvent onPlaceHide;

	// Token: 0x04000517 RID: 1303
	public GameObject remove;

	// Token: 0x04000518 RID: 1304
	public UnityEvent onRemoveHide;

	// Token: 0x04000519 RID: 1305
	public GameObject rotateRight;

	// Token: 0x0400051A RID: 1306
	public GameObject rotateLeft;

	// Token: 0x0400051B RID: 1307
	[Header("Vehicle")]
	public GameObject vehicleA;

	// Token: 0x0400051C RID: 1308
	public UnityEvent onVehicleAHide;

	// Token: 0x0400051D RID: 1309
	public GameObject vehicleB;

	// Token: 0x0400051E RID: 1310
	public UnityEvent onVehicleBHide;

	// Token: 0x0400051F RID: 1311
	[Header("Other")]
	public GameObject toggleSlowMotion;
}
