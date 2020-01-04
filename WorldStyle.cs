using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class WorldStyle : MonoBehaviour
{
	// Token: 0x06000490 RID: 1168 RVA: 0x00005D06 File Offset: 0x00003F06
	private void Start()
	{
		GameData.OnStyleChanged += this.SetFloorColor;
		GameData.OnStyleChanged += this.SetItemColor;
		this.SetFloorColor();
		this.SetItemColor();
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00005D36 File Offset: 0x00003F36
	private void OnDestroy()
	{
		GameData.OnStyleChanged -= this.SetFloorColor;
		GameData.OnStyleChanged -= this.SetItemColor;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0001892C File Offset: 0x00016B2C
	private void SetFloorColor()
	{
		if (this.colors.TryGetValue(GameData.styling.FloorColor, out this.selectedFloorColor))
		{
			this.floorMaterial.SetColor("Color_2E05FA25", new Color(this.selectedFloorColor.x, this.selectedFloorColor.y, this.selectedFloorColor.z));
		}
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0001898C File Offset: 0x00016B8C
	private void SetItemColor()
	{
		if (this.colors.TryGetValue(GameData.styling.BlockColor, out this.selectedItemColor))
		{
			this.ItemMaterial.SetColor("Color_2E05FA25", new Color(this.selectedItemColor.x, this.selectedItemColor.y, this.selectedItemColor.z));
		}
	}

	// Token: 0x06000494 RID: 1172
	public WorldStyle()
	{
		this.colors = new Dictionary<WorldStyle.ColorStyle, Vector3>
		{
			{
				WorldStyle.ColorStyle.White,
				new Vector3(1f, 1f, 1f)
			},
			{
				WorldStyle.ColorStyle.Grey,
				new Vector3(0.6f, 0.6f, 0.6f)
			},
			{
				WorldStyle.ColorStyle.Black,
				new Vector3(0.25f, 0.25f, 0.25f)
			},
			{
				WorldStyle.ColorStyle.Blue,
				new Vector3(0.3f, 0.64f, 0.9f)
			},
			{
				WorldStyle.ColorStyle.Purple,
				new Vector3(0.52f, 0.3f, 0.82f)
			},
			{
				WorldStyle.ColorStyle.Green,
				new Vector3(0.2f, 0.82f, 0.24f)
			},
			{
				WorldStyle.ColorStyle.Red,
				new Vector3(0.82f, 0.27f, 0.2f)
			},
			{
				WorldStyle.ColorStyle.Orange,
				new Vector3(1f, 0.65f, 0.25f)
			},
			{
				WorldStyle.ColorStyle.Yellow,
				new Vector3(1f, 0.85f, 0.25f)
			},
			{
				WorldStyle.ColorStyle.Custom1,
				new Vector3(0f, 0f, 1f)
			},
			{
				WorldStyle.ColorStyle.Custom2,
				new Vector3(0f, 1f, 0f)
			}
		};
	}

	// Token: 0x04000612 RID: 1554
	private Dictionary<WorldStyle.ColorStyle, Vector3> colors;

	// Token: 0x04000613 RID: 1555
	public Material floorMaterial;

	// Token: 0x04000614 RID: 1556
	public Material ItemMaterial;

	// Token: 0x04000615 RID: 1557
	private Vector3 selectedFloorColor;

	// Token: 0x04000616 RID: 1558
	private Vector3 selectedItemColor;

	// Token: 0x020000E7 RID: 231
	public enum ColorStyle
	{
		// Token: 0x04000618 RID: 1560
		White,
		// Token: 0x04000619 RID: 1561
		Grey,
		// Token: 0x0400061A RID: 1562
		Black,
		// Token: 0x0400061B RID: 1563
		Blue,
		// Token: 0x0400061C RID: 1564
		Green,
		// Token: 0x0400061D RID: 1565
		Red,
		// Token: 0x0400061E RID: 1566
		Yellow,
		// Token: 0x0400061F RID: 1567
		Orange,
		// Token: 0x04000620 RID: 1568
		Purple,
		// Token: 0x04000AE7 RID: 2791
		Custom1,
		// Token: 0x04000AE8 RID: 2792
		Custom2
	}
}
