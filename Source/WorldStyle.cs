using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class WorldStyle : MonoBehaviour
{
	// Token: 0x060004A4 RID: 1188 RVA: 0x00005D33 File Offset: 0x00003F33
	private void Start()
	{
		GameData.OnStyleChanged += this.SetFloorColor;
		GameData.OnStyleChanged += this.SetItemColor;
		this.SetFloorColor();
		this.SetItemColor();
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00005D63 File Offset: 0x00003F63
	private void OnDestroy()
	{
		GameData.OnStyleChanged -= this.SetFloorColor;
		GameData.OnStyleChanged -= this.SetItemColor;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x000192C8 File Offset: 0x000174C8
	private void SetFloorColor()
	{
		if (this.colors.TryGetValue(GameData.styling.FloorColor, out this.selectedFloorColor))
		{
			this.floorMaterial.SetColor("Color_2E05FA25", new Color(this.selectedFloorColor.x, this.selectedFloorColor.y, this.selectedFloorColor.z));
		}
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00019328 File Offset: 0x00017528
	private void SetItemColor()
	{
		if (this.colors.TryGetValue(GameData.styling.BlockColor, out this.selectedItemColor))
		{
			this.ItemMaterial.SetColor("Color_2E05FA25", new Color(this.selectedItemColor.x, this.selectedItemColor.y, this.selectedItemColor.z));
		}
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00019388 File Offset: 0x00017588
	public WorldStyle()
	{
		if (File.Exists(WorldStyle.path))
		{
			string[] array = File.ReadAllText(WorldStyle.path).Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					':'
				});
				if (array2[0] == "CustomColor1")
				{
					string[] array3 = array2[1].Split(new char[]
					{
						','
					});
					int[] array4 = new int[array3.Length];
					for (int j = 0; j < array3.Length; j++)
					{
						array4[j] = int.Parse(array3[j]);
					}
					WorldStyle.Color1 = new Vector3((float)(array4[0] / 255), (float)(array4[1] / 255), (float)(array4[2] / 255));
				}
				else if (array2[0] == "CustomColor2")
				{
					string[] array5 = array2[1].Split(new char[]
					{
						','
					});
					int[] array6 = new int[array5.Length];
					for (int k = 0; k < array5.Length; k++)
					{
						array6[k] = int.Parse(array5[k]);
					}
					WorldStyle.Color2 = new Vector3((float)(array6[0] / 255), (float)(array6[1] / 255), (float)(array6[2] / 255));
				}
			}
		}
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
				new Vector3(0f, 0f, 0f)
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
				WorldStyle.Color1
			},
			{
				WorldStyle.ColorStyle.Custom2,
				WorldStyle.Color2
			}
		};
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x000195FC File Offset: 0x000177FC
	static WorldStyle()
	{
		WorldStyle.Color1 = new Vector3(0f, 0f, 0f);
		WorldStyle.Color2 = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x04000622 RID: 1570
	private Dictionary<WorldStyle.ColorStyle, Vector3> colors;

	// Token: 0x04000623 RID: 1571
	public Material floorMaterial;

	// Token: 0x04000624 RID: 1572
	public Material ItemMaterial;

	// Token: 0x04000625 RID: 1573
	private Vector3 selectedFloorColor;

	// Token: 0x04000626 RID: 1574
	private Vector3 selectedItemColor;

	// Token: 0x04000627 RID: 1575
	public static Vector3 Color1;

	// Token: 0x04000628 RID: 1576
	public static Vector3 Color2;

	// Token: 0x04000629 RID: 1577
	private static string path = Application.persistentDataPath + "\\mod_settings.txt";

	// Token: 0x020000EA RID: 234
	public enum ColorStyle
	{
		// Token: 0x0400062B RID: 1579
		White,
		// Token: 0x0400062C RID: 1580
		Grey,
		// Token: 0x0400062D RID: 1581
		Black,
		// Token: 0x0400062E RID: 1582
		Blue,
		// Token: 0x0400062F RID: 1583
		Green,
		// Token: 0x04000630 RID: 1584
		Red,
		// Token: 0x04000631 RID: 1585
		Yellow,
		// Token: 0x04000632 RID: 1586
		Orange,
		// Token: 0x04000633 RID: 1587
		Purple,
		// Token: 0x04000634 RID: 1588
		Custom1,
		// Token: 0x04000635 RID: 1589
		Custom2
	}
}
