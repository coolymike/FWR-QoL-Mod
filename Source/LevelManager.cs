using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class LevelManager : MonoBehaviour
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000619 RID: 1561 RVA: 0x00006EB7 File Offset: 0x000050B7
	// (set) Token: 0x0600061A RID: 1562 RVA: 0x00006ED2 File Offset: 0x000050D2
	public static bool SlowMotion
	{
		get
		{
			return !(LevelManager.instance == null) && LevelManager.instance.slowMotion;
		}
		set
		{
			if (LevelManager.instance == null)
			{
				return;
			}
			LevelManager.instance.slowMotion = value;
		}
	}

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x0600061B RID: 1563 RVA: 0x0001C788 File Offset: 0x0001A988
	// (remove) Token: 0x0600061C RID: 1564 RVA: 0x0001C7BC File Offset: 0x0001A9BC
	public static event LevelManager.GamePauseToggleHandler OnGamePauseToggle;

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x0600061D RID: 1565 RVA: 0x00006EED File Offset: 0x000050ED
	// (set) Token: 0x0600061E RID: 1566 RVA: 0x0001C7F0 File Offset: 0x0001A9F0
	public static bool GamePaused
	{
		get
		{
			return !(LevelManager.instance == null) && LevelManager.instance.gamePaused;
		}
		set
		{
			if (LevelManager.instance == null)
			{
				return;
			}
			if (LevelManager.instance._winState != LevelManager.WinState.None)
			{
				return;
			}
			LevelManager.instance.gamePaused = value;
			LevelManager.GamePauseToggleHandler onGamePauseToggle = LevelManager.OnGamePauseToggle;
			if (onGamePauseToggle == null)
			{
				return;
			}
			onGamePauseToggle(LevelManager.instance.gamePaused);
		}
	}

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x0600061F RID: 1567 RVA: 0x0001C83C File Offset: 0x0001AA3C
	// (remove) Token: 0x06000620 RID: 1568 RVA: 0x0001C870 File Offset: 0x0001AA70
	public static event LevelManager.BuildModeToggleHandler OnBuildModeToggle;

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000621 RID: 1569 RVA: 0x00006F08 File Offset: 0x00005108
	// (set) Token: 0x06000622 RID: 1570 RVA: 0x00006F23 File Offset: 0x00005123
	public static bool BuildModeOn
	{
		get
		{
			return !(LevelManager.instance == null) && LevelManager.instance.buildModeOn;
		}
		set
		{
			if (LevelManager.instance == null)
			{
				return;
			}
			LevelManager.instance.buildModeOn = value;
			LevelManager.BuildModeToggleHandler onBuildModeToggle = LevelManager.OnBuildModeToggle;
			if (onBuildModeToggle == null)
			{
				return;
			}
			onBuildModeToggle(LevelManager.instance.buildModeOn);
		}
	}

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x06000623 RID: 1571 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
	// (remove) Token: 0x06000624 RID: 1572 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
	public static event LevelManager.WinStateChangeHandler OnWinStateChange;

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000625 RID: 1573 RVA: 0x00006F57 File Offset: 0x00005157
	// (set) Token: 0x06000626 RID: 1574 RVA: 0x00006F5F File Offset: 0x0000515F
	public LevelManager.WinState winState
	{
		get
		{
			return this._winState;
		}
		set
		{
			this._winState = value;
			if (value != LevelManager.WinState.None)
			{
				this.gamePaused = false;
			}
			LevelManager.WinStateChangeHandler onWinStateChange = LevelManager.OnWinStateChange;
			if (onWinStateChange == null)
			{
				return;
			}
			onWinStateChange(this._winState);
		}
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00006F87 File Offset: 0x00005187
	private void Awake()
	{
		LevelManager.instance = this;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00006F8F File Offset: 0x0000518F
	private void Start()
	{
		if (PlayerSettings.instance != null)
		{
			PlayerSettings.instance.simulatedRagdoll.OnRagdollToggle += this.OnRagdollToggle;
		}
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00006FB9 File Offset: 0x000051B9
	private void OnRagdollToggle(bool ragdollModeEnabled)
	{
		if (!ragdollModeEnabled)
		{
			this.winState = LevelManager.WinState.None;
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00006FC5 File Offset: 0x000051C5
	private void Update()
	{
		if (InputManager.SlowMotionToggle() && LevelManager.instance.allowSlowMotion)
		{
			LevelManager.SlowMotion = !LevelManager.SlowMotion;
		}
		this.TimeLogic();
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0001C90C File Offset: 0x0001AB0C
	private void TimeLogic()
	{
		if (LevelManager.instance == null)
		{
			return;
		}
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
		if (this.gamePaused)
		{
			Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0f, Time.unscaledDeltaTime * 3f);
			this.skipTimeTransition = true;
			return;
		}
		if (this.skipTimeTransition)
		{
			this.skipTimeTransition = false;
			this.ResetTime();
			return;
		}
		if (LevelManager.StatusIsWinOrLose(true))
		{
			Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0.15f, Time.unscaledDeltaTime * 3f);
			return;
		}
		if (LevelManager.instance.slowMotion || LevelManager.instance.forceSlowMotion)
		{
			if (GameData.cheats.slowMotionInPlayMode && !LevelManager.instance.forceSlowMotion && !PlayerSettings.instance.simulatedRagdoll.RagdollModeEnabled && !LevelManager.BuildModeOn && this.allowSlowMotionDuringPlayMode)
			{
				Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0.5f, Time.unscaledDeltaTime * 3f);
				return;
			}
			if (PlayerSettings.instance.simulatedRagdoll.RagdollModeEnabled)
			{
				Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0.25f, Time.unscaledDeltaTime * 3f);
				return;
			}
		}
		Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1f, Time.unscaledDeltaTime * 3f);
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00006FED File Offset: 0x000051ED
	private void OnDestroy()
	{
		if (PlayerSettings.instance != null)
		{
			PlayerSettings.instance.simulatedRagdoll.OnRagdollToggle -= this.OnRagdollToggle;
		}
		this.ResetTime();
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0000701D File Offset: 0x0000521D
	private void ResetTime()
	{
		Time.timeScale = 1f;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
		LevelManager.GamePaused = false;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0000703F File Offset: 0x0000523F
	public static bool StatusIsWinOrLose(bool defaultIfLevelManagerIsNull = true)
	{
		if (LevelManager.instance == null)
		{
			return defaultIfLevelManagerIsNull;
		}
		return LevelManager.instance.winState > LevelManager.WinState.None;
	}

	// Token: 0x04000735 RID: 1845
	public static LevelManager instance;

	// Token: 0x04000736 RID: 1846
	public bool allowSlowMotionDuringPlayMode = true;

	// Token: 0x04000737 RID: 1847
	public bool slowMotion;

	// Token: 0x04000739 RID: 1849
	private bool gamePaused;

	// Token: 0x0400073B RID: 1851
	private bool buildModeOn;

	// Token: 0x0400073D RID: 1853
	public LevelManager.WinState _winState;

	// Token: 0x0400073E RID: 1854
	public bool forceSlowMotion;

	// Token: 0x0400073F RID: 1855
	public bool allowSlowMotion = true;

	// Token: 0x04000740 RID: 1856
	private bool skipTimeTransition;

	// Token: 0x0200011C RID: 284
	// (Invoke) Token: 0x06000631 RID: 1585
	public delegate void GamePauseToggleHandler(bool pause);

	// Token: 0x0200011D RID: 285
	// (Invoke) Token: 0x06000635 RID: 1589
	public delegate void BuildModeToggleHandler(bool buildModeIsOn);

	// Token: 0x0200011E RID: 286
	public enum WinState
	{
		// Token: 0x04000742 RID: 1858
		None,
		// Token: 0x04000743 RID: 1859
		Win,
		// Token: 0x04000744 RID: 1860
		Lose
	}

	// Token: 0x0200011F RID: 287
	// (Invoke) Token: 0x06000639 RID: 1593
	public delegate void WinStateChangeHandler(LevelManager.WinState winStatus);
}
