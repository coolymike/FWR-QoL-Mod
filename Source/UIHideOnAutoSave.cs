using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class UIHideOnAutoSave : MonoBehaviour
{
	// Token: 0x06000381 RID: 897 RVA: 0x00005073 File Offset: 0x00003273
	private void Update()
	{
		this.Hide();
	}

	// Token: 0x06000382 RID: 898 RVA: 0x000158F0 File Offset: 0x00013AF0
	private void Hide()
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
		if (WorldData.instance.autoSave)
		{
			base.gameObject.SetActive(false);
			this.itemHidden = true;
		}
	}

	// Token: 0x040004D3 RID: 1235
	private bool itemHidden;
}
