using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class RagdollAnimatorPlayer : MonoBehaviour
{
	// Token: 0x06000115 RID: 277 RVA: 0x00003066 File Offset: 0x00001266
	private void Awake()
	{
		this.playerSettings = base.transform.root.GetComponent<PlayerSettings>();
		this.playerAnimator = this.playerSettings.animatedRagdoll.GetComponent<Animator>();
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00003094 File Offset: 0x00001294
	private void Start()
	{
		base.StartCoroutine(this.IdleState());
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000BAF8 File Offset: 0x00009CF8
	private void Update()
	{
		this.tripCoolDown -= Time.deltaTime;
		this.playerAnimator.SetBool("grounded", this.playerSettings.playerController.IsGrounded);
		if (this.playerSettings.playerController.IsGrounded && !this.triggeredCameraJumpShake)
		{
			CameraController.TriggerCameraJumpShake(this.playerSettings.mainCamera, Mathf.Clamp(this.airTime * 3f, 0f, 9f));
			this.triggeredCameraJumpShake = true;
			this.airTime = 0f;
		}
		else if (!this.playerSettings.playerController.IsGrounded)
		{
			this.triggeredCameraJumpShake = false;
			this.airTime += Time.deltaTime;
		}
		if (this.playerSettings.simulatedRagdoll.RagdollModeEnabled && !this.triggeredCameraTremor)
		{
			CameraController.TriggerCameraJumpShake(this.playerSettings.mainCamera, 7f);
			this.triggeredCameraTremor = true;
		}
		else if (!this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			this.triggeredCameraTremor = false;
		}
		this.playerAnimator.SetBool("flying", this.playerSettings.playerController.FlyMode);
		this.playerAnimator.SetBool("ragdollMode", this.playerSettings.simulatedRagdoll.RagdollModeEnabled);
		if (!PlayerSettings.disablePlayerControlls && !this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			this.playerAnimator.SetFloat("velocityXZ", this.playerSettings.playerController.velocityXZ);
			this.playerAnimator.SetFloat("velocityY", this.playerSettings.playerController.velocityY);
		}
		else
		{
			this.playerAnimator.SetFloat("velocityXZ", 0f);
			this.playerAnimator.SetFloat("velocityY", 0f);
		}
		if (this.playerSettings.playerController.velocityXZ < 0.5f || !this.playerSettings.playerController.IsGrounded)
		{
			this.playerAnimator.ResetTrigger("Trip");
		}
		if (this.playerSettings.simulatedRagdoll.ragdollModeStrength > 0f && this.playerSettings.simulatedRagdoll.RagdollModeEnabled)
		{
			this.playerAnimator.SetBool("falling", true);
			return;
		}
		this.playerAnimator.SetBool("falling", false);
	}

	// Token: 0x06000118 RID: 280 RVA: 0x000030A3 File Offset: 0x000012A3
	public void AnimateTrip()
	{
		if (this.tripCoolDown < 0f)
		{
			this.playerAnimator.SetTrigger("Trip");
			this.tripCoolDown = 3f;
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x000030CD File Offset: 0x000012CD
	private IEnumerator IdleState()
	{
		for (;;)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(6f, 15f));
			this.playerAnimator.SetTrigger("Idling");
			this.playerAnimator.SetInteger("idleState", UnityEngine.Random.Range(0, 2));
			yield return new WaitForEndOfFrame();
			this.playerAnimator.ResetTrigger("Idling");
		}
		yield break;
	}

	// Token: 0x04000198 RID: 408
	private PlayerSettings playerSettings;

	// Token: 0x04000199 RID: 409
	private Animator playerAnimator;

	// Token: 0x0400019A RID: 410
	private Vector2 movingSpeed;

	// Token: 0x0400019B RID: 411
	private float tripCoolDown;

	// Token: 0x0400019C RID: 412
	private bool triggeredCameraJumpShake;

	// Token: 0x0400019D RID: 413
	private bool triggeredCameraTremor;

	// Token: 0x0400019E RID: 414
	private float airTime;
}
