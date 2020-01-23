using System;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class PlayerBuildSystem : MonoBehaviour
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x060004E5 RID: 1253 RVA: 0x0001A998 File Offset: 0x00018B98
	// (remove) Token: 0x060004E6 RID: 1254 RVA: 0x0001A9D0 File Offset: 0x00018BD0
	public event PlayerBuildSystem.PlaceHandler OnPlace;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x060004E7 RID: 1255 RVA: 0x0001AA08 File Offset: 0x00018C08
	// (remove) Token: 0x060004E8 RID: 1256 RVA: 0x0001AA40 File Offset: 0x00018C40
	public event PlayerBuildSystem.RemoveHandler OnRemove;

	// Token: 0x060004E9 RID: 1257 RVA: 0x0000603A File Offset: 0x0000423A
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x00006052 File Offset: 0x00004252
	private void Update()
	{
		this.ToggleBuildMode();
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0000605A File Offset: 0x0000425A
	private void FixedUpdate()
	{
		this.BuildLogic();
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0001AA78 File Offset: 0x00018C78
	private void BuildLogic()
	{
		if (!LevelManager.BuildModeOn)
		{
			this.RemovePlaceholderObject();
			return;
		}
		if (this.updatePlaceholder)
		{
			if (this.selectedBuildItemVariant.prefab == null)
			{
				this.RemovePlaceholderObject();
			}
			else
			{
				this.UpdatePlaceholder();
			}
			this.updatePlaceholder = false;
		}
		if (this.placeholder != null)
		{
			this.placeholder.transform.position = this.buildPlacement.positionLerp;
			this.placeholder.transform.rotation = this.buildPlacement.rotationLerp;
		}
		if (InputManager.PlaceObject() && this.canPlaceItem)
		{
			this.PlaceItem();
			if (GameData.cheats.ignoreBuildCollisionCheck)
			{
				base.Invoke("SetCanPlaceItem", 0.4f);
			}
			else
			{
				base.Invoke("SetCanPlaceItem", 0.025f);
			}
			this.canPlaceItem = false;
		}
		if (InputManager.RemoveObject())
		{
			this.RemoveItem();
		}
		if (InputManager.RemoveLastItem() && WorldData.instance != null && WorldData.instance.data.items.Count > 0)
		{
			WorldData.RemoveLastItem();
			if (this.OnRemove != null)
			{
				this.OnRemove();
			}
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00006062 File Offset: 0x00004262
	private void SetCanPlaceItem()
	{
		this.canPlaceItem = true;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0001ABA4 File Offset: 0x00018DA4
	private void ToggleBuildMode()
	{
		if ((this.playerSettings.simulatedRagdoll.RagdollModeEnabled || this.playerSettings.mainCamera.target == CameraController.Target.Cinematic) && LevelManager.BuildModeOn)
		{
			LevelManager.BuildModeOn = false;
			return;
		}
		if ((InputManager.ToggleBuildMode() || (InputManager.ToggleBuildItemMenu() && !LevelManager.BuildModeOn)) && this.playerSettings.mainCamera.target == CameraController.Target.Player)
		{
			if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
			{
				this.playerSettings.simulatedRagdoll.RagdollModeEnabled = false;
			}
			LevelManager.BuildModeOn = !LevelManager.BuildModeOn;
			this.updatePlaceholder = true;
			return;
		}
		if (InputManager.MenuIsActive(false) && InputManager.ToggleBuildMode())
		{
			LevelManager.BuildModeOn = false;
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0001AC5C File Offset: 0x00018E5C
	private void PlaceItem()
	{
		if (!this.PlaceholderIsClear() && !GameData.cheats.ignoreBuildCollisionCheck)
		{
			return;
		}
		this.placedItem = this.buildItemCatalog.SpawnItem(this.selectedBuildItemVariant.id, this.buildPlacement.position, this.buildPlacement.rotation);
		if (this.placedItem != null)
		{
			WorldData.AddItemToLog(this.placedItem.transform, this.selectedBuildItemVariant.id);
		}
		if (this.OnPlace != null)
		{
			this.OnPlace();
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0001ACEC File Offset: 0x00018EEC
	private void RemoveItem()
	{
		if (this.placeholder == null)
		{
			return;
		}
		if (this.placeholder.collidingItem == null)
		{
			return;
		}
		this.objectOverlaping = this.placeholder.collidingItem.root.GetComponent<BuildItemContainer>();
		if (this.objectOverlaping == null)
		{
			return;
		}
		if (Tag.IsBuildItem(this.objectOverlaping.transform))
		{
			WorldData.RemoveItemFromLog(this.objectOverlaping.transform);
			UnityEngine.Object.Destroy(this.objectOverlaping.gameObject);
		}
		if (this.OnRemove != null)
		{
			this.OnRemove();
		}
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0001AD8C File Offset: 0x00018F8C
	private void UpdatePlaceholder()
	{
		this.RemovePlaceholderObject();
		this.placeholder = UnityEngine.Object.Instantiate<GameObject>(this.placeholderPrefab, this.buildPlacement.positionLerp, this.buildPlacement.rotationLerp).GetComponent<BuildItemContainer>();
		if (this.placeholder == null)
		{
			return;
		}
		this.placeholder.isSelectedObject = true;
		this.placeholder.buildItemVariant = this.selectedBuildItemVariant;
		this.placeholder.buildPlacement = this.buildPlacement;
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0000606B File Offset: 0x0000426B
	private void RemovePlaceholderObject()
	{
		if (this.placeholder != null)
		{
			UnityEngine.Object.Destroy(this.placeholder.gameObject);
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0001AE08 File Offset: 0x00019008
	public bool PlaceholderIsClear()
	{
		return !(this.placeholder == null) && !(this.placeholder.collidingItem != null) && this.buildPlacement.position.y >= 0f;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0000608B File Offset: 0x0000428B
	public void SetPlaceholder(int itemID)
	{
		this.selectedBuildItemVariant = this.buildItemCatalog.GetBuildItemVariant(itemID);
		this.updatePlaceholder = true;
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x000060A6 File Offset: 0x000042A6
	public void SetPlaceholder(BuildItem.Variant itemVariant)
	{
		this.selectedBuildItemVariant = itemVariant;
		this.updatePlaceholder = true;
	}

	// Token: 0x04000672 RID: 1650
	private PlayerSettings playerSettings;

	// Token: 0x04000673 RID: 1651
	public PlayerBuildPlacement buildPlacement;

	// Token: 0x04000674 RID: 1652
	public BuildItemCatalog buildItemCatalog;

	// Token: 0x04000675 RID: 1653
	[Header("Placeholder")]
	public GameObject placeholderPrefab;

	// Token: 0x04000676 RID: 1654
	[HideInInspector]
	public BuildItemContainer placeholder;

	// Token: 0x04000677 RID: 1655
	public bool updatePlaceholder;

	// Token: 0x04000678 RID: 1656
	[Space]
	public BuildItemContainer objectOverlaping;

	// Token: 0x04000679 RID: 1657
	[Header("Selected Item")]
	[HideInInspector]
	public BuildItem.Variant selectedBuildItemVariant;

	// Token: 0x0400067C RID: 1660
	private bool canPlaceItem = true;

	// Token: 0x0400067D RID: 1661
	private GameObject placedItem;

	// Token: 0x0400067E RID: 1662
	private Transform OverlapingTransform;

	// Token: 0x020000F5 RID: 245
	// (Invoke) Token: 0x060004F8 RID: 1272
	public delegate void PlaceHandler();

	// Token: 0x020000F6 RID: 246
	// (Invoke) Token: 0x060004FC RID: 1276
	public delegate void RemoveHandler();
}
