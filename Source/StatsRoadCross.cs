using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class StatsRoadCross : MonoBehaviour
{
	// Token: 0x06000681 RID: 1665 RVA: 0x0001D2D4 File Offset: 0x0001B4D4
	private void Start()
	{
		this.playerSettings = PlayerSettings.instance;
		LevelManager.OnWinStateChange += this.SendStats;
		if (this.playerSettings != null)
		{
			this.playerSettings.simulatedRagdoll.OnRagdollToggle += this.OnRagdoll;
		}
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x00007422 File Offset: 0x00005622
	private void OnDestroy()
	{
		LevelManager.OnWinStateChange -= this.SendStats;
		if (this.playerSettings != null)
		{
			this.playerSettings.simulatedRagdoll.OnRagdollToggle -= this.OnRagdoll;
		}
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0000745F File Offset: 0x0000565F
	private void Update()
	{
		if (!LevelManager.StatusIsWinOrLose(true))
		{
			this.time += Time.deltaTime;
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0000747B File Offset: 0x0000567B
	private void OnRagdoll(bool ragdollModeEnabled)
	{
		if (ragdollModeEnabled)
		{
			this.ragdollCount++;
		}
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0001D328 File Offset: 0x0001B528
	private void SendStats(LevelManager.WinState winState)
	{
		this.addedPoints = 0;
		this.time = Mathf.Round(this.time * 10f) / 10f;
		UIEndScreen.SetStats("Time (Whip's Mod used)", 0);
		UIEndScreen.SetValues(this.time.ToString() + "s", 0);
		if (this.time < GameData.highScores.roadCrossTime || GameData.highScores.roadCrossNewSave)
		{
			UIEndScreen.SetBestValues(this.time.ToString() + "s", 0);
		}
		else
		{
			UIEndScreen.SetBestValues(GameData.highScores.roadCrossTime.ToString() + "s", 0);
		}
		UIEndScreen.SetStats("Ragdoll Count", 1);
		UIEndScreen.SetValues(this.ragdollCount.ToString(), 1);
		if (this.ragdollCount < GameData.highScores.roadCrossRagdollCount || GameData.highScores.roadCrossNewSave)
		{
			UIEndScreen.SetBestValues(this.ragdollCount.ToString(), 1);
		}
		else
		{
			UIEndScreen.SetBestValues(GameData.highScores.roadCrossRagdollCount.ToString(), 1);
		}
		if (this.ragdollCount < GameData.highScores.roadCrossRagdollCount || GameData.highScores.roadCrossNewSave)
		{
			GameData.highScores.roadCrossRagdollCount = this.ragdollCount;
		}
		if (this.time < GameData.highScores.roadCrossTime || GameData.highScores.roadCrossNewSave)
		{
			GameData.highScores.roadCrossTime = this.time;
		}
		if (GameData.highScores.roadCrossNewSave)
		{
			GameData.highScores.roadCrossNewSave = false;
		}
		this.addedPoints += Mathf.RoundToInt(Mathf.Clamp(1200f - this.time * 8f, 0f, 1200f));
		this.addedPoints += Mathf.RoundToInt(Mathf.Clamp(1000f - (float)this.ragdollCount * 100f, 0f, 1000f));
		GameData.stats.XP += this.addedPoints;
		GameData.SaveStats();
	}

	// Token: 0x04000776 RID: 1910
	private float time;

	// Token: 0x04000777 RID: 1911
	private int ragdollCount;

	// Token: 0x04000778 RID: 1912
	private PlayerSettings playerSettings;

	// Token: 0x04000779 RID: 1913
	private int addedPoints;
}
