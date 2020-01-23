using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x020000DF RID: 223
public class NavMeshGenerator : MonoBehaviour
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600046C RID: 1132 RVA: 0x00005A8F File Offset: 0x00003C8F
	public static bool IsReady
	{
		get
		{
			return NavMeshGenerator.instance == null || NavMeshGenerator.instance.isReady;
		}
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00005AAA File Offset: 0x00003CAA
	private void Awake()
	{
		NavMeshGenerator.instance = this;
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
		WorldData.OnItemCountUpdated += this.OnItemCountUpdated;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00005AD4 File Offset: 0x00003CD4
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
		WorldData.OnItemCountUpdated -= this.OnItemCountUpdated;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00005AF8 File Offset: 0x00003CF8
	private void Start()
	{
		this.RebuildAll();
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00005B00 File Offset: 0x00003D00
	private void OnItemCountUpdated(int itemCount)
	{
		if (this.worldItemCount != itemCount)
		{
			this.worldEdited = true;
		}
		this.worldItemCount = itemCount;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00005B19 File Offset: 0x00003D19
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (!this.worldEdited || buildModeOn)
		{
			return;
		}
		this.RebuildAll();
		this.worldEdited = false;
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00005B36 File Offset: 0x00003D36
	public void RebuildAll()
	{
		if (this.navMeshSurface.navMeshData == null)
		{
			this.navMeshSurface.BuildNavMesh();
			return;
		}
		base.StartCoroutine(this.BuildNavmesh());
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00005B64 File Offset: 0x00003D64
	private IEnumerator BuildNavmesh()
	{
		this.isReady = false;
		this.navMeshGeneratorOperation = this.navMeshSurface.UpdateNavMesh(this.navMeshSurface.navMeshData);
		yield return new WaitUntil(() => this.navMeshGeneratorOperation.isDone);
		this.isReady = true;
		yield break;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00005B73 File Offset: 0x00003D73
	public void DestroyAll()
	{
		this.navMeshSurface.RemoveData();
	}

	// Token: 0x040005DB RID: 1499
	public NavMeshSurface navMeshSurface;

	// Token: 0x040005DC RID: 1500
	public static NavMeshGenerator instance;

	// Token: 0x040005DD RID: 1501
	private bool isReady;

	// Token: 0x040005DE RID: 1502
	private int worldItemCount;

	// Token: 0x040005DF RID: 1503
	private bool worldEdited;

	// Token: 0x040005E0 RID: 1504
	private AsyncOperation navMeshGeneratorOperation;
}
