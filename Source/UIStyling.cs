using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class UIStyling : MonoBehaviour
{
	// Token: 0x06000301 RID: 769 RVA: 0x000142A4 File Offset: 0x000124A4
	private void Start()
	{
		GameData.OnLevelUpgrade += this.UpdateButtons;
		GameData.OnStyleChanged += this.UpdateButtons;
		switch (this.styleSelection)
		{
		case UIStyling.StyleSelection.Skin:
			this.CreateSkinTypeSelection();
			break;
		case UIStyling.StyleSelection.Face:
			this.CreateFaceTypeSelection();
			break;
		case UIStyling.StyleSelection.FloorColor:
			this.CreateFloorTypeSelection();
			break;
		case UIStyling.StyleSelection.ItemColor:
			this.CreateItemTypeSelection();
			break;
		}
		this.UpdateButtons();
	}

	// Token: 0x06000302 RID: 770 RVA: 0x000049D4 File Offset: 0x00002BD4
	private void OnDestroy()
	{
		GameData.OnLevelUpgrade -= this.UpdateButtons;
		GameData.OnStyleChanged -= this.UpdateButtons;
	}

	// Token: 0x06000303 RID: 771 RVA: 0x00014318 File Offset: 0x00012518
	public void CreateSkinTypeSelection()
	{
		for (int i = 0; i < this.ragdollStylePack.skins.Length; i++)
		{
			this.buttonSettings = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, this.buttonContainer).GetComponent<UIButtonCard>();
			this.buttonSettings.gameObject.SetActive(true);
			this.buttonSettings.title.text = this.ragdollStylePack.skins[i].name;
			int currentIndex = i;
			this.buttonSettings.button.onClick.AddListener(delegate
			{
				this.SelectSkin((int)this.ragdollStylePack.skins[currentIndex].type);
			});
			this.buttons.Add(this.buttonSettings);
			this.buttonSkinValues.Add(this.ragdollStylePack.skins[i]);
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x000143F0 File Offset: 0x000125F0
	public void CreateFaceTypeSelection()
	{
		for (int i = 0; i < this.ragdollStylePack.faces.Length; i++)
		{
			this.buttonSettings = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, this.buttonContainer).GetComponent<UIButtonCard>();
			this.buttonSettings.gameObject.SetActive(true);
			this.buttonSettings.title.text = this.ragdollStylePack.faces[i].name;
			int currentIndex = i;
			this.buttonSettings.button.onClick.AddListener(delegate
			{
				this.SelectFace((int)this.ragdollStylePack.faces[currentIndex].type);
			});
			this.buttons.Add(this.buttonSettings);
			this.buttonFaceValues.Add(this.ragdollStylePack.faces[i]);
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x000144C8 File Offset: 0x000126C8
	public void CreateFloorTypeSelection()
	{
		for (int i = 0; i < Enum.GetValues(typeof(WorldStyle.ColorStyle)).Length; i++)
		{
			this.buttonSettings = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, this.buttonContainer).GetComponent<UIButtonCard>();
			this.buttonSettings.gameObject.SetActive(true);
			this.buttonSettings.title.text = Enum.GetName(typeof(WorldStyle.ColorStyle), i);
			this.buttonSettings.image.gameObject.SetActive(GameData.styling.FloorColor == (WorldStyle.ColorStyle)i);
			this.buttonSettings.priceTag.SetActive(false);
			int currentIndex = i;
			this.buttonSettings.button.onClick.AddListener(delegate
			{
				this.SelectFloorColor(currentIndex);
			});
			this.buttons.Add(this.buttonSettings);
			this.buttonFloorValues.Add((WorldStyle.ColorStyle)i);
		}
	}

	// Token: 0x06000306 RID: 774 RVA: 0x000145D4 File Offset: 0x000127D4
	public void CreateItemTypeSelection()
	{
		for (int i = 0; i < Enum.GetValues(typeof(WorldStyle.ColorStyle)).Length; i++)
		{
			this.buttonSettings = UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab, this.buttonContainer).GetComponent<UIButtonCard>();
			this.buttonSettings.gameObject.SetActive(true);
			this.buttonSettings.title.text = Enum.GetName(typeof(WorldStyle.ColorStyle), i);
			this.buttonSettings.image.gameObject.SetActive(GameData.styling.FloorColor == (WorldStyle.ColorStyle)i);
			this.buttonSettings.priceTag.SetActive(false);
			int currentIndex = i;
			this.buttonSettings.button.onClick.AddListener(delegate
			{
				this.SelectItemColor(currentIndex);
			});
			this.buttons.Add(this.buttonSettings);
			this.buttonItemValues.Add((WorldStyle.ColorStyle)i);
		}
	}

	// Token: 0x06000307 RID: 775 RVA: 0x000146E0 File Offset: 0x000128E0
	public void SelectSkin(int typeInt)
	{
		for (int i = 0; i < this.buttonSkinValues.Count; i++)
		{
			if (this.buttonSkinValues[i].type == (RagdollStylePack.SkinType)typeInt)
			{
				this.selectedSkin = this.buttonSkinValues[i];
			}
		}
		if (!this.selectedSkin.Unlocked())
		{
			return;
		}
		GameData.styling.Skin = this.selectedSkin.type;
		GameData.SaveStyling();
		this.UpdateButtons();
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00014758 File Offset: 0x00012958
	public void SelectFace(int typeInt)
	{
		for (int i = 0; i < this.buttonFaceValues.Count; i++)
		{
			if (this.buttonFaceValues[i].type == (RagdollStylePack.FaceType)typeInt)
			{
				this.selectedFace = this.buttonFaceValues[i];
			}
		}
		if (!this.selectedFace.Unlocked())
		{
			return;
		}
		GameData.styling.Face = (RagdollStylePack.FaceType)typeInt;
		GameData.SaveStyling();
		this.UpdateButtons();
	}

	// Token: 0x06000309 RID: 777 RVA: 0x000049F8 File Offset: 0x00002BF8
	public void SelectFloorColor(int typeInt)
	{
		GameData.styling.FloorColor = (WorldStyle.ColorStyle)typeInt;
		GameData.SaveStyling();
		this.UpdateButtons();
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00004A10 File Offset: 0x00002C10
	public void SelectItemColor(int typeInt)
	{
		GameData.styling.BlockColor = (WorldStyle.ColorStyle)typeInt;
		GameData.SaveStyling();
		this.UpdateButtons();
	}

	// Token: 0x0600030B RID: 779 RVA: 0x000147C8 File Offset: 0x000129C8
	public void UpdateButtons()
	{
		for (int i = 0; i < this.buttonSkinValues.Count; i++)
		{
			this.buttons[i].image.gameObject.SetActive(GameData.styling.Skin == this.buttonSkinValues[i].type);
			this.buttons[i].priceTag.SetActive(!this.buttonSkinValues[i].Unlocked());
			this.buttons[i].price.text = "Lvl " + this.buttonSkinValues[i].unlockLevel.ToString();
		}
		for (int j = 0; j < this.buttonFaceValues.Count; j++)
		{
			this.buttons[j].image.gameObject.SetActive(GameData.styling.Face == this.buttonFaceValues[j].type);
			this.buttons[j].priceTag.SetActive(!this.buttonFaceValues[j].Unlocked());
			this.buttons[j].price.text = "Lvl " + this.buttonFaceValues[j].unlockLevel.ToString();
		}
		for (int k = 0; k < this.buttonFloorValues.Count; k++)
		{
			this.buttons[k].image.gameObject.SetActive(GameData.styling.FloorColor == this.buttonFloorValues[k]);
		}
		for (int l = 0; l < this.buttonItemValues.Count; l++)
		{
			this.buttons[l].image.gameObject.SetActive(GameData.styling.BlockColor == this.buttonItemValues[l]);
		}
	}

	// Token: 0x0400045F RID: 1119
	public Transform buttonContainer;

	// Token: 0x04000460 RID: 1120
	public GameObject buttonPrefab;

	// Token: 0x04000461 RID: 1121
	[Header("Style Type")]
	public UIStyling.StyleSelection styleSelection;

	// Token: 0x04000462 RID: 1122
	public RagdollStylePack ragdollStylePack;

	// Token: 0x04000463 RID: 1123
	private UIButtonCard buttonSettings;

	// Token: 0x04000464 RID: 1124
	private List<UIButtonCard> buttons = new List<UIButtonCard>();

	// Token: 0x04000465 RID: 1125
	private List<RagdollStylePack.Skin> buttonSkinValues = new List<RagdollStylePack.Skin>();

	// Token: 0x04000466 RID: 1126
	private List<RagdollStylePack.Face> buttonFaceValues = new List<RagdollStylePack.Face>();

	// Token: 0x04000467 RID: 1127
	private List<WorldStyle.ColorStyle> buttonFloorValues = new List<WorldStyle.ColorStyle>();

	// Token: 0x04000468 RID: 1128
	private List<WorldStyle.ColorStyle> buttonItemValues = new List<WorldStyle.ColorStyle>();

	// Token: 0x04000469 RID: 1129
	private RagdollStylePack.Skin selectedSkin;

	// Token: 0x0400046A RID: 1130
	private RagdollStylePack.Face selectedFace;

	// Token: 0x02000095 RID: 149
	public enum StyleSelection
	{
		// Token: 0x0400046C RID: 1132
		Skin,
		// Token: 0x0400046D RID: 1133
		Face,
		// Token: 0x0400046E RID: 1134
		FloorColor,
		// Token: 0x0400046F RID: 1135
		ItemColor
	}
}
