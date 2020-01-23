using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020000F0 RID: 240
public class BuildItemContainer : MonoBehaviour
{
	// Token: 0x060004CD RID: 1229 RVA: 0x00005F57 File Offset: 0x00004157
	private void Start()
	{
		LevelManager.OnBuildModeToggle += this.UpdateColliders;
		this.InitContainer();
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00005F70 File Offset: 0x00004170
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.UpdateColliders;
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00005F83 File Offset: 0x00004183
	private void Update()
	{
		this.BoundsIntersection();
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x00019CFC File Offset: 0x00017EFC
	private void InitContainer()
	{
		if (this.buildItemVariant == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.spawnedItem = UnityEngine.Object.Instantiate<GameObject>(this.buildItemVariant.prefab, base.transform, false);
		Tag.Transfer(this.spawnedItem, base.gameObject, Tag.Tags.None, true);
		base.gameObject.layer = 2;
		this.CreateContainerBoundingBox();
		if (this.buildItemVariant.enablePlayerCollisionBox)
		{
			this.CreatePlayerCollisionBox();
		}
		this.meshRenderers = base.GetComponentsInChildren<Renderer>();
		this.skinnedMeshes = base.GetComponentsInChildren<SkinnedMeshRenderer>();
		this.SetItemDisabilitySettings();
		this.UpdateColliders(LevelManager.BuildModeOn);
		this.UpdatePlaceholderMaterial();
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00019DA4 File Offset: 0x00017FA4
	private void UpdatePlaceholderMaterial()
	{
		if (!this.isSelectedObject)
		{
			return;
		}
		for (int i = 0; i < this.meshRenderers.Length; i++)
		{
			this.meshRenderers[i].materials = new Material[0];
			if (this.collidingItem == null)
			{
				this.meshRenderers[i].material = this.placeholderAddMaterial;
			}
			else
			{
				this.meshRenderers[i].material = this.placeholderRemoveMaterial;
			}
			this.meshRenderers[i].shadowCastingMode = ShadowCastingMode.Off;
		}
		for (int j = 0; j < this.skinnedMeshes.Length; j++)
		{
			this.skinnedMeshes[j].materials = new Material[0];
			if (this.collidingItem == null)
			{
				this.skinnedMeshes[j].material = this.placeholderAddMaterial;
			}
			else
			{
				this.skinnedMeshes[j].material = this.placeholderRemoveMaterial;
			}
			this.skinnedMeshes[j].shadowCastingMode = ShadowCastingMode.Off;
		}
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00019E90 File Offset: 0x00018090
	private void SetItemDisabilitySettings()
	{
		this.itemDisability = base.GetComponent<ItemBuildModeDisability>();
		if (this.itemDisability != null)
		{
			this.itemDisability.disableColliders = (this.isSelectedObject || this.buildItemVariant.disableCollidersWhileInBuildMode);
			this.itemDisability.item = this.spawnedItem;
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00019EEC File Offset: 0x000180EC
	private void UpdateColliders(bool buildModeOn)
	{
		if (this.bounds != null)
		{
			this.bounds.enabled = (!this.isSelectedObject && buildModeOn);
		}
		if (this.playerCollisionBounds != null)
		{
			this.playerCollisionBounds.enabled = (!this.isSelectedObject && buildModeOn);
		}
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00019F44 File Offset: 0x00018144
	private void CreateContainerBoundingBox()
	{
		this.bounds = base.gameObject.AddComponent<BoxCollider>();
		this.bounds.isTrigger = true;
		this.bounds.size = this.buildItemVariant.bounds;
		this.bounds.center = new Vector3(0f, this.buildItemVariant.bounds.y * 0.5f, 0f) + this.buildItemVariant.boundOffset;
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00019FC4 File Offset: 0x000181C4
	private void CreatePlayerCollisionBox()
	{
		this.playerCollisionBounds = base.gameObject.AddComponent<BoxCollider>();
		this.playerCollisionBounds.isTrigger = false;
		this.playerCollisionBounds.size = this.buildItemVariant.bounds;
		this.playerCollisionBounds.center = new Vector3(0f, this.buildItemVariant.bounds.y * 0.45f, 0f) + this.buildItemVariant.boundOffset;
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0001A044 File Offset: 0x00018244
	private void BoundsIntersection()
	{
		if (!this.isSelectedObject || this.buildPlacement == null)
		{
			return;
		}
		this.boxCollisions = Physics.OverlapBox(this.buildPlacement.position + new Vector3(0f, this.buildItemVariant.bounds.y * 0.5f, 0f) + this.buildPlacement.rotation * this.buildItemVariant.boundOffset, (this.buildItemVariant.bounds - Vector3.one * PlayerBuildPlacement.boundsOffset) * 0.5f, this.buildPlacement.rotation);
		if (this.boxCollisions.Length == 0)
		{
			this.collidingItem = null;
		}
		else
		{
			this.collidingItem = this.boxCollisions[0].transform.root;
		}
		this.UpdatePlaceholderMaterial();
	}

	// Token: 0x0400064B RID: 1611
	public PlayerBuildPlacement buildPlacement;

	// Token: 0x0400064C RID: 1612
	public bool isSelectedObject;

	// Token: 0x0400064D RID: 1613
	public Transform collidingItem;

	// Token: 0x0400064E RID: 1614
	private ItemBuildModeDisability itemDisability;

	// Token: 0x0400064F RID: 1615
	[Header("Item Info")]
	public BuildItem.Variant buildItemVariant;

	// Token: 0x04000650 RID: 1616
	private GameObject spawnedItem;

	// Token: 0x04000651 RID: 1617
	[Header("Materials")]
	public Material placeholderAddMaterial;

	// Token: 0x04000652 RID: 1618
	public Material placeholderRemoveMaterial;

	// Token: 0x04000653 RID: 1619
	[Space]
	private Renderer[] meshRenderers;

	// Token: 0x04000654 RID: 1620
	private SkinnedMeshRenderer[] skinnedMeshes;

	// Token: 0x04000655 RID: 1621
	[Header("Physics")]
	private BoxCollider bounds;

	// Token: 0x04000656 RID: 1622
	private BoxCollider playerCollisionBounds;

	// Token: 0x04000657 RID: 1623
	private Collider[] boxCollisions;
}
