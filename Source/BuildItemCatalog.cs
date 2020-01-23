using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000020 RID: 32
[CreateAssetMenu(fileName = "Build Items", menuName = "Build Items/Build Item Pack")]
public class BuildItemCatalog : ScriptableObject
{
	// Token: 0x0600007B RID: 123 RVA: 0x000098F8 File Offset: 0x00007AF8
	public BuildItem GetBuildItem(int itemID)
	{
		for (int i = 0; i < this.buildItems.Count; i++)
		{
			if (this.buildItems[i].id == itemID)
			{
				return this.buildItems[i];
			}
		}
		Debug.LogWarning("Build item " + itemID + " is missing in BuildData!");
		return null;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00009958 File Offset: 0x00007B58
	public BuildItem GetBuildItemByVariant(int itemVariantID)
	{
		for (int i = 0; i < this.buildItems.Count; i++)
		{
			for (int j = 0; j < this.buildItems[i].variants.Count; j++)
			{
				if (this.buildItems[i].variants[j].id == itemVariantID)
				{
					return this.buildItems[i];
				}
			}
		}
		Debug.LogWarning("Build item " + itemVariantID + " is missing in BuildData!");
		return null;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000099E4 File Offset: 0x00007BE4
	public BuildItem.Variant GetBuildItemVariant(int itemVariantID)
	{
		BuildItem buildItemByVariant = this.GetBuildItemByVariant(itemVariantID);
		if (buildItemByVariant == null)
		{
			return null;
		}
		for (int i = 0; i < buildItemByVariant.variants.Count; i++)
		{
			if (buildItemByVariant.variants[i].id == itemVariantID)
			{
				return buildItemByVariant.variants[i];
			}
		}
		Debug.Log("No item variant found for ID " + itemVariantID);
		return null;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00002802 File Offset: 0x00000A02
	public GameObject SpawnItem(int itemVariantID, Vector3 position, Quaternion rotation)
	{
		BuildItemContainer component = UnityEngine.Object.Instantiate<GameObject>(this.buildItemContainerPrefab, position, rotation).GetComponent<BuildItemContainer>();
		component.buildItemVariant = this.GetBuildItemVariant(itemVariantID);
		return component.gameObject;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00009A54 File Offset: 0x00007C54
	public float TotalBuildItems()
	{
		this.buildItemCount = 0;
		for (int i = 0; i < this.buildItems.Count; i++)
		{
			if (!this.buildItems[i].deprecated)
			{
				this.buildItemCount++;
			}
		}
		return (float)this.buildItemCount;
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00009AA8 File Offset: 0x00007CA8
	[ContextMenu("Check For Duplicate IDs")]
	public void CheckForDuplicateIDs()
	{
		string text = "";
		string text2 = "";
		Debug.Log("Checking Build Item IDs\n");
		for (int i = 0; i < this.buildItems.Count; i++)
		{
			for (int j = 0; j < this.buildItems.Count; j++)
			{
				if (i != j && this.buildItems[i].id == this.buildItems[j].id)
				{
					text = string.Concat(new string[]
					{
						text,
						this.buildItems[i].name,
						" / ",
						this.buildItems[j].name,
						"\n"
					});
				}
			}
		}
		Debug.Log((text == "") ? "NONE FOUND!\n\n----------" : (text + "\n\n----------"));
		Debug.Log("Checking Build Item Variant IDs\n");
		for (int k = 0; k < this.buildItems.Count; k++)
		{
			for (int l = 0; l < this.buildItems[k].variants.Count; l++)
			{
				for (int m = 0; m < this.buildItems.Count; m++)
				{
					for (int n = 0; n < this.buildItems[m].variants.Count; n++)
					{
						if ((k != m || l != n) && this.buildItems[k].variants[l].id == this.buildItems[m].variants[n].id)
						{
							text2 = string.Concat(new string[]
							{
								text2,
								this.buildItems[k].name,
								".",
								this.buildItems[k].variants[l].name,
								" / ",
								this.buildItems[m].name,
								".",
								this.buildItems[m].variants[n].name,
								" SHARE IDs\n"
							});
						}
					}
				}
			}
		}
		Debug.Log((text2 == "") ? "NONE FOUND!\n\n----------" : (text2 + "\n\n----------"));
		Debug.Log("Done!\n");
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00009D54 File Offset: 0x00007F54
	[ContextMenu("Get All Item ID's")]
	public void GetAllItemIDs()
	{
		string text = "";
		for (int i = 0; i < this.buildItems.Count; i++)
		{
			text = string.Concat(new object[]
			{
				text,
				this.buildItems[i].id,
				" : ",
				this.buildItems[i].leadingName,
				"\n"
			});
			for (int j = 0; j < this.buildItems[i].variants.Count; j++)
			{
				text = string.Concat(new object[]
				{
					text,
					this.buildItems[i].variants[j].id,
					" : ",
					this.buildItems[i].leadingName,
					" / ",
					this.buildItems[i].variants[j].name,
					"\n"
				});
			}
			text += "\n";
		}
		Debug.Log(text);
	}

	// Token: 0x040000BF RID: 191
	public GameObject buildItemContainerPrefab;

	// Token: 0x040000C0 RID: 192
	public List<BuildItem> buildItems = new List<BuildItem>();

	// Token: 0x040000C1 RID: 193
	private int buildItemCount;
}
