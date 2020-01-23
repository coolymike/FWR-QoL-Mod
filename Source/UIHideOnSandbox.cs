using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class UIHideOnSandbox : MonoBehaviour
{
	// Token: 0x06000384 RID: 900 RVA: 0x0000507B File Offset: 0x0000327B
	private void Update()
	{
		this.Hide();
	}

	// Token: 0x06000385 RID: 901 RVA: 0x00015948 File Offset: 0x00013B48
	public void Hide()
	{
		if (this.itemHidden)
		{
			return;
		}
		if (WorldData.instance == null)
		{
			base.gameObject.SetActive(false);
			this.itemHidden = true;
			return;
		}
		if (!WorldData.instance.allowWorldSave && this.displayWhenSaved)
		{
			base.gameObject.SetActive(false);
			this.itemHidden = true;
		}
		if (WorldData.instance.allowWorldSave && WorldData.instance.autoSave && !this.displayWhenSaved)
		{
			base.gameObject.SetActive(false);
			this.itemHidden = true;
		}
	}

	// Token: 0x040004D4 RID: 1236
	public bool displayWhenSaved;

	// Token: 0x040004D5 RID: 1237
	private bool itemHidden;
}
