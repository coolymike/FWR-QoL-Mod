using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000092 RID: 146
public class UISaveWorld : MonoBehaviour
{
	// Token: 0x060002F3 RID: 755 RVA: 0x00013FA0 File Offset: 0x000121A0
	public void SaveWorld()
	{
		if (WorldData.instance == null)
		{
			return;
		}
		if (FileManager.TextIsNotValid(this.inputField.text))
		{
			return;
		}
		WorldData.instance.data.worldName = this.inputField.text;
		WorldData.instance.SaveWorld();
		this.menuManager.GoToMenu(2);
	}

	// Token: 0x0400044E RID: 1102
	public UIMenuManager menuManager;

	// Token: 0x0400044F RID: 1103
	public InputField inputField;
}
