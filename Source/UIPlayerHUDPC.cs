using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200008F RID: 143
public class UIPlayerHUDPC : MonoBehaviour
{
	// Token: 0x060002E9 RID: 745 RVA: 0x00013BFC File Offset: 0x00011DFC
	private void LateUpdate()
	{
		this.levelTimer.SetActive(LevelTimer.reference != null && LevelTimer.reference.enabled && LevelTimer.reference.active);
		if (this.levelTimer.activeInHierarchy && LevelTimer.reference != null)
		{
			this.progressImage.fillAmount = (LevelManager.StatusIsWinOrLose(true) ? 0f : (LevelTimer.reference.timer / LevelTimer.reference.timeLimit));
			this.timeText.text = LevelTimer.reference.timeVisual;
		}
		this.aimReticleImage.gameObject.SetActive(LevelManager.BuildModeOn || this.playerSettings.vehicleController.Mounted);
		this.grabReticleImage.gameObject.SetActive(!this.playerSettings.simulatedRagdoll.RagdollModeEnabled && !LevelManager.BuildModeOn && !this.playerSettings.vehicleController.Mounted && this.playerSettings.playerGrabbing != null && this.playerSettings.playerGrabbing.enabled && (this.playerSettings.playerGrabbing.Grabbing || InputManager.Grabbing()));
		if (this.playerSettings.playerGrabbing != null && this.playerSettings.playerGrabbing.Grabbing)
		{
			this.grabReticleImage.sprite = this.handClosedSprite;
		}
		else
		{
			this.grabReticleImage.sprite = this.handOpenSprite;
		}
		this.buildModeIndicatorImage.gameObject.SetActive(LevelManager.BuildModeOn);
		this.slowMotionIndicatorImage.gameObject.SetActive(!LevelManager.StatusIsWinOrLose(true) && LevelManager.instance != null && !LevelManager.instance.forceSlowMotion && LevelManager.SlowMotion && !LevelManager.instance.forceSlowMotion);
		this.cameraIndicatorImage.gameObject.SetActive(!LevelManager.StatusIsWinOrLose(true) && (this.playerSettings.mainCamera.target == CameraController.Target.Cinematic || this.playerSettings.mainCamera.target == CameraController.Target.BodyCam));
		this.vehicleIndicatorImage.gameObject.SetActive(!LevelManager.BuildModeOn && !LevelManager.StatusIsWinOrLose(true) && (this.playerSettings.vehicleController.focusVehicle != null || this.playerSettings.vehicleController.Mounted));
	}

	// Token: 0x04000439 RID: 1081
	public PlayerSettings playerSettings;

	// Token: 0x0400043A RID: 1082
	[Header("Timer")]
	public GameObject levelTimer;

	// Token: 0x0400043B RID: 1083
	public Image progressImage;

	// Token: 0x0400043C RID: 1084
	public Text timeText;

	// Token: 0x0400043D RID: 1085
	[Header("Reticle")]
	public Image aimReticleImage;

	// Token: 0x0400043E RID: 1086
	public Image grabReticleImage;

	// Token: 0x0400043F RID: 1087
	public Sprite handOpenSprite;

	// Token: 0x04000440 RID: 1088
	public Sprite handClosedSprite;

	// Token: 0x04000441 RID: 1089
	[Header("Indicators")]
	public Image buildModeIndicatorImage;

	// Token: 0x04000442 RID: 1090
	public Image slowMotionIndicatorImage;

	// Token: 0x04000443 RID: 1091
	public Image cameraIndicatorImage;

	// Token: 0x04000444 RID: 1092
	public Image vehicleIndicatorImage;
}
