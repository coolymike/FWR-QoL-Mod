using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000089 RID: 137
public class UILoading : MonoBehaviour
{
	// Token: 0x060002C1 RID: 705 RVA: 0x0000465F File Offset: 0x0000285F
	private void Start()
	{
		this.progressBar.fillAmount = 0f;
		this.loadingInProgressCanvasGroup.alpha = 1f;
		this.loadingIsDoneCanvasGroup.alpha = 0f;
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00004691 File Offset: 0x00002891
	private void Update()
	{
		if (this.sceneController == null)
		{
			return;
		}
		this.UpdateProgressBar();
		this.UpdateCanvasGroup();
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x000136F8 File Offset: 0x000118F8
	private void UpdateCanvasGroup()
	{
		if (this.loadingInProgressCanvasGroup == null || this.loadingIsDoneCanvasGroup == null)
		{
			return;
		}
		this.loadingInProgressCanvasGroup.alpha = Mathf.Lerp(this.loadingInProgressCanvasGroup.alpha, (float)(this.sceneController.IsSceneReady() ? 0 : 1), this.canvasTransitionSpeed * Time.deltaTime);
		this.loadingInProgressCanvasGroup.interactable = (this.loadingInProgressCanvasGroup.blocksRaycasts = this.sceneController.IsSceneReady());
		this.loadingIsDoneCanvasGroup.alpha = Mathf.Lerp(this.loadingIsDoneCanvasGroup.alpha, (float)((this.sceneController.IsSceneReady() && this.loadingInProgressCanvasGroup.alpha < 0.05f) ? 1 : 0), this.canvasTransitionSpeed * Time.deltaTime);
		this.loadingIsDoneCanvasGroup.interactable = (this.loadingIsDoneCanvasGroup.blocksRaycasts = this.sceneController.IsSceneReady());
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x000137F0 File Offset: 0x000119F0
	private void UpdateProgressBar()
	{
		if (this.progressBar != null)
		{
			this.progressBar.fillAmount = Mathf.Lerp(this.progressBar.fillAmount, this.sceneController.loadingProgress, this.progressBarTransitionSpeed * Time.deltaTime);
		}
	}

	// Token: 0x04000424 RID: 1060
	public SceneController sceneController;

	// Token: 0x04000425 RID: 1061
	public Image progressBar;

	// Token: 0x04000426 RID: 1062
	public CanvasGroup loadingInProgressCanvasGroup;

	// Token: 0x04000427 RID: 1063
	public CanvasGroup loadingIsDoneCanvasGroup;

	// Token: 0x04000428 RID: 1064
	private float canvasTransitionSpeed = 6f;

	// Token: 0x04000429 RID: 1065
	private float progressBarTransitionSpeed = 6f;
}
