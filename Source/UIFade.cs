using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class UIFade : MonoBehaviour
{
	// Token: 0x0600037A RID: 890 RVA: 0x00004FD7 File Offset: 0x000031D7
	private void Awake()
	{
		this.canvasGroup.alpha = 1f;
		base.Invoke("StartFadeIn", 0.6f);
		base.Invoke("StopFadeIn", 6f);
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00005009 File Offset: 0x00003209
	private void Update()
	{
		if (this.fadeIn)
		{
			this.canvasGroup.alpha = Mathf.MoveTowards(this.canvasGroup.alpha, 0f, 0.25f * Time.deltaTime);
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0000503E File Offset: 0x0000323E
	private void StartFadeIn()
	{
		this.fadeIn = true;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00005047 File Offset: 0x00003247
	private void StopFadeIn()
	{
		this.canvasGroup.alpha = 0f;
		this.fadeIn = false;
	}

	// Token: 0x040004CF RID: 1231
	public CanvasGroup canvasGroup;

	// Token: 0x040004D0 RID: 1232
	private bool fadeIn;
}
