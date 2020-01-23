using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class DisableScreenSleep : MonoBehaviour
{
	// Token: 0x0600043E RID: 1086 RVA: 0x00005888 File Offset: 0x00003A88
	private void Update()
	{
		Screen.sleepTimeout = -1;
	}
}
