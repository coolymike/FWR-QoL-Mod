using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B8 RID: 184
public class UITextDisplayWorldSaveLocation : MonoBehaviour
{
	// Token: 0x060003A4 RID: 932 RVA: 0x00005254 File Offset: 0x00003454
	private void Start()
	{
		this.text.text = "World Saves: " + FileManager.worldPath;
	}

	// Token: 0x040004FE RID: 1278
	public Text text;
}
