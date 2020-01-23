using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
[CreateAssetMenu(fileName = "New UI Theme", menuName = "UI Theme")]
public class UITheme : ScriptableObject
{
	// Token: 0x040000F9 RID: 249
	[Header("Button Sprites")]
	public Sprite primaryButtonSprite;

	// Token: 0x040000FA RID: 250
	public Sprite primaryButtonHighlightedSprite;

	// Token: 0x040000FB RID: 251
	public Sprite primaryButtonPressedSprite;

	// Token: 0x040000FC RID: 252
	[Space]
	public Sprite secondaryButtonSprite;

	// Token: 0x040000FD RID: 253
	public Sprite secondaryButtonHighlightedSprite;

	// Token: 0x040000FE RID: 254
	public Sprite secondaryButtonPressedSprite;

	// Token: 0x040000FF RID: 255
	[Space]
	public Sprite disabledButtonSprite;

	// Token: 0x04000100 RID: 256
	public Sprite disabledButtonHighlightedSprite;

	// Token: 0x04000101 RID: 257
	public Sprite disabledButtonPressedSprite;

	// Token: 0x04000102 RID: 258
	[Space]
	public Sprite whiteButtonSprite;

	// Token: 0x04000103 RID: 259
	public Sprite whiteButtonHighlightedSprite;

	// Token: 0x04000104 RID: 260
	public Sprite whiteButtonPressedSprite;

	// Token: 0x04000105 RID: 261
	[Header("Toggle Sprites")]
	public Sprite toggleOnSprite;

	// Token: 0x04000106 RID: 262
	public Sprite toggleOffSprite;

	// Token: 0x04000107 RID: 263
	[Header("Icons")]
	public Sprite gearSrite;
}
