using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A4 RID: 164
public class UIEndScreen : MonoBehaviour
{
	// Token: 0x0600033A RID: 826 RVA: 0x00004D4F File Offset: 0x00002F4F
	private void Awake()
	{
		UIEndScreen.instance = this;
		this.stats = new string[4];
		this.values = new string[4];
		this.bestValues = new string[4];
		this.isHighScore = new bool[4];
		this.UpdateEndScreen();
	}

	// Token: 0x0600033B RID: 827 RVA: 0x00004D8D File Offset: 0x00002F8D
	public static void SetStats(string text, int line)
	{
		if (UIEndScreen.instance == null)
		{
			return;
		}
		UIEndScreen.instance.stats[line] = text;
		UIEndScreen.instance.UpdateEndScreen();
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00004DB4 File Offset: 0x00002FB4
	public static void SetValues(string text, int line)
	{
		if (UIEndScreen.instance == null)
		{
			return;
		}
		UIEndScreen.instance.values[line] = text;
		UIEndScreen.instance.UpdateEndScreen();
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00004DDB File Offset: 0x00002FDB
	public static void SetBestValues(string text, int line)
	{
		if (UIEndScreen.instance == null)
		{
			return;
		}
		UIEndScreen.instance.bestValues[line] = text;
		UIEndScreen.instance.UpdateEndScreen();
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00004E02 File Offset: 0x00003002
	public static void SetHighScore(bool isHighScore, int line)
	{
		if (UIEndScreen.instance == null)
		{
			return;
		}
		UIEndScreen.instance.isHighScore[line] = isHighScore;
		UIEndScreen.instance.UpdateEndScreen();
	}

	// Token: 0x0600033F RID: 831 RVA: 0x00014ED0 File Offset: 0x000130D0
	public void UpdateEndScreen()
	{
		for (int i = 0; i < this.stats.Length; i++)
		{
			this.statsDescription.text = "";
			Text text = this.statsDescription;
			text.text = text.text + this.stats[0] + "\n";
			Text text2 = this.statsDescription;
			text2.text = text2.text + this.stats[1] + "\n";
			Text text3 = this.statsDescription;
			text3.text = text3.text + this.stats[2] + "\n";
			Text text4 = this.statsDescription;
			text4.text = text4.text + this.stats[3] + "\n";
		}
		for (int j = 0; j < this.values.Length; j++)
		{
			this.statsValue.text = "";
			Text text5 = this.statsValue;
			text5.text = text5.text + this.values[0] + "\n";
			Text text6 = this.statsValue;
			text6.text = text6.text + this.values[1] + "\n";
			Text text7 = this.statsValue;
			text7.text = text7.text + this.values[2] + "\n";
			Text text8 = this.statsValue;
			text8.text = text8.text + this.values[3] + "\n";
		}
		for (int k = 0; k < this.bestValues.Length; k++)
		{
			this.statsBestValue.text = "";
			Text text9 = this.statsBestValue;
			text9.text = text9.text + this.bestValues[0] + "\n";
			Text text10 = this.statsBestValue;
			text10.text = text10.text + this.bestValues[1] + "\n";
			Text text11 = this.statsBestValue;
			text11.text = text11.text + this.bestValues[2] + "\n";
			Text text12 = this.statsBestValue;
			text12.text = text12.text + this.bestValues[3] + "\n";
		}
		for (int l = 0; l < this.isHighScore.Length; l++)
		{
			this.highScoreText.text = "";
			Text text13 = this.highScoreText;
			text13.text += (this.isHighScore[0] ? "High Score!\n" : "\n");
			Text text14 = this.highScoreText;
			text14.text += (this.isHighScore[1] ? "High Score!\n" : "\n");
			Text text15 = this.highScoreText;
			text15.text += (this.isHighScore[2] ? "High Score!\n" : "\n");
			Text text16 = this.highScoreText;
			text16.text += (this.isHighScore[3] ? "High Score!\n" : "\n");
		}
	}

	// Token: 0x040004A2 RID: 1186
	public static UIEndScreen instance;

	// Token: 0x040004A3 RID: 1187
	public Text statsDescription;

	// Token: 0x040004A4 RID: 1188
	public Text statsValue;

	// Token: 0x040004A5 RID: 1189
	public Text statsBestValue;

	// Token: 0x040004A6 RID: 1190
	public Text highScoreText;

	// Token: 0x040004A7 RID: 1191
	public int totalWonCollisionPoints;

	// Token: 0x040004A8 RID: 1192
	private string[] stats = new string[4];

	// Token: 0x040004A9 RID: 1193
	private string[] values = new string[4];

	// Token: 0x040004AA RID: 1194
	private string[] bestValues = new string[4];

	// Token: 0x040004AB RID: 1195
	private bool[] isHighScore = new bool[4];
}
