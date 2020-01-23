using System;
using System.Collections;
using ExtendedEssentials;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class ObjectSpawner : MonoBehaviour
{
	// Token: 0x0600047D RID: 1149 RVA: 0x00005BA4 File Offset: 0x00003DA4
	private void Start()
	{
		this.pooler.limit = this.maxObjectCount;
		this.FillPool();
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0001885C File Offset: 0x00016A5C
	private void OnEnable()
	{
		base.StartCoroutine(this.SpawnRoutine());
		for (int i = 0; i < this.pooler.pool.Count; i++)
		{
			this.resetPlacement = this.pooler.pool[i].GetComponentInChildren<ResetPlacement>();
			if (!(this.resetPlacement == null))
			{
				this.resetPlacement.specifyResetPlacement = true;
				this.resetPlacement.resetPosition = base.transform.position;
				this.resetPlacement.resetRotation = base.transform.rotation;
			}
		}
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x000188F4 File Offset: 0x00016AF4
	private void OnDrawGizmosSelected()
	{
		if (this.spawnOnlyWhenPlayerIsClose)
		{
			Gizmos.color = new Color(1f, 0f, 0f, 0.15f);
			Gizmos.DrawSphere(base.transform.position, this.startSpawnWhenPlayerInRange);
			Gizmos.color = new Color(0f, 0f, 1f, 0.15f);
			Gizmos.DrawSphere(base.transform.position, this.stopSpawnWhenPlayerInRange);
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00005BBD File Offset: 0x00003DBD
	private void Update()
	{
		this.playerDistance = Vector3.Distance(PlayerSettings.instance.simulatedRagdoll.bodyElements.ragdollJoints[0].transform.position, base.transform.position);
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00005BF5 File Offset: 0x00003DF5
	private IEnumerator SpawnRoutine()
	{
		for (;;)
		{
			this.SpawnNextObjectFromPool();
			yield return new WaitForSeconds(UnityEngine.Random.Range(this.spawnRateRangeMinMax.x, this.spawnRateRangeMinMax.y));
		}
		yield break;
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00018974 File Offset: 0x00016B74
	private void SpawnNextObjectFromPool()
	{
		if (LevelManager.StatusIsWinOrLose(true))
		{
			return;
		}
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		if (!this.spawnOnlyWhenPlayerIsClose || (this.spawnOnlyWhenPlayerIsClose && this.playerDistance < this.startSpawnWhenPlayerInRange && this.playerDistance > this.stopSpawnWhenPlayerInRange))
		{
			this.pooledObject = this.pooler.Next();
			if (this.pooledObject == null)
			{
				return;
			}
			if (this.spawnObject == ObjectSpawner.SpawnObject.Ragdoll)
			{
				this.pooledObject.GetComponent<RagdollSettings>().ResetRagdoll();
			}
			else if (this.spawnObject == ObjectSpawner.SpawnObject.Car)
			{
				this.carControls = this.pooledObject.GetComponentInChildren<VehicleCarController>();
				this.carControls.autoMotor = UnityEngine.Random.Range(this.carSpeedMin, this.carSpeedMax);
				this.carControls.autoSteer = UnityEngine.Random.Range(this.carSteerDirectionMin, this.carSteerDirectionMax);
			}
			this.pooledObject.SetActive(true);
			if (this.addForce)
			{
				this.AddForce(this.pooledObject, true);
			}
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00018A78 File Offset: 0x00016C78
	private GameObject SpawnRagdoll()
	{
		this.spawnedObject = UnityEngine.Object.Instantiate<GameObject>(this.ragdollPrefab, base.transform.position, base.transform.rotation);
		this.ragdollSettings = this.spawnedObject.transform.root.GetComponentInChildren<RagdollSettings>();
		this.ragdollSettings.CollisionDetectionMode = this.CollisionDetectionMode;
		this.spawnedObject.AddComponent<AntiTouchObject>();
		AutoHide.AddComponent(this.spawnedObject, this.destroyTime);
		return this.spawnedObject;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00018AFC File Offset: 0x00016CFC
	private GameObject SpawnCar()
	{
		this.spawnedObject = UnityEngine.Object.Instantiate<GameObject>(this.carPrefab, base.transform.position, base.transform.rotation);
		this.carControls = this.spawnedObject.GetComponentInChildren<VehicleCarController>();
		if (this.driveToPlayer)
		{
			this.carControls.driveToTarget = true;
			this.carControls.targetIsPlayer = true;
		}
		this.carControls.autoDrive = true;
		AutoHide.AddComponent(this.spawnedObject, this.destroyTime);
		return this.spawnedObject;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00018B88 File Offset: 0x00016D88
	private GameObject SpawnMine()
	{
		this.spawnedObject = UnityEngine.Object.Instantiate<GameObject>(this.minePrefab, base.transform.position, base.transform.rotation);
		this.mineSettings = this.spawnedObject.transform.root.GetComponentInChildren<Mine>();
		AutoHide.AddComponent(this.spawnedObject, this.destroyTime);
		return this.spawnedObject;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00018BF0 File Offset: 0x00016DF0
	private void AddForce(GameObject item, bool enableAngularRotation)
	{
		if (item != null)
		{
			foreach (Rigidbody rigidbody in item.GetComponentsInChildren<Rigidbody>())
			{
				rigidbody.velocity = new Vector3(0f, 0f, 0f);
				rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
				rigidbody.AddRelativeForce(this.localForceDirection * UnityEngine.Random.Range(this.ForceStrengthMinMax.x, this.ForceStrengthMinMax.y), ForceMode.Impulse);
				if (enableAngularRotation)
				{
					rigidbody.angularVelocity = new Vector3(UnityEngine.Random.Range(this.forceRotationVariation * -1f, this.forceRotationVariation), UnityEngine.Random.Range(this.forceRotationVariation * -1f, this.forceRotationVariation), UnityEngine.Random.Range(this.forceRotationVariation * -1f, this.forceRotationVariation));
				}
			}
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00018CDC File Offset: 0x00016EDC
	private void FillPool()
	{
		for (int i = 0; i < this.maxObjectCount; i++)
		{
			switch (this.spawnObject)
			{
			case ObjectSpawner.SpawnObject.Ragdoll:
				this.pooler.Add(this.SpawnRagdoll(), false);
				break;
			case ObjectSpawner.SpawnObject.Mine:
				this.pooler.Add(this.SpawnMine(), false);
				break;
			case ObjectSpawner.SpawnObject.Car:
				this.pooler.Add(this.SpawnCar(), false);
				break;
			}
		}
	}

	// Token: 0x040005E4 RID: 1508
	private Pooler pooler = new Pooler();

	// Token: 0x040005E5 RID: 1509
	private GameObject pooledObject;

	// Token: 0x040005E6 RID: 1510
	[Header("Objects")]
	public ObjectSpawner.SpawnObject spawnObject = ObjectSpawner.SpawnObject.Ragdoll;

	// Token: 0x040005E7 RID: 1511
	public CollisionDetectionMode CollisionDetectionMode;

	// Token: 0x040005E8 RID: 1512
	public GameObject ragdollPrefab;

	// Token: 0x040005E9 RID: 1513
	public GameObject minePrefab;

	// Token: 0x040005EA RID: 1514
	public GameObject carPrefab;

	// Token: 0x040005EB RID: 1515
	[Header("Direction & Force")]
	public bool addForce;

	// Token: 0x040005EC RID: 1516
	public Vector3 localForceDirection = Vector3.forward;

	// Token: 0x040005ED RID: 1517
	public Vector2 ForceStrengthMinMax = new Vector2(5f, 10f);

	// Token: 0x040005EE RID: 1518
	public float forceRotationVariation;

	// Token: 0x040005EF RID: 1519
	[Header("Spawn Settings")]
	public int maxObjectCount = 6;

	// Token: 0x040005F0 RID: 1520
	public float destroyTime = 6f;

	// Token: 0x040005F1 RID: 1521
	public Vector2 spawnRateRangeMinMax = new Vector2(1f, 2f);

	// Token: 0x040005F2 RID: 1522
	[Header("Player Proximity")]
	public bool spawnOnlyWhenPlayerIsClose;

	// Token: 0x040005F3 RID: 1523
	public float startSpawnWhenPlayerInRange = 70f;

	// Token: 0x040005F4 RID: 1524
	public float stopSpawnWhenPlayerInRange = 25f;

	// Token: 0x040005F5 RID: 1525
	private float playerDistance;

	// Token: 0x040005F6 RID: 1526
	[Header("Object Settings")]
	[Header("Car Settings")]
	[Space(10f)]
	public bool driveToPlayer;

	// Token: 0x040005F7 RID: 1527
	[Range(-1f, 1f)]
	public float carSpeedMin;

	// Token: 0x040005F8 RID: 1528
	[Range(-1f, 1f)]
	public float carSpeedMax;

	// Token: 0x040005F9 RID: 1529
	[Range(-1f, 1f)]
	public float carSteerDirectionMin;

	// Token: 0x040005FA RID: 1530
	[Range(-1f, 1f)]
	public float carSteerDirectionMax;

	// Token: 0x040005FB RID: 1531
	private GameObject spawnedObject;

	// Token: 0x040005FC RID: 1532
	private ResetPlacement resetPlacement;

	// Token: 0x040005FD RID: 1533
	private RagdollSettings ragdollSettings;

	// Token: 0x040005FE RID: 1534
	private VehicleCarController carControls;

	// Token: 0x040005FF RID: 1535
	private Mine mineSettings;

	// Token: 0x020000E2 RID: 226
	public enum SpawnObject
	{
		// Token: 0x04000601 RID: 1537
		None,
		// Token: 0x04000602 RID: 1538
		Ragdoll,
		// Token: 0x04000603 RID: 1539
		Mine,
		// Token: 0x04000604 RID: 1540
		Car,
		// Token: 0x04000605 RID: 1541
		Cube
	}
}
