using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class LevelTimer : MonoBehaviour
{
	// Token: 0x060003F0 RID: 1008 RVA: 0x000054BA File Offset: 0x000036BA
	private void Awake()
	{
		LevelTimer.reference = base.gameObject.GetComponent<LevelTimer>();
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x000054CC File Offset: 0x000036CC
	private void Start()
	{
		if (this.startWhenPlayersInVehicles)
		{
			this.active = false;
		}
		this.timer = this.timeLimit;
		this.timeVisual = "0:00";
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00017388 File Offset: 0x00015588
	private void Update()
	{
		if (this.active)
		{
			this.TimeVisual();
		}
		if (this.startWhenPlayersInVehicles)
		{
			this.PlayersInVehicle();
		}
		if (!LevelManager.StatusIsWinOrLose(true) && this.active)
		{
			this.Timer();
		}
		if (LevelManager.StatusIsWinOrLose(true) || this.ragdollModeResetsTime)
		{
			this.timer = this.timeLimit;
		}
		this.SetWinOrLose();
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000173EC File Offset: 0x000155EC
	private void Timer()
	{
		if (this.preTimer > 0f)
		{
			this.preTimer -= Time.deltaTime;
			return;
		}
		this.timer -= Time.deltaTime;
		this.warning = (this.startWarningAtTime > 0f && this.timer <= this.startWarningAtTime);
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x000054F4 File Offset: 0x000036F4
	private void SetWinOrLose()
	{
		if (this.timer <= 0f && !LevelManager.StatusIsWinOrLose(true))
		{
			if (this.endOnWin)
			{
				LevelManager.instance.winState = LevelManager.WinState.Win;
				return;
			}
			LevelManager.instance.winState = LevelManager.WinState.Lose;
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0000552A File Offset: 0x0000372A
	private void PlayersInVehicle()
	{
		if (!PlayerSettings.instance.vehicleController.Mounted)
		{
			return;
		}
		this.active = true;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00017454 File Offset: 0x00015654
	private void TimeVisual()
	{
		this.minutes = Mathf.FloorToInt(this.timer / 60f);
		this.seconds = Mathf.FloorToInt(this.timer - (float)(this.minutes * 60));
		this.timeVisual = string.Format("{0:0}:{1:00}", this.minutes, this.seconds);
	}

	// Token: 0x04000550 RID: 1360
	public static LevelTimer reference;

	// Token: 0x04000551 RID: 1361
	[Header("Settings")]
	public bool active = true;

	// Token: 0x04000552 RID: 1362
	[Space]
	public float timeLimit = 60f;

	// Token: 0x04000553 RID: 1363
	public float preTimer;

	// Token: 0x04000554 RID: 1364
	public float startWarningAtTime = 5f;

	// Token: 0x04000555 RID: 1365
	[Header("Status")]
	public float timer;

	// Token: 0x04000556 RID: 1366
	[Space]
	public bool warning;

	// Token: 0x04000557 RID: 1367
	[Header("Conditions")]
	public bool startWhenPlayersInVehicles;

	// Token: 0x04000558 RID: 1368
	public bool endOnWin;

	// Token: 0x04000559 RID: 1369
	public bool ragdollModeResetsTime;

	// Token: 0x0400055A RID: 1370
	[Header("Time Visual")]
	public string timeVisual;

	// Token: 0x0400055B RID: 1371
	private int minutes;

	// Token: 0x0400055C RID: 1372
	private int seconds;
}
