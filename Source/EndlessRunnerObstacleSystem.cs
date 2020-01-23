using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class EndlessRunnerObstacleSystem : MonoBehaviour
{
	// Token: 0x060003C6 RID: 966 RVA: 0x00016C0C File Offset: 0x00014E0C
	private void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (base.transform.GetChild(i).gameObject.activeInHierarchy)
			{
				this.obstacles.Add(base.transform.GetChild(i).gameObject);
				base.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00005326 File Offset: 0x00003526
	private void OnEnable()
	{
		this.ActivateRandomObstacle();
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0000532E File Offset: 0x0000352E
	private void Update()
	{
		if (this.parentTile.isFalling)
		{
			this.DisableAllObstacles();
		}
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00016C7C File Offset: 0x00014E7C
	private void ActivateRandomObstacle()
	{
		this.activeObstacle = UnityEngine.Random.Range(0, this.obstacles.Count);
		for (int i = 0; i < this.obstacles.Count; i++)
		{
			this.obstacles[i].SetActive(this.activeObstacle == i);
		}
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00016CD0 File Offset: 0x00014ED0
	private void DisableAllObstacles()
	{
		for (int i = 0; i < this.obstacles.Count; i++)
		{
			this.obstacles[i].SetActive(false);
		}
	}

	// Token: 0x04000526 RID: 1318
	public EndlessRunnerTile parentTile;

	// Token: 0x04000527 RID: 1319
	public List<GameObject> obstacles = new List<GameObject>();

	// Token: 0x04000528 RID: 1320
	public int activeObstacle;
}
