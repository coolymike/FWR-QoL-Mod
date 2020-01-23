using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EE RID: 238
public class BuildGridBackup : MonoBehaviour
{
	// Token: 0x060004B9 RID: 1209 RVA: 0x00005E71 File Offset: 0x00004071
	private void OnLevelWasLoaded()
	{
		BuildGridBackup.registeredPoints.Clear();
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00019928 File Offset: 0x00017B28
	public static Vector3 PositionToGrid(Vector3 position)
	{
		Vector3 result;
		result.x = Mathf.Round(position.x / BuildGridBackup.worldGridSize) * BuildGridBackup.worldGridSize;
		result.y = Mathf.Round(position.y / BuildGridBackup.worldGridSize) * BuildGridBackup.worldGridSize;
		result.z = Mathf.Round(position.z / BuildGridBackup.worldGridSize) * BuildGridBackup.worldGridSize;
		return result;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00019990 File Offset: 0x00017B90
	public static bool Add(Transform containerTransform, Vector3 position, Vector3 containerSize)
	{
		List<BuildGridBackup.Point> list = BuildGridBackup.FillCubicVolume(containerTransform, position, containerSize);
		for (int i = 0; i < list.Count; i++)
		{
			if (!BuildGridBackup.registeredPoints.ContainsKey(list[i]))
			{
				BuildGridBackup.registeredPoints.Add(list[i], containerTransform);
			}
		}
		return true;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x000199E0 File Offset: 0x00017BE0
	public static bool Remove(Transform containerTransform, Vector3 position, Vector3 containerSize)
	{
		List<BuildGridBackup.Point> list = BuildGridBackup.FillCubicVolume(containerTransform, position, containerSize);
		for (int i = 0; i < list.Count; i++)
		{
			if (BuildGridBackup.registeredPoints.ContainsKey(list[i]))
			{
				BuildGridBackup.registeredPoints.Remove(list[i]);
			}
		}
		return true;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00019A30 File Offset: 0x00017C30
	public static Transform OverlapCheck(Transform containerTransform, Vector3 position, Vector3 containerSize)
	{
		List<BuildGridBackup.Point> list = BuildGridBackup.FillCubicVolume(containerTransform, position, containerSize);
		for (int i = 0; i < list.Count; i++)
		{
			if (BuildGridBackup.registeredPoints.ContainsKey(list[i]))
			{
				return BuildGridBackup.registeredPoints[list[i]];
			}
		}
		return null;
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00019A80 File Offset: 0x00017C80
	private static List<BuildGridBackup.Point> FillCubicVolume(Transform containerTransform, Vector3 position, Vector3 containerSize)
	{
		Vector3[] array = new Vector3[]
		{
			new Vector3(1f, 1f, 1f),
			new Vector3(-1f, 1f, 1f),
			new Vector3(1f, -1f, 1f),
			new Vector3(1f, 1f, -1f)
		};
		List<BuildGridBackup.Point> list = new List<BuildGridBackup.Point>();
		Vector3 vector;
		vector.x = BuildGridBackup.PointPositionTotalFromDistance(BuildGridBackup.PositionOfCubeVertex(containerTransform, position, containerSize, array[0]), BuildGridBackup.PositionOfCubeVertex(containerTransform, position, containerSize, array[1]));
		vector.y = BuildGridBackup.PointPositionTotalFromDistance(BuildGridBackup.PositionOfCubeVertex(containerTransform, position, containerSize, array[0]), BuildGridBackup.PositionOfCubeVertex(containerTransform, position, containerSize, array[2]));
		vector.z = BuildGridBackup.PointPositionTotalFromDistance(BuildGridBackup.PositionOfCubeVertex(containerTransform, position, containerSize, array[0]), BuildGridBackup.PositionOfCubeVertex(containerTransform, position, containerSize, array[3]));
		int num = 0;
		while ((float)num < vector.x)
		{
			int num2 = 0;
			while ((float)num2 < vector.y)
			{
				int num3 = 0;
				while ((float)num3 < vector.z)
				{
					list.Add(BuildGridBackup.PositionToPoint(BuildGridBackup.PositionOfCubeVertex(containerTransform, position, containerSize, new Vector3((float)num / vector.x * 2f - 1f, (float)num2 / vector.y * 2f - 1f, (float)num3 / vector.z * 2f - 1f))));
					num3++;
				}
				num2++;
			}
			num++;
		}
		return list;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00019C24 File Offset: 0x00017E24
	private static Vector3 PositionBetweenVectors(Vector3 from, Vector3 to, int iteration)
	{
		return from + (to - from).normalized * BuildGridBackup.worldGridSize * (float)iteration;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00005E7D File Offset: 0x0000407D
	private static float PointPositionTotalFromDistance(Vector3 from, Vector3 to)
	{
		return Vector3.Distance(from, to) / BuildGridBackup.worldGridSize;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00019C58 File Offset: 0x00017E58
	private static Vector3 PositionOfCubeVertex(Transform transform, Vector3 position, Vector3 scale, Vector3 vertex)
	{
		return position + new Vector3(0f, scale.y * 0.5f, 0f) + transform.right * (scale.x * 0.5f) * vertex.x + transform.up * (scale.y * 0.5f) * vertex.y + transform.forward * (scale.z * 0.5f) * vertex.z;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00005E8C File Offset: 0x0000408C
	private static Vector3 PointToPosition(BuildGridBackup.Point point)
	{
		return new Vector3((float)point.x * BuildGridBackup.worldGridSize, (float)point.y * BuildGridBackup.worldGridSize, (float)point.z * BuildGridBackup.worldGridSize);
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00005EBD File Offset: 0x000040BD
	private static BuildGridBackup.Point PositionToPoint(Vector3 position)
	{
		return new BuildGridBackup.Point(Mathf.RoundToInt(position.x / BuildGridBackup.worldGridSize), Mathf.RoundToInt(position.y / BuildGridBackup.worldGridSize), Mathf.RoundToInt(position.z / BuildGridBackup.worldGridSize));
	}

	// Token: 0x04000646 RID: 1606
	public static readonly float worldGridSize = 0.6f;

	// Token: 0x04000647 RID: 1607
	public static Dictionary<BuildGridBackup.Point, Transform> registeredPoints = new Dictionary<BuildGridBackup.Point, Transform>();

	// Token: 0x020000EF RID: 239
	public struct Point
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00005F0D File Offset: 0x0000410D
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00005F15 File Offset: 0x00004115
		public int x { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00005F1E File Offset: 0x0000411E
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00005F26 File Offset: 0x00004126
		public int y { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00005F2F File Offset: 0x0000412F
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00005F37 File Offset: 0x00004137
		public int z { get; set; }

		// Token: 0x060004CC RID: 1228 RVA: 0x00005F40 File Offset: 0x00004140
		public Point(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}
}
