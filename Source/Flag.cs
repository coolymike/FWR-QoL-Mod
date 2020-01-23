using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class Flag : MonoBehaviour
{
	// Token: 0x0600020C RID: 524 RVA: 0x00003DBC File Offset: 0x00001FBC
	private void Awake()
	{
		if (this.type != Flag.Type.Spawner && this.type != Flag.Type.RagdollToggle)
		{
			Flag.flagsInScene.Add(this);
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00003DDB File Offset: 0x00001FDB
	private void OnDestroy()
	{
		Flag.flagsInScene.Remove(this);
	}

	// Token: 0x0600020E RID: 526 RVA: 0x000102A0 File Offset: 0x0000E4A0
	public static Flag GetClosestFlag(Vector3 position, Flag.Type flagType)
	{
		Flag flag = null;
		for (int i = 0; i < Flag.flagsInScene.Count; i++)
		{
			if (Flag.flagsInScene[i].gameObject.activeInHierarchy && Flag.flagsInScene[i].type == flagType)
			{
				if (flag == null)
				{
					flag = Flag.flagsInScene[i];
				}
				else if (Vector3.Distance(position, Flag.flagsInScene[i].transform.position) < Vector3.Distance(position, flag.transform.position))
				{
					flag = Flag.flagsInScene[i];
				}
			}
		}
		return flag;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00003DE9 File Offset: 0x00001FE9
	public static float DistanceToClosestFlag(Vector3 position, Flag.Type flagType)
	{
		return Vector3.Distance(position, Flag.GetClosestFlag(position, flagType).transform.position);
	}

	// Token: 0x040002F3 RID: 755
	public static List<Flag> flagsInScene = new List<Flag>();

	// Token: 0x040002F4 RID: 756
	public Flag.Type type;

	// Token: 0x02000066 RID: 102
	public enum Type
	{
		// Token: 0x040002F6 RID: 758
		None,
		// Token: 0x040002F7 RID: 759
		Follow,
		// Token: 0x040002F8 RID: 760
		Avoid,
		// Token: 0x040002F9 RID: 761
		RagdollToggle,
		// Token: 0x040002FA RID: 762
		Spawner
	}
}
