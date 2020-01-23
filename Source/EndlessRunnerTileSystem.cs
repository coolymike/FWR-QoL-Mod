using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class EndlessRunnerTileSystem : MonoBehaviour
{
	// Token: 0x060003DB RID: 987 RVA: 0x00005414 File Offset: 0x00003614
	private void Awake()
	{
		EndlessRunnerTileSystem.reference = this;
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00016EE0 File Offset: 0x000150E0
	private void Start()
	{
		this.player = PlayerSettings.instance.simulatedRagdoll.bodyElements.GetJoint(RagdollBodyElements.Joint.Core).transform;
		this.navMeshGenerator.RebuildAll();
		this.tilesUntilTurnRight = this.tilesUntilTurn;
		this.tilesUntilTurnLeft = this.tilesUntilTurn;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00016F30 File Offset: 0x00015130
	private void Update()
	{
		if (this.latestTile != null)
		{
			while (Vector3.Distance(this.player.position, this.latestTile.transform.position) < 120f || !this.latestTileIsCorner)
			{
				this.SpawnNextTile();
			}
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00016F84 File Offset: 0x00015184
	public void SpawnNextTile()
	{
		if (this.latestTile == null)
		{
			return;
		}
		this.latestTileComponent = this.latestTile.GetComponent<EndlessRunnerTile>();
		if (this.latestTileComponent == null)
		{
			return;
		}
		this.nextTile = this.NextTileToSpawn();
		this.searchedTile = this.TileExistsInPool(this.nextTile.name);
		if (this.searchedTile == null)
		{
			this.latestTile = UnityEngine.Object.Instantiate<GameObject>(this.nextTile, this.latestTileComponent.tileEnd.position, this.latestTileComponent.tileEnd.rotation);
			this.latestTile.name = this.nextTile.name;
			this.pool.Add(this.latestTile);
			return;
		}
		this.latestTile = this.searchedTile;
		this.latestTile.transform.position = this.latestTileComponent.tileEnd.position;
		this.latestTile.transform.rotation = this.latestTileComponent.tileEnd.rotation;
		this.latestTile.SetActive(true);
	}

	// Token: 0x060003DF RID: 991 RVA: 0x000170A4 File Offset: 0x000152A4
	private GameObject TileExistsInPool(string tileName)
	{
		for (int i = 0; i < this.pool.Count; i++)
		{
			if (this.pool[i].name == tileName && !this.pool[i].activeInHierarchy)
			{
				return this.pool[i];
			}
		}
		return null;
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00017104 File Offset: 0x00015304
	public GameObject NextTileToSpawn()
	{
		this.tilesUntilTurnRight--;
		this.tilesUntilTurnLeft--;
		this.nextDirection = UnityEngine.Random.Range(0, 3);
		if (this.tilesUntilTurnRight <= 0 && this.nextDirection == 1)
		{
			this.tilesUntilTurnRight = this.tilesUntilTurn;
			this.latestTileIsCorner = true;
			this.navMeshGenerator.RebuildAll();
			return this.tilePrefabsRight[UnityEngine.Random.Range(0, this.tilePrefabsRight.Length)];
		}
		if (this.tilesUntilTurnLeft <= 0 && this.nextDirection == 2)
		{
			this.latestTileIsCorner = true;
			this.tilesUntilTurnLeft = this.tilesUntilTurn;
			this.navMeshGenerator.RebuildAll();
			return this.tilePrefabsLeft[UnityEngine.Random.Range(0, this.tilePrefabsLeft.Length)];
		}
		this.latestTileIsCorner = false;
		return this.tilePrefabsCenter[UnityEngine.Random.Range(0, this.tilePrefabsCenter.Length)];
	}

	// Token: 0x04000535 RID: 1333
	public static EndlessRunnerTileSystem reference;

	// Token: 0x04000536 RID: 1334
	public NavMeshGenerator navMeshGenerator;

	// Token: 0x04000537 RID: 1335
	public GameObject[] tilePrefabsCenter;

	// Token: 0x04000538 RID: 1336
	public GameObject[] tilePrefabsRight;

	// Token: 0x04000539 RID: 1337
	public GameObject[] tilePrefabsLeft;

	// Token: 0x0400053A RID: 1338
	public List<GameObject> pool = new List<GameObject>();

	// Token: 0x0400053B RID: 1339
	public Transform player;

	// Token: 0x0400053C RID: 1340
	public int nextDirection;

	// Token: 0x0400053D RID: 1341
	public int tilesUntilTurn = 4;

	// Token: 0x0400053E RID: 1342
	public int tilesUntilTurnRight;

	// Token: 0x0400053F RID: 1343
	public int tilesUntilTurnLeft;

	// Token: 0x04000540 RID: 1344
	public GameObject latestTile;

	// Token: 0x04000541 RID: 1345
	public bool latestTileIsCorner;

	// Token: 0x04000542 RID: 1346
	private EndlessRunnerTile latestTileComponent;

	// Token: 0x04000543 RID: 1347
	private GameObject nextTile;

	// Token: 0x04000544 RID: 1348
	private GameObject searchedTile;

	// Token: 0x04000545 RID: 1349
	private bool foundNextPrefab;
}
