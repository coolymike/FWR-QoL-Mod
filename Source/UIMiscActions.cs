using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class UIMiscActions : MonoBehaviour
{
	// Token: 0x060003A9 RID: 937 RVA: 0x00005238 File Offset: 0x00003438
	public void QuitGame()
	{
		Application.Quit();
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0000523F File Offset: 0x0000343F
	public void WatchFWR()
	{
		Application.OpenURL("https://www.youtube.com/watch?v=oB33HLoceiw&list=PLRlxUmfINzCQgR7zXyKwU-xIuse68nKGR");
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0000524B File Offset: 0x0000344B
	public void GoToDiscord()
	{
		Application.OpenURL("https://discord.gg/NrxaCW6");
	}
}
