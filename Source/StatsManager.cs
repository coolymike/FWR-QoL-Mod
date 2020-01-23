using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000129 RID: 297
public class StatsManager : MonoBehaviour
{
	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000676 RID: 1654 RVA: 0x0000731F File Offset: 0x0000551F
	// (set) Token: 0x06000677 RID: 1655 RVA: 0x0000733F File Offset: 0x0000553F
	public static string[] Stats
	{
		get
		{
			if (StatsManager.instance == null)
			{
				return new string[4];
			}
			return StatsManager.instance.stats;
		}
		set
		{
			if (StatsManager.instance == null)
			{
				return;
			}
			StatsManager.instance.stats = value;
			StatsManager.instance.UpdateEndScreen();
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000678 RID: 1656 RVA: 0x00007364 File Offset: 0x00005564
	// (set) Token: 0x06000679 RID: 1657 RVA: 0x00007384 File Offset: 0x00005584
	public static string[] StatsValues
	{
		get
		{
			if (StatsManager.instance == null)
			{
				return new string[4];
			}
			return StatsManager.instance.statsValues;
		}
		set
		{
			if (StatsManager.instance == null)
			{
				return;
			}
			StatsManager.instance.statsValues = value;
			StatsManager.instance.UpdateEndScreen();
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x0600067A RID: 1658 RVA: 0x000073A9 File Offset: 0x000055A9
	// (set) Token: 0x0600067B RID: 1659 RVA: 0x000073C9 File Offset: 0x000055C9
	public static bool[] IsHighScore
	{
		get
		{
			if (StatsManager.instance == null)
			{
				return new bool[4];
			}
			return StatsManager.instance.isHighScore;
		}
		set
		{
			if (StatsManager.instance == null)
			{
				return;
			}
			StatsManager.instance.isHighScore = value;
			StatsManager.instance.UpdateEndScreen();
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x000073EE File Offset: 0x000055EE
	private void Awake()
	{
		StatsManager.instance = this;
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x00002145 File Offset: 0x00000345
	public void UpdateEndScreen()
	{
	}

	// Token: 0x0400076D RID: 1901
	public static StatsManager instance;

	// Token: 0x0400076E RID: 1902
	public Text statsText;

	// Token: 0x0400076F RID: 1903
	public Text highScoreText;

	// Token: 0x04000770 RID: 1904
	public int totalWonCollisionPoints;

	// Token: 0x04000771 RID: 1905
	private string[] stats = new string[4];

	// Token: 0x04000772 RID: 1906
	private string[] statsValues = new string[4];

	// Token: 0x04000773 RID: 1907
	private bool[] isHighScore = new bool[4];
}
