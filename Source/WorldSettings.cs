using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class WorldSettings : MonoBehaviour
{
	// Token: 0x0600049F RID: 1183 RVA: 0x00019188 File Offset: 0x00017388
	private void Start()
	{
		if (this.activateColorCodeRagdollsCalculations)
		{
			WorldData.OnWorldPropertiesChanged += this.ColorCodeSkin;
			this.ColorCodeSkin();
			return;
		}
		if (this.activateRagdollOnGroundTouchCalculations)
		{
			WorldData.OnWorldPropertiesChanged += this.UpdateGroundTouchability;
			this.UpdateGroundTouchability();
		}
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00005CE0 File Offset: 0x00003EE0
	private void OnDestroy()
	{
		WorldData.OnWorldPropertiesChanged -= this.UpdateGroundTouchability;
		WorldData.OnWorldPropertiesChanged -= this.ColorCodeSkin;
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00005D04 File Offset: 0x00003F04
	private void UpdateGroundTouchability()
	{
		if (WorldData.instance == null)
		{
			return;
		}
		this.antiTouchObject.colliderCollisions = WorldData.instance.data.settings.RagdollOnGroundTouch;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000191D4 File Offset: 0x000173D4
	private void ColorCodeSkin()
	{
		if (WorldData.instance == null)
		{
			return;
		}
		if (!WorldData.instance.allowWorldSave)
		{
			return;
		}
		if (WorldData.instance.data.settings.ColorCodeRagdolls)
		{
			this.ragdollStyle.assignRandomSkin = false;
			switch (this.smartRagdollController.logic)
			{
			case SmartRagdollController.Logic.DoNothing:
				this.ragdollStyle.skin = RagdollStylePack.SkinType.Yellow;
				break;
			case SmartRagdollController.Logic.Follow:
				this.ragdollStyle.skin = RagdollStylePack.SkinType.Green;
				break;
			case SmartRagdollController.Logic.Attack:
				this.ragdollStyle.skin = RagdollStylePack.SkinType.Red;
				break;
			case SmartRagdollController.Logic.Defend:
				this.ragdollStyle.skin = RagdollStylePack.SkinType.Blue;
				break;
			case SmartRagdollController.Logic.Explore:
				this.ragdollStyle.skin = RagdollStylePack.SkinType.Orange;
				break;
			case SmartRagdollController.Logic.Avoid:
				this.ragdollStyle.skin = RagdollStylePack.SkinType.Purple;
				break;
			default:
				this.ragdollStyle.skin = RagdollStylePack.SkinType.Blue;
				break;
			}
		}
		else
		{
			this.ragdollStyle.assignRandomSkin = true;
		}
		this.ragdollStyle.StyleUpdate();
	}

	// Token: 0x0400061D RID: 1565
	[Header("Color Code Ragdolls")]
	public bool activateColorCodeRagdollsCalculations;

	// Token: 0x0400061E RID: 1566
	public RagdollStyleManager ragdollStyle;

	// Token: 0x0400061F RID: 1567
	public SmartRagdollController smartRagdollController;

	// Token: 0x04000620 RID: 1568
	[Header("Ragdoll On Ground Touch")]
	public bool activateRagdollOnGroundTouchCalculations;

	// Token: 0x04000621 RID: 1569
	public AntiTouchObject antiTouchObject;
}
