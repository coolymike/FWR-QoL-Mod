using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BA RID: 186
public class UIStatusWidget : MonoBehaviour
{
	// Token: 0x060003B3 RID: 947 RVA: 0x000052AB File Offset: 0x000034AB
	private void Start()
	{
		base.InvokeRepeating("SlowUpdate", 0f, 2f);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00016208 File Offset: 0x00014408
	private void SlowUpdate()
	{
		this.sliderImage.fillAmount = GameData.stats.LevelCompletePercentage();
		this.levelText.text = "Lvl " + GameData.stats.Level.ToString();
	}

	// Token: 0x04000502 RID: 1282
	public Text pointCountText;

	// Token: 0x04000503 RID: 1283
	[Space]
	public RagdollStylePack skinPack;

	// Token: 0x04000504 RID: 1284
	public Text skinsUnlockedText;

	// Token: 0x04000505 RID: 1285
	public Text facesUnlockedText;

	// Token: 0x04000506 RID: 1286
	[Space]
	public BuildItemCatalog buildItemCatalog;

	// Token: 0x04000507 RID: 1287
	public Text itemsUnlockedText;

	// Token: 0x04000508 RID: 1288
	[Space]
	public Image sliderImage;

	// Token: 0x04000509 RID: 1289
	public Text levelText;
}
