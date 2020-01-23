using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000126 RID: 294
public class ScoreBoard : MonoBehaviour
{
	// Token: 0x06000667 RID: 1639 RVA: 0x00007272 File Offset: 0x00005472
	private void Awake()
	{
		ScoreBoard.reference = base.GetComponent<ScoreBoard>();
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0001CD24 File Offset: 0x0001AF24
	private void Update()
	{
		if (LevelManager.instance != null)
		{
			if (LevelManager.StatusIsWinOrLose(true) && !this.updatedElements)
			{
				this.canvas.gameObject.SetActive(true);
				this.UpdateText();
				this.SetWinLoseImage();
				this.updatedElements = true;
				Image[] array = this.confettiImages;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].gameObject.SetActive(LevelManager.instance.winState == LevelManager.WinState.Win);
				}
				return;
			}
			if (!LevelManager.StatusIsWinOrLose(true))
			{
				this.updatedElements = false;
				this.canvas.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0001CDC8 File Offset: 0x0001AFC8
	private void SetWinLoseImage()
	{
		if (LevelManager.instance != null)
		{
			LevelManager.WinState winState = LevelManager.instance.winState;
			if (winState == LevelManager.WinState.Win)
			{
				this.winLoseImage.sprite = this.winSprites[UnityEngine.Random.Range(0, this.winSprites.Length)];
				return;
			}
			if (winState != LevelManager.WinState.Lose)
			{
				return;
			}
			this.winLoseImage.sprite = this.loseSprites[UnityEngine.Random.Range(0, this.loseSprites.Length)];
		}
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0001CE38 File Offset: 0x0001B038
	private void UpdateText()
	{
		this.textElementStats.text = "";
		this.textElementHighScore.text = "";
		for (int i = 0; i < this.stats.Count; i++)
		{
			if (this.stats[i] != null)
			{
				Text text = this.textElementStats;
				text.text = string.Concat(new string[]
				{
					text.text,
					this.stats[i].label,
					": ",
					this.stats[i].result,
					"\n"
				});
				if (this.stats[i].isNewHighScore)
				{
					Text text2 = this.textElementHighScore;
					text2.text += "High Score!\n";
				}
				else
				{
					Text text3 = this.textElementHighScore;
					text3.text += "\n";
				}
			}
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0001CF38 File Offset: 0x0001B138
	public static void UpdateScoreBoard(string _label, string _value, bool _isNewHighScore = false)
	{
		if (ScoreBoard.reference != null && LevelManager.StatusIsWinOrLose(true) && !ScoreBoard.reference.StatExists(_label))
		{
			ScoreBoard.reference.stats.Add(new ScoreBoard.Stat
			{
				label = _label,
				result = _value,
				isNewHighScore = _isNewHighScore
			});
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0001CF90 File Offset: 0x0001B190
	public bool StatExists(string _label)
	{
		for (int i = 0; i < this.stats.Count; i++)
		{
			if (this.stats[i] != null && this.stats[i].label == _label)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400075B RID: 1883
	public static ScoreBoard reference;

	// Token: 0x0400075C RID: 1884
	[Space]
	[Header("Elements")]
	public Canvas canvas;

	// Token: 0x0400075D RID: 1885
	public Text textElementStats;

	// Token: 0x0400075E RID: 1886
	public Text textElementHighScore;

	// Token: 0x0400075F RID: 1887
	[Space]
	public Image winLoseImage;

	// Token: 0x04000760 RID: 1888
	public Sprite[] winSprites;

	// Token: 0x04000761 RID: 1889
	public Sprite[] loseSprites;

	// Token: 0x04000762 RID: 1890
	public Image[] confettiImages;

	// Token: 0x04000763 RID: 1891
	private bool updatedElements;

	// Token: 0x04000764 RID: 1892
	[Space]
	public List<ScoreBoard.Stat> stats = new List<ScoreBoard.Stat>();

	// Token: 0x02000127 RID: 295
	[Serializable]
	public class Stat
	{
		// Token: 0x04000765 RID: 1893
		public string label;

		// Token: 0x04000766 RID: 1894
		public string result;

		// Token: 0x04000767 RID: 1895
		public bool isNewHighScore;
	}
}
