using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class UIDisplayConditions : MonoBehaviour
{
	// Token: 0x06000333 RID: 819 RVA: 0x00004CD7 File Offset: 0x00002ED7
	private void Start()
	{
		base.gameObject.SetActive(this.MobileCheck());
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00004CEA File Offset: 0x00002EEA
	private bool MobileCheck()
	{
		return (this.mobileOnly && Application.isMobilePlatform) || (!this.mobileOnly && !Application.isMobilePlatform);
	}

	// Token: 0x040004A0 RID: 1184
	public bool mobileOnly;
}
