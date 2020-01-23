using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class UIHideUntilLevelReached : MonoBehaviour
{
	// Token: 0x06000387 RID: 903 RVA: 0x00005083 File Offset: 0x00003283
	private void Start()
	{
		this.objectToHide.SetActive(GameData.stats.Level >= this.levelToReveal);
	}

	// Token: 0x040004D6 RID: 1238
	public GameObject objectToHide;

	// Token: 0x040004D7 RID: 1239
	public int levelToReveal;
}
