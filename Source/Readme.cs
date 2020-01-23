using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class Readme : ScriptableObject
{
	// Token: 0x04000036 RID: 54
	public Texture2D icon;

	// Token: 0x04000037 RID: 55
	public string title;

	// Token: 0x04000038 RID: 56
	public Readme.Section[] sections;

	// Token: 0x04000039 RID: 57
	public bool loadedLayout;

	// Token: 0x0200000D RID: 13
	[Serializable]
	public class Section
	{
		// Token: 0x0400003A RID: 58
		public string heading;

		// Token: 0x0400003B RID: 59
		public string text;

		// Token: 0x0400003C RID: 60
		public string linkText;

		// Token: 0x0400003D RID: 61
		public string url;
	}
}
