using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class GunShoot : MonoBehaviour
{
	// Token: 0x0600001E RID: 30 RVA: 0x0000220B File Offset: 0x0000040B
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
		this.gunAim = base.GetComponentInParent<GunAim>();
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000825C File Offset: 0x0000645C
	private void Update()
	{
		if (Input.GetButtonDown("Fire1") && Time.time > this.nextFire && !this.gunAim.GetIsOutOfBounds())
		{
			this.nextFire = Time.time + this.fireRate;
			this.muzzleFlash.Play();
			this.cartridgeEjection.Play();
			this.anim.SetTrigger("Fire");
			RaycastHit hit;
			if (Physics.Raycast(this.gunEnd.position, this.gunEnd.forward, out hit, this.weaponRange))
			{
				this.HandleHit(hit);
			}
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000082F4 File Offset: 0x000064F4
	private void HandleHit(RaycastHit hit)
	{
		if (hit.collider.sharedMaterial != null)
		{
			string name = hit.collider.sharedMaterial.name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1044434307U)
			{
				if (num <= 329707512U)
				{
					if (num != 81868168U)
					{
						if (num != 329707512U)
						{
							return;
						}
						if (!(name == "WaterFilledExtinguish"))
						{
							return;
						}
						this.SpawnDecal(hit, this.waterLeakExtinguishEffect);
						this.SpawnDecal(hit, this.metalHitEffect);
					}
					else
					{
						if (!(name == "Wood"))
						{
							return;
						}
						this.SpawnDecal(hit, this.woodHitEffect);
						return;
					}
				}
				else if (num != 970575400U)
				{
					if (num != 1044434307U)
					{
						return;
					}
					if (!(name == "Sand"))
					{
						return;
					}
					this.SpawnDecal(hit, this.sandHitEffect);
					return;
				}
				else
				{
					if (!(name == "WaterFilled"))
					{
						return;
					}
					this.SpawnDecal(hit, this.waterLeakEffect);
					this.SpawnDecal(hit, this.metalHitEffect);
					return;
				}
			}
			else if (num <= 2840670588U)
			{
				if (num != 1842662042U)
				{
					if (num != 2840670588U)
					{
						return;
					}
					if (!(name == "Metal"))
					{
						return;
					}
					this.SpawnDecal(hit, this.metalHitEffect);
					return;
				}
				else
				{
					if (!(name == "Stone"))
					{
						return;
					}
					this.SpawnDecal(hit, this.stoneHitEffect);
					return;
				}
			}
			else if (num != 3966976176U)
			{
				if (num != 4022181330U)
				{
					return;
				}
				if (!(name == "Meat"))
				{
					return;
				}
				this.SpawnDecal(hit, this.fleshHitEffects[UnityEngine.Random.Range(0, this.fleshHitEffects.Length)]);
				return;
			}
			else
			{
				if (!(name == "Character"))
				{
					return;
				}
				this.SpawnDecal(hit, this.fleshHitEffects[UnityEngine.Random.Range(0, this.fleshHitEffects.Length)]);
				return;
			}
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002225 File Offset: 0x00000425
	private void SpawnDecal(RaycastHit hit, GameObject prefab)
	{
		UnityEngine.Object.Instantiate<GameObject>(prefab, hit.point, Quaternion.LookRotation(hit.normal)).transform.SetParent(hit.collider.transform);
	}

	// Token: 0x04000017 RID: 23
	public float fireRate = 0.25f;

	// Token: 0x04000018 RID: 24
	public float weaponRange = 20f;

	// Token: 0x04000019 RID: 25
	public Transform gunEnd;

	// Token: 0x0400001A RID: 26
	public ParticleSystem muzzleFlash;

	// Token: 0x0400001B RID: 27
	public ParticleSystem cartridgeEjection;

	// Token: 0x0400001C RID: 28
	public GameObject metalHitEffect;

	// Token: 0x0400001D RID: 29
	public GameObject sandHitEffect;

	// Token: 0x0400001E RID: 30
	public GameObject stoneHitEffect;

	// Token: 0x0400001F RID: 31
	public GameObject waterLeakEffect;

	// Token: 0x04000020 RID: 32
	public GameObject waterLeakExtinguishEffect;

	// Token: 0x04000021 RID: 33
	public GameObject[] fleshHitEffects;

	// Token: 0x04000022 RID: 34
	public GameObject woodHitEffect;

	// Token: 0x04000023 RID: 35
	private float nextFire;

	// Token: 0x04000024 RID: 36
	private Animator anim;

	// Token: 0x04000025 RID: 37
	private GunAim gunAim;
}
