using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BB RID: 187
public class UITextDisplayWorldSaveLocation : MonoBehaviour
{
	// Token: 0x060003B6 RID: 950 RVA: 0x000052C2 File Offset: 0x000034C2
	private void Start()
	{
		this.text.text = "World Saves: " + FileManager.worldPath;
	}

	// Token: 0x0400050A RID: 1290
	public Text text;
}
