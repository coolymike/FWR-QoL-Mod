using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class PlayerCollisionDetection : MonoBehaviour
{
	// Token: 0x0600008E RID: 142 RVA: 0x000028E7 File Offset: 0x00000AE7
	private void Awake()
	{
		this.playerSettings = base.GetComponent<PlayerSettings>();
		this.ragdollCollisionDetection = this.playerSettings.simulatedRagdoll.GetComponent<RagdollCollisionDetection>();
		this.playerRagdollAnimator = this.playerSettings.animatedRagdoll.GetComponent<RagdollAnimatorPlayer>();
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00002921 File Offset: 0x00000B21
	private void Start()
	{
		this.ragdollCollisionDetection.OnCollision += this.CollisionDetected;
		base.StartCoroutine(this.SaveStatsRoutine());
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00002947 File Offset: 0x00000B47
	private void OnDestroy()
	{
		this.ragdollCollisionDetection.OnCollision -= this.CollisionDetected;
		GameData.SaveStats();
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00002965 File Offset: 0x00000B65
	private IEnumerator SaveStatsRoutine()
	{
		for (;;)
		{
			GameData.SaveStats();
			yield return new WaitForSecondsRealtime(60f);
		}
		yield break;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x0000296D File Offset: 0x00000B6D
	public void CollisionDetected(Transform collidedObject, Vector3 impactVelocity)
	{
		AntiTouchSystem.PlayerTouchedRagdoll(this.playerSettings, collidedObject);
		if (Tag.Compare(collidedObject, Tag.Tags.Ragdoll) && this.playerRagdollAnimator != null)
		{
			this.playerRagdollAnimator.AnimateTrip();
		}
	}

	// Token: 0x0400010C RID: 268
	private PlayerSettings playerSettings;

	// Token: 0x0400010D RID: 269
	private RagdollCollisionDetection ragdollCollisionDetection;

	// Token: 0x0400010E RID: 270
	private RagdollAnimatorPlayer playerRagdollAnimator;
}
