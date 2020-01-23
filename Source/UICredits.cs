using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class UICredits : MonoBehaviour
{
	// Token: 0x06000330 RID: 816 RVA: 0x00004CB6 File Offset: 0x00002EB6
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000331 RID: 817 RVA: 0x00014E18 File Offset: 0x00013018
	private void Update()
	{
		if (this.parentCanvasGroup.interactable)
		{
			this.rectTransform.offsetMax = new Vector2(this.rectTransform.offsetMax.x, this.rectTransform.offsetMax.y + Time.deltaTime * this.scrollSpeed);
			return;
		}
		this.rectTransform.offsetMax = new Vector2(this.rectTransform.offsetMax.x, 0f);
	}

	// Token: 0x0400049D RID: 1181
	private RectTransform rectTransform;

	// Token: 0x0400049E RID: 1182
	public float scrollSpeed = 10f;

	// Token: 0x0400049F RID: 1183
	public CanvasGroup parentCanvasGroup;
}
