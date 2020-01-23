using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class StatsFortInvasion : MonoBehaviour
{
	// Token: 0x0600066F RID: 1647 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
	private void Start()
	{
		LevelManager.OnWinStateChange += this.GameEnded;
		for (int i = 0; i < this.killZones.Length; i++)
		{
			this.killZones[i].OnAcceptableRagdollEnter += this.AcceptableRagdollEntered;
			this.killZones[i].OnUnacceptableRagdollEnter += this.UnacceptableRagdollEntered;
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0001D044 File Offset: 0x0001B244
	private void OnDestroy()
	{
		LevelManager.OnWinStateChange -= this.GameEnded;
		for (int i = 0; i < this.killZones.Length; i++)
		{
			this.killZones[i].OnAcceptableRagdollEnter -= this.AcceptableRagdollEntered;
			this.killZones[i].OnUnacceptableRagdollEnter -= this.UnacceptableRagdollEntered;
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00007292 File Offset: 0x00005492
	private void AcceptableRagdollEntered(int ragdollsEntered)
	{
		if (LevelManager.StatusIsWinOrLose(true))
		{
			return;
		}
		this.totalAcceptableRagdolls += ragdollsEntered;
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x000072AB File Offset: 0x000054AB
	private void UnacceptableRagdollEntered(int ragdollsEntered)
	{
		if (LevelManager.StatusIsWinOrLose(true))
		{
			return;
		}
		this.totalUnacceptableRagdolls += ragdollsEntered;
		if (this.loseOnUnacceptableRagdollLimit > 0 && this.totalUnacceptableRagdolls >= this.loseOnUnacceptableRagdollLimit)
		{
			LevelManager.instance.winState = LevelManager.WinState.Lose;
		}
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x000072E6 File Offset: 0x000054E6
	private void Update()
	{
		if (this.totalAcceptableRagdolls > GameData.highScores.fortInvasionGoodEntered)
		{
			LevelTimer.reference.endOnWin = true;
			return;
		}
		if (this.totalUnacceptableRagdolls < this.loseOnUnacceptableRagdollLimit)
		{
			LevelTimer.reference.endOnWin = true;
		}
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0001D0A8 File Offset: 0x0001B2A8
	private void GameEnded(LevelManager.WinState winState)
	{
		if (winState == LevelManager.WinState.None)
		{
			return;
		}
		this.addedPoints = 0;
		UIEndScreen.SetStats("Peaceful Ragdolls in Base", 0);
		UIEndScreen.SetValues(this.totalAcceptableRagdolls.ToString(), 0);
		if (this.totalAcceptableRagdolls > GameData.highScores.fortInvasionGoodEntered || GameData.highScores.fortInvasionNewSave)
		{
			UIEndScreen.SetBestValues(this.totalAcceptableRagdolls.ToString(), 0);
		}
		else
		{
			UIEndScreen.SetBestValues(GameData.highScores.fortInvasionGoodEntered.ToString(), 0);
		}
		UIEndScreen.SetStats("Invader-dolls in Base", 1);
		UIEndScreen.SetValues(this.totalUnacceptableRagdolls.ToString(), 1);
		if (this.totalUnacceptableRagdolls < GameData.highScores.fortInvasionBadEntered || GameData.highScores.fortInvasionNewSave)
		{
			UIEndScreen.SetBestValues(this.totalUnacceptableRagdolls.ToString(), 1);
		}
		else
		{
			UIEndScreen.SetBestValues(GameData.highScores.fortInvasionBadEntered.ToString(), 1);
		}
		if (winState == LevelManager.WinState.Lose)
		{
			this.addedPoints += 75;
		}
		else
		{
			this.addedPoints += Mathf.RoundToInt((float)(10 * (10 - 10 * (this.totalUnacceptableRagdolls / this.loseOnUnacceptableRagdollLimit))));
			this.addedPoints += Mathf.RoundToInt((float)(this.totalAcceptableRagdolls * 10));
		}
		GameData.stats.XP += this.addedPoints;
		GameData.SaveStats();
		if (this.totalAcceptableRagdolls > GameData.highScores.fortInvasionGoodEntered || GameData.highScores.fortInvasionNewSave)
		{
			GameData.highScores.fortInvasionGoodEntered = this.totalAcceptableRagdolls;
		}
		if (this.totalUnacceptableRagdolls < GameData.highScores.fortInvasionBadEntered || GameData.highScores.fortInvasionNewSave)
		{
			GameData.highScores.fortInvasionBadEntered = this.totalUnacceptableRagdolls;
		}
		GameData.highScores.fortInvasionNewSave = false;
		GameData.SaveHighScores();
	}

	// Token: 0x04000768 RID: 1896
	public RagdollKillZone[] killZones;

	// Token: 0x04000769 RID: 1897
	public int totalAcceptableRagdolls;

	// Token: 0x0400076A RID: 1898
	public int totalUnacceptableRagdolls;

	// Token: 0x0400076B RID: 1899
	public int loseOnUnacceptableRagdollLimit;

	// Token: 0x0400076C RID: 1900
	private int addedPoints;
}
