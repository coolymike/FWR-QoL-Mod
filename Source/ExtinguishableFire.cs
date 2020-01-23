using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class ExtinguishableFire : MonoBehaviour
{
	// Token: 0x06000009 RID: 9 RVA: 0x00002156 File Offset: 0x00000356
	private void Start()
	{
		this.m_isExtinguished = true;
		this.smokeParticleSystem.Stop();
		this.fireParticleSystem.Stop();
		base.StartCoroutine(this.StartingFire());
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002182 File Offset: 0x00000382
	public void Extinguish()
	{
		if (this.m_isExtinguished)
		{
			return;
		}
		this.m_isExtinguished = true;
		base.StartCoroutine(this.Extinguishing());
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021A1 File Offset: 0x000003A1
	private IEnumerator Extinguishing()
	{
		this.fireParticleSystem.Stop();
		this.smokeParticleSystem.time = 0f;
		this.smokeParticleSystem.Play();
		for (float elapsedTime = 0f; elapsedTime < 2f; elapsedTime += Time.deltaTime)
		{
			float d = Mathf.Max(0f, 1f - elapsedTime / 2f);
			this.fireParticleSystem.transform.localScale = Vector3.one * d;
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		this.smokeParticleSystem.Stop();
		this.fireParticleSystem.transform.localScale = Vector3.one;
		yield return new WaitForSeconds(4f);
		base.StartCoroutine(this.StartingFire());
		yield break;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021B0 File Offset: 0x000003B0
	private IEnumerator StartingFire()
	{
		this.smokeParticleSystem.Stop();
		this.fireParticleSystem.time = 0f;
		this.fireParticleSystem.Play();
		for (float elapsedTime = 0f; elapsedTime < 2f; elapsedTime += Time.deltaTime)
		{
			float d = Mathf.Min(1f, elapsedTime / 2f);
			this.fireParticleSystem.transform.localScale = Vector3.one * d;
			yield return null;
		}
		this.fireParticleSystem.transform.localScale = Vector3.one;
		this.m_isExtinguished = false;
		yield break;
	}

	// Token: 0x04000005 RID: 5
	public ParticleSystem fireParticleSystem;

	// Token: 0x04000006 RID: 6
	public ParticleSystem smokeParticleSystem;

	// Token: 0x04000007 RID: 7
	protected bool m_isExtinguished;

	// Token: 0x04000008 RID: 8
	private const float m_FireStartingTime = 2f;
}
