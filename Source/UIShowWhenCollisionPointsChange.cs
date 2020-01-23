using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B9 RID: 185
public class UIShowWhenCollisionPointsChange : MonoBehaviour
{
	// Token: 0x060003B0 RID: 944 RVA: 0x00005285 File Offset: 0x00003485
	private void Start()
	{
		this.startingPosition = this.container.anchoredPosition;
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x000160E8 File Offset: 0x000142E8
	private void Update()
	{
		this.pointsText.text = GameData.collisionPoints.ToString("#,##0");
		if (GameData.collisionPoints != this.lastCollisionPoints)
		{
			this.displayTimer = 2f;
			this.container.anchoredPosition = new Vector2(this.container.anchoredPosition.x, -25f);
			this.lastCollisionPoints = GameData.collisionPoints;
		}
		this.container.anchoredPosition = Vector2.Lerp(this.container.anchoredPosition, this.startingPosition, 10f * Time.deltaTime);
		if (this.displayTimer > 0f)
		{
			this.displayTimer -= Time.deltaTime;
		}
		if (this.displayTimer > 0f)
		{
			this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 1f, 10f * Time.deltaTime);
			return;
		}
		this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 0f, 2f * Time.deltaTime);
	}

	// Token: 0x040004FC RID: 1276
	public RectTransform container;

	// Token: 0x040004FD RID: 1277
	private Vector2 startingPosition;

	// Token: 0x040004FE RID: 1278
	public Text pointsText;

	// Token: 0x040004FF RID: 1279
	public CanvasGroup canvasGroup;

	// Token: 0x04000500 RID: 1280
	private int lastCollisionPoints;

	// Token: 0x04000501 RID: 1281
	private float displayTimer = 2f;
}
