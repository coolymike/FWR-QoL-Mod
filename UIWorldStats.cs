using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000099 RID: 153
public class UIWorldStats : MonoBehaviour
{
	// Token: 0x06000309 RID: 777 RVA: 0x00004AB8 File Offset: 0x00002CB8
	private void Awake()
	{
		this.menu.OnMenuActivated += this.MenuActivated;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00004AD1 File Offset: 0x00002CD1
	private void OnDestroy()
	{
		this.menu.OnMenuActivated -= this.MenuActivated;
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0001478C File Offset: 0x0001298C
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

	// Token: 0x04000475 RID: 1141
	public Text statsText;

	// Token: 0x04000476 RID: 1142
	public UIMenu menu;
}
