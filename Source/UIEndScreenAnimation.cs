using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A5 RID: 165
public class UIEndScreenAnimation : MonoBehaviour
{
	// Token: 0x06000341 RID: 833 RVA: 0x00004E61 File Offset: 0x00003061
	private void Start()
	{
		this.ResetAll();
		LevelManager.OnWinStateChange += this.OnWinStateChange;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00004E7A File Offset: 0x0000307A
	private void OnDestroy()
	{
		LevelManager.OnWinStateChange -= this.OnWinStateChange;
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00004E8D File Offset: 0x0000308D
	private void OnWinStateChange(LevelManager.WinState winState)
	{
		this.ResetAll();
		base.StartCoroutine(this.BackgroundImageRoutine());
		if (winState == LevelManager.WinState.Win)
		{
			this.StartWinRoutine();
			return;
		}
		if (winState == LevelManager.WinState.Lose)
		{
			this.StartLoseRoutine();
		}
	}

	// Token: 0x06000344 RID: 836 RVA: 0x000151D8 File Offset: 0x000133D8
	private void StartWinRoutine()
	{
		this.winStateGraphic.sprite = this.winTexts[UnityEngine.Random.Range(0, this.winTexts.Length)];
		base.StartCoroutine(this.WinStatusGraphicWinRoutine());
		if (this.displayStats)
		{
			base.StartCoroutine(this.EnableStatsWinRoutine());
		}
		if (this.displayButtons)
		{
			base.StartCoroutine(this.EnableButtonsWinRoutine());
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0001523C File Offset: 0x0001343C
	private void StartLoseRoutine()
	{
		this.winStateGraphic.sprite = this.loseTexts[UnityEngine.Random.Range(0, this.loseTexts.Length)];
		base.StartCoroutine(this.WinStatusGraphicLoseRoutine());
		if (this.displayStats)
		{
			base.StartCoroutine(this.EnableStatsLoseRoutine());
		}
		if (this.displayButtons)
		{
			base.StartCoroutine(this.EnableButtonsLoseRoutine());
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x000152A0 File Offset: 0x000134A0
	private void ResetAll()
	{
		base.StopAllCoroutines();
		this.winStateGraphicTransform.anchorMin = new Vector2(0f, 0f);
		this.winStateGraphicTransform.localScale = new Vector3(0f, 0f, 0f);
		this.backgroundImage.color = new Color(0f, 0f, 0f, 0f);
		this.ToggleButtons(false);
		this.statsDescription.SetActive(false);
		this.statsValues.SetActive(false);
		this.statsHighScore.SetActive(false);
		this.statsHeadingDescription.SetActive(false);
		this.statsHeadingValues.SetActive(false);
		this.statsHeadingHighScore.SetActive(false);
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00004EB7 File Offset: 0x000030B7
	private IEnumerator BackgroundImageRoutine()
	{
		yield return new WaitForSecondsRealtime(3.8f);
		for (;;)
		{
			this.backgroundImage.color = Vector4.Lerp(this.backgroundImage.color, new Vector4(0f, 0f, 0f, 0.95f), 2f * Time.unscaledDeltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000348 RID: 840 RVA: 0x00004EC6 File Offset: 0x000030C6
	private IEnumerator WinStatusGraphicWinRoutine()
	{
		yield return new WaitForSecondsRealtime(5f);
		for (;;)
		{
			this.winStateGraphicTransform.localScale = Vector3.Lerp(this.winStateGraphicTransform.localScale, new Vector3(1f, 1f, 1f), 3f * Time.unscaledDeltaTime);
			this.winStateGraphicTransform.anchorMin = Vector2.Lerp(this.winStateGraphicTransform.anchorMin, new Vector2(0f, 0.5f), 0.8f * Time.unscaledDeltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00004ED5 File Offset: 0x000030D5
	private IEnumerator WinStatusGraphicLoseRoutine()
	{
		yield return new WaitForSecondsRealtime(3.2f);
		for (;;)
		{
			this.winStateGraphicTransform.localScale = Vector3.Lerp(this.winStateGraphicTransform.localScale, new Vector3(1f, 1f, 1f), 2f * Time.unscaledDeltaTime);
			this.winStateGraphicTransform.anchorMin = Vector2.Lerp(this.winStateGraphicTransform.anchorMin, new Vector2(0f, 0.5f), 0.7f * Time.unscaledDeltaTime);
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00004EE4 File Offset: 0x000030E4
	private IEnumerator EnableStatsWinRoutine()
	{
		yield return new WaitForSecondsRealtime(8.65f);
		this.statsHeadingDescription.SetActive(true);
		this.statsDescription.SetActive(true);
		yield return new WaitForSecondsRealtime(0.15f);
		this.statsHeadingValues.SetActive(true);
		this.statsValues.SetActive(true);
		yield return new WaitForSecondsRealtime(0.15f);
		this.statsHeadingHighScore.SetActive(true);
		this.statsHighScore.SetActive(true);
		yield break;
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00004EF3 File Offset: 0x000030F3
	private IEnumerator EnableButtonsWinRoutine()
	{
		yield return new WaitForSecondsRealtime(9f);
		this.ToggleButtons(true);
		yield break;
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00004F02 File Offset: 0x00003102
	private IEnumerator EnableStatsLoseRoutine()
	{
		yield return new WaitForSecondsRealtime(6.38f);
		this.statsHeadingDescription.SetActive(true);
		this.statsDescription.SetActive(true);
		yield return new WaitForSecondsRealtime(0.05f);
		this.statsHeadingValues.SetActive(true);
		this.statsValues.SetActive(true);
		yield return new WaitForSecondsRealtime(0.05f);
		this.statsHeadingHighScore.SetActive(true);
		this.statsHighScore.SetActive(true);
		yield break;
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00004F11 File Offset: 0x00003111
	private IEnumerator EnableButtonsLoseRoutine()
	{
		yield return new WaitForSecondsRealtime(6.55f);
		this.ToggleButtons(true);
		yield break;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00015360 File Offset: 0x00013560
	private void ToggleButtons(bool enable)
	{
		for (int i = 0; i < this.buttons.Length; i++)
		{
			this.buttons[i].SetActive(enable);
		}
	}

	// Token: 0x040004AC RID: 1196
	public Sprite[] winTexts;

	// Token: 0x040004AD RID: 1197
	public Sprite[] loseTexts;

	// Token: 0x040004AE RID: 1198
	public bool displayStats = true;

	// Token: 0x040004AF RID: 1199
	public bool displayButtons = true;

	// Token: 0x040004B0 RID: 1200
	[Space]
	public Image backgroundImage;

	// Token: 0x040004B1 RID: 1201
	public Image winStateGraphic;

	// Token: 0x040004B2 RID: 1202
	public RectTransform winStateGraphicTransform;

	// Token: 0x040004B3 RID: 1203
	public GameObject[] buttons;

	// Token: 0x040004B4 RID: 1204
	public GameObject statsDescription;

	// Token: 0x040004B5 RID: 1205
	public GameObject statsValues;

	// Token: 0x040004B6 RID: 1206
	public GameObject statsHighScore;

	// Token: 0x040004B7 RID: 1207
	public GameObject statsHeadingDescription;

	// Token: 0x040004B8 RID: 1208
	public GameObject statsHeadingValues;

	// Token: 0x040004B9 RID: 1209
	public GameObject statsHeadingHighScore;
}
