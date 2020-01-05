using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class Mine : MonoBehaviour
{
	// Token: 0x06000223 RID: 547 RVA: 0x00010A04 File Offset: 0x0000EC04
	private void Start()
	{
		this.spawnedExplosion = UnityEngine.Object.Instantiate<GameObject>(this.explosionEffect, base.transform.position, base.transform.rotation);
		this.spawnedExplosion.SetActive(false);
		this.meshRenderers = base.GetComponentsInChildren<MeshRenderer>();
		this.Reset();
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00003EF3 File Offset: 0x000020F3
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00010A68 File Offset: 0x0000EC68
	private void FixedUpdate()
	{
		if (this.remoteDetonate && !this.bombTimerActivated)
		{
			base.StartCoroutine(this.BombTimer());
		}
		if (this.meshRenderers != null)
		{
			MeshRenderer[] array = this.meshRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = !this.detonated;
			}
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00003F06 File Offset: 0x00002106
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (buildModeOn)
		{
			this.Reset();
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00003F11 File Offset: 0x00002111
	private void OnDisable()
	{
		this.Reset();
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00003F19 File Offset: 0x00002119
	private IEnumerator BombTimer()
	{
		this.bombTimerActivated = true;
		yield return new WaitForSeconds(0.4f);
		if (this.bombTimerActivated)
		{
			this.Detonate();
		}
		yield break;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00010AC0 File Offset: 0x0000ECC0
	public void Reset()
	{
		this.detonated = false;
		this.bombTimerActivated = false;
		this.remoteDetonate = false;
		if (this.spawnedExplosion != null && this.spawnedExplosion.activeInHierarchy)
		{
			this.spawnedExplosion.SetActive(false);
		}
		this.mineObject.SetActive(true);
	}

	// Token: 0x0600022A RID: 554 RVA: 0x00003F28 File Offset: 0x00002128
	public void CollisionDetected(Collision collision)
	{
		if (this.detonateOnTouchAnything)
		{
			this.Detonate();
			return;
		}
		if (Tag.Compare(collision.transform, new Tag.Tags[]
		{
			Tag.Tags.Player,
			Tag.Tags.Ragdoll,
			Tag.Tags.Domino,
			Tag.Tags.Vehicle
		}))
		{
			this.Detonate();
		}
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00010B18 File Offset: 0x0000ED18
	private void Detonate()
	{
		if (this.detonated || LevelManager.BuildModeOn)
		{
			return;
		}
		if (UIVersion.NukeEnabled)
		{
			this.colliders = Physics.OverlapSphere(this.mineObject.transform.position, 1000f);
		}
		else
		{
			this.colliders = Physics.OverlapSphere(this.mineObject.transform.position, this.radius);
		}
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
				if (UIVersion.NukeEnabled)
				{
					component.AddExplosionForce(5000f * ((component.mass < 10f) ? 1f : (component.mass * 0.1f)), this.mineObject.transform.position, 1000f, 0f, ForceMode.Impulse);
				}
				else
				{
					component.AddExplosionForce(this.power * ((component.mass < 10f) ? 1f : (component.mass * 0.1f)), this.mineObject.transform.position, this.radius, 0f, ForceMode.Impulse);
				}
				component.AddTorque(Vector3.Normalize(base.transform.position - component.position));
			}
			this.anotherMine = this.colliders[i].transform.GetComponentInParent<Mine>();
			if (this.anotherMine != null)
			{
				this.anotherMine.remoteDetonate = true;
			}
		}
		this.spawnedExplosion.transform.position = this.mineObject.transform.position;
		this.spawnedExplosion.SetActive(true);
		this.mineObject.SetActive(false);
		this.detonated = true;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00003F62 File Offset: 0x00002162
	private void FractureObjects(Transform thing)
	{
		this.destructibleObject = thing.root.GetComponentInChildren<DestructibleObject>();
		if (this.destructibleObject != null)
		{
			this.destructibleObject.Fracture = true;
		}
	}

	// Token: 0x04000322 RID: 802
	public GameObject explosionEffect;

	// Token: 0x04000323 RID: 803
	private GameObject spawnedExplosion;

	// Token: 0x04000324 RID: 804
	public GameObject mineObject;

	// Token: 0x04000325 RID: 805
	private MeshRenderer[] meshRenderers;

	// Token: 0x04000326 RID: 806
	public bool detonateOnTouchAnything;

	// Token: 0x04000327 RID: 807
	public bool dismemberRagdolls;

	// Token: 0x04000328 RID: 808
	public float radius = 10f;

	// Token: 0x04000329 RID: 809
	public float power = 50f;

	// Token: 0x0400032A RID: 810
	public bool remoteDetonate;

	// Token: 0x0400032B RID: 811
	private bool detonated;

	// Token: 0x0400032C RID: 812
	private bool bombTimerActivated;

	// Token: 0x0400032D RID: 813
	private Collider[] colliders;

	// Token: 0x0400032E RID: 814
	private Mine anotherMine;

	// Token: 0x0400032F RID: 815
	private DestructibleObject destructibleObject;
}
