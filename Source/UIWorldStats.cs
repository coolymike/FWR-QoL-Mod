using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009C RID: 156
public class UIWorldStats : MonoBehaviour
{
	// Token: 0x0600031B RID: 795 RVA: 0x00004B2B File Offset: 0x00002D2B
	private void Awake()
	{
		this.menu.OnMenuActivated += this.MenuActivated;
	}

	// Token: 0x0600031C RID: 796 RVA: 0x00004B44 File Offset: 0x00002D44
	private void OnDestroy()
	{
		this.menu.OnMenuActivated -= this.MenuActivated;
	}

	// Token: 0x0600031D RID: 797 RVA: 0x00014A78 File Offset: 0x00012C78
	private void MenuActivated(bool menuActive)
	{
		if (!menuActive)
		{
			return;
		}
		this.statsText.text = "";
		Text text = this.statsText;
		text.text = text.text + "Building Blocks: " + WorldData.GetBuildItemsInScene(false).ToString("N0");
		Text text2 = this.statsText;
		text2.text += "\n";
		Text text3 = this.statsText;
		text3.text = text3.text + "Special Items: " + WorldData.GetBuildItemsInScene(true).ToString("N0");
		Text text4 = this.statsText;
		text4.text += "\n\n";
		Text text5 = this.statsText;
		text5.text = text5.text + "Ragdolls: " + WorldData.ItemsPlacedFromBuildItemID(176509).ToString("N0");
		Text text6 = this.statsText;
		text6.text += "\n";
		Text text7 = this.statsText;
		text7.text = text7.text + "Flags: " + WorldData.ItemsPlacedFromBuildItemID(584134).ToString("N0");
	}

	// Token: 0x04000481 RID: 1153
	public Text statsText;

	// Token: 0x04000482 RID: 1154
	public UIMenu menu;
}
