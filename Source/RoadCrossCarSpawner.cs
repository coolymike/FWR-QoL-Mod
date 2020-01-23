using System;
using System.Collections;
using System.Collections.Generic;
using ExtendedEssentials;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class RoadCrossCarSpawner : MonoBehaviour
{
	// Token: 0x06000415 RID: 1045 RVA: 0x00017B28 File Offset: 0x00015D28
	private void Start()
	{
		this.SetDifficultySettings();
		if (this.spawnArray)
		{
			for (int i = 1; i < this.spawnLanes; i++)
			{
				this.spawnedSpawner = UnityEngine.Object.Instantiate<Transform>(base.transform, new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + 30f * (float)i), base.transform.rotation);
				this.spawnedSpawner.GetComponent<RoadCrossCarSpawner>().spawnArray = false;
			}
		}
		this.CreatePool();
		base.InvokeRepeating("SpawnNextCar", UnityEngine.Random.Range(0f, 4f), this.maxSpawnTime);
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00017BE8 File Offset: 0x00015DE8
	private void Update()
	{
		for (int i = 0; i < this.objectsInTrigger.Count; i++)
		{
			if (!this.objectsInTrigger[i].activeInHierarchy)
			{
				this.objectsInTrigger.Remove(this.objectsInTrigger[i]);
			}
		}
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00017C38 File Offset: 0x00015E38
	private void CreatePool()
	{
		for (int i = 0; i < this.maxCars; i++)
		{
			this.car = UnityEngine.Object.Instantiate<GameObject>(this.carPrefab, base.transform.position, Quaternion.Euler(base.transform.eulerAngles.x, base.transform.eulerAngles.y + UnityEngine.Random.Range(-5f, 5f), base.transform.eulerAngles.z));
			this.pool.Add(this.car);
			AutoHide.AddComponent(this.car, (float)this.timeAlive);
			this.carControls = this.car.GetComponentInChildren<VehicleCarController>();
			this.carControls.autoDrive = true;
			this.carControls.autoMotor = this.vehicleSpeed;
			this.car.SetActive(false);
		}
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0000567A File Offset: 0x0000387A
	private IEnumerator SpawnNextCarRoutine()
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.maxSpawnTime);
			if (this.objectsInTrigger.Count == 0)
			{
				this.pool.ActivateNext();
			}
		}
		yield break;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00005689 File Offset: 0x00003889
	private void SpawnNextCar()
	{
		if (this.objectsInTrigger.Count == 0)
		{
			this.pool.ActivateNext();
		}
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x000056A4 File Offset: 0x000038A4
	private void SetDifficultySettings()
	{
		this.maxSpawnTime = 1.03f;
		this.vehicleSpeed = 0.45f;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x000056BC File Offset: 0x000038BC
	private void OnTriggerEnter(Collider other)
	{
		if (!this.objectsInTrigger.Contains(other.transform.root.gameObject))
		{
			this.objectsInTrigger.Add(other.transform.root.gameObject);
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x000056F6 File Offset: 0x000038F6
	private void OnTriggerExit(Collider other)
	{
		if (this.objectsInTrigger.Contains(other.transform.root.gameObject))
		{
			this.objectsInTrigger.Remove(other.transform.root.gameObject);
		}
	}

	// Token: 0x04000588 RID: 1416
	public GameObject carPrefab;

	// Token: 0x04000589 RID: 1417
	private Vector3 vehicleStartPosition;

	// Token: 0x0400058A RID: 1418
	private float vehicleSpeed;

	// Token: 0x0400058B RID: 1419
	private float spawnTimer;

	// Token: 0x0400058C RID: 1420
	private float maxSpawnTime = 1.7f;

	// Token: 0x0400058D RID: 1421
	[Header("Lanes")]
	public bool spawnArray;

	// Token: 0x0400058E RID: 1422
	public int spawnLanes = 5;

	// Token: 0x0400058F RID: 1423
	private Transform spawnedSpawner;

	// Token: 0x04000590 RID: 1424
	[Header("Pool")]
	public int maxCars = 2;

	// Token: 0x04000591 RID: 1425
	public int timeAlive = 10;

	// Token: 0x04000592 RID: 1426
	private Pooler pool = new Pooler();

	// Token: 0x04000593 RID: 1427
	private GameObject car;

	// Token: 0x04000594 RID: 1428
	private VehicleCarController carControls;

	// Token: 0x04000595 RID: 1429
	public List<GameObject> objectsInTrigger = new List<GameObject>();
}
