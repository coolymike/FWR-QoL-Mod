using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class CanonShoot : MonoBehaviour
{
	// Token: 0x060001CD RID: 461 RVA: 0x00003B2E File Offset: 0x00001D2E
	private void Start()
	{
		this.vehicle = base.GetComponent<Vehicle>();
		this.SpawnLaser();
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000EF0C File Offset: 0x0000D10C
	private void Update()
	{
		if (this.shotObjects.Count > this.maxObjectsInScene)
		{
			UnityEngine.Object.Destroy(this.shotObjects[0]);
			this.shotObjects.RemoveAt(0);
		}
		if (LevelManager.BuildModeOn)
		{
			this.DestroyAllShotObjects();
		}
		if (this.vehicle != null && this.vehicle.playerControlling != null)
		{
			this.PlayerShoot();
			return;
		}
		if (!LevelManager.BuildModeOn)
		{
			this.AutoShoot();
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000EF8C File Offset: 0x0000D18C
	private void FixedUpdate()
	{
		this.BarrelInside = this.canonBarrel.position + this.canonBarrel.forward * 0f;
		this.BarrelEnd = this.canonBarrel.position + this.canonBarrel.forward * 2f;
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
	private void DestroyAllShotObjects()
	{
		for (int i = 0; i < this.shotObjects.Count; i++)
		{
			UnityEngine.Object.Destroy(this.shotObjects[i]);
		}
		this.TurnLaserOff();
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000F02C File Offset: 0x0000D22C
	private void PlayerShoot()
	{
		if (InputManager.VehicleA() && !this.isShooting)
		{
			base.StartCoroutine(this.Shoot(this.playerShootCooldown));
			if (this.shootObject == CanonShoot.ShootObject.Laser)
			{
				return;
			}
			if (this.vehicle.playerControlling == null)
			{
				return;
			}
			if (this.vehicle.playerControlling.mainCamera == null)
			{
				return;
			}
			CameraController.TriggerCameraJumpShake(this.vehicle.playerControlling.mainCamera, 3f);
		}
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00003B42 File Offset: 0x00001D42
	private void OnDisable()
	{
		this.isShooting = false;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00003B4B File Offset: 0x00001D4B
	private void AutoShoot()
	{
		if (!this.autoShoot)
		{
			return;
		}
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		if (this.isShooting)
		{
			return;
		}
		if (this.shootObject == CanonShoot.ShootObject.Player)
		{
			return;
		}
		base.StartCoroutine(this.Shoot(this.autoShootCooldown));
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00003B84 File Offset: 0x00001D84
	private IEnumerator Shoot(float _timeDelay)
	{
		this.isShooting = true;
		switch (this.shootObject)
		{
		case CanonShoot.ShootObject.Player:
			this.ShootPlayer();
			this.PlaySound();
			break;
		case CanonShoot.ShootObject.Ragdoll:
			this.ShootRagdoll();
			this.PlaySound();
			break;
		case CanonShoot.ShootObject.Cube:
			this.ShootCube();
			this.PlaySound();
			break;
		case CanonShoot.ShootObject.Mine:
			this.ShootMine();
			this.PlaySound();
			break;
		case CanonShoot.ShootObject.Laser:
			this.TurnLaserOn();
			if (!CanonShoot.LaserPermanentOn)
			{
				yield return new WaitForSeconds(_timeDelay * 0.5f);
				this.TurnLaserOff();
			}
			break;
		}
		yield return new WaitForSeconds(_timeDelay);
		this.isShooting = false;
		yield break;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00003B9A File Offset: 0x00001D9A
	private void PlaySound()
	{
		this.audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		this.audioSource.Play();
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000F0AC File Offset: 0x0000D2AC
	private void ShootRagdoll()
	{
		this.shotObject = UnityEngine.Object.Instantiate<GameObject>(this.ragdoll, this.BarrelInside, this.canonBarrel.rotation);
		this.shotObject.transform.rotation = Quaternion.LookRotation(this.shotObject.transform.up, this.canonBarrel.forward);
		this.shotObjects.Add(this.shotObject);
		if (this.destroyShotObjects)
		{
			UnityEngine.Object.Destroy(this.shotObject, this.destroyTime);
		}
		this.ragdollSettings = this.shotObject.GetComponent<RagdollSettings>();
		this.ragdollSettings.CollisionDetectionMode = CollisionDetectionMode.Discrete;
		this.objectRigidbodies = this.shotObject.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < this.objectRigidbodies.Length; i++)
		{
			this.objectRigidbodies[i].AddRelativeForce(new Vector3(0f, 0f, 1f) * this.shootStrength, ForceMode.Impulse);
			this.objectRigidbodies[i].AddTorque(new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)) * 10f);
		}
		this.antiTouchObject = this.shotObject.AddComponent<AntiTouchObject>();
		this.antiTouchObject.distributionMethod = AntiTouchObject.ComponentDistributionMethod.ApplyToChildren;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000F20C File Offset: 0x0000D40C
	private void ShootPlayer()
	{
		RagdollSettings simulatedRagdoll = this.vehicle.playerControlling.simulatedRagdoll;
		Transform transform = simulatedRagdoll.bodyElements.ragdollJoints[0].transform;
		PlayerCameraController playerCameraController = this.vehicle.playerControlling.playerCameraController;
		simulatedRagdoll.RagdollModeEnabled = true;
		simulatedRagdoll.ResetRagdoll();
		transform.eulerAngles = new Vector3(this.canonBarrel.eulerAngles.x + 90f, this.canonBarrel.eulerAngles.y, this.canonBarrel.eulerAngles.z);
		transform.position = this.BarrelEnd;
		for (int i = 0; i < simulatedRagdoll.rigidbodyChildren.Length; i++)
		{
			simulatedRagdoll.rigidbodyChildren[i].AddForce(this.canonBarrel.forward * this.shootStrength, ForceMode.Impulse);
		}
		playerCameraController.ResetLookRotation(new Vector3(0f, transform.eulerAngles.y, 0f));
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000F300 File Offset: 0x0000D500
	private void ShootCube()
	{
		this.shotObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		this.shotObject.transform.position = this.BarrelEnd;
		this.shotObject.transform.rotation = this.canonBarrel.rotation;
		if (this.destroyShotObjects)
		{
			UnityEngine.Object.Destroy(this.shotObject, this.destroyTime);
		}
		this.shotObjects.Add(this.shotObject);
		this.shotObject.GetComponent<Renderer>().material = this.defaultMaterial;
		this.shotObject.AddComponent<Rigidbody>().AddRelativeForce(new Vector3(0f, 0f, 1f) * this.shootStrength, ForceMode.Impulse);
		Physics.IgnoreCollision(this.shotObject.GetComponent<Collider>(), PlayerSettings.instance.playerController.GetComponent<Collider>());
		AntiTouchSystem.AddAntiTouchComponent(this.shotObject, false);
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
	private void ShootMine()
	{
		this.shotObject = (this.shotObject = UnityEngine.Object.Instantiate<GameObject>(this.mine, this.BarrelInside, this.canonBarrel.rotation));
		this.shotObject.transform.position = this.BarrelEnd;
		this.shotObject.transform.rotation = this.canonBarrel.rotation;
		this.shotObject.transform.localEulerAngles = new Vector3(this.shotObject.transform.localEulerAngles.x + 90f, this.shotObject.transform.localEulerAngles.y, this.shotObject.transform.localEulerAngles.z);
		if (this.destroyShotObjects)
		{
			UnityEngine.Object.Destroy(this.shotObject, this.destroyTime);
		}
		this.shotObjects.Add(this.shotObject);
		this.rigidbody = this.shotObject.GetComponentInChildren<Rigidbody>();
		if (this.rigidbody != null)
		{
			this.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			this.rigidbody.AddForce(this.shotObject.transform.up * this.shootStrength, ForceMode.Impulse);
		}
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000F528 File Offset: 0x0000D728
	private void SpawnLaser()
	{
		if (this.spawnedLaser != null)
		{
			UnityEngine.Object.Destroy(this.spawnedLaser);
		}
		this.spawnedLaser = UnityEngine.Object.Instantiate<GameObject>(this.laser, this.canonBarrel);
		this.spawnedLaser.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.spawnedLaser.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		this.laserSettings = this.spawnedLaser.GetComponent<Laser>();
		this.TurnLaserOff();
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000F5C4 File Offset: 0x0000D7C4
	private void UpdateLaserPosition()
	{
		if (this.spawnedLaser != null && this.shootObject == CanonShoot.ShootObject.Laser)
		{
			this.spawnedLaser.transform.position = this.BarrelInside;
			this.spawnedLaser.transform.rotation = this.canonBarrel.rotation;
		}
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00003BC1 File Offset: 0x00001DC1
	private void TurnLaserOn()
	{
		if (this.laserSettings != null)
		{
			this.laserSettings.laserOn = true;
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00003BDD File Offset: 0x00001DDD
	private void TurnLaserOff()
	{
		if (this.laserSettings != null)
		{
			this.laserSettings.laserOn = false;
		}
	}

	// Token: 0x0400029B RID: 667
	private Vehicle vehicle;

	// Token: 0x0400029C RID: 668
	public CameraShake cameraShake;

	// Token: 0x0400029D RID: 669
	[Header("Objects")]
	public Transform canonBarrel;

	// Token: 0x0400029E RID: 670
	private GameObject shotObject;

	// Token: 0x0400029F RID: 671
	private List<GameObject> shotObjects = new List<GameObject>();

	// Token: 0x040002A0 RID: 672
	[Header("Ammo")]
	public CanonShoot.ShootObject shootObject = CanonShoot.ShootObject.Ragdoll;

	// Token: 0x040002A1 RID: 673
	public GameObject ragdoll;

	// Token: 0x040002A2 RID: 674
	public GameObject mine;

	// Token: 0x040002A3 RID: 675
	public GameObject laser;

	// Token: 0x040002A4 RID: 676
	private GameObject spawnedLaser;

	// Token: 0x040002A5 RID: 677
	private Laser laserSettings;

	// Token: 0x040002A6 RID: 678
	public bool laserAlwaysOn;

	// Token: 0x040002A7 RID: 679
	[Header("Shoot Status")]
	public bool isShooting;

	// Token: 0x040002A8 RID: 680
	public bool autoShoot;

	// Token: 0x040002A9 RID: 681
	public float shootStrength = 25f;

	// Token: 0x040002AA RID: 682
	[Header("Shoot Timer")]
	public float playerShootCooldown = 0.5f;

	// Token: 0x040002AB RID: 683
	public float autoShootCooldown = 1f;

	// Token: 0x040002AC RID: 684
	[Header("Destroy Settings")]
	public bool destroyShotObjects;

	// Token: 0x040002AD RID: 685
	public float destroyTime = 4f;

	// Token: 0x040002AE RID: 686
	[Header("Object Settings")]
	public Material defaultMaterial;

	// Token: 0x040002AF RID: 687
	public int maxObjectsInScene = 6;

	// Token: 0x040002B0 RID: 688
	private Vector3 BarrelEnd;

	// Token: 0x040002B1 RID: 689
	private Vector3 BarrelInside;

	// Token: 0x040002B2 RID: 690
	public AudioSource audioSource;

	// Token: 0x040002B3 RID: 691
	private RagdollSettings ragdollSettings;

	// Token: 0x040002B4 RID: 692
	private Rigidbody[] objectRigidbodies;

	// Token: 0x040002B5 RID: 693
	private AntiTouchObject antiTouchObject;

	// Token: 0x040002B6 RID: 694
	private Rigidbody rigidbody;

	// Token: 0x040002B7 RID: 695
	public static bool LaserPermanentOn;

	// Token: 0x0200005C RID: 92
	public enum ShootObject
	{
		// Token: 0x040002B9 RID: 697
		Player = 1,
		// Token: 0x040002BA RID: 698
		Ragdoll,
		// Token: 0x040002BB RID: 699
		Cube,
		// Token: 0x040002BC RID: 700
		Mine,
		// Token: 0x040002BD RID: 701
		Laser
	}
}
