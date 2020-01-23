using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class UIMenu : MonoBehaviour
{
	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060002C6 RID: 710 RVA: 0x000046CC File Offset: 0x000028CC
	// (set) Token: 0x060002C7 RID: 711 RVA: 0x000046D4 File Offset: 0x000028D4
	public bool Active
	{
		get
		{
			return this.active;
		}
		set
		{
			if (this.active == value)
			{
				return;
			}
			this.active = value;
			UIMenu.BoolChangeHandler onMenuActivated = this.OnMenuActivated;
			if (onMenuActivated != null)
			{
				onMenuActivated(value);
			}
			this.UpdateCanvasGroupInteractability();
		}
	}

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x060002C8 RID: 712 RVA: 0x00013840 File Offset: 0x00011A40
	// (remove) Token: 0x060002C9 RID: 713 RVA: 0x00013878 File Offset: 0x00011A78
	public event UIMenu.BoolChangeHandler OnMenuActivated;

	// Token: 0x060002CA RID: 714 RVA: 0x000046FF File Offset: 0x000028FF
	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
		this.UpdateCanvasGroupInteractability();
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00004713 File Offset: 0x00002913
	private void Update()
	{
		this.UpdateCanvasGroup();
	}

	// Token: 0x060002CC RID: 716 RVA: 0x000138B0 File Offset: 0x00011AB0
	private void UpdateCanvasGroup()
	{
		if (!this.controlCanvasGroup)
		{
			return;
		}
		this.canvasGroup.alpha = Mathf.Lerp(this.canvasGroup.alpha, this.active ? 1f : 0f, this.transitionSpeed * Time.unscaledDeltaTime);
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000471B File Offset: 0x0000291B
	private void UpdateCanvasGroupInteractability()
	{
		if (!this.controlCanvasGroup)
		{
			return;
		}
		this.canvasGroup.interactable = this.active;
		this.canvasGroup.blocksRaycasts = this.active;
	}

	// Token: 0x0400042A RID: 1066
	[HideInInspector]
	public UIMenuManager menuManager;

	// Token: 0x0400042B RID: 1067
	[HideInInspector]
	public int thisMenuID;

	// Token: 0x0400042C RID: 1068
	private bool active;

	// Token: 0x0400042E RID: 1070
	public bool controlCanvasGroup;

	// Token: 0x0400042F RID: 1071
	private CanvasGroup canvasGroup;

	// Token: 0x04000430 RID: 1072
	private float transitionSpeed = 10f;

	// Token: 0x0200008B RID: 139
	// (Invoke) Token: 0x060002D0 RID: 720
	public delegate void BoolChangeHandler(bool value);
}
