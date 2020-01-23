using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class PlayerSettings : MonoBehaviour
{
	// Token: 0x06000107 RID: 263 RVA: 0x00002F33 File Offset: 0x00001133
	private void Awake()
	{
		PlayerSettings.instance = this;
		this.ragdollStyle = this.simulatedRagdoll.GetComponent<RagdollStyleManager>();
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00002F4C File Offset: 0x0000114C
	private void Update()
	{
		if (PlayerSettings.disablePlayerControlls && this.simulatedRagdoll.RagdollModeEnabled)
		{
			PlayerSettings.disablePlayerControlls = false;
			return;
		}
		PlayerSettings.disablePlayerControlls = (this.vehicleController != null && this.vehicleController.Mounted);
	}

	// Token: 0x04000181 RID: 385
	public static PlayerSettings instance;

	// Token: 0x04000182 RID: 386
	public CameraController mainCamera;

	// Token: 0x04000183 RID: 387
	[HideInInspector]
	public RagdollStyleManager ragdollStyle;

	// Token: 0x04000184 RID: 388
	public PlayerController playerController;

	// Token: 0x04000185 RID: 389
	public RagdollSettings simulatedRagdoll;

	// Token: 0x04000186 RID: 390
	public GameObject animatedRagdoll;

	// Token: 0x04000187 RID: 391
	public PlayerCameraController playerCameraController;

	// Token: 0x04000188 RID: 392
	public VehicleController vehicleController;

	// Token: 0x04000189 RID: 393
	public PlayerGrabbing playerGrabbing;

	// Token: 0x0400018A RID: 394
	public PlayerTeleport playerTeleport;

	// Token: 0x0400018B RID: 395
	public bool ragdollsCanTouchThisPlayer;

	// Token: 0x0400018C RID: 396
	[Range(0f, 2f)]
	public int playerNumber;

	// Token: 0x0400018D RID: 397
	public static bool disablePlayerControlls;
}
