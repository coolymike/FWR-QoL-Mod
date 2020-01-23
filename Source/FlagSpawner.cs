using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class FlagSpawner : MonoBehaviour
{
	// Token: 0x06000212 RID: 530 RVA: 0x00003E0E File Offset: 0x0000200E
	private void Start()
	{
		this.FillPool();
		base.StartCoroutine(this.ActivateNextRagdollRoutine());
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00010344 File Offset: 0x0000E544
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
		for (int i = 0; i < this.pool.Count; i++)
		{
			UnityEngine.Object.Destroy(this.pool[i]);
		}
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0001038C File Offset: 0x0000E58C
	private void FillPool()
	{
		for (int i = 0; i < this.max; i++)
		{
			switch (this.spawn)
			{
			case FlagSpawner.Spawn.Stupid:
				this.spawnedRagdoll = UnityEngine.Object.Instantiate<GameObject>(this.prefabStupid, new Vector3(base.transform.position.x + UnityEngine.Random.Range(-1.2f, 1.2f), base.transform.position.y, base.transform.position.z + UnityEngine.Random.Range(-1.2f, 1.2f)), base.transform.rotation);
				break;
			case FlagSpawner.Spawn.Follower:
				this.spawnedRagdoll = UnityEngine.Object.Instantiate<GameObject>(this.prefabFollower, new Vector3(base.transform.position.x + UnityEngine.Random.Range(-1.2f, 1.2f), base.transform.position.y, base.transform.position.z + UnityEngine.Random.Range(-1.2f, 1.2f)), base.transform.rotation);
				this.ragdollController = this.spawnedRagdoll.GetComponent<SmartRagdollController>();
				break;
			case FlagSpawner.Spawn.Attacker:
				this.spawnedRagdoll = UnityEngine.Object.Instantiate<GameObject>(this.prefabAttacker, new Vector3(base.transform.position.x + UnityEngine.Random.Range(-1.2f, 1.2f), base.transform.position.y, base.transform.position.z + UnityEngine.Random.Range(-1.2f, 1.2f)), base.transform.rotation);
				this.ragdollController = this.spawnedRagdoll.GetComponent<SmartRagdollController>();
				break;
			case FlagSpawner.Spawn.Defender:
				this.spawnedRagdoll = UnityEngine.Object.Instantiate<GameObject>(this.prefabDefender, new Vector3(base.transform.position.x + UnityEngine.Random.Range(-1.2f, 1.2f), base.transform.position.y, base.transform.position.z + UnityEngine.Random.Range(-1.2f, 1.2f)), base.transform.rotation);
				this.ragdollController = this.spawnedRagdoll.GetComponent<SmartRagdollController>();
				break;
			case FlagSpawner.Spawn.Explorer:
				this.spawnedRagdoll = UnityEngine.Object.Instantiate<GameObject>(this.prefabExplorer, new Vector3(base.transform.position.x + UnityEngine.Random.Range(-1.2f, 1.2f), base.transform.position.y, base.transform.position.z + UnityEngine.Random.Range(-1.2f, 1.2f)), base.transform.rotation);
				this.ragdollController = this.spawnedRagdoll.GetComponent<SmartRagdollController>();
				break;
			default:
				return;
			}
			if (this.ragdollController != null)
			{
				this.ragdollController.doOnRagdollMode = SmartRagdollController.OnRagdollMode.Hide;
				this.ragdollController.onRagdollModeTimer = this.hideTime;
			}
			this.pool.Add(this.spawnedRagdoll);
			this.spawnedRagdoll.SetActive(false);
		}
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00003E34 File Offset: 0x00002034
	private IEnumerator ActivateNextRagdollRoutine()
	{
		for (;;)
		{
			if (LevelManager.BuildModeOn)
			{
				yield return null;
			}
			else
			{
				this.ActivateNextRagdoll();
				yield return new WaitForSeconds(this.spawnRate);
			}
		}
		yield break;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x00003E43 File Offset: 0x00002043
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (buildModeOn)
		{
			this.HideAllRagdolls();
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x000106A8 File Offset: 0x0000E8A8
	private void HideAllRagdolls()
	{
		for (int i = 0; i < this.pool.Count; i++)
		{
			this.pool[i].SetActive(false);
		}
	}

	// Token: 0x06000218 RID: 536 RVA: 0x000106E0 File Offset: 0x0000E8E0
	private void ActivateNextRagdoll()
	{
		for (int i = 0; i < this.pool.Count; i++)
		{
			if (!this.pool[i].activeInHierarchy)
			{
				this.pool[i].SetActive(true);
				return;
			}
		}
	}

	// Token: 0x040002FB RID: 763
	public GameObject prefabStupid;

	// Token: 0x040002FC RID: 764
	public GameObject prefabFollower;

	// Token: 0x040002FD RID: 765
	public GameObject prefabAttacker;

	// Token: 0x040002FE RID: 766
	public GameObject prefabDefender;

	// Token: 0x040002FF RID: 767
	public GameObject prefabExplorer;

	// Token: 0x04000300 RID: 768
	public FlagSpawner.Spawn spawn;

	// Token: 0x04000301 RID: 769
	public int max = 10;

	// Token: 0x04000302 RID: 770
	public float spawnRate = 1f;

	// Token: 0x04000303 RID: 771
	public float hideTime = 5f;

	// Token: 0x04000304 RID: 772
	private List<GameObject> pool = new List<GameObject>();

	// Token: 0x04000305 RID: 773
	private GameObject spawnedRagdoll;

	// Token: 0x04000306 RID: 774
	private SmartRagdollController ragdollController;

	// Token: 0x02000068 RID: 104
	public enum Spawn
	{
		// Token: 0x04000308 RID: 776
		Stupid,
		// Token: 0x04000309 RID: 777
		Follower,
		// Token: 0x0400030A RID: 778
		Attacker,
		// Token: 0x0400030B RID: 779
		Defender,
		// Token: 0x0400030C RID: 780
		Explorer
	}
}
