using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class RagdollKillZone : MonoBehaviour
{
	// Token: 0x14000009 RID: 9
	// (add) Token: 0x060003F8 RID: 1016 RVA: 0x000174BC File Offset: 0x000156BC
	// (remove) Token: 0x060003F9 RID: 1017 RVA: 0x000174F4 File Offset: 0x000156F4
	public event RagdollKillZone.RagdollEnterHandler OnAcceptableRagdollEnter;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x060003FA RID: 1018 RVA: 0x0001752C File Offset: 0x0001572C
	// (remove) Token: 0x060003FB RID: 1019 RVA: 0x00017564 File Offset: 0x00015764
	public event RagdollKillZone.RagdollEnterHandler OnUnacceptableRagdollEnter;

	// Token: 0x060003FC RID: 1020 RVA: 0x0001759C File Offset: 0x0001579C
	private void OnTriggerEnter(Collider other)
	{
		if (Tag.Compare(other.transform, Tag.Tags.Ragdoll))
		{
			this.LogEnteredRagdoll(other.transform);
			if (this.hideRagdollsOnly)
			{
				other.transform.root.gameObject.SetActive(false);
				return;
			}
			UnityEngine.Object.Destroy(other.transform.root.gameObject);
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x000175F8 File Offset: 0x000157F8
	private void LogEnteredRagdoll(Transform ragdoll)
	{
		this.ragdollSkin = RagdollStyleManager.GetSmartRagdollSkinType(ragdoll);
		if (this.OnAcceptableRagdollEnter != null)
		{
			this.OnAcceptableRagdollEnter(this.LogRagdoll(this.ragdollSkin, this.acceptableRagdolls));
		}
		if (this.OnUnacceptableRagdollEnter != null)
		{
			this.OnUnacceptableRagdollEnter(this.LogRagdoll(this.ragdollSkin, this.UnacceptableRagdolls));
		}
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x0001765C File Offset: 0x0001585C
	private int LogRagdoll(RagdollStylePack.SkinType skin, List<RagdollStylePack.SkinType> ragdollSkinList)
	{
		if (ragdollSkinList.Count > 0)
		{
			foreach (RagdollStylePack.SkinType skinType in ragdollSkinList)
			{
				if (skin == skinType)
				{
					return 1;
				}
			}
			return 0;
		}
		return 0;
	}

	// Token: 0x0400055D RID: 1373
	public bool hideRagdollsOnly = true;

	// Token: 0x0400055E RID: 1374
	[Space]
	public int acceptableRagdollsEntered;

	// Token: 0x0400055F RID: 1375
	public int UnacceptableRagdollsEntered;

	// Token: 0x04000562 RID: 1378
	public List<RagdollStylePack.SkinType> acceptableRagdolls = new List<RagdollStylePack.SkinType>();

	// Token: 0x04000563 RID: 1379
	public List<RagdollStylePack.SkinType> UnacceptableRagdolls = new List<RagdollStylePack.SkinType>();

	// Token: 0x04000564 RID: 1380
	private RagdollStylePack.SkinType ragdollSkin;

	// Token: 0x020000C9 RID: 201
	// (Invoke) Token: 0x06000401 RID: 1025
	public delegate void RagdollEnterHandler(int ragdollCount);
}
