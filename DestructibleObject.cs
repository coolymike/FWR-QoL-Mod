using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class DestructibleObject : MonoBehaviour
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000426 RID: 1062 RVA: 0x000057C9 File Offset: 0x000039C9
	// (set) Token: 0x06000427 RID: 1063 RVA: 0x000057D1 File Offset: 0x000039D1
	public bool Fracture
	{
		get
		{
			return this.fracture;
		}
		set
		{
			if (value == this.fracture)
			{
				return;
			}
			this.FractureItem(value);
			this.fracture = value;
		}
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00017A78 File Offset: 0x00015C78
	private void Awake()
	{
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
		this.rigidBodyFracturePieces = this.fractureObject.GetComponentsInChildren<Rigidbody>();
		this.colliderFracturePieces = this.fractureObject.GetComponentsInChildren<Collider>();
		this.SaveFracturePositions();
		if (this.spawnVFX)
		{
			this.spawnedVFXEffect = UnityEngine.Object.Instantiate<GameObject>(this.vfxPrefab, base.transform.position, base.transform.rotation, base.transform.root);
			this.spawnedVFXEffect.SetActive(false);
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x000057EB File Offset: 0x000039EB
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00017B04 File Offset: 0x00015D04
	private void Start()
	{
		this.solidObject.SetActive(true);
		this.fractureObject.SetActive(false);
		this.SetFractureKinematics(true);
		if (!this.collideWithController)
		{
			for (int i = 0; i < this.rigidBodyFracturePieces.Length; i++)
			{
				Physics.IgnoreCollision(this.colliderFracturePieces[i], PlayerSettings.instance.playerController.GetComponent<Collider>());
			}
		}
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00017B68 File Offset: 0x00015D68
	private void FixedUpdate()
	{
		if (this.fracture && this.fractureObject.activeInHierarchy)
		{
			for (int i = 0; i < this.rigidBodyFracturePieces.Length; i++)
			{
				if (this.rigidBodyFracturePieces[i].IsSleeping())
				{
					this.rigidBodyFracturePieces[i].WakeUp();
				}
			}
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x000057FE File Offset: 0x000039FE
	private void OnTriggerEnter(Collider collider)
	{
		this.TriggerCheck(collider.transform);
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x0000580C File Offset: 0x00003A0C
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (buildModeOn)
		{
			this.Fracture = false;
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00017BBC File Offset: 0x00015DBC
	private void TriggerCheck(Transform colliderTransform)
	{
		if (this.Fracture || LevelManager.BuildModeOn)
		{
			return;
		}
		if (this.playerCanFracture && Tag.Compare(colliderTransform, Tag.Tags.Player))
		{
			this.Fracture = true;
			this.playerCollided = colliderTransform.root.GetComponent<PlayerSettings>();
			if (this.playerCollided != null)
			{
				CameraController.TriggerCameraTremor(this.playerCollided.mainCamera);
			}
		}
		if (this.smartRagdollsCanFracture && Tag.Compare(colliderTransform, Tag.Tags.Ragdoll))
		{
			AntiTouchSystem.KillSmartRagdoll(colliderTransform);
			this.Fracture = true;
		}
		if (this.creatureCanFracture && Tag.Compare(colliderTransform, Tag.Tags.Creature))
		{
			this.Fracture = true;
		}
		if (this.carCanFracture && Tag.Compare(colliderTransform, Tag.Tags.Vehicle))
		{
			this.Fracture = true;
		}
		if (Tag.Compare(colliderTransform, Tag.Tags.Domino))
		{
			this.Fracture = true;
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00017C80 File Offset: 0x00015E80
	private void FractureItem(bool fractureItem)
	{
		this.solidObject.SetActive(!fractureItem);
		this.fractureObject.SetActive(fractureItem);
		this.SetFractureKinematics(!fractureItem);
		this.EnableTriggers(!fractureItem);
		if (this.spawnVFX)
		{
			this.spawnedVFXEffect.SetActive(fractureItem);
		}
		if (fractureItem)
		{
			base.Invoke("BeginHide", this.hideFracturedAfter);
			if (this.audioSource != null && this.audioClips.Length != 0)
			{
				this.audioSource.clip = this.audioClips[UnityEngine.Random.Range(0, this.audioClips.Length)];
				this.audioSource.pitch = UnityEngine.Random.Range(this.randomPitchRange.x, this.randomPitchRange.y);
				this.audioSource.Play();
				return;
			}
		}
		else
		{
			this.LoadFracturePositions();
			this.SetFractureColliderToTrigger(false);
			base.CancelInvoke("BeginHide");
			base.CancelInvoke("Hide");
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00017D70 File Offset: 0x00015F70
	private void SaveFracturePositions()
	{
		for (int i = 0; i < this.rigidBodyFracturePieces.Length; i++)
		{
			this.fracturePositions.Add(this.rigidBodyFracturePieces[i].transform.GetInstanceID(), this.rigidBodyFracturePieces[i].transform.position);
			this.fractureRotations.Add(this.rigidBodyFracturePieces[i].transform.GetInstanceID(), this.rigidBodyFracturePieces[i].transform.rotation);
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00017DF0 File Offset: 0x00015FF0
	private void LoadFracturePositions()
	{
		for (int i = 0; i < this.rigidBodyFracturePieces.Length; i++)
		{
			this.rigidBodyFracturePieces[i].transform.position = this.fracturePositions[this.rigidBodyFracturePieces[i].transform.GetInstanceID()];
			this.rigidBodyFracturePieces[i].transform.rotation = this.fractureRotations[this.rigidBodyFracturePieces[i].transform.GetInstanceID()];
		}
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00017E70 File Offset: 0x00016070
	private void SetFractureKinematics(bool enable)
	{
		for (int i = 0; i < this.rigidBodyFracturePieces.Length; i++)
		{
			this.rigidBodyFracturePieces[i].isKinematic = enable;
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00017EA0 File Offset: 0x000160A0
	private void SetFractureColliderToTrigger(bool enable)
	{
		for (int i = 0; i < this.colliderFracturePieces.Length; i++)
		{
			this.colliderFracturePieces[i].isTrigger = enable;
		}
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00017ED0 File Offset: 0x000160D0
	private void EnableTriggers(bool enable)
	{
		for (int i = 0; i < this.triggers.Length; i++)
		{
			this.triggers[i].enabled = enable;
		}
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00005818 File Offset: 0x00003A18
	private void BeginHide()
	{
		if (!DestructibleObject.LastsInfinite)
		{
			base.Invoke("Hide", 3f);
			this.SetFractureColliderToTrigger(true);
		}
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00005838 File Offset: 0x00003A38
	private void Hide()
	{
		if (!DestructibleObject.LastsInfinite)
		{
			this.fractureObject.SetActive(false);
		}
	}

	// Token: 0x04000594 RID: 1428
	public GameObject solidObject;

	// Token: 0x04000595 RID: 1429
	public GameObject fractureObject;

	// Token: 0x04000596 RID: 1430
	public Collider[] triggers;

	// Token: 0x04000597 RID: 1431
	public bool spawnVFX;

	// Token: 0x04000598 RID: 1432
	public GameObject vfxPrefab;

	// Token: 0x04000599 RID: 1433
	private GameObject spawnedVFXEffect;

	// Token: 0x0400059A RID: 1434
	[Header("What Can Fracture")]
	private PlayerSettings playerCollided;

	// Token: 0x0400059B RID: 1435
	public bool playerCanFracture = true;

	// Token: 0x0400059C RID: 1436
	public bool carCanFracture = true;

	// Token: 0x0400059D RID: 1437
	public bool creatureCanFracture = true;

	// Token: 0x0400059E RID: 1438
	public bool smartRagdollsCanFracture;

	// Token: 0x0400059F RID: 1439
	[Header("Fracture State")]
	public bool collideWithController;

	// Token: 0x040005A0 RID: 1440
	private bool fracture;

	// Token: 0x040005A1 RID: 1441
	[Header("Hiding")]
	public float hideFracturedAfter;

	// Token: 0x040005A2 RID: 1442
	[Header("Audio")]
	public AudioSource audioSource;

	// Token: 0x040005A3 RID: 1443
	public Vector2 randomPitchRange = new Vector2(0.8f, 1.2f);

	// Token: 0x040005A4 RID: 1444
	public AudioClip[] audioClips;

	// Token: 0x040005A5 RID: 1445
	private Rigidbody[] rigidBodyFracturePieces;

	// Token: 0x040005A6 RID: 1446
	private Collider[] colliderFracturePieces;

	// Token: 0x040005A7 RID: 1447
	private Dictionary<int, Vector3> fracturePositions = new Dictionary<int, Vector3>();

	// Token: 0x040005A8 RID: 1448
	private Dictionary<int, Quaternion> fractureRotations = new Dictionary<int, Quaternion>();

	// Token: 0x040005A9 RID: 1449
	public static bool LastsInfinite;
}
