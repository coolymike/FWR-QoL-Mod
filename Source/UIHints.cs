using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B2 RID: 178
public class UIHints : MonoBehaviour
{
	// Token: 0x06000389 RID: 905 RVA: 0x000050A5 File Offset: 0x000032A5
	private void Start()
	{
		base.StartCoroutine(this.DisplayMissionRoutine());
		this.playerSettings.vehicleController.OnFocusVehicle += this.OnFocusVehicle;
	}

	// Token: 0x0600038A RID: 906 RVA: 0x000050D0 File Offset: 0x000032D0
	private void OnDestroy()
	{
		this.playerSettings.vehicleController.OnFocusVehicle -= this.OnFocusVehicle;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000159DC File Offset: 0x00013BDC
	private void Update()
	{
		this.missionCanvasGroup.alpha = Mathf.MoveTowards(this.missionCanvasGroup.alpha, this.displayMission ? 1f : 0f, Time.unscaledDeltaTime * 1f);
		this.hintsCanvasGroup.alpha = Mathf.MoveTowards(this.hintsCanvasGroup.alpha, this.displayHints ? 1f : 0f, Time.unscaledDeltaTime * 3f);
	}

	// Token: 0x0600038C RID: 908 RVA: 0x000050EE File Offset: 0x000032EE
	private IEnumerator DisplayMissionRoutine()
	{
		if (!this.displayFortInvasionMission && !this.displayRoadCrossMission)
		{
			base.StartCoroutine(this.DisplayStartupHints());
			yield break;
		}
		yield return new WaitForSecondsRealtime(2f);
		if (this.displayRoadCrossMission)
		{
			this.mission.text = "Get to the beam without getting hit";
		}
		if (this.displayFortInvasionMission)
		{
			this.mission.text = "Keep RED ragdolls OUT of your base";
		}
		this.displayMission = true;
		yield return new WaitForSecondsRealtime(4f);
		this.displayMission = false;
		yield return new WaitForSecondsRealtime(1f);
		base.StartCoroutine(this.DisplayStartupHints());
		yield break;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x000050FD File Offset: 0x000032FD
	private void OnFocusVehicle(bool isFocused)
	{
		this.displayHints = isFocused;
		this.hints.text = GameData.controls.interact.ToString().ToUpper() + " - Interact";
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00005135 File Offset: 0x00003335
	private IEnumerator DisplayStartupHints()
	{
		this.hints.text = "";
		if (LevelManager.instance != null && LevelManager.instance.allowSlowMotion && this.displaySlowMotionHint)
		{
			Text text = this.hints;
			text.text = text.text + GameData.controls.toggleSlowMotion.ToString().ToUpper() + " - Toggle Slow Motion (While in Ragdoll Mode)";
		}
		if (this.displayBuildModeHints)
		{
			Text text2 = this.hints;
			text2.text += "\n";
			Text text3 = this.hints;
			text3.text = text3.text + GameData.controls.toggleBuildMode.ToString().ToUpper() + " - Toggle Build Mode\n";
			Text text4 = this.hints;
			text4.text = text4.text + GameData.controls.toggleBuildItemMenu.ToString().ToUpper() + " - Toggle Build Item Menu\n";
			Text text5 = this.hints;
			text5.text = text5.text + GameData.controls.rotateItemRight.ToString().ToUpper() + " - Rotate Right\n";
			Text text6 = this.hints;
			text6.text = text6.text + GameData.controls.rotateItemLeft.ToString().ToUpper() + " - Rotate Left";
		}
		this.displayHints = true;
		yield return new WaitForSecondsRealtime(6f);
		this.displayHints = false;
		for (;;)
		{
			if (this.playerSettings.vehicleController.Mounted && !this.displayedVehicleHint)
			{
				this.hints.text = "WASD - Move / Look \n";
				Text text7 = this.hints;
				text7.text += "Space / LMB - Action";
				this.displayedVehicleHint = true;
				this.displayHints = true;
				yield return new WaitForSecondsRealtime(6f);
				this.displayHints = false;
			}
			else if (!this.playerSettings.vehicleController.Mounted)
			{
				this.displayedVehicleHint = false;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00005144 File Offset: 0x00003344
	private IEnumerator DisplayVehicleHints()
	{
		if (this.displayBuildModeHints)
		{
			this.hints.text = "WASD - Move / Look \n";
			Text text = this.hints;
			text.text += "Space / LMB - Action";
		}
		Text text2 = this.hints;
		text2.text = text2.text + GameData.controls.toggleSlowMotion.ToString().ToUpper() + " - Toggle Slow Motion";
		this.displayHints = true;
		yield return new WaitForSecondsRealtime(4f);
		this.displayHints = false;
		yield return null;
		yield break;
	}

	// Token: 0x040004D8 RID: 1240
	public PlayerSettings playerSettings;

	// Token: 0x040004D9 RID: 1241
	[Header("Mission")]
	public CanvasGroup missionCanvasGroup;

	// Token: 0x040004DA RID: 1242
	public Text mission;

	// Token: 0x040004DB RID: 1243
	private bool displayMission;

	// Token: 0x040004DC RID: 1244
	public bool displayFortInvasionMission;

	// Token: 0x040004DD RID: 1245
	public bool displayRoadCrossMission;

	// Token: 0x040004DE RID: 1246
	[Header("Hints")]
	public CanvasGroup hintsCanvasGroup;

	// Token: 0x040004DF RID: 1247
	public Text hints;

	// Token: 0x040004E0 RID: 1248
	private bool displayHints;

	// Token: 0x040004E1 RID: 1249
	public bool displayBuildModeHints;

	// Token: 0x040004E2 RID: 1250
	public bool displaySlowMotionHint = true;

	// Token: 0x040004E3 RID: 1251
	private bool displayedVehicleHint;
}
