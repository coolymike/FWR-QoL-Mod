using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[CreateAssetMenu(fileName = "New Ragdoll Style Pack", menuName = "Ragdoll Style/Pack")]
public class RagdollStylePack : ScriptableObject
{
	// Token: 0x06000083 RID: 131 RVA: 0x00009E88 File Offset: 0x00008088
	public static RagdollStylePack.Skin GetSkin(RagdollStylePack.SkinType skinType, RagdollStylePack ragdollStylePack)
	{
		for (int i = 0; i < ragdollStylePack.skins.Length; i++)
		{
			if (ragdollStylePack.skins[i].type == skinType)
			{
				return ragdollStylePack.skins[i];
			}
		}
		return new RagdollStylePack.Skin();
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00009EC8 File Offset: 0x000080C8
	public static RagdollStylePack.Face GetFace(RagdollStylePack.FaceType faceType, RagdollStylePack ragdollStylePack)
	{
		for (int i = 0; i < ragdollStylePack.faces.Length; i++)
		{
			if (ragdollStylePack.faces[i].type == faceType)
			{
				return ragdollStylePack.faces[i];
			}
		}
		return new RagdollStylePack.Face();
	}

	// Token: 0x040000C2 RID: 194
	public RagdollStylePack.Skin[] skins = new RagdollStylePack.Skin[0];

	// Token: 0x040000C3 RID: 195
	public RagdollStylePack.Face[] faces = new RagdollStylePack.Face[0];

	// Token: 0x02000022 RID: 34
	public enum SkinType
	{
		// Token: 0x040000C5 RID: 197
		Blue = 1,
		// Token: 0x040000C6 RID: 198
		Red,
		// Token: 0x040000C7 RID: 199
		Orange,
		// Token: 0x040000C8 RID: 200
		Green,
		// Token: 0x040000C9 RID: 201
		Purple,
		// Token: 0x040000CA RID: 202
		Yellow,
		// Token: 0x040000CB RID: 203
		White,
		// Token: 0x040000CC RID: 204
		Black,
		// Token: 0x040000CD RID: 205
		Wood,
		// Token: 0x040000CE RID: 206
		Steel,
		// Token: 0x040000CF RID: 207
		Camo,
		// Token: 0x040000D0 RID: 208
		Chrome,
		// Token: 0x040000D1 RID: 209
		Gold,
		// Token: 0x040000D2 RID: 210
		HologramBlue,
		// Token: 0x040000D3 RID: 211
		HologramRed,
		// Token: 0x040000D4 RID: 212
		CreditCrawler,
		// Token: 0x040000D5 RID: 213
		DummyDanny,
		// Token: 0x040000D6 RID: 214
		RainbowBerry,
		// Token: 0x040000D7 RID: 215
		Glass,
		// Token: 0x040000D8 RID: 216
		Rickie = 21,
		// Token: 0x040000D9 RID: 217
		Grey = 20
	}

	// Token: 0x02000023 RID: 35
	public enum FaceColor
	{
		// Token: 0x040000DB RID: 219
		White,
		// Token: 0x040000DC RID: 220
		Black
	}

	// Token: 0x02000024 RID: 36
	public enum FaceType
	{
		// Token: 0x040000DE RID: 222
		None,
		// Token: 0x040000DF RID: 223
		Idle,
		// Token: 0x040000E0 RID: 224
		Happy,
		// Token: 0x040000E1 RID: 225
		Confused,
		// Token: 0x040000E2 RID: 226
		Smirk,
		// Token: 0x040000E3 RID: 227
		Unamused,
		// Token: 0x040000E4 RID: 228
		Scared,
		// Token: 0x040000E5 RID: 229
		Smile,
		// Token: 0x040000E6 RID: 230
		Curious,
		// Token: 0x040000E7 RID: 231
		Dead,
		// Token: 0x040000E8 RID: 232
		TheChad,
		// Token: 0x040000E9 RID: 233
		Special,
		// Token: 0x040000EA RID: 234
		CuteSmile,
		// Token: 0x040000EB RID: 235
		Angry
	}

	// Token: 0x02000025 RID: 37
	[Serializable]
	public class Skin
	{
		// Token: 0x06000086 RID: 134 RVA: 0x0000285B File Offset: 0x00000A5B
		public bool Unlocked()
		{
			return GameData.stats.Level >= this.unlockLevel || GameData.cheats.UnlockEverything;
		}

		// Token: 0x040000EC RID: 236
		public string name;

		// Token: 0x040000ED RID: 237
		public RagdollStylePack.SkinType type;

		// Token: 0x040000EE RID: 238
		public Material material;

		// Token: 0x040000EF RID: 239
		public RagdollStylePack.FaceColor faceColor;

		// Token: 0x040000F0 RID: 240
		public bool isSpecial;

		// Token: 0x040000F1 RID: 241
		public bool hiddenUntilUnlocked;

		// Token: 0x040000F2 RID: 242
		public int unlockLevel;

		// Token: 0x040000F3 RID: 243
		public int collisionPointsPerHit = 1;
	}

	// Token: 0x02000026 RID: 38
	[Serializable]
	public class Face
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000288A File Offset: 0x00000A8A
		public bool Unlocked()
		{
			return GameData.stats.Level >= this.unlockLevel || GameData.cheats.UnlockEverything;
		}

		// Token: 0x040000F4 RID: 244
		public string name;

		// Token: 0x040000F5 RID: 245
		public RagdollStylePack.FaceType type;

		// Token: 0x040000F6 RID: 246
		public Material materialLight;

		// Token: 0x040000F7 RID: 247
		public Material materialDark;

		// Token: 0x040000F8 RID: 248
		public int unlockLevel;
	}
}
