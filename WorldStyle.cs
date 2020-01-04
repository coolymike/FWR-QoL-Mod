using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class WorldStyle : MonoBehaviour
{
	// Token: 0x06000497 RID: 1175
	private void Start()
	{
		GameData.OnStyleChanged += this.SetFloorColor;
		GameData.OnStyleChanged += this.SetItemColor;
		this.SetFloorColor();
		this.SetItemColor();
	}

	// Token: 0x06000498 RID: 1176
	private void OnDestroy()
	{
		GameData.OnStyleChanged -= this.SetFloorColor;
		GameData.OnStyleChanged -= this.SetItemColor;
	}

	// Token: 0x06000499 RID: 1177
	private void SetFloorColor()
	{
		if (this.colors.TryGetValue(GameData.styling.FloorColor, out this.selectedFloorColor))
		{
			this.floorMaterial.SetColor("Color_2E05FA25", new Color(this.selectedFloorColor.x, this.selectedFloorColor.y, this.selectedFloorColor.z));
		}
	}

	// Token: 0x0600049A RID: 1178
	private void SetItemColor()
	{
		if (this.colors.TryGetValue(GameData.styling.BlockColor, out this.selectedItemColor))
		{
			this.ItemMaterial.SetColor("Color_2E05FA25", new Color(this.selectedItemColor.x, this.selectedItemColor.y, this.selectedItemColor.z));
		}
	}

	// Token: 0x0600049B RID: 1179
	public WorldStyle()
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
				string[] Colors1Str = array2[1].Split(new char[]
				{
					','
				});
				int[] Colors1Int = new int[Colors1Str.Length];
				for (int count = 0; count < Colors1Str.Length; count++)
				{
					Colors1Int[count] = int.Parse(Colors1Str[count]);
				}
				WorldStyle.Color1 = new Vector3((float)(Colors1Int[0] / 255), (float)(Colors1Int[1] / 255), (float)(Colors1Int[2] / 255));
			}
			else if (array2[0] == "CustomColor2")
			{
				string[] Colors2Str = array2[1].Split(new char[]
				{
					','
				});
				int[] Colors2Int = new int[Colors2Str.Length];
				for (int count2 = 0; count2 < Colors2Str.Length; count2++)
				{
					Colors2Int[count2] = int.Parse(Colors2Str[count2]);
				}
				WorldStyle.Color2 = new Vector3((float)(Colors2Int[0] / 255), (float)(Colors2Int[1] / 255), (float)(Colors2Int[2] / 255));
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

	// Token: 0x06000790 RID: 1936
	static WorldStyle()
	{
		WorldStyle.Color1 = new Vector3(0f, 0f, 0f);
		WorldStyle.Color2 = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x04000615 RID: 1557
	private Dictionary<WorldStyle.ColorStyle, Vector3> colors;

	// Token: 0x04000616 RID: 1558
	public Material floorMaterial;

	// Token: 0x04000617 RID: 1559
	public Material ItemMaterial;

	// Token: 0x04000618 RID: 1560
	private Vector3 selectedFloorColor;

	// Token: 0x04000619 RID: 1561
	private Vector3 selectedItemColor;

	// Token: 0x040009CB RID: 2507
	public static Vector3 Color1;

	// Token: 0x040009CC RID: 2508
	public static Vector3 Color2;

	// Token: 0x040009EF RID: 2543
	private static string path = Application.persistentDataPath + "\\mod_settings.txt";

	// Token: 0x020000E8 RID: 232
	public enum ColorStyle
	{
		// Token: 0x04000621 RID: 1569
		White,
		// Token: 0x04000622 RID: 1570
		Grey,
		// Token: 0x04000623 RID: 1571
		Black,
		// Token: 0x04000624 RID: 1572
		Blue,
		// Token: 0x04000625 RID: 1573
		Green,
		// Token: 0x04000626 RID: 1574
		Red,
		// Token: 0x04000627 RID: 1575
		Yellow,
		// Token: 0x04000628 RID: 1576
		Orange,
		// Token: 0x04000629 RID: 1577
		Purple,
		// Token: 0x0400062A RID: 1578
		Custom1,
		// Token: 0x0400062B RID: 1579
		Custom2
	}
}
