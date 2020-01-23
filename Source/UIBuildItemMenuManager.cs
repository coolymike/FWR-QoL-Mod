using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000086 RID: 134
public class UIBuildItemMenuManager : MonoBehaviour
{
	// Token: 0x060002AC RID: 684 RVA: 0x00004515 File Offset: 0x00002715
	private void Start()
	{
		this.playerBuildSystem = base.transform.root.GetComponent<PlayerBuildSystem>();
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000452D File Offset: 0x0000272D
	private void Update()
	{
		if (!this.filledBuildItemMenu)
		{
			this.FillBuildItemMenu();
			this.filledBuildItemMenu = true;
		}
		if (this.rebuildCount > 0)
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
			this.rebuildCount--;
		}
	}

	// Token: 0x060002AE RID: 686 RVA: 0x000132F0 File Offset: 0x000114F0
	public void FillBuildItemMenu()
	{
		this.groupLabel = UnityEngine.Object.Instantiate<GameObject>(this.groupLabelPrefab, this.BuildItemContentRowDefault.transform).GetComponent<Text>();
		this.groupLabel.gameObject.SetActive(true);
		this.groupLabel.text = "Blocks";
		this.groupLabel = UnityEngine.Object.Instantiate<GameObject>(this.groupLabelPrefab, this.BuildItemContentRowSpecial.transform).GetComponent<Text>();
		this.groupLabel.gameObject.SetActive(true);
		this.groupLabel.text = "Special";
		for (int i = 0; i < this.buildItemCatalog.buildItems.Count; i++)
		{
			if (!this.buildItemCatalog.buildItems[i].deprecated)
			{
				if (this.buildItemCatalog.buildItems[i].special)
				{
					this.buildItemButtonScript = UnityEngine.Object.Instantiate<GameObject>(this.buildItemButtonPrefab, this.BuildItemContentRowSpecial.transform).GetComponent<UIBuildItemButton>();
				}
				else
				{
					this.buildItemButtonScript = UnityEngine.Object.Instantiate<GameObject>(this.buildItemButtonPrefab, this.BuildItemContentRowDefault.transform).GetComponent<UIBuildItemButton>();
				}
				if (this.buildItemCatalog.buildItems[i].variants.Count > 1)
				{
					this.buildItemButtonScript.button.onClick.AddListener(new UnityAction(this.buildItemButtonScript.FillMenu));
				}
				else
				{
					this.buildItemButtonScript.button.onClick.AddListener(new UnityAction(this.buildItemButtonScript.SelectBuildItem));
				}
				this.FillButtonDetails(ref this.buildItemButtonScript, this.buildItemCatalog.buildItems[i]);
			}
		}
	}

	// Token: 0x060002AF RID: 687 RVA: 0x000134A4 File Offset: 0x000116A4
	public void FillVariantMenu(int itemID)
	{
		for (int i = 0; i < this.variantMenuButtons.Count; i++)
		{
			UnityEngine.Object.Destroy(this.variantMenuButtons[i]);
		}
		this.variantMenuButtons.Clear();
		this.buildItem = this.buildItemCatalog.GetBuildItem(itemID);
		this.buildItemVariantMenuText.text = this.buildItem.name;
		for (int j = 0; j < this.buildItem.variants.Count; j++)
		{
			if (!this.buildItem.variants[j].depricated)
			{
				this.buildItemButtonScript = UnityEngine.Object.Instantiate<GameObject>(this.buildItemButtonPrefab, this.buildItemVariantElementContainer.transform).GetComponent<UIBuildItemButton>();
				this.FillButtonVariantDetails(ref this.buildItemButtonScript, ref this.buildItem, ref j);
				this.buildItemButtonScript.button.onClick.AddListener(new UnityAction(this.buildItemButtonScript.SelectBuildItem));
				this.variantMenuButtons.Add(this.buildItemButtonScript.gameObject);
			}
		}
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x00004566 File Offset: 0x00002766
	public void FillButtonDetails(ref UIBuildItemButton buildItemButton, BuildItem buildItem)
	{
		this.buildItemButtonScript.menu = this.buildItemMenu;
		this.buildItemButtonScript.assocBuildItem = buildItem;
		this.buildItemButtonScript.displayAsVariant = false;
		this.buildItemButtonScript.BuildItemMenuManager = this;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000459D File Offset: 0x0000279D
	public void FillButtonVariantDetails(ref UIBuildItemButton buildItemButton, ref BuildItem buildItem, ref int variantIndex)
	{
		this.buildItemButtonScript.menu = this.buildItemVariantMenu;
		this.buildItemButtonScript.assocBuildItem = buildItem;
		this.buildItemButtonScript.displayAsVariant = true;
		buildItemButton.itemVariantID = variantIndex;
		buildItemButton.BuildItemMenuManager = this;
	}

	// Token: 0x0400040A RID: 1034
	public UIMenu buildItemMenu;

	// Token: 0x0400040B RID: 1035
	public UIMenu buildItemVariantMenu;

	// Token: 0x0400040C RID: 1036
	public UIMenuManager menuManager;

	// Token: 0x0400040D RID: 1037
	public BuildItemCatalog buildItemCatalog;

	// Token: 0x0400040E RID: 1038
	[Space]
	public RectTransform content;

	// Token: 0x0400040F RID: 1039
	public GameObject buildItemElementContainer;

	// Token: 0x04000410 RID: 1040
	public GameObject BuildItemContentRowDefault;

	// Token: 0x04000411 RID: 1041
	public GameObject BuildItemContentRowSpecial;

	// Token: 0x04000412 RID: 1042
	[Space]
	public GameObject buildItemVariantElementContainer;

	// Token: 0x04000413 RID: 1043
	public Text buildItemVariantMenuText;

	// Token: 0x04000414 RID: 1044
	[Space]
	public GameObject buildItemButtonPrefab;

	// Token: 0x04000415 RID: 1045
	public GameObject groupSpacer;

	// Token: 0x04000416 RID: 1046
	public GameObject groupLabelPrefab;

	// Token: 0x04000417 RID: 1047
	private Text groupLabel;

	// Token: 0x04000418 RID: 1048
	private UIBuildItemButton buildItemButtonScript;

	// Token: 0x04000419 RID: 1049
	[HideInInspector]
	public PlayerBuildSystem playerBuildSystem;

	// Token: 0x0400041A RID: 1050
	private List<GameObject> variantMenuButtons = new List<GameObject>();

	// Token: 0x0400041B RID: 1051
	private bool filledBuildItemMenu;

	// Token: 0x0400041C RID: 1052
	private int rebuildCount = 3;

	// Token: 0x0400041D RID: 1053
	private BuildItem buildItem;
}
