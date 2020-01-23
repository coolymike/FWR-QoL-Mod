using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009A RID: 154
public class UIWorldInfo : MonoBehaviour
{
	// Token: 0x06000315 RID: 789 RVA: 0x00004ADF File Offset: 0x00002CDF
	public void UpdateWorldInfo(string fileLocation, string worldName)
	{
		this.worldFileLocation = fileLocation;
		this.placeholderText.text = worldName;
		this.inputField.text = "";
	}

	// Token: 0x06000316 RID: 790 RVA: 0x000149D0 File Offset: 0x00012BD0
	public void SaveChanges()
	{
		if (FileManager.TextIsNotValid(this.inputField.text))
		{
			return;
		}
		if (!FileManager.RenameFile(this.worldFileLocation, this.inputField.text))
		{
			return;
		}
		this.sandboxMenu.RefreshWorldButtonList();
		this.menuManager.GoToMenu(1);
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00004B04 File Offset: 0x00002D04
	public void DeleteWorld()
	{
		if (!FileManager.Delete(this.worldFileLocation))
		{
			return;
		}
		this.sandboxMenu.RefreshWorldButtonList();
		this.menuManager.GoToMenu(1);
	}

	// Token: 0x04000478 RID: 1144
	public UIMenuManager menuManager;

	// Token: 0x04000479 RID: 1145
	public UISandbox sandboxMenu;

	// Token: 0x0400047A RID: 1146
	public InputField inputField;

	// Token: 0x0400047B RID: 1147
	public Text placeholderText;

	// Token: 0x0400047C RID: 1148
	public Button saveButton;

	// Token: 0x0400047D RID: 1149
	public string worldFileLocation;
}
