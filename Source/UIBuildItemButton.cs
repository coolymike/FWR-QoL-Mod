using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000081 RID: 129
public class UIBuildItemButton : MonoBehaviour
{
	// Token: 0x0600028F RID: 655 RVA: 0x00012BD8 File Offset: 0x00010DD8
	private void Start()
	{
		GameData.OnLevelUpgrade += this.LevelUpgrade;
		this.menu.OnMenuActivated += this.OnMenuUpdated;
		if (this.displayAsVariant)
		{
			this.titleCentered.text = this.assocBuildItem.variants[this.itemVariantID].name;
			this.titleUp.text = this.assocBuildItem.variants[this.itemVariantID].name;
			this.itemDescription.text = this.assocBuildItem.variants[this.itemVariantID].description;
			this.imageIcon.sprite = ((this.assocBuildItem.variants[this.itemVariantID].thumbnail == null) ? this.assocBuildItem.thumbnail : this.assocBuildItem.variants[this.itemVariantID].thumbnail);
			if (this.assocBuildItem.variants[this.itemVariantID].variantThumbnail != null)
			{
				this.imageVariant.gameObject.SetActive(true);
				this.imageVariant.sprite = this.assocBuildItem.variants[this.itemVariantID].variantThumbnail;
				this.imageVariant.color = this.assocBuildItem.variants[this.itemVariantID].variantColor;
			}
		}
		else
		{
			this.titleCentered.text = this.assocBuildItem.leadingName;
			this.titleUp.text = this.assocBuildItem.leadingName;
			this.itemDescription.text = this.assocBuildItem.description;
			this.imageIcon.sprite = this.assocBuildItem.thumbnail;
		}
		this.gameObjectPrice.SetActive(!this.assocBuildItem.Unlocked());
		this.textPrice.text = "Lvl " + this.assocBuildItem.unlockLevel;
		this.titleCentered.gameObject.SetActive(this.assocBuildItem.description == null || this.assocBuildItem.description == "");
		this.titleUp.gameObject.SetActive(this.assocBuildItem.description != null && this.assocBuildItem.description != "");
		this.itemDescription.gameObject.SetActive(this.assocBuildItem.description != null && this.assocBuildItem.description != "");
		this.LevelUpgrade();
	}

	// Token: 0x06000290 RID: 656 RVA: 0x000043D8 File Offset: 0x000025D8
	private void OnDestroy()
	{
		GameData.OnLevelUpgrade -= this.LevelUpgrade;
		this.menu.OnMenuActivated -= this.OnMenuUpdated;
	}

	// Token: 0x06000291 RID: 657 RVA: 0x00004402 File Offset: 0x00002602
	private void OnMenuUpdated(bool menuActivated)
	{
		if (menuActivated)
		{
			this.LevelUpgrade();
		}
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000440D File Offset: 0x0000260D
	private void LevelUpgrade()
	{
		this.gameObjectPrice.SetActive(!this.assocBuildItem.Unlocked());
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00012EA0 File Offset: 0x000110A0
	public void SelectBuildItem()
	{
		if (!this.assocBuildItem.Unlocked())
		{
			return;
		}
		this.BuildItemMenuManager.playerBuildSystem.SetPlaceholder(this.assocBuildItem.variants[this.itemVariantID]);
		this.BuildItemMenuManager.menuManager.CloseAllMenus();
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00004428 File Offset: 0x00002628
	public void FillMenu()
	{
		if (!this.assocBuildItem.Unlocked())
		{
			return;
		}
		this.BuildItemMenuManager.FillVariantMenu(this.assocBuildItem.id);
		this.BuildItemMenuManager.menuManager.GoToMenu(1);
	}

	// Token: 0x040003DF RID: 991
	public AudioSource purchaseAudioSource;

	// Token: 0x040003E0 RID: 992
	public AudioSource errorAudioSource;

	// Token: 0x040003E1 RID: 993
	[Header("References")]
	public Button button;

	// Token: 0x040003E2 RID: 994
	public Image imageIcon;

	// Token: 0x040003E3 RID: 995
	public Image imageVariant;

	// Token: 0x040003E4 RID: 996
	public Text titleCentered;

	// Token: 0x040003E5 RID: 997
	public Text titleUp;

	// Token: 0x040003E6 RID: 998
	public Text itemDescription;

	// Token: 0x040003E7 RID: 999
	public GameObject gameObjectPrice;

	// Token: 0x040003E8 RID: 1000
	public Text textPrice;

	// Token: 0x040003E9 RID: 1001
	[HideInInspector]
	public UIMenu menu;

	// Token: 0x040003EA RID: 1002
	[HideInInspector]
	public UIMenuManager menuManager;

	// Token: 0x040003EB RID: 1003
	[HideInInspector]
	public UIBuildItemMenuManager BuildItemMenuManager;

	// Token: 0x040003EC RID: 1004
	public BuildItem assocBuildItem;

	// Token: 0x040003ED RID: 1005
	public bool displayAsVariant;

	// Token: 0x040003EE RID: 1006
	public int itemVariantID;
}
