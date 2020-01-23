using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F7 RID: 247
public class WorldData : MonoBehaviour
{
	// Token: 0x1400000D RID: 13
	// (add) Token: 0x060004FF RID: 1279 RVA: 0x0001AE54 File Offset: 0x00019054
	// (remove) Token: 0x06000500 RID: 1280 RVA: 0x0001AE88 File Offset: 0x00019088
	public static event WorldData.WorldDataChangeHandler OnWorldPropertiesChanged;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06000501 RID: 1281 RVA: 0x0001AEBC File Offset: 0x000190BC
	// (remove) Token: 0x06000502 RID: 1282 RVA: 0x0001AEF0 File Offset: 0x000190F0
	public static event WorldData.ItemCountUpdateHandler OnItemCountUpdated;

	// Token: 0x06000503 RID: 1283 RVA: 0x0001AF24 File Offset: 0x00019124
	private void Awake()
	{
		List<string> options = new List<string>
		{
			"Custom Gravity"
		};
		this.gravityDropdown.AddOptions(options);
		WorldData.instance = this;
		if (WorldData.loadWorldOnStart && this.allowWorldSave)
		{
			this.LoadWorld(WorldData.worldFileLocation);
		}
		if (this.allowWorldSave)
		{
			base.StartCoroutine(this.AutoSaveRoutine());
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0001AF84 File Offset: 0x00019184
	public void SaveWorld()
	{
		if (this.autoSave)
		{
			FileManager.Save(WorldData.worldFileLocation, this.data, true);
		}
		else
		{
			WorldData.worldFileLocation = FileManager.GetPotentialWorldPath(this.data.worldName);
			FileManager.Save(this.data.worldName, this.data, false);
		}
		this.autoSave = true;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x000060C5 File Offset: 0x000042C5
	private IEnumerator AutoSaveRoutine()
	{
		for (;;)
		{
			if (this.autoSave)
			{
				this.SaveWorld();
			}
			yield return new WaitForSecondsRealtime(60f);
		}
		yield break;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x000060D4 File Offset: 0x000042D4
	private void OnDestroy()
	{
		if (this.autoSave)
		{
			this.SaveWorld();
		}
		Physics.gravity = new Vector3(0f, this.defaultGravityStrength, 0f);
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x0001AFE0 File Offset: 0x000191E0
	public void LoadWorld(string fileLocation)
	{
		WorldData.Data data = FileManager.Load<WorldData.Data>(fileLocation);
		if (data == null)
		{
			return;
		}
		this.data.worldName = data.worldName;
		this.data.player = data.player;
		this.data.settings = data.settings;
		for (int i = 0; i < data.items.Count; i++)
		{
			WorldData.AddItemToLog(WorldData.instance.buildItemCatalog.SpawnItem(data.items[i].itemVariantID, data.items[i].position, Quaternion.Euler(data.items[i].eularRotation)).transform, data.items[i].itemVariantID);
		}
		this.autoSave = true;
		WorldData.loadWorldOnStart = false;
		if (this.gravityDropdown != null)
		{
			this.gravityDropdown.value = this.data.settings.gravityStrength;
			this.ApplyAndSaveGravityStrength(this.gravityDropdown);
		}
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x0001B0E8 File Offset: 0x000192E8
	public static void AddItemToLog(Transform buildItemTransform, int itemVariantID)
	{
		if (WorldData.instance == null)
		{
			return;
		}
		WorldData.Data.Item item = new WorldData.Data.Item();
		item.instanceID = buildItemTransform.GetInstanceID();
		item.itemVariantID = itemVariantID;
		item.position = buildItemTransform.position;
		item.eularRotation = buildItemTransform.rotation.eulerAngles;
		WorldData.instance.data.items.Add(item);
		WorldData.instance.placedTransforms.Add(buildItemTransform.GetInstanceID(), buildItemTransform);
		WorldData.ItemCountUpdateHandler onItemCountUpdated = WorldData.OnItemCountUpdated;
		if (onItemCountUpdated == null)
		{
			return;
		}
		onItemCountUpdated(WorldData.instance.data.items.Count);
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0001B18C File Offset: 0x0001938C
	public static void RemoveItemFromLog(Transform buildItem)
	{
		if (WorldData.instance == null)
		{
			return;
		}
		int instanceID = buildItem.GetInstanceID();
		if (WorldData.instance.placedTransforms.ContainsKey(instanceID))
		{
			WorldData.instance.placedTransforms.Remove(instanceID);
		}
		for (int i = 0; i < WorldData.instance.data.items.Count; i++)
		{
			if (WorldData.instance.data.items[i].instanceID == instanceID)
			{
				WorldData.instance.data.items.RemoveAt(i);
			}
		}
		WorldData.ItemCountUpdateHandler onItemCountUpdated = WorldData.OnItemCountUpdated;
		if (onItemCountUpdated == null)
		{
			return;
		}
		onItemCountUpdated(WorldData.instance.data.items.Count);
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0001B248 File Offset: 0x00019448
	public static void RemoveLastItem()
	{
		if (WorldData.instance == null)
		{
			return;
		}
		if (WorldData.instance.data.items.Count == 0)
		{
			return;
		}
		WorldData.Data.Item item = WorldData.instance.data.items[WorldData.instance.data.items.Count - 1];
		if (!WorldData.instance.placedTransforms.ContainsKey(item.instanceID))
		{
			return;
		}
		UnityEngine.Object.Destroy(WorldData.instance.placedTransforms[item.instanceID].gameObject);
		WorldData.instance.placedTransforms.Remove(item.instanceID);
		WorldData.instance.data.items.RemoveAt(WorldData.instance.data.items.Count - 1);
		WorldData.ItemCountUpdateHandler onItemCountUpdated = WorldData.OnItemCountUpdated;
		if (onItemCountUpdated == null)
		{
			return;
		}
		onItemCountUpdated(WorldData.instance.data.items.Count);
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0001B340 File Offset: 0x00019540
	public static int ItemsPlacedFromBuildItemVariantID(int itemVariantID)
	{
		if (WorldData.instance == null)
		{
			return 0;
		}
		BuildItem buildItemByVariant = WorldData.instance.buildItemCatalog.GetBuildItemByVariant(itemVariantID);
		if (buildItemByVariant == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < buildItemByVariant.variants.Count; i++)
		{
			for (int j = 0; j < WorldData.instance.data.items.Count; j++)
			{
				if (buildItemByVariant.variants[i].id == WorldData.instance.data.items[j].itemVariantID)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0001B3E4 File Offset: 0x000195E4
	public static int ItemsPlacedFromBuildItemID(int buildItemID)
	{
		if (WorldData.instance == null)
		{
			return 0;
		}
		BuildItem buildItem = WorldData.instance.buildItemCatalog.GetBuildItem(buildItemID);
		if (buildItem == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < buildItem.variants.Count; i++)
		{
			for (int j = 0; j < WorldData.instance.data.items.Count; j++)
			{
				if (buildItem.variants[i].id == WorldData.instance.data.items[j].itemVariantID)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0001B488 File Offset: 0x00019688
	public static int GetBuildItemsInScene(bool isSpecial)
	{
		if (WorldData.instance == null)
		{
			return 0;
		}
		int num = 0;
		for (int i = 0; i < WorldData.instance.data.items.Count; i++)
		{
			BuildItem buildItemByVariant = WorldData.instance.buildItemCatalog.GetBuildItemByVariant(WorldData.instance.data.items[i].itemVariantID);
			if ((isSpecial && buildItemByVariant.special) || (!isSpecial && !buildItemByVariant.special))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0001B50C File Offset: 0x0001970C
	public void ApplyAndSaveGravityStrength(Dropdown strength)
	{
		if (WorldData.instance == null)
		{
			return;
		}
		WorldData.instance.data.settings.gravityStrength = strength.value;
		switch (strength.value)
		{
		case 0:
			this.selectedGravityStrength = this.defaultGravityStrength;
			break;
		case 1:
			this.selectedGravityStrength = -1f;
			break;
		case 2:
			this.selectedGravityStrength = this.defaultGravityStrength + 10f;
			break;
		case 3:
			this.selectedGravityStrength = this.defaultGravityStrength + 5f;
			break;
		case 4:
			this.selectedGravityStrength = this.defaultGravityStrength - 10f;
			break;
		case 5:
			this.selectedGravityStrength = this.defaultGravityStrength - 20f;
			break;
		case 6:
			this.selectedGravityStrength = this.defaultGravityStrength - 40f;
			break;
		case 7:
			this.selectedGravityStrength = WorldData.customGravity;
			break;
		}
		Physics.gravity = new Vector3(0f, this.selectedGravityStrength, 0f);
	}

	// Token: 0x04000680 RID: 1664
	private readonly float defaultGravityStrength = -14.81f;

	// Token: 0x04000681 RID: 1665
	public static WorldData instance;

	// Token: 0x04000683 RID: 1667
	public static bool loadWorldOnStart;

	// Token: 0x04000684 RID: 1668
	public static string worldFileLocation;

	// Token: 0x04000685 RID: 1669
	public bool allowWorldSave;

	// Token: 0x04000686 RID: 1670
	public bool autoSave;

	// Token: 0x04000687 RID: 1671
	public BuildItemCatalog buildItemCatalog;

	// Token: 0x04000688 RID: 1672
	public WorldData.Data data = new WorldData.Data();

	// Token: 0x04000689 RID: 1673
	public Dictionary<int, Transform> placedTransforms = new Dictionary<int, Transform>();

	// Token: 0x0400068A RID: 1674
	public Dropdown gravityDropdown;

	// Token: 0x0400068B RID: 1675
	private float selectedGravityStrength;

	// Token: 0x0400068C RID: 1676
	public static float customGravity;

	// Token: 0x020000F8 RID: 248
	// (Invoke) Token: 0x06000511 RID: 1297
	public delegate void WorldDataChangeHandler();

	// Token: 0x020000F9 RID: 249
	[Serializable]
	public class Data
	{
		// Token: 0x0400068D RID: 1677
		public string worldName = "New World";

		// Token: 0x0400068E RID: 1678
		public WorldData.Data.Player player = new WorldData.Data.Player();

		// Token: 0x0400068F RID: 1679
		public WorldData.Data.Settings settings = new WorldData.Data.Settings();

		// Token: 0x04000690 RID: 1680
		public List<WorldData.Data.Item> items = new List<WorldData.Data.Item>();

		// Token: 0x020000FA RID: 250
		[Serializable]
		public class Player
		{
			// Token: 0x04000691 RID: 1681
			public Vector3 spawnPosition;

			// Token: 0x04000692 RID: 1682
			public Vector3 spawnRotation;
		}

		// Token: 0x020000FB RID: 251
		[Serializable]
		public class Item
		{
			// Token: 0x04000693 RID: 1683
			public int instanceID;

			// Token: 0x04000694 RID: 1684
			public int itemVariantID;

			// Token: 0x04000695 RID: 1685
			public Vector3 position;

			// Token: 0x04000696 RID: 1686
			public Vector3 eularRotation;
		}

		// Token: 0x020000FC RID: 252
		[Serializable]
		public class Settings
		{
			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000615B File Offset: 0x0000435B
			// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001B618 File Offset: 0x00019818
			public bool ColorCodeRagdolls
			{
				get
				{
					return this.colorCodeRagdolls;
				}
				set
				{
					if (this.colorCodeRagdolls == value)
					{
						return;
					}
					this.colorCodeRagdolls = value;
					WorldData.WorldDataChangeHandler onWorldPropertiesChanged = WorldData.OnWorldPropertiesChanged;
					if (onWorldPropertiesChanged == null)
					{
						return;
					}
					onWorldPropertiesChanged();
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x06000519 RID: 1305 RVA: 0x00006163 File Offset: 0x00004363
			// (set) Token: 0x0600051A RID: 1306 RVA: 0x0001B648 File Offset: 0x00019848
			public bool RagdollOnGroundTouch
			{
				get
				{
					return this.ragdollOnGroundTouch;
				}
				set
				{
					if (this.ragdollOnGroundTouch == value)
					{
						return;
					}
					this.ragdollOnGroundTouch = value;
					WorldData.WorldDataChangeHandler onWorldPropertiesChanged = WorldData.OnWorldPropertiesChanged;
					if (onWorldPropertiesChanged == null)
					{
						return;
					}
					onWorldPropertiesChanged();
				}
			}

			// Token: 0x04000697 RID: 1687
			public bool lockSpawn;

			// Token: 0x04000698 RID: 1688
			public int gravityStrength;

			// Token: 0x04000699 RID: 1689
			[SerializeField]
			private bool colorCodeRagdolls = true;

			// Token: 0x0400069A RID: 1690
			[SerializeField]
			private bool ragdollOnGroundTouch;

			// Token: 0x0400069B RID: 1691
			[SerializeField]
			private int timer;
		}
	}

	// Token: 0x020000FD RID: 253
	// (Invoke) Token: 0x0600051D RID: 1309
	public delegate void ItemCountUpdateHandler(int itemCount);
}
