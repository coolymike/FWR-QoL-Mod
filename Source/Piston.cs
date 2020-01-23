using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class Piston : MonoBehaviour
{
	// Token: 0x06000249 RID: 585 RVA: 0x00004106 File Offset: 0x00002306
	private void Awake()
	{
		LevelManager.OnBuildModeToggle += this.OnBuildModeToggle;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00004119 File Offset: 0x00002319
	private void OnDestroy()
	{
		LevelManager.OnBuildModeToggle -= this.OnBuildModeToggle;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0001121C File Offset: 0x0000F41C
	private void Start()
	{
		this.startPosition = this.piston.position;
		this.endPosition = this.piston.position + this.piston.up * 4.6f;
		this.ejected = this.startEjected;
		this.ejectionRoutine = this.EjectionRoutine();
		this.OnBuildModeToggle(LevelManager.BuildModeOn);
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000412C File Offset: 0x0000232C
	private void FixedUpdate()
	{
		this.EjectionLoop();
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00011288 File Offset: 0x0000F488
	private void OnBuildModeToggle(bool buildModeOn)
	{
		if (buildModeOn)
		{
			if (this.ejectionRoutine != null)
			{
				base.StopCoroutine(this.ejectionRoutine);
			}
		}
		else if (this.ejectionRoutine != null)
		{
			base.StartCoroutine(this.ejectionRoutine);
		}
		if (buildModeOn)
		{
			this.ejected = this.startEjected;
			this.piston.position = (this.startEjected ? this.endPosition : this.startPosition);
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x000112F4 File Offset: 0x0000F4F4
	private void EjectionLoop()
	{
		if (LevelManager.BuildModeOn)
		{
			return;
		}
		this.pistonRigidbody.MovePosition(Vector3.Lerp(this.piston.position, this.ejected ? this.endPosition : this.startPosition, Time.fixedDeltaTime * this.speed));
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00004134 File Offset: 0x00002334
	private IEnumerator EjectionRoutine()
	{
		while (!LevelManager.BuildModeOn)
		{
			this.ejected = !this.ejected;
			this.PlaySound();
			yield return new WaitForSeconds(this.delay);
		}
		yield return null;
		yield break;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00011348 File Offset: 0x0000F548
	private void PlaySound()
	{
		this.audioSource.clip = this.audioClips[UnityEngine.Random.Range(0, this.audioClips.Length)];
		this.audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
		this.audioSource.Play();
	}

	// Token: 0x04000350 RID: 848
	public Transform piston;

	// Token: 0x04000351 RID: 849
	public Rigidbody pistonRigidbody;

	// Token: 0x04000352 RID: 850
	private Vector3 startPosition;

	// Token: 0x04000353 RID: 851
	private Vector3 endPosition;

	// Token: 0x04000354 RID: 852
	public bool startEjected;

	// Token: 0x04000355 RID: 853
	private bool ejected;

	// Token: 0x04000356 RID: 854
	public float speed = 10f;

	// Token: 0x04000357 RID: 855
	public float delay = 1.3f;

	// Token: 0x04000358 RID: 856
	public AudioSource audioSource;

	// Token: 0x04000359 RID: 857
	public AudioClip[] audioClips;

	// Token: 0x0400035A RID: 858
	private IEnumerator ejectionRoutine;
}
