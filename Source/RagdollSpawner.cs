using System;
using System.Collections;
using System.Collections.Generic;
using ExtendedEssentials;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class RagdollSpawner : MonoBehaviour
{
	// Token: 0x06000404 RID: 1028 RVA: 0x0000558F File Offset: 0x0000378F
	private void Start()
	{
		this.pooler.limit = this.ragdollSpawnLimit;
		this.savedSpawnRate = this.spawnRate;
		this.savedRunSpeed = this.ragdollRunSpeed;
		this.FillRagdollPool();
		base.StartCoroutine(this.SpawnNextRagdoll());
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x000055CD File Offset: 0x000037CD
	private void Update()
	{
		if (this.enableTargetSpawnRate)
		{
			this.TargetSpawnRate();
		}
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x000055DD File Offset: 0x000037DD
	private IEnumerator SpawnNextRagdoll()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.spawnRate);
			if ((!this.spawnOnActiveTimer || !(this.levelTimer != null) || this.levelTimer.active) && (!this.spawnOnlyWhenPlayerIsClose || Vector3.Distance(PlayerSettings.instance.simulatedRagdoll.bodyElements.ragdollJoints[0].transform.position, base.transform.position) <= this.spawnPlayerRange))
			{
				this.SpawnRagdollFromPool();
			}
		}
		yield break;
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x000176B8 File Offset: 0x000158B8
	private GameObject SpawnRagdoll()
	{
		this.spawnedRagdoll = UnityEngine.Object.Instantiate<GameObject>(this.ragdollPrefab, base.transform.position, base.transform.rotation);
		if (this.spawnSpecificSkins.Count > 0)
		{
			RagdollStyleManager.AssignSmartRagdollSkin(this.spawnedRagdoll.transform, this.spawnSpecificSkins[UnityEngine.Random.Range(0, this.spawnSpecificSkins.Count)]);
		}
		this.ApplyRagdollSettings(this.spawnedRagdoll);
		this.spawnedRagdoll.SetActive(false);
		return this.spawnedRagdoll;
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00017744 File Offset: 0x00015944
	private void ApplyRagdollSettings(GameObject ragdoll)
	{
		if (this.spawnSpecificSkins.Count > 0)
		{
			RagdollStyleManager.AssignSmartRagdollSkin(ragdoll.transform, this.spawnSpecificSkins[UnityEngine.Random.Range(0, this.spawnSpecificSkins.Count)]);
		}
		this.antiTouchObject = ragdoll.GetComponentInChildren<AntiTouchObject>();
		this.ragdollController = ragdoll.GetComponentInChildren<SmartRagdollController>();
		this.agentController = this.ragdollController.agentController;
		if (this.antiTouchObject != null)
		{
			this.antiTouchObject.badForPlayer = this.RagdollIsAntiTouchBasedOnSkin(RagdollStyleManager.GetSmartRagdollSkinType(ragdoll.transform));
		}
		if (this.ragdollController != null)
		{
			this.ragdollController.enableSuicide = this.suicide;
			this.ragdollController.suicideTimeFrame = this.suicideTimeFrame;
			this.ragdollController.doOnRagdollMode = SmartRagdollController.OnRagdollMode.Hide;
			if (this.agentController != null)
			{
				this.agentController.speed = this.ragdollRunSpeed;
				if (this.targetIsPlayer)
				{
					this.ragdollController.logic = SmartRagdollController.Logic.Attack;
					return;
				}
				if (this.targets.Count > 0)
				{
					this.ragdollController.useLogicConditions = false;
					this.agentController.target = this.targets[UnityEngine.Random.Range(0, this.targets.Count)];
					this.agentController.moveMethod = AgentController.MoveMethod.MoveToPosition;
					return;
				}
				this.ragdollController.useLogicConditions = true;
				this.ragdollController.logic = SmartRagdollController.Logic.Explore;
			}
		}
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x000055EC File Offset: 0x000037EC
	private void SpawnRagdollFromPool()
	{
		this.pooledObject = this.pooler.Next();
		if (this.pooledObject != null)
		{
			this.ApplyRagdollSettings(this.pooledObject);
			this.pooledObject.SetActive(true);
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x000178B8 File Offset: 0x00015AB8
	private void FillRagdollPool()
	{
		if (this.pooler != null)
		{
			for (int i = 0; i < this.ragdollSpawnLimit; i++)
			{
				this.pooler.Add(this.SpawnRagdoll());
			}
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x000178F0 File Offset: 0x00015AF0
	private void TargetSpawnRate()
	{
		if (this.targetRateTimer < this.timeToTargetRate)
		{
			this.targetRateTimer += Time.deltaTime;
		}
		this.spawnRate = this.savedSpawnRate - (this.savedSpawnRate - this.targetSpawnRate) / this.timeToTargetRate * this.targetRateTimer;
		this.ragdollRunSpeed = this.savedRunSpeed - (this.savedRunSpeed - this.targetRunSpeed) / this.timeToTargetRate * this.targetRateTimer;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00017970 File Offset: 0x00015B70
	private bool RagdollIsAntiTouchBasedOnSkin(RagdollStylePack.SkinType skin)
	{
		if (this.allAreAntiTouch)
		{
			return true;
		}
		if (this.nonTouchableRagdolls.Count <= 0)
		{
			return false;
		}
		for (int i = 0; i < this.nonTouchableRagdolls.Count; i++)
		{
			if (this.nonTouchableRagdolls[i] == skin)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00005625 File Offset: 0x00003825
	private void OnDrawGizmosSelected()
	{
		if (this.spawnOnlyWhenPlayerIsClose)
		{
			Gizmos.color = new Color(1f, 0f, 0f, 0.2f);
			Gizmos.DrawSphere(base.transform.position, this.spawnPlayerRange);
		}
	}

	// Token: 0x04000565 RID: 1381
	public GameObject ragdollPrefab;

	// Token: 0x04000566 RID: 1382
	private Pooler pooler = new Pooler();

	// Token: 0x04000567 RID: 1383
	[Space(20f)]
	[Header("Targeting")]
	public bool targetIsPlayer;

	// Token: 0x04000568 RID: 1384
	public List<Transform> targets = new List<Transform>();

	// Token: 0x04000569 RID: 1385
	[Header("Time Settings")]
	[Space(20f)]
	public float timeToTargetRate = 60f;

	// Token: 0x0400056A RID: 1386
	private float targetRateTimer;

	// Token: 0x0400056B RID: 1387
	[Header("Spawn Settings")]
	[Space(20f)]
	public float spawnRate = 2f;

	// Token: 0x0400056C RID: 1388
	private float savedSpawnRate;

	// Token: 0x0400056D RID: 1389
	public bool enableTargetSpawnRate;

	// Token: 0x0400056E RID: 1390
	public float targetSpawnRate = 1.5f;

	// Token: 0x0400056F RID: 1391
	[Space]
	public LevelTimer levelTimer;

	// Token: 0x04000570 RID: 1392
	public bool spawnOnActiveTimer;

	// Token: 0x04000571 RID: 1393
	[Space]
	public int ragdollSpawnLimit = 5;

	// Token: 0x04000572 RID: 1394
	public bool spawnOnlyWhenPlayerIsClose;

	// Token: 0x04000573 RID: 1395
	public float spawnPlayerRange = 30f;

	// Token: 0x04000574 RID: 1396
	[Space(20f)]
	[Header("Ragdoll Settings")]
	public float ragdollRunSpeed = 8f;

	// Token: 0x04000575 RID: 1397
	private float savedRunSpeed;

	// Token: 0x04000576 RID: 1398
	public bool enableTargetRunSpeed;

	// Token: 0x04000577 RID: 1399
	public float targetRunSpeed = 8f;

	// Token: 0x04000578 RID: 1400
	[Space]
	public bool suicide;

	// Token: 0x04000579 RID: 1401
	public Vector2 suicideTimeFrame = new Vector2(5f, 20f);

	// Token: 0x0400057A RID: 1402
	[HideInInspector]
	public SmartRagdollController.OnRagdollMode onRagdollMode = SmartRagdollController.OnRagdollMode.Hide;

	// Token: 0x0400057B RID: 1403
	[Space]
	public bool allAreAntiTouch;

	// Token: 0x0400057C RID: 1404
	public bool deathOnTouch = true;

	// Token: 0x0400057D RID: 1405
	[Header("Spawn Specific Skins")]
	[Space(20f)]
	public List<RagdollStylePack.SkinType> spawnSpecificSkins = new List<RagdollStylePack.SkinType>();

	// Token: 0x0400057E RID: 1406
	public List<RagdollStylePack.SkinType> nonTouchableRagdolls = new List<RagdollStylePack.SkinType>();

	// Token: 0x0400057F RID: 1407
	private GameObject spawnedRagdoll;

	// Token: 0x04000580 RID: 1408
	private AgentController agentController;

	// Token: 0x04000581 RID: 1409
	private SmartRagdollController ragdollController;

	// Token: 0x04000582 RID: 1410
	private AntiTouchObject antiTouchObject;

	// Token: 0x04000583 RID: 1411
	private SmartRagdollController pooledSmartRagdollController;

	// Token: 0x04000584 RID: 1412
	private GameObject pooledObject;
}
