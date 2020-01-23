using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class Missile : MonoBehaviour
{
	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600023C RID: 572 RVA: 0x0000400A File Offset: 0x0000220A
	// (set) Token: 0x0600023D RID: 573 RVA: 0x00004012 File Offset: 0x00002212
	public bool Explode
	{
		get
		{
			return this.explode;
		}
		set
		{
			if (value == this.explode)
			{
				return;
			}
			this.explode = value;
			this.TriggerExplosion(value);
		}
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00010E60 File Offset: 0x0000F060
	private void Start()
	{
		this.target = PlayerSettings.instance.simulatedRagdoll.bodyElements.ragdollJoints[2].transform;
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
		this.startPosition = base.transform.position;
		this.startRotation = base.transform.rotation;
		this.spawnedVfx = UnityEngine.Object.Instantiate<GameObject>(this.vfxAsset);
		this.spawnedVfx.SetActive(false);
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000402C File Offset: 0x0000222C
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000403F File Offset: 0x0000223F
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (buildModeOn)
		{
			this.Reset();
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000404A File Offset: 0x0000224A
	private void FixedUpdate()
	{
		this.Move();
	}

	// Token: 0x06000242 RID: 578 RVA: 0x00010EE0 File Offset: 0x0000F0E0
	private void Move()
	{
		if (this.Explode)
		{
			this.playedLaunchSound = false;
			this.audioSourceMissileFlying.volume = 0f;
			this.audioSourceMissileLaunch.volume = 0f;
			return;
		}
		if (LevelManager.BuildModeOn)
		{
			this.playedLaunchSound = false;
			this.audioSourceMissileFlying.volume = 0f;
			this.audioSourceMissileLaunch.volume = 0f;
			return;
		}
		if (!this.playedLaunchSound)
		{
			this.audioSourceMissileLaunch.clip = this.missileLaunchClips[UnityEngine.Random.Range(0, this.missileLaunchClips.Length)];
			this.audioSourceMissileLaunch.Play();
			this.playedLaunchSound = true;
		}
		this.audioSourceMissileFlying.volume = 1f;
		this.audioSourceMissileLaunch.volume = 1f;
		base.transform.Translate(new Vector3(0f, 0f, 1f) * this.speed, Space.Self);
		if (this.isTracking)
		{
			this.targetDirection = Vector3.Normalize(this.target.position - base.transform.position);
			this.targetLookRotation = Quaternion.LookRotation(this.targetDirection);
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, this.targetLookRotation, this.turnStep * Time.fixedDeltaTime);
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00004052 File Offset: 0x00002252
	private void OnTriggerEnter(Collider other)
	{
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		if (other.GetComponentInParent<Missile>() == null && other.isTrigger)
		{
			return;
		}
		this.Explode = true;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00011040 File Offset: 0x0000F240
	private void TriggerExplosion(bool setForExplode)
	{
		this.missileCollider.enabled = !setForExplode;
		this.missileObject.SetActive(!setForExplode);
		if (setForExplode)
		{
			this.spawnedVfx.transform.position = base.transform.position;
			this.spawnedVfx.transform.rotation = base.transform.rotation;
			this.spawnedVfx.transform.localEulerAngles = this.spawnedVfx.transform.localEulerAngles + new Vector3(90f, 0f, 0f);
			this.AddForces();
		}
		this.spawnedVfx.SetActive(setForExplode);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x000110F0 File Offset: 0x0000F2F0
	private void AddForces()
	{
		this.colliders = Physics.OverlapSphere(base.transform.position, this.explosionRadius);
		for (int i = 0; i < this.colliders.Length; i++)
		{
			AntiTouchSystem.KillPlayer(this.colliders[i].transform.root.GetComponent<PlayerSettings>());
			AntiTouchSystem.KillSmartRagdoll(this.colliders[i].transform);
			this.FractureObjects(this.colliders[i].transform);
			Rigidbody component = this.colliders[i].GetComponent<Rigidbody>();
			if (component != null)
			{
				if (this.dismemberRagdolls && UnityEngine.Random.Range(0, 4) == 0)
				{
					RagdollSettings.DismemberJoint(component.transform);
				}
				component.AddExplosionForce(this.explosionPower, base.transform.position, this.explosionRadius, 0f, ForceMode.Impulse);
				component.AddTorque(Vector3.Normalize(base.transform.position - component.position));
			}
			this.anotherMine = this.colliders[i].transform.GetComponentInParent<Mine>();
			if (this.anotherMine != null)
			{
				this.anotherMine.remoteDetonate = true;
			}
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000407A File Offset: 0x0000227A
	private void FractureObjects(Transform thing)
	{
		this.destructibleObject = thing.root.GetComponentInChildren<DestructibleObject>();
		if (this.destructibleObject != null)
		{
			this.destructibleObject.Fracture = true;
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x000040A7 File Offset: 0x000022A7
	private void Reset()
	{
		this.Explode = false;
		base.transform.position = this.startPosition;
		base.transform.rotation = this.startRotation;
	}

	// Token: 0x04000339 RID: 825
	public Transform target;

	// Token: 0x0400033A RID: 826
	private bool explode;

	// Token: 0x0400033B RID: 827
	public bool isTracking;

	// Token: 0x0400033C RID: 828
	private Vector3 targetDirection;

	// Token: 0x0400033D RID: 829
	private Quaternion targetLookRotation;

	// Token: 0x0400033E RID: 830
	[Space]
	public bool dismemberRagdolls;

	// Token: 0x0400033F RID: 831
	public float explosionPower = 40f;

	// Token: 0x04000340 RID: 832
	public float explosionRadius = 8f;

	// Token: 0x04000341 RID: 833
	public float speed = 0.4f;

	// Token: 0x04000342 RID: 834
	public float turnStep = 150f;

	// Token: 0x04000343 RID: 835
	[Space]
	public GameObject vfxAsset;

	// Token: 0x04000344 RID: 836
	private GameObject spawnedVfx;

	// Token: 0x04000345 RID: 837
	public BoxCollider missileCollider;

	// Token: 0x04000346 RID: 838
	public GameObject missileObject;

	// Token: 0x04000347 RID: 839
	private Vector3 startPosition;

	// Token: 0x04000348 RID: 840
	private Quaternion startRotation;

	// Token: 0x04000349 RID: 841
	[Header("Audio")]
	public AudioSource audioSourceMissileFlying;

	// Token: 0x0400034A RID: 842
	public AudioSource audioSourceMissileLaunch;

	// Token: 0x0400034B RID: 843
	public AudioClip[] missileLaunchClips;

	// Token: 0x0400034C RID: 844
	private bool playedLaunchSound;

	// Token: 0x0400034D RID: 845
	private Collider[] colliders;

	// Token: 0x0400034E RID: 846
	private Mine anotherMine;

	// Token: 0x0400034F RID: 847
	private DestructibleObject destructibleObject;
}
