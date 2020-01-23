using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009F RID: 159
public class UICardRevealer : MonoBehaviour
{
	// Token: 0x06000326 RID: 806 RVA: 0x00014D04 File Offset: 0x00012F04
	private void Start()
	{
		this.cardTransform.offsetMax = new Vector2(this.cardTransform.offsetMax.x, -this.canvasScaler.referenceResolution.y);
		this.thisMenu.OnMenuActivated += this.OnMenuActivated;
	}

	// Token: 0x06000327 RID: 807 RVA: 0x00004C06 File Offset: 0x00002E06
	private void OnDestroy()
	{
		this.thisMenu.OnMenuActivated -= this.OnMenuActivated;
	}

	// Token: 0x06000328 RID: 808 RVA: 0x00004C1F File Offset: 0x00002E1F
	private void OnMenuActivated(bool menuActivation)
	{
		this.active = menuActivation;
		if (!this.active && this.resetScroll)
		{
			this.scrollRect.verticalNormalizedPosition = 1f;
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00014D5C File Offset: 0x00012F5C
	private void Update()
	{
		this.cardTransform.offsetMax = new Vector2(this.cardTransform.offsetMax.x, Mathf.Lerp(this.cardTransform.offsetMax.y, this.active ? 0f : (-this.canvasScaler.referenceResolution.y), this.transitionSpeed * Time.unscaledDeltaTime));
	}

	// Token: 0x04000493 RID: 1171
	private bool active;

	// Token: 0x04000494 RID: 1172
	public UIMenu thisMenu;

	// Token: 0x04000495 RID: 1173
	public CanvasScaler canvasScaler;

	// Token: 0x04000496 RID: 1174
	public RectTransform cardTransform;

	// Token: 0x04000497 RID: 1175
	public ScrollRect scrollRect;

	// Token: 0x04000498 RID: 1176
	public bool resetScroll = true;

	// Token: 0x04000499 RID: 1177
	private float transitionSpeed = 7f;
}
