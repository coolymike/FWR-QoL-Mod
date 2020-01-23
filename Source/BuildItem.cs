using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001E RID: 30
[CreateAssetMenu(fileName = "New Build Item", menuName = "Build Items/Build Item")]
public class BuildItem : ScriptableObject
{
	// Token: 0x06000078 RID: 120 RVA: 0x000027CF File Offset: 0x000009CF
	public bool Unlocked()
	{
		return GameData.stats.Level >= this.unlockLevel || GameData.cheats.UnlockEverything;
	}

	// Token: 0x040000AA RID: 170
	public string leadingName;

	// Token: 0x040000AB RID: 171
	public string description;

	// Token: 0x040000AC RID: 172
	public Sprite thumbnail;

	// Token: 0x040000AD RID: 173
	public bool special;

	// Token: 0x040000AE RID: 174
	public bool deprecated;

	// Token: 0x040000AF RID: 175
	public int id;

	// Token: 0x040000B0 RID: 176
	[Header("XP Details")]
	public int unlockLevel;

	// Token: 0x040000B1 RID: 177
	[Header("Varients")]
	public List<BuildItem.Variant> variants = new List<BuildItem.Variant>();

	// Token: 0x0200001F RID: 31
	[Serializable]
	public class Variant
	{
		// Token: 0x040000B2 RID: 178
		public string name;

		// Token: 0x040000B3 RID: 179
		public GameObject prefab;

		// Token: 0x040000B4 RID: 180
		public int id;

		// Token: 0x040000B5 RID: 181
		public bool depricated;

		// Token: 0x040000B6 RID: 182
		public Sprite thumbnail;

		// Token: 0x040000B7 RID: 183
		public Sprite variantThumbnail;

		// Token: 0x040000B8 RID: 184
		public Color variantColor = new Color(1f, 1f, 1f);

		// Token: 0x040000B9 RID: 185
		public string description;

		// Token: 0x040000BA RID: 186
		public Vector3 bounds = new Vector3(4.8f, 4.8f, 4.8f);

		// Token: 0x040000BB RID: 187
		public Vector3 boundOffset = new Vector3(0f, 0f, 0f);

		// Token: 0x040000BC RID: 188
		[Range(1f, 8f)]
		public int gridPlacementMultiplier = 2;

		// Token: 0x040000BD RID: 189
		public bool disableCollidersWhileInBuildMode;

		// Token: 0x040000BE RID: 190
		public bool enablePlayerCollisionBox;
	}
}
