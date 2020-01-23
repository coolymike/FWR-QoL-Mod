using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class UIMenuManagerPlayerInput : MonoBehaviour
{
	// Token: 0x060002E1 RID: 737 RVA: 0x0000479C File Offset: 0x0000299C
	private void Awake()
	{
		LevelManager.OnWinStateChange += this.OnWinsStateChange;
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
		LevelManager.OnGamePauseToggle += this.OnGamePauseToggle;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x000047D1 File Offset: 0x000029D1
	private void OnDestroy()
	{
		LevelManager.OnWinStateChange -= this.OnWinsStateChange;
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
		LevelManager.OnGamePauseToggle -= this.OnGamePauseToggle;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00013B64 File Offset: 0x00011D64
	private void LateUpdate()
	{
		if (InputManager.TogglePauseMenu())
		{
			LevelManager.GamePaused = !LevelManager.GamePaused;
		}
		if (!Application.isFocused && LevelManager.StatusIsWinOrLose(true))
		{
			LevelManager.GamePaused = true;
		}
		if (InputManager.ToggleBuildItemMenu() && LevelManager.BuildModeOn && !LevelManager.GamePaused && !LevelManager.StatusIsWinOrLose(true) && this.enableBuildItemMenu)
		{
			if (this.menuManager.GetActiveMenu() == 0 || this.menuManager.GetActiveMenu() == 1)
			{
				this.menuManager.CloseMenus(new int[]
				{
					0,
					1
				});
				return;
			}
			this.menuManager.GoToMenu(0);
		}
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00004806 File Offset: 0x00002A06
	private void OnWinsStateChange(LevelManager.WinState winState)
	{
		if (winState == LevelManager.WinState.None)
		{
			this.menuManager.CloseAllMenus();
			return;
		}
		this.menuManager.GoToMenu(4);
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00004823 File Offset: 0x00002A23
	private void OnGamePauseToggle(bool gamePaused)
	{
		if (gamePaused)
		{
			this.menuManager.GoToMenu(2);
			return;
		}
		this.menuManager.CloseAllMenus();
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00004840 File Offset: 0x00002A40
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (LevelManager.GamePaused)
		{
			return;
		}
		if (LevelManager.StatusIsWinOrLose(true))
		{
			return;
		}
		if (!buildModeOn)
		{
			this.menuManager.CloseMenus(new int[]
			{
				0,
				1
			});
		}
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0000486B File Offset: 0x00002A6B
	public void Resume()
	{
		LevelManager.GamePaused = false;
		this.menuManager.CloseAllMenus();
	}

	// Token: 0x04000437 RID: 1079
	public UIMenuManager menuManager;

	// Token: 0x04000438 RID: 1080
	public bool enableBuildItemMenu = true;
}
