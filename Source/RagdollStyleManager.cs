using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class RagdollStyleManager : MonoBehaviour
{
	// Token: 0x0600015B RID: 347 RVA: 0x000033AF File Offset: 0x000015AF
	private void Awake()
	{
		this.StyleUpdate();
		if (this.playerStyling)
		{
			GameData.OnStyleChanged += this.StyleUpdate;
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x000033D0 File Offset: 0x000015D0
	private void OnDestroy()
	{
		GameData.OnStyleChanged -= this.StyleUpdate;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
	public void StyleUpdate()
	{
		if (this.playerStyling)
		{
			this.assignRandomFace = false;
			this.assignRandomSkin = false;
			this.skin = GameData.styling.Skin;
			this.face = GameData.styling.Face;
		}
		this.UpdateSkin();
		this.UpdateFace();
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000D920 File Offset: 0x0000BB20
	public void UpdateSkin()
	{
		if (this.assignRandomSkin)
		{
			this.skin = this.stylePack.skins[UnityEngine.Random.Range(0, this.stylePack.skins.Length)].type;
		}
		this.ragdollBodyMesh.material = this.GetSkinMaterial(this.skin);
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000D978 File Offset: 0x0000BB78
	public void UpdateFace()
	{
		if (this.assignRandomFace)
		{
			this.face = this.stylePack.faces[UnityEngine.Random.Range(0, this.stylePack.faces.Length)].type;
		}
		this.ragdollFaceMesh.material = this.GetFaceMaterial(this.skin, this.face);
	}

	// Token: 0x06000160 RID: 352 RVA: 0x000033E3 File Offset: 0x000015E3
	public Material GetSkinMaterial(RagdollStylePack.SkinType requestedSkin)
	{
		return RagdollStylePack.GetSkin(requestedSkin, this.stylePack).material;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x000033F6 File Offset: 0x000015F6
	public Material GetFaceMaterial(RagdollStylePack.SkinType requestedSkin, RagdollStylePack.FaceType requestedFace)
	{
		if (RagdollStylePack.GetSkin(requestedSkin, this.stylePack).faceColor != RagdollStylePack.FaceColor.White)
		{
			return RagdollStylePack.GetFace(requestedFace, this.stylePack).materialDark;
		}
		return RagdollStylePack.GetFace(requestedFace, this.stylePack).materialLight;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000342E File Offset: 0x0000162E
	public static RagdollStyleManager GetSmartRagdollStyleProperties(Transform ragdoll)
	{
		return ragdoll.root.GetComponentInChildren<RagdollStyleManager>();
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000343B File Offset: 0x0000163B
	public static RagdollStylePack.SkinType GetSmartRagdollSkinType(Transform ragdoll)
	{
		return RagdollStyleManager.GetSmartRagdollStyleProperties(ragdoll).skin;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
	public static void AssignSmartRagdollSkin(Transform ragdoll, RagdollStylePack.SkinType skinType)
	{
		RagdollStyleManager smartRagdollStyleProperties = RagdollStyleManager.GetSmartRagdollStyleProperties(ragdoll);
		if (smartRagdollStyleProperties == null)
		{
			return;
		}
		smartRagdollStyleProperties.assignRandomSkin = false;
		smartRagdollStyleProperties.skin = skinType;
		smartRagdollStyleProperties.StyleUpdate();
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00003448 File Offset: 0x00001648
	public static RagdollStylePack.Skin GetPlayerSkin(PlayerSettings player)
	{
		if (player == null)
		{
			return null;
		}
		return RagdollStylePack.GetSkin(player.ragdollStyle.skin, player.ragdollStyle.stylePack);
	}

	// Token: 0x04000213 RID: 531
	public SkinnedMeshRenderer ragdollBodyMesh;

	// Token: 0x04000214 RID: 532
	public MeshRenderer ragdollFaceMesh;

	// Token: 0x04000215 RID: 533
	[Header("Settings")]
	public bool playerStyling;

	// Token: 0x04000216 RID: 534
	[Header("Design")]
	public RagdollStylePack.SkinType skin = RagdollStylePack.SkinType.Blue;

	// Token: 0x04000217 RID: 535
	public RagdollStylePack.FaceType face = RagdollStylePack.FaceType.Idle;

	// Token: 0x04000218 RID: 536
	[Space]
	public bool assignRandomFace = true;

	// Token: 0x04000219 RID: 537
	public bool assignRandomSkin = true;

	// Token: 0x0400021A RID: 538
	public RagdollStylePack.SkinType[] randomSkinSelection = new RagdollStylePack.SkinType[0];

	// Token: 0x0400021B RID: 539
	[Header("References")]
	public RagdollStylePack stylePack;

	// Token: 0x0400021C RID: 540
	private RagdollStylePack.Skin skinCache;
}
