using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B6 RID: 182
public class UIHudXPUpdate : MonoBehaviour
{
	// Token: 0x060003A3 RID: 931 RVA: 0x000051A7 File Offset: 0x000033A7
	private void Awake()
	{
		this.canvasGroup.alpha = 0f;
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x000051B9 File Offset: 0x000033B9
	private void Start()
	{
		GameData.OnXPUpdate += this.XPUpdate;
		GameData.OnLevelUpgrade += this.XPUpdate;
		this.XPUpdate();
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x000051E3 File Offset: 0x000033E3
	private void OnDestroy()
	{
		GameData.OnXPUpdate -= this.XPUpdate;
		GameData.OnLevelUpgrade -= this.XPUpdate;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00015EC4 File Offset: 0x000140C4
	private void Update()
	{
		this.timer -= Time.deltaTime;
		if (this.timer > 0f)
		{
			this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 1f, 10f * Time.deltaTime);
			this.sliderImage.fillAmount = Mathf.Lerp(this.sliderImage.fillAmount, GameData.stats.LevelCompletePercentage(), 10f * Time.deltaTime);
			return;
		}
		this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 0f, 2f * Time.deltaTime);
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00005207 File Offset: 0x00003407
	private void XPUpdate()
	{
		this.timer = 2f;
		this.levelText.text = "Lvl " + GameData.stats.Level;
	}

	// Token: 0x040004ED RID: 1261
	public CanvasGroup canvasGroup;

	// Token: 0x040004EE RID: 1262
	public Image sliderImage;

	// Token: 0x040004EF RID: 1263
	public Text levelText;

	// Token: 0x040004F0 RID: 1264
	private bool displayXPBar;

	// Token: 0x040004F1 RID: 1265
	private float timer;
}
