using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class CarAudio : MonoBehaviour
{
	// Token: 0x06000196 RID: 406 RVA: 0x00003794 File Offset: 0x00001994
	private void FixedUpdate()
	{
		this.EngineAudio();
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000E398 File Offset: 0x0000C598
	private void EngineAudio()
	{
		if (this.audioSourceDriving == null)
		{
			return;
		}
		if (LevelManager.BuildModeOn)
		{
			this.audioSourceDriving.volume = 0f;
		}
		else
		{
			this.audioSourceDriving.volume = Mathf.Lerp(this.audioSourceDriving.volume, Vector3.Distance(this.lastPosition, this.vehicleBody.position), 4f * Time.fixedDeltaTime);
		}
		this.audioSourceDriving.pitch = Mathf.Clamp(Mathf.Lerp(this.audioSourceDriving.pitch, Vector3.Distance(this.lastPosition, this.vehicleBody.position) * 2f, 2f * Time.fixedDeltaTime), 0.8f, 1f);
		this.lastPosition = this.vehicleBody.position;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000E46C File Offset: 0x0000C66C
	private void OnCollisionEnter(Collision other)
	{
		if (other.relativeVelocity.magnitude > 20f)
		{
			this.PlayImpact();
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000E494 File Offset: 0x0000C694
	private void PlayImpact()
	{
		if (this.audioSourceImpact == null)
		{
			return;
		}
		this.audioSourceImpact.clip = this.impacts[UnityEngine.Random.Range(0, this.impacts.Length)];
		this.audioSourceImpact.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
		this.audioSourceImpact.Play();
	}

	// Token: 0x0400025F RID: 607
	public Vehicle vehicle;

	// Token: 0x04000260 RID: 608
	public AudioSource audioSourceDriving;

	// Token: 0x04000261 RID: 609
	public AudioSource audioSourceImpact;

	// Token: 0x04000262 RID: 610
	public AudioClip[] impacts;

	// Token: 0x04000263 RID: 611
	public Transform vehicleBody;

	// Token: 0x04000264 RID: 612
	private Vector3 lastPosition;
}
