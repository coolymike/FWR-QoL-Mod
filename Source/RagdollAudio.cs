using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class RagdollAudio : MonoBehaviour
{
	// Token: 0x06000129 RID: 297 RVA: 0x0000C338 File Offset: 0x0000A538
	private void Start()
	{
		this.instanceID = base.transform.GetInstanceID();
		if (this.collisionDetection != null)
		{
			this.collisionDetection.OnCollision += this.OnCollision;
		}
		if (this.playerSettings != null)
		{
			this.playerSettings.playerController.OnGroundedToggle += this.OnGroundedToggle;
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00003154 File Offset: 0x00001354
	private void FixedUpdate()
	{
		this.coreVelocity = Vector3.Distance(this.ragdollCore.position, this.lastCorePosition);
		this.lastCorePosition = this.ragdollCore.position;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
	private void OnDestroy()
	{
		if (this.collisionDetection != null)
		{
			this.collisionDetection.OnCollision -= this.OnCollision;
		}
		if (this.playerSettings != null)
		{
			this.playerSettings.playerController.OnGroundedToggle -= this.OnGroundedToggle;
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000C404 File Offset: 0x0000A604
	private void OnGroundedToggle(bool isGrounded)
	{
		this.collisionSource.volume = 0.7f;
		this.collisionSource.pitch = UnityEngine.Random.Range(0.9f, 1.3f);
		if (isGrounded)
		{
			this.collisionSource.clip = this.jumpLandingClips[UnityEngine.Random.Range(0, this.jumpLandingClips.Length)];
		}
		else
		{
			this.collisionSource.clip = this.softBodyImpactsClips[UnityEngine.Random.Range(0, this.softBodyImpactsClips.Length)];
		}
		this.collisionSource.Play();
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000C48C File Offset: 0x0000A68C
	public void CueSoundFootstep()
	{
		this.footstepSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
		this.footstepSource.volume = this.coreVelocity * 10f;
		this.footstepSource.clip = this.footstepsClips[UnityEngine.Random.Range(0, this.footstepsClips.Length)];
		this.footstepSource.Play();
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
	private void OnCollision(Transform objectTransform, Vector3 impactVelocity)
	{
		if (!this.ragdollSettings.RagdollModeEnabled)
		{
			return;
		}
		if (!this.readyForCollision)
		{
			return;
		}
		this.impactVelocityMagnitude = impactVelocity.magnitude;
		if (this.impactVelocityMagnitude < 5f)
		{
			return;
		}
		this.CueVoiceImpact(this.impactVelocityMagnitude);
		this.collisionSource.volume = this.impactVelocityMagnitude * 0.05f;
		this.collisionSource.pitch = UnityEngine.Random.Range(0.9f, 1.3f);
		this.collisionSource.clip = this.bodyImpactsClips[UnityEngine.Random.Range(0, this.bodyImpactsClips.Length)];
		this.collisionSource.Play();
		this.readyForCollision = false;
		base.Invoke("MarkReadyForCollision", 0.15f);
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00003183 File Offset: 0x00001383
	private void MarkReadyForCollision()
	{
		this.readyForCollision = true;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
	private void CueVoiceImpact(float impactVelocityMagnitude)
	{
		if (!this.useVocals)
		{
			return;
		}
		if (this.ragdollSettings.RagdollIsDismembered)
		{
			return;
		}
		UnityEngine.Random.InitState(DateTime.Now.Millisecond + this.instanceID);
		if (UnityEngine.Random.Range(0, this.increaseVocalChance ? 2 : 4) > 0)
		{
			return;
		}
		if (!this.readyForImpactVoice)
		{
			return;
		}
		if (this.vocalSource.isPlaying)
		{
			return;
		}
		if (impactVelocityMagnitude > 30f)
		{
			for (int i = 0; i < this.voices.Count; i++)
			{
				if (this.voices[i].voiceID == this.selectedVoiceID)
				{
					this.vocalSource.clip = this.voices[i].impactsHard[UnityEngine.Random.Range(0, this.voices[i].impactsHard.Length)];
				}
			}
		}
		else if (impactVelocityMagnitude > 20f)
		{
			for (int j = 0; j < this.voices.Count; j++)
			{
				if (this.voices[j].voiceID == this.selectedVoiceID)
				{
					this.vocalSource.clip = this.voices[j].impactsMedium[UnityEngine.Random.Range(0, this.voices[j].impactsMedium.Length)];
				}
			}
		}
		else if (impactVelocityMagnitude > 8f)
		{
			for (int k = 0; k < this.voices.Count; k++)
			{
				if (this.voices[k].voiceID == this.selectedVoiceID)
				{
					this.vocalSource.clip = this.voices[k].impactsSoft[UnityEngine.Random.Range(0, this.voices[k].impactsSoft.Length)];
				}
			}
		}
		this.vocalSource.Play();
		this.readyForImpactVoice = false;
		base.Invoke("MarkReadyForImpactVoice", UnityEngine.Random.Range(1f, 4f));
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000318C File Offset: 0x0000138C
	private void MarkReadyForImpactVoice()
	{
		this.readyForImpactVoice = true;
	}

	// Token: 0x040001B2 RID: 434
	private int instanceID;

	// Token: 0x040001B3 RID: 435
	public PlayerSettings playerSettings;

	// Token: 0x040001B4 RID: 436
	public RagdollSettings ragdollSettings;

	// Token: 0x040001B5 RID: 437
	public RagdollCollisionDetection collisionDetection;

	// Token: 0x040001B6 RID: 438
	public Transform ragdollCore;

	// Token: 0x040001B7 RID: 439
	private Vector3 lastCorePosition;

	// Token: 0x040001B8 RID: 440
	private float coreVelocity;

	// Token: 0x040001B9 RID: 441
	public AudioSource collisionSource;

	// Token: 0x040001BA RID: 442
	public AudioSource footstepSource;

	// Token: 0x040001BB RID: 443
	public AudioSource vocalSource;

	// Token: 0x040001BC RID: 444
	[Header("Settings")]
	public bool increaseVocalChance;

	// Token: 0x040001BD RID: 445
	[Header("Foley")]
	public AudioClip[] softBodyImpactsClips;

	// Token: 0x040001BE RID: 446
	public AudioClip[] bodyImpactsClips;

	// Token: 0x040001BF RID: 447
	public AudioClip[] jumpLandingClips;

	// Token: 0x040001C0 RID: 448
	public AudioClip[] footstepsClips;

	// Token: 0x040001C1 RID: 449
	[Header("Vocals")]
	public bool useVocals = true;

	// Token: 0x040001C2 RID: 450
	public int selectedVoiceID;

	// Token: 0x040001C3 RID: 451
	public List<RagdollAudio.Voice> voices = new List<RagdollAudio.Voice>();

	// Token: 0x040001C4 RID: 452
	private bool readyForCollision = true;

	// Token: 0x040001C5 RID: 453
	private float impactVelocityMagnitude;

	// Token: 0x040001C6 RID: 454
	private bool readyForImpactVoice = true;

	// Token: 0x02000040 RID: 64
	[Serializable]
	public class Voice
	{
		// Token: 0x040001C7 RID: 455
		public int voiceID;

		// Token: 0x040001C8 RID: 456
		public AudioClip[] impactsSoft;

		// Token: 0x040001C9 RID: 457
		public AudioClip[] impactsMedium;

		// Token: 0x040001CA RID: 458
		public AudioClip[] impactsHard;
	}
}
